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
// Author: unidoz
// Date   : 2011-03-13
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deep.entity
{
    /// <summary>
    /// ISequence 接口约定实体的内部编号
    /// </summary>
    /// <remarks>
    /// <para> 持久化的任何实体都要实现此接口</para>
    /// <para>实现此接口的派生类中在持久化时由平台系统负责数据库内部ID的自动维护</para>
    /// <para>接口仅对持久化对象的内部ID做出约定，对于其它任何扩展属性都由用户自定义.</para>
    /// </remarks>
    public interface ISequence
    {
        /// <summary>
        /// 实体的内部唯一编号
        /// </summary>
        int Id { get; }
    }
}
