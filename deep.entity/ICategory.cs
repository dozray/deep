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
// Author : Liu Yingxian  unidoz@yahoo.com
// Date :  05/21/2011
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using deep.entity;

namespace deep.entity
{
    /// <summary>
    /// 定义了基础实体对象之一：分类实体对象的接口
    /// </summary>
    /// <typeparam name="T">泛型的T要实现deep.entity.ISequence接口</typeparam>
    public interface ICategory<T>:ITreeEntity ,IContainer<T> where T : ISequence 
    {        
    }
}
