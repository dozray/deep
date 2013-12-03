#region << Copyright & License >>
/*
 * ========================================================================
 * Copyright(c) 2008-2012 The ACO Co.,Ltd. All Rights Reserved.
 * ========================================================================
 * Licensed under the ACO end user License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at 
 * http://www.acortinfo.com/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. 
 * ========================================================================
*/
#endregion

#region File Description
/*
 * -----------------------------------------------------------------------
 * File Name : SessionManager
 * CLR Version : 4.0.30319.225 
 * Create At : MATRIXSRV    2012/10/20 13:46:57 
 * Author : root   hodoinfo@gmail.com
 * Description :   
 *  
 * ----------------------------------------------------------------------- 
 */
#endregion

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Exceptions;
using log4net;
using log4net.Config;
using deep.exceptions;
namespace deep.dao
{
    public sealed class SessionManager:ISessionManager
    {       
        private Configuration cfg;       
       
        [ThreadStatic]
        private static volatile SessionManager instance = null;
        private static readonly Object syncRoot = new Object();
        [ThreadStatic]
        private  ISessionFactory sessionFactory = null;
        [ThreadStatic]
        private ISession tranSession = null;
        [ThreadStatic]
        private static ITransaction transaction = null;
        private static int insCnt = 0;
        private SessionManager()
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("正在配置系统数据服务......");
                cfg = new Configuration();
                cfg.Configure(baseDirectory + @"\hibernate.cfg.xml");
                Console.WriteLine("正在生成会话工厂......");
                sessionFactory = cfg.BuildSessionFactory();
                Console.WriteLine("正在启动日志......");
                //FileInfo configFile = new FileInfo(baseDirectory + @"\log4net.cfg.xml");
                //XmlConfigurator.Configure(configFile);
                //Logger.log("系统数据服务已启动......");
                //Logger.log("\r\n"+ ++insCnt+"\r\n");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        /// <summary>
        /// 生成SessionManager的实例对象
        /// </summary>
        /// <returns>SessionManager对象</returns>
        public static SessionManager GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SessionManager();
                    }
                }
            }
            return instance;
        }

        public IDbConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }
        public void BeginTransaction()
        {
            try
            {                                   
                transaction = tranSession.BeginTransaction();                
            }
            catch (HibernateException exception)
            {
                throw new DaoException("Fail to Begin Transaction.", exception);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CommitTransaction()
        {
            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                    transaction.Commit();//此处会自动调用session的flush方法
                transaction = null;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new deep.exceptions.DaoException("Fail to commit transaction.", ex);
            }
            finally
            {
                if (tranSession != null)
                {
                    if (tranSession.IsOpen)
                        tranSession.Close();
                    tranSession = null;
                }
                    
            }
        }
        public void RollbackTransaction()
        {
            try
            {
                if (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack)
                    transaction.Rollback();
                transaction = null;
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to rollback transaction.", ex);
            }
            finally
            {
                if (tranSession != null)
                {
                    if (tranSession.IsOpen)
                        tranSession.Close();
                    tranSession = null;
                }
            }
        }

       
        /// <summary>
        /// 打开一个新的session
        /// </summary>
        /// <returns></returns>
        public ISession openSession()
        {
            try
            {              
                    return  sessionFactory.OpenSession();  
            }
            catch (Exception e)
            {                
                //Logger.Info("生成Session时出现异常:",e);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }
        /// <summary>
        /// 返回当前的session
        /// </summary>
        /// <returns></returns>
        public ISession getTranSession()
        {
            if (tranSession != null &&  tranSession.IsOpen)
                return tranSession;
            else
                return tranSession=sessionFactory.OpenSession(); 
        }

    }
}
