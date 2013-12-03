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
// Author : Liu Yingxian
// Date : 04/20/2011
#endregion
using System;
using System.Runtime.Serialization;

namespace deep.entity
{
    /// <summary>
    /// 用以描述树型存储结点的实体对象,实现了ITreeEntity接口
    /// 使用场景：树型分类结点描述(没有关联实体，只做为树型展示)。
    /// </summary>
    [DataContract]
    public class AbstractTreeEntity : AbstractCode, ITreeEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public virtual String Name { get; set; }
        /// <summary>
        /// 是否为叶子结点
        /// </summary>
        [DataMember]
        public virtual bool IsLeaf { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public virtual int Index { get; set; }
        /// <summary>
        /// 父对象
        /// </summary>
        
        public virtual ITreeEntity Parent { get; set; }
        [DataMember]
        public virtual int ParentId
        {
            get { return Parent.Id; }
            set { ; }
        }

        /// <summary>
        /// IComparable T 的实现，此实现为在同一层级下的排序
        /// 若两个对象不在同一层级下，排序的返回结果可能不代表
        /// 客户端程序所期望的值
        /// </summary>
        /// <returns>
        /// 小于零  此对象小于 CompareTo 方法指定的对象。
        /// Zero  此对象等于方法参数
        /// 大于零  此对象大于方法参数。 
        /// </returns>
        public virtual int CompareTo(ITreeEntity entity)
        {
            return entity.Index.CompareTo(Index);
        }
        /// <summary>
        /// 返回对象的HashCode值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Name.GetHashCode();
        }
        /// <summary>
        /// 返回对象是否为同一个对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj) && Name == ((AbstractTreeEntity)obj).Name && IsLeaf == ((AbstractTreeEntity)obj).IsLeaf;// && Parent ==((AbstractTreeEntity)obj).Parent;
        }
        /// <summary>
        /// 返回对象的文本描述
        /// </summary>
        /// <returns>该对象的文本描述</returns>
        public override string ToString()
        {
            return Code + " " + Name;
        }
    }
}
