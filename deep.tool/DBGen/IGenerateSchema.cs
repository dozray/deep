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
//  File Name : IGenerateSchema 
//  CLR Version : 2.0.50727.3623
//  Create At : MATRIX   2011-7-20 10:49:18 
//  Author : unidoz   hodoinfo@gmail.com
//  Description : 
//  
//  --------------------------------------------------------------------------------------------
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deep.tools.db
{
    /// <summary>
    /// 定义数据库生成接口
    /// </summary>
    interface IGenerateSchema
    {
        /// <summary>
        /// 生成数据库结构
        /// </summary>
        void GenerateSchema();
        /// <summary>
        /// 生成SQL语句,而不生成数据库结构
        /// </summary>
        void GenerateSQLFile();

        /// <summary>
        /// 更新数据库
        /// </summary>
        void UpdateSchema();

        /// <summary>
        /// 删除数据库的数据和结构
        /// </summary>
        void DropSchema();
    }
}
