#region Copyright & License
// ========================================================
// Copyright (C) 2001-2011 The ACO Co.,Ltd.
// All rights reserved.
//
// Licensed under the ACO end user License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.acortinfo.com/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ========================================================
#endregion Copyright & License

#region File Description
///  --------------------------------------------------------------------------------------------
///  File Name : ContainerDao 
///  CLR Version : 2.0.50727.3623
///  Create At : MATRIX   2011-7-22 11:45:24 
///  Author : unidoz   hodoinfo@gmail.com
///  Description :   
///  --------------------------------------------------------------------------------------------
#endregion


using System;
using System.Collections.Generic;
using System.Text;

namespace deep.dao
{
    /// <summary>
    /// 此类用以对强关系操作的实体的操作重写
    /// </summary>
    /// <remarks>
    /// 强关系操作是指含有关系的关联实体在操作是一次性的将所有分步操作写入一个事务中，并提交事务
    /// </remarks>
    /// <typeparam name="C">指定参数必须实现IContainer接口</typeparam>
    /// <typeparam name="T">指示IContainer接口的参数必须实现ISequence</typeparam>
    public class ContainerDao<C, T> : Dao, IContainerDao
        where C : deep.entity.IContainer<T>
        where T : deep.entity.ISequence
    {
        private static SessionManager sm = SessionManager.GetInstance();
        /// <summary>
        /// 保存持久化对象
        /// </summary>
        /// <param name="container">IContainer接口的对象</param>
        /// <returns>主对象的保存返回值</returns>
        /// <remarks>保存操作会自动关闭当前Session</remarks>
        public object Save(C container)
        {            
            object obj = null;         
            try
            {
                sm.BeginTransaction();
                var ss = sm.getTranSession();
                obj=ss.Save(container);
                
                foreach (deep.entity.ISequence seq in container.Members)
                {
                    ss.Save(seq);
                }
                sm.CommitTransaction();
            }
            catch (Exception ex)
            {               
                throw new deep.exceptions.DaoException("保存数据时发生异常！", ex);
            }
           
            return obj;
        }
        /// <summary>
        /// 删除一个持久化对象
        /// </summary>
        /// <param name="container">实现了IContainer接口的持久化对象</param>
        /// <remarks>操作不会关闭当前的session</remarks>
        public  void Delete(C container)
        {
            try
            {
                sm.BeginTransaction();
                var ss = sm.getTranSession();

                foreach(deep.entity.ISequence seq in container.Members)
                {
                    ss.Delete(seq);
                }
                ss.Delete(container);
                sm.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw new deep.exceptions.DaoException("删除数据时发生异常!",ex);
            }
            finally
            {
                //if (sm.GetSession() != null)
                //    sm.GetSession().Close();
            }
        }
        ///Method
    }
}
