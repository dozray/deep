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
// Author : Liu Yingxian unidoz@yahoo.com
// Date : 05/13/2011
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace deep.entity
{
    /// <summary>
    /// 用以描述含有分类的并且分类末级有关联对象的组件对象的类
    /// </summary>
    /// <remarks>
    /// <para>分类的实体描述，用以将不同的对象分类，每个分类可以有和它关联的对象集合.
    /// 此类和AbstractTreeEntity类有所区别，主要在于叶子结点是否有关联对象，若有关联对象，则用此类，否则请用AbstractTreeEntity类来实现之.</para>
    /// <para>应用场景：分类中包含分类的明细对象列表</para>
    /// 
    /// <example>示例：
    /// <para>本示例是描述了一个组织和其拥有的员工，每一个员工都属于组织的末级结点对象所拥有</para>
    /// <code>
    ///  <para>人员类的接口定义</para>
    ///  /// <summary>
    ///  /// 人员实体接口
    /// ///</summary>
    ///  public interface IEmployee : IEntity
    ///  {
    ///  }
    ///  <para>看看员工类是如何的实现</para>
    /// /// <summary>
    /// /// 员工类
    /// /// </summary>
    /// [Serializable]
    /// public class Employee:AbstractEntity,IEmployee
    /// {
    ///   public virtual Orgnization Parent { get; set; }
    ///  }
    ///  <para>可以看到Employee实现了<see cref="ISequence"/>接口</para> 
    ///  
    /// <para>以下是员工组织结构类的实现</para>
    ///  /// <summary>
    ///  /// 组织架构类
    /// ///</summary>
    ///      [Serializable]
    ///      public class Orgnization : AbstractCategory &lt;IEmployee&gt;
    ///      {
    ///
    ///      }      
    /// <para>看到上面的实现是不是觉得很简单？</para>
    /// </code>
    /// </example>
    /// </remarks>
    /// <typeparam name="T">泛型参数需要实现<see cref="ISequence"/>接口</typeparam>
    
  [DataContract]
    public abstract class AbstractCategory<T> : AbstractTreeEntity, ICategory<T> where T : ISequence
    {
        private IList<T> member = new List<T>();
        /// <summary>
        /// 关联的对象列表
        /// </summary>
        [DataMember]
        public virtual IList<T> Members
        {
            get
            {
                if (IsLeaf)
                    return member;
                else
                    return null;
            }
            set
            {
                if (IsLeaf)
                    member = value;
                else
                    //member = null;
                    throw new System.NotSupportedException("系统不支持在非末级结点上设置数据");
            }
        }
    }
}
