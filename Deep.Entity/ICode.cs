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
// Author : unidoz   hodoinfo@gmail.com
// Date     : 2011-03-13
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace deep.entity
{
    /// <summary>
    /// Code 接口约定实体的编号
    /// </summary>
    /// <remarks>
    /// <para>对于需要编号的实体，实现此接口</para>
    /// </remarks>
    public interface ICode:ISequence
    {
        /// <summary>
        /// 实体编号(不可为空值)
        /// </summary>
        String Code { get; set; }
    }
}
