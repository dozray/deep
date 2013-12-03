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
//  --------------------------------------------------------------------------------------------
//  File Name : SchemaGenerator 
//  CLR Version : 2.0.50727.3623
//  Create At : MATRIX   2011-7-20 11:23:38 
//  Author : unidoz   hodoinfo@gmail.com
//  Description : 
//  
//  --------------------------------------------------------------------------------------------
#endregion

#region 使用的方法说明

//new SchemaExport(cfg)
//    .SetOutputFile("user_role_ddlScript.sql")
//    //.Create(true,true);
//    //.Execute(true, // write schema to output
//    //             false, // do not export to DB
//    //             false, // not drop statements only
//    //             );
//  .Execute(true, true, false);

//Execute(script, export, justDrop, format) 方法说明
//script为True就是把DDL语句输出到控制台；
//export为True就是根据持久类和映射文件在数据库中先执行删除再执行创建操作；
//justDrop为false表示不是仅仅执行Drop语句还执行创建操作，这个参数的不同就扩展了上面两个方法；

//Execute(script, export, justDrop, connection, exportOutput)
//前四个参数与前面相同
//connection为自定义连接,当export为true执行语句时必须打开连接。该方法不关闭连接，null就是使用默认连接；
//exportOutput参数自定义输出，例如我们输入到一下TextWriter中可以使用如下方式

//    var export = new SchemaExport(cfg);
//    var sb = new StringBuilder();
//    TextWriter output = new StringWriter(sb);
//    export.Execute(true, false, false, false, null, output);
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
namespace deep.dao.tools
{
    /// <summary>
    /// 数据库表结构操作类
    /// </summary>
    /// <remarks>
    /// 实现了<see cref="IGenerateSchema"/>接口，将扩展了多编译外部加载
    /// 后生成的能力。
    /// </remarks>
    
    public class SchemaGenerator : IGenerateSchema
    {
        private Configuration cfg = new Configuration();
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SchemaGenerator()
        {
            cfg.Configure(System.AppDomain.CurrentDomain.BaseDirectory + "\\hibernate.cfg.xml");
            //cfg.Configure(Assembly.Load("NHibernateDao"), "NHibernateDao.hibernate.cfg.xml");
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">hibernate config file</param>
        public SchemaGenerator(string file)
        {
            cfg.Configure(file);
        }
        #endregion Constructor


        #region IGenerateSchema的生成数据库结构

        /// <summary>
        /// 由hibernate配置中的编译集生成对应的数据库结构
        /// </summary>
        /// <remarks>
        /// 生成数据库结构的同进会在执行文件的文件夹下生成一个以当前
        /// 时间为文件名的.sql文件，此文件记录了执行的所有sql语句。
        /// </remarks>
        public void GenerateSchema()
        {
            new SchemaExport(cfg)
                .SetOutputFile(DateTime.Now.ToString("yy-M-d") + ".sql")
                //.Execute(true, true, false);
                .Create(true, true);
            //我们也可以使用new SchemaExport(cfg).Create(bool /*Script*/,bool/*export to DB*/)
            //它其实是Schema.Execute的简化
            //public void Create(bool script, bool export)
            //{
            //    Execute(script, export, false, true);
            //}       
        }


        /// <summary>
        /// 由编译集生成数据库(可以指定多个编译集一次性生成) 
        /// </summary>
        /// <remarks>
        /// Note : 编译集是指hibernate配置文件中指定的编译集和
        /// 此方法中传入的实参中的编译集的并集。如果在hibernate
        /// 的配置文件中指定了某个编译集，而且此方法的实参中也
        /// 含有此编译集，系统将抛出两次被加载异常。
        /// </remarks>
        /// <param name="list">编译集的集合</param>
        private void GenerateSchema(List<Assembly> list)
        {
            foreach (Assembly ass in list)
                cfg.AddAssembly(ass);
            new SchemaExport(cfg)
                .SetOutputFile(DateTime.Now.ToString("yy-M-d") + ".sql")
                .Execute(true, true, false);
        }
        #endregion


        #region 更新数据库
        public void UpdateSchema()
        {
            new SchemaUpdate(cfg)
                .Execute(true, true);
            //new SchemaExport(cfg)
            //    .SetOutputFile(DateTime.Now.ToString("yy-M-d") + ".sql")
            //    .Execute(true, false, false);
        }
        #endregion

        #region 生成SQL语句
        /// <summary>
        /// 由hibernate配置中的编译集生成对应的数据库结构的SQL语句
        /// </summary>
        /// <remarks>
        /// 此操作不会更改数据库的任何结构，它只被用作生成SQL语句使用。
        /// 例如：对于在一个已生成数据结构的编译集中新增加的类，通常我们
        /// 会将这个编译集单独增加到hibernate的配置文件中,调用此方法生成
        /// 对应的SQL语句，再执行我们感兴趣的SQL部分语句。
        /// </remarks>
        public void GenerateSQLFile()
        {
            new SchemaExport(cfg)
               .SetOutputFile(DateTime.Now.ToString("yy-M-d")+ ".sql")
               .Execute(true, false, false);
        }

        #endregion 生成SQL语句


        #region IGenerateSchema的删除数据库数据和结构
        /// <summary>
        /// 删除数据库中的数据和结构
        /// </summary>
        public void DropSchema()
        {
            new SchemaExport(cfg).Drop(true, true);       
        }
        #endregion
    }
}
