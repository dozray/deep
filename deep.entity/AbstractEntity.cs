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
// Author : unidoz  hodoinfo@gmail.com
// Date    : 03/13/2011
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace deep.entity
{
    /// <summary>
    /// 为参照实体类的基类
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// 此抽象类为<see cref="IEntity"/>的实现(借用了dotNet的自动属性),
    /// 实体类的抽象类，实现了IEntity接口，重写了Object的GetHashCode() 和 Equals(Object obj)以及ToString()方法。
    /// </para>
    /// <para>实体类可以继承并扩展此类</para>
    /// </remarks>
    [DataContract]
    public abstract class AbstractEntity : AbstractCode, IEntity
    {
        private string abbrName=string.Empty;
        /// <summary>
        /// 实体对象的名称
        /// </summary>
        [DataMember]
        public virtual String Name { get; set; }
        /// <summary>
        /// 实体对象的简称
        /// </summary>
        [DataMember]
        public virtual String FullName {
            get
            {
                return abbrName;//!=string.Empty?abbrName:("abbrName"+new Random(DateTime.Now.Millisecond>>2).NextDouble().ToString());
            }
            set
            {
                if (null != value && value.Length < EntityConstant.ABBRNAME_MAX_LENGTH)
                    abbrName = value;
                //else
                    //throw new ArgumentOutOfRangeException("Invalid value for FullName",value ,value.ToString());
            }
        }
        /// <summary>
        /// 实体对象的快捷码
        /// </summary>
        [DataMember]
        public virtual String QuickCode { get; set; }
        /// <summary>
        /// 实体对象的描述
        /// </summary>
        [DataMember]
        public virtual String Description { get; set; }
        /// <summary>
        /// 重写父类的GetHashCode()方法
        /// </summary>
        /// <returns>实体对象的hashCode</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() * Code.GetHashCode() * FullName.GetHashCode();
        }
        /// <summary>
        /// 重写了父类的比较是否为同一对象的Equals(Object obj)方法
        /// </summary>
        /// <param name="obj">比较的对象</param>
        /// <returns>若比较的对象相同，则返回true，否则返回false</returns>
        public override bool Equals(Object obj)
        {
            //throw new  NotImplementedException();
            if (obj == null || GetType() != obj.GetType()) return false;
            IEntity entity = (IEntity)obj;
            return (entity.Id == Id) && (entity.Code == Code) && (entity.FullName == FullName);
        }
        /// <summary>
        /// 重写父类的方法，用以返回实体对象的描述
        /// </summary>
        /// <returns>实体对象的描述</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetType().ToString());
            sb.Append(" : Code is :");
            sb.Append(Code);
            sb.Append(" Name is :");
            sb.Append(FullName);
            return sb.ToString();
        }
    }
}
