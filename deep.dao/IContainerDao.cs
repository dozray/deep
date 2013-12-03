#region Copyright & License
// ========================================================
// Copyright (C) 2001-2011 The ACO Co.,Ltd.
// All rights reserved.
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
// ========================================================
#endregion Copyright & License

#region File Description
///  --------------------------------------------------------------------------------------------
///  File Name : IContainerDao 
///  CLR Version : 2.0.50727.3623
///  Create At : MATRIX   2011-7-22 10:07:43 
///  Author : unidoz   hodoinfo@gmail.com
///  Description :   
///  --------------------------------------------------------------------------------------------
#endregion


using System;
using System.Collections.Generic;
using System.Text;

namespace deep.dao
{
    /// <summary>
    /// 接口定义了含有关系的关联对象的操作，重写了Dao中的CRUD部分
    /// </summary>
    /// <remarks>
    /// <para>含有关系的对象</para>
    /// 
    /// 含有关系的集合对象操作较为复杂，它要完成关系双方的实体持久化，同时还要维护
    /// 双方间的关系。在ORMMAPPING中我们可以通过配置文件的方式来实现。
    /// 
    /// <para>   第一种关系： 一对多 </para>
    /// 一对多的关系较为简单，可以理解为A对象Contains 多个B对象，在类的定义中可以定
    /// 义一个集合属性用以保存多个对象B的实例。
    /// 
    /// <para>       示例     </para>
    /// 
    /// <code> 
    /// class A{
    ///    public String Code{get;set;}  
    ///    public IList Bs{get;set;}
    /// }
    /// class B{
    ///    public int ID{get;set;}
    ///    public String Name{get;set;}    
    /// }    
    /// </code>   
    /// 
    /// <para>以上的这种情况我们称之为单向一对多。也就是说只能从一方
    /// 向多方导航(A.Bs 可以列出所有的B对象)，多方向一方导航的情况不可用。
    /// 单向一对多的情况在我们的程序中大量出现，比如所有的单据类中主信息
    /// 和详细信息的关系就是如此。那么如何配置实体的持久化文件呢？   
    /// 先写A的持久化配置如下： A.hbm.xml
    /// </para>
    /// <code>
    ///   <class name="A" table="A">
    ///    <id name="Id" column="aid" type="int">
    ///        <generator class="native"></generator>
    ///    </id>    
    ///    <property name="Code" type="String" column="code"></property>
    ///    <property name="Name" type="String" column="TitleName"></property>
    ///    <!--一个实体对应多个实体-->
    ///    <bag name="Bs" generic="true" table="B" inverse="true" lazy="true">
    ///      <key column="aid" foreign-key="FK_aid"/>
    ///      <one-to-many class="B" />
    ///     </bag>    
    ///    </class>
   
    /// </code>
    /// <para> 
    ///    对于B.hbm.xml文件正常配置就行，没有向A方的导航。
    ///   
    ///    以上的配置文件中可以看到A中有一个B的集合，我们使用了Bag来存放B实体对象的集合，在B表
    ///    中生成一列列名为aid，其中存放的是当前行所属的A对象，这一列有一个外键属性为FK_aid表示
    ///    参照A表中的主键列aid，此外,配置还宣告A和B实体的关系由B方来维护，也就是说在你持久化一
    ///    个A对象时，只要不对其属性集合中B做持久化，A和B之间的关系就没有维护。
    ///    在做持久化的各种操作时，我们怎么办呢？
    ///    当然以下所有的操作都要求在一个Session中:
    ///    1.新增操作
    ///        1).实例化A对象
    ///        2).实例化一个或多个B对象
    ///        3).将实例化的B对象逐个加入到A对象的集合属性Bs中
    ///        4).启动事务
    ///        5).将A对象Save()
    ///        6).将A对象的属性Bs集合中的每个B对逐个Save()
    ///        7).提交事务
    ///        <code>
    ///            A a = new A();              Step:1
    ///            a.Code="c01";
    ///            B b1= new B();             Step:2
    ///            b1.Name="n1";   
    ///            B b2= new B();
    ///            b2.Name="n2";
    ///            a.Bs.add(b1);                Step:3
    ///            a.Bs.add(b2);
    ///            sm.BeginTran()             Step:4
    ///            session.Save(a);            Step:5
    ///            foreach(B b in a.Bs){     Step:6
    ///                session.Save(B);
    ///            }
    ///            sm.CommitTran()         Step:7
    ///        </code>
    ///    2.删除操作
    ///    3.更新操作
    ///    4.查询操作
    /// </para>
    /// <para></para>
    /// </remarks>
    public interface IContainerDao : IDao
    {
        
    }
}

