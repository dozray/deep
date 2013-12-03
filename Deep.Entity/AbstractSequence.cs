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
// Author : Liu Yingxian   hodoinfo@gmail.com
// Date : 05/20/2011
#endregion
using System;
using System.Runtime.Serialization;

namespace deep.entity
{
    /// <summary>
    /// 描述含有系统内置编号的实体的抽象基类    
    /// </summary>
    /// <remarks>
    /// <para>
    /// 此抽象类实现了<see cref="ISequence"/>接口，所有
    /// 的实体类都直接或间接的继承自此此抽象类
    /// </para>
    /// <para>该类重写了Object的GetHashCode()方法</para>
    /// <para>同时也重写了Object的Equals(Object obj)方法</para>
    /// </remarks>
    /// <author>Liu Yingxian</author>
    
    [DataContract]
    public class AbstractSequence : ISequence
    {
        /// <summary>
        /// 实体的系统内部编号
        /// </summary>
        [DataMember]
        public virtual int Id { get; private set; }
        /// <summary>
        /// 重写了GetHashCode()方法
        /// </summary>
        /// <returns>hashCode</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ this.GetType().GetHashCode();
        }
        /// <summary>
        /// 重写了比较两个对象是否相等的方法
        /// </summary>
        /// <param name="obj">比较的对象</param>
        /// <returns>true : they are equal otherwise they are not the same.</returns>
        public override bool Equals(Object obj)
        {
            //throw new  NotImplementedException();
            if (obj == null || GetType() != obj.GetType()) return false;
            ISequence seq = (ISequence)obj;
            return (seq.Id == Id);
        }
    }
}
