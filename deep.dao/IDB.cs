#region Copyright & License
//
// Copyright 2001-2011 The ACO Co.,Ltd.
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
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
//Author  :    Liu Yingxian  hodoinfo@gmail.com
//Date      :   2011-03-25
namespace deep.dao
{
    /// <summary>
    /// 此借口定义数据库连接和事务的协议
    /// </summary>
    public interface IDB
    {
        /// <summary>
        /// 返回当前的数据库连接
        /// </summary>
        /// <returns>返回当前的数据库连接</returns>
        IDbConnection GetConnection();

        /// <summary>
        /// 关闭当前数据库连接
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTransaction();
        /// <summary>
        /// 自动关闭Session属性
        /// </summary>
        bool AutoCloseSession { get; set; }
        /// <summary>
        /// 关闭Session
        /// </summary>
        void CloseSession();
        /// <summary>
        /// 清除连接
        /// </summary>
        void ClearSession();
    }
}
