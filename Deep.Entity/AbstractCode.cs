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
using System.Runtime.Serialization;

namespace deep.entity
{
    /// <summary>
    /// 实现了ICode接口.
    /// 使用数据库内部ID和Code编码的实体类的基类（抽象类） 
    /// 实现了自身属性的Equals(object obj)和GetHashCode()方法
    /// </summary>
    [DataContract]
    public class AbstractCode : AbstractSequence, ICode, IComparable<ICode>
    {
        private string code=string.Empty;
        /// <summary>
        /// 实体的编号
        /// </summary>
        [DataMember]        
        public virtual String Code
        {
            get
            {
                return code;// != string.Empty ? code : ("code"+new Random(DateTime.Now.Millisecond).NextDouble().ToString());
            }
            set
            {
                if (null != value && value.Length < EntityConstant.CODE_MAX_LENGTH)
                    code = value;
                else
                    throw new ArgumentOutOfRangeException("Invalid value for Code.", value, value.ToString());
            }
        }
        /// <summary>
        /// 返回对象的Hash码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() * Code.GetHashCode();
        }
        /// <summary>
        /// 判定两个对象是否是同一个对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            return base.Equals(obj) && Code == ((AbstractCode)obj).Code;
        }

        #region IComparable<ICode> 成员
        /// <summary>
        /// 实现了编码排序
        /// </summary>
        /// <param name="other">比较的对象</param>
        /// <returns>对象的比较结果</returns>
        public virtual int CompareTo(ICode other)
        {
            if (null != other)
                return other.Code.CompareTo(Code);
            return 1;
            //throw new NotImplementedException();
        }

        #endregion
    }
}
