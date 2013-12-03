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
// Author : Liu Yingxian  hodoinfo@gamil.com
// Date    : 03/31/2011
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deep.entity
{
    /// <summary>
    /// 基础实体类接口,用以描述系统中的基础数据对象   
    /// </summary>
    /// <remarks>
    /// <para>系统中的基础数据都应实现此接口</para>
    /// <para>该接口是对于扩展性更强的<see cref="ICode "/>和<see cref="ISequence "/> 做了扩展 </para>
    /// </remarks>
    public interface IEntity :ICode
    {
        /// <summary>
        /// 实体名称(简称) Note:不可为空值
        /// </summary>
        String FullName { get; set; }
        /// <summary>
        /// 实体名称(全称)
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 快捷码：用于快速定位某个对象
        /// </summary>
        string QuickCode { get; set; }
        /// <summary>
        /// 描述详细
        /// </summary>
        string Description { get; set; }
    }
}
