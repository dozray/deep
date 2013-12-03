#region Copyright & License
//========================================================
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
//========================================================



#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
// Author   :       Liu Yingxian  hodoinfo@gmail.com
// Date      :       2011-04-20
// Version  :       1.0
namespace deep.dao
{
    /// <summary>
    /// 此借口定义数据库转换成的对象的基本操作的协议
    /// </summary>
    public interface IDao
    {
        /// <summary>
        /// 由类型和id读取一个对象
        /// </summary>
        /// <param name="clazz">类型</param>
        /// <param name="id">对象的id值</param>
        /// <returns>对象</returns>
        Object Get(Type clazz, object id);

        /// <summary>
        /// 由类型和id读取一个T类型的对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="id">对象的id值</param>
        /// <returns></returns>
        T Get<T>(object id); 

        /// <summary>
        /// 持久化类的空实例和一个存在的类的实例的标识符来加载对象
        /// </summary>
        /// <param name="obj">持久化类的空实例(瞬时对象)</param>
        /// <param name="id">一个存在的持久化类的实例的标识</param>
        void Load(object obj, object id);

        /// <summary>
        /// 由类型和id截入一个对象（要求对象在数据库中一定存在，否则抛出异常）
        /// </summary>
        /// <param name="clazz">类型</param>
        /// <param name="id">对象的id值</param>
        /// <returns>对象</returns>
        Object Load(Type clazz, object id);
  
        /// <summary>
        /// 由和id截入一个指定类型对象（要求对象在数据库中一定存在，否则抛出异常）
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="id">对象的id</param>
        /// <returns>指定类型的对象</returns>
        T Load<T>(object id);

        /// <summary>
        /// 返回指定类型的所有对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <returns>所有对象</returns>
        IList<T> LoadAll<T>();

        /// <summary>
        /// 返回指定类型的部分对象
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="firstResult">开始index</param>
        /// <param name="maxResult">取得的记录数</param>
        /// <returns>指定类型的指定对象记录</returns>
        IList<T> Load<T>(int firstResult, int maxResult);

        /// <summary>
        /// 返回指定类型的所有持久化对象组成的集合
        /// </summary>
        /// <param name="clazz">指定的类型</param>
        /// <returns>所有持久化对象组成的集合</returns>        
        IList LoadAll(Type clazz);

        /// <summary>
        /// 返回指定类型的所有持久化对象组成的集合
        /// 此结果集的索引范围从firstResult 到 maxResult.      
        /// </summary>
        /// <param name="clazz">指定的类型</param>
        /// <param name="firstResult">起始index</param>
        /// <param name="maxResult">终止index</param>
        /// <returns>所有持久化对象组成的集合</returns>

        IList Load(Type clazz, int firstResult, int maxResult);


        /// <summary>
        /// 持久化一个对象到数据库中
        /// </summary>
        /// <param name="obj">持久化对象</param>
        object Save(Object obj);

        /// <summary>
        /// 持久化一个对象到数据库中（可以支持两种方式）
        /// </summary>
        /// <param name="obj">持久化对象</param>
        void SaveOrUpdate(Object obj);

        /// <summary>
        /// 更新持久化对象
        /// </summary>
        /// <param name=" persistentObj">待更新的持久化对象</param>
        void Update(Object persistentObj);

        /// <summary>
        /// 删除持久化对象
        /// </summary>
        /// <param name="persistentObj">待删除的持久化对象</param>
        void Delete(object persistentObj);

        /// <summary>
        /// 使用一个HQL string 来删除对象
        /// 示例： session.delete("from Customer as c where c.id>8"); 
        /// </summary>
        /// <param name="HQLStr">HQL 语句</param>
        int Delete(string HQLStr);

        /// <summary>
        /// 使用指定类型的属性和对应的值删除对象
        /// </summary>
        /// <typeparam name="T">指定的类</typeparam>
        /// <param name="attribName">属性名称</param>
        /// <param name="attribValue">属性的值</param>
        /// <returns>执行方法影响的对象个数</returns>
        int Delete<T>(string attribName, string attribValue);
        int GetBeanCount<T>();
    }
}
