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
using System.Text;
// Author : Liu Yingxian  hodoinfo@gmail.com
// Date :     04/21/2011
namespace deep.entity
{
    /// <summary>
    /// 树型结构存储的实体对象接口,主要为处理树型结构实体对象的模型而建立。
    /// </summary>
    /// <remarks>
    /// <para>每一个实现此接口的类的对象都有一个父对象，若父对象为null，则表示为根对象。</para>
    /// </remarks>
    public interface ITreeEntity:ICode,IComparable<ITreeEntity>
    {
        /// <summary>
        /// 实体的名称
        /// </summary>
        String Name { get; set; }
        /// <summary>
        /// 是否为叶子结点
        /// </summary>
        bool IsLeaf { get; set; }
        /// <summary>
        /// 排序index
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// 对象的父对象
        /// </summary>
        ITreeEntity Parent { get; set; }
    }
}
