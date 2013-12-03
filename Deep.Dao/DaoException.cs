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
//  --------------------------------------------------------------------------------------------
//  File Name : DaoException 
//  CLR Version : 2.0.50727.3623
//  Create At : MATRIX   2011-7-20 10:49:18 
//  Author : unidoz   hodoinfo@gmail.com
//  Description : 
//  
//  --------------------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace deep.exceptions
{
    /// <summary>
    /// Dao的异常类
    /// </summary>
    [Serializable]
    public class DaoException:ApplicationException
    {
        /// <summary>
        /// 初始化一个<see cref="DaoException"/>类的实例对象
        /// </summary>
        public DaoException():base("An exception occurred in the Dao layer.")
        {
        }
        /// <summary>
        /// 初始化一个<see cref="DaoException"/>类的实例对象
        /// </summary>
        /// <param name="message">用以描述错误的信息</param>
        public DaoException(String message):base(message){
        }
        /// <summary>
        /// 初始化一个<see cref="DaoException"/>类的实例对象
        /// </summary>
        /// <param name="innerException">当前异常的起因异常,如果此参数为非空值,那么在try catch 
        /// 块中捕获的异常将会引发当前异常.</param>
        public DaoException(Exception innerException):base(innerException.Message,innerException)
        {
        }
        /// <summary>
        /// 初始化一个<see cref="DaoException"/>类的实例对象
        /// </summary>
        /// <param name="message">用以描述错误的信息</param>
        /// <param name="innerExecption">当前异常的起因异常,如果此参数为非空值,那么在try catch 
        /// 块中捕获的异常将会引发当前异常.</param>
        public DaoException(string message,Exception innerExecption):base(message,innerExecption)
        {
        }
     
        /// <summary>
        /// 使用串行化数据来初始化一个<see cref="DaoException"/>类的实例对象
        /// </summary>
        /// <param name="info">用于生成此异常的串行化对象数据</param>
        /// <param name="context">用于指示源或目标上下文信息的<see cref="StreamingContext"/>类对象</param>
        protected DaoException(SerializationInfo info,StreamingContext context):base(info,context)
        {
        }
    }
}
