using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Collection;
using NHibernate.Criterion;
using criterion = NHibernate.Criterion;
using deep.exceptions;
namespace deep.dao
{
    /// <summary>
    ///  此类为数据操纵基础类   
    /// </summary>
    /// <remarks>
    /// <para>此类包装了SessionManager类，可以用于管理Session Connection SessionFactory</para>
    /// <para></para>
    /// </remarks>
    public class Dao : IDao,IQueryable, IFind
    {
       
        public static SessionManager sm = SessionManager.GetInstance();
        /// <summary>
        /// 每一个dao的对象含有一个session对象，这个对象在dao的生命周期内不关闭
        /// 在dao的析构中关闭
        /// </summary>
        private ISession session;
        public Dao()
        {
            session = sm.openSession();
        }
        public ISession getCurrentSession()
        {
            return session;
        }
        #region 缓存相关
        /// <summary>
        /// Update the query instance according to the parameter name, and its value.
        /// </summary>
        protected void ApplyNamedParameterToQuery(IQuery queryObject, string paramName, object value)
        {
            if (value.GetType().Equals(typeof(ICollection)))
            {
                queryObject.SetParameterList(paramName, (ICollection)value);
            }
            else if (value.GetType().Equals(typeof(object[])))
            {
                queryObject.SetParameterList(paramName, (object[])value);
            }
            else
            {
                queryObject.SetParameter(paramName, value);
            }
        }

        
        /// <summary>
        /// Prepare the Query according to IQuery instance.
        /// </summary>
        //protected void PrepareQuery(IQuery queryObject)
        //{
        //    if (IsCacheQueries())
        //    {
        //        queryObject.SetCacheable(true);
        //        if (GetQueryCacheRegion() != null)
        //        {
        //            queryObject.SetCacheRegion(GetQueryCacheRegion());
        //        }
        //    }
        //}
        /// <summary>
        /// 刷新当前的session 
        /// </summary>
     
        #endregion
        #region  CRUD
        /// <summary>
        /// 保存持久化对象到数据库 
        /// </summary>
        /// <param name="persistentObj"></param>
        public virtual object Save(object persistentObj)
        {
            object obj = null;            
            try
            {                                        
                obj = session.Save(persistentObj);               
                return obj;
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to save persistent object.", ex);
            }
            finally
            {
                session.Flush();
            }
        }
        /// <summary>
        ///  保存或更新持久化对象到数据库
        /// </summary>
        /// <param name="persistentObj"></param>
        public virtual void SaveOrUpdate(object persistentObj)
        {
            try
            {                           
                session.SaveOrUpdate(persistentObj);
                session.Flush();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to save or update persistent object to database.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                
            }
        }
        /// <summary>
        /// 删除持久化对象
        /// </summary>
        /// <param name="persistentObj">待删除的持久化对象</param>
        public virtual void Delete(object persistentObj)
        {
            try
            {                 
                session.Delete(persistentObj);
                session.Flush();
            }
            catch (HibernateException ex)
            {                
                throw new deep.exceptions.DaoException("Fail to delete persistent object.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
               
            }
        }
        /// <summary>
        /// 使用一个HQL string 来删除对象 
        /// </summary>
        /// <remarks>
        /// 由于该方法可以执行批量操作，所在使用此方法时一定要注意HSQL的正确性，
        /// 并确认你要删除的过滤条件是否是正确的业务逻辑。
        /// <example>示例：
        /// <code>session.delete("from Customer as c where c.id>8");</code>
        /// </example>
        /// </remarks>
        /// <param name="HQLStr">HSQL 语句</param>
        public int Delete(string HQLStr)
        {
            try
            {                        
                int effect = session.Delete(HQLStr);  
                session.Flush();
                return effect;
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to delete persistent object. Transation is rollback.", ex);
            }
            catch (Exception e)
            {
                string err = e.Message + "\r\n" + e.InnerException.Message + "\r\n" + e.InnerException.InnerException.Message;
                throw new deep.exceptions.DaoException(err);
                //throw e;
            }
            finally
            {
               
            }
        }   
        
        /// <summary>
        /// 通过主键删除对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="primaryKey">主键值</param>
        /// <returns>删除的成功 失败)</returns>
       

        public bool Delete<T>(int id)
        {
            bool success = false;
           
            try
            {
                session.Delete(session.Load<T>(id));
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                
            }
            return success;
        }
        /// <summary>
        /// 使用属性的等值删除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="attribName">属性名称</param>
        /// <param name="attribValue">属性值</param>
        /// <returns></returns>
        public int Delete<T>(string attribName, string attribValue)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("from {0} t where t.{1} = {2}", typeof(T).Name, attribName, attribValue);
            return Delete(sb.ToString());
        }

        /// <summary>
        ///  更新一个持久化对象
        /// </summary>
        /// <param name="persistentObj"></param>
        public void Update(object persistentObj)
        {
            try
            {                       
                session.Update(persistentObj);
                session.Flush();
            }
            catch (HibernateException ex)
            {               
                throw new deep.exceptions.DaoException("Fail to update persistent object.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
             
            }
        }

        /// <summary>
        /// 由类型和id读取一个对象
        /// </summary>
        /// <param name="clazz">类型</param>
        /// <param name="id">对象的id值</param>
        /// <returns>对象</returns>
        public Object Get(Type clazz, object id)
        {           
            return session.Get(clazz, id);
        }
        /// <summary>
        /// 由类型和id读取一个T类型的对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="id">对象的id值</param>
        /// <returns></returns>
        public T Get<T>(object id)
        {           
            return session.Get<T>(id);
        }
        /// <summary>
        /// 持久化类的空实例和一个存在的类的实例的标识符来加载对象
        /// </summary>
        /// <param name="obj">持久化类的空实例(瞬时对象)</param>
        /// <param name="id">一个存在的持久化类的实例的标识</param>
        public void Load(Object obj, object id)
        {            
            session.Load(obj, id);
        }
        /// <summary>
        /// 由类型和id截入一个对象（要求对象在数据库中一定存在，否则抛出异常）
        /// </summary>
        /// <param name="clazz">类型</param>
        /// <param name="id">对象的id值</param>
        /// <returns>对象</returns>
        public Object Load(Type clazz, object id)
        {
            try
            {               
                return session.Load(clazz, id);
            }
            catch (HibernateException he)
            {
                throw new deep.exceptions.DaoException("Fail to load entity, Method : [ Object Load(Type clazz,object id) ]", he);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {                
            }
        }
        /// <summary>
        /// 由和id截入一个指定类型对象（要求对象在数据库中一定存在，否则抛出异常）
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="id">对象的id</param>
        /// <returns>指定类型的对象</returns>
        public T Load<T>(object id)
        {
            try
            {              
                return session.Load<T>(id);
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
              
            }
            
        }
        /// <summary>
        /// 返回指定类型的所有持久化对象组成的集合
        /// </summary>
        /// <param name="entityType">指定的类型</param>
        /// <returns>所有持久化对象组成的集合</returns>
        public IList LoadAll(Type entityType)
        {
            
            try
            {
                ICriteria criteria = session.CreateCriteria(entityType);                
                return criteria.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to load all.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                
            }
        }
        /// <summary>
        /// 返回指定类型的所有持久化对象组成的集合
        /// 此结果集的索引范围从firstResult 到 maxResult.      
        /// </summary>
        public IList Load(Type entityType, int firstResult, int maxResult)
        {
           
            try
            {               
                ICriteria criteria = session.CreateCriteria(entityType).SetFirstResult(firstResult).SetMaxResults(maxResult);
                
                return criteria.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to load all", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                
            }
        }


        /// <summary>
        /// 返回指定类型的所有对象 
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <returns>所有对象</returns>
        public IList<T> LoadAll<T>()
        {
          
            try
            {
               
                ICriteria criteria = session.CreateCriteria(typeof(T)) ;
                
                return criteria.List<T>();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to load all.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
               
            }
        }
        /// <summary>
        /// 返回指定类型的部分对象 
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="firstResult">开始index</param>
        /// <param name="maxResult">取得的记录数</param>
        /// <returns>指定类型的指定对象记录</returns>
        public IList<T> Load<T>(int firstResult, int maxResult)
        {
          
            try
            {               
                ICriteria criteria = session.CreateCriteria(typeof(T)).SetFirstResult(firstResult).SetMaxResults(maxResult);
                return criteria.List<T>();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to load all.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                
            }
        }
     
        /// <summary>
        /// 通过给定的对象查询数据库中的记录
        /// </summary>
        /// <param name="entityType">给定对象的类型</param>
        /// <param name="persistentObj">给定的对象</param>
        /// <returns>结果集合</returns>
        public IList QueryByExample(Type entityType, object persistentObj)
        {
            IList list = new ArrayList();
            try
            {
                               
                list = session.CreateCriteria(entityType).Add(Example.Create(persistentObj)).List();
                
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to query by example.", ex);
            }
            finally
            {
              
            }
            return list;
        }
        /// <summary>
        /// 通过给定的对象查询数据库中的记录
        /// </summary>
        /// <param name="entityType">给定对象的类型</param>
        /// <param name="persistentObj">给定的对象</param>
        /// <param name="firstResult">起始索引</param>
        /// <param name="maxResult">终止索引</param>
        /// <returns>结果集合</returns>
        public IList QueryByExample(Type entityType, object persistentObj, int firstResult, int maxResult)
        {
            IList list = new ArrayList();
            try
            {
                
               
                list = session.CreateCriteria(entityType).SetFirstResult(firstResult).SetMaxResults(maxResult).Add(Example.Create(persistentObj)).List();
                
            }
            catch (HibernateException ex)
            {
                
                throw new deep.exceptions.DaoException("Fail to query by example.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                
            }
            return list;
        }
        /// <summary>
        /// 由一个实例查询数据中的对象
        /// 此方法使用了数据库中的like 比较 并且 忽略大小写
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="obj">实例对象</param>
        /// <returns>查询到的对象列表</returns>
        public IList<T> UseQueryByExample<T>(T obj)
        {
            IList<T> list = new List<T>();
            try
            {
                               
                Example exam = Example.Create(obj)
                    .IgnoreCase()
                    .EnableLike()
                    .SetEscapeCharacter('&');

                session.CreateCriteria(typeof(T))
                    .Add(exam)
                    .List<T>();
               
            }
            catch (Exception e)
            {
                throw new deep.exceptions.DaoException("Fail to find object via example object!", e);
            }
            finally
            {
               
            }
            return list;
        }
        #endregion

        #region 总记录数
        /// <summary>
        /// 返回对象的总记录数
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <returns>Int32类型的总记录数</returns>
        public int GetBeanCount<T>()
        {
            int rowCount = 0;
              try
            {
                              
                ICriteria cr = session.CreateCriteria(typeof(T));
                rowCount =  (int)cr.SetProjection(Projections.RowCount()).UniqueResult();
               
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to get bean row count", ex);
            }
            finally
            {
               
            }    
            return rowCount;
        }
        #endregion

        #region  查询对象
        /// <summary>
        /// 由查询语句和实例对象(example instance)返回对象列表 
        /// </summary>
        public IList Find(string queryString, object value)
        {
            return Find(queryString, new object[] { value });
        }
        /// <summary>
        /// 由HQL查询语句和参数集合来查找所有的对象，并将对象的集合返回
        /// </summary>
        /// <param name="queryString">查询字符串</param>
        /// <param name="values">参数列表</param>
        /// <returns>对象集合列表</returns>
        public IList Find(string queryString, object[] values)
        {
            try
            {              
                IQuery queryObject = session.CreateQuery(queryString);
                //PrepareQuery(queryObject);
                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        queryObject.SetParameter(i, values[i]);
                    }
                }
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find objects.", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                               
            }
        }
        /// <summary>
        /// 由HQL查询语句和参数集合来查找所有的对象，并将对象的集合返回 
        /// 结果集的索引从 firstResult 到 maxResult.
        /// </summary>
        /// <param name="queryString">查询语句</param>
        /// <param name="values">参数列表</param>
        /// <param name="firstResult">对象索引起始值</param>
        /// <param name="maxResult">对象索引的终止值</param>
        /// <returns>对象集合</returns>
        public IList Find(string queryString, object[] values, int firstResult, int maxResult)
        {
           
            try
            {
                IQuery queryObject = session.CreateQuery(queryString).SetFirstResult(firstResult).SetMaxResults(maxResult);
                //PrepareQuery(queryObject);
                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        queryObject.SetParameter(i, values[i]);
                    }
                }
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find by query string", ex);
            }
            finally
            {
               
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public IList Find(string queryString)
        {
            return Find(queryString, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString">查询语句</param>
        /// <param name="firstResult">起始索引</param>
        /// <param name="maxResult">终止索引</param>
        /// <returns></returns>
        public IList Find(string queryString, int firstResult, int maxResult)
        {
            return Find(queryString, null, firstResult, maxResult);
        }
        /// <summary>
        /// 使用HSQL返回所有的实例        
        /// </summary>
        public IList FindByHQLQuery(string hqlQuery)
        {
            IList result = new ArrayList();
            try
            {
               
                result = session.CreateQuery(hqlQuery).List();
               
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to execute HSQL query", ex);
            }
            finally
            {
               
            }
            return result;
        }
        /// <summary>
        /// 由指定的类型查询所有的实例 
        /// </summary>
        public IList FindAll(Type type)
        {
            IList objs = new ArrayList();
            try
            {
                              
                objs = session.CreateCriteria(type).List();
                
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to find all  objects", ex);
            }
            finally
            {
                
            }
            return objs;
        }

        /// <summary>
        /// 返回指定数量的符合条件列表的对象集合
        /// </summary>
        /// <param name="maxResult">返回对象数量</param>
        /// <param name="type">类型</param>
        /// <param name="criterions">条件</param>
        /// <returns></returns>
        public IList FindByCriterions(int maxResult, Type type, params ICriterion[] criterions)
        {
            IList list = new ArrayList();
            try
            {
                               
                ICriteria criteria = session.CreateCriteria(type);
                criteria.SetMaxResults(maxResult);
                foreach (ICriterion criterion in criterions)
                {
                    criteria.Add(criterion);
                }
                list = criteria.List();
                
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to find objects by criterions", ex);
            }
            finally
            {
               
            }
            return list;
        }

        /// <summary>
        /// 返回指定类型的符合条件列表的对象集合
        /// </summary>
        /// <param name="type">指定的类型</param>
        /// <param name="restrictions">条件列表</param>
        /// <returns>实例化对象的集合</returns>
        public IList FindByCriterions(Type type, IList restrictions)
        {
            IList list = new ArrayList();
            try
            {
                            
                ICriteria criteria = session.CreateCriteria(type);
                foreach (ICriterion criterion in restrictions)
                {
                    criteria.Add(criterion);
                }
                list = criteria.List();
                
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to find objects by criterions", ex);
            }
            finally
            {
               
            }
            return list;
        }
        /// <summary>
        /// 返回指定类型的符合条件列表的对象集合
        /// 此集合的索引从firstResult到maxResult
        /// </summary>
        /// <param name="type">指定的类型</param>
        /// <param name="restrictions">条件列表</param>
        /// <param name="firstResult">起始索引</param>
        /// <param name="maxResult">终止索引</param>
        /// <returns>实例化对象的集合</returns>
        public IList FindByCriterions(Type type, IList restrictions, int firstResult, int maxResult)
        {
            IList list = new ArrayList();
            try
            {
                              
                ICriteria criteria = session.CreateCriteria(type).SetFirstResult(firstResult).SetMaxResults(maxResult);
                foreach (ICriterion criterion in restrictions)
                {
                    criteria.Add(criterion);
                }
                list = criteria.List();
                
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to find objects by criterions", ex);
            }
            finally
            {
              
            }
            return list;
        }

        /// <summary>
        /// 使用HSQL语句和参数查询对象集合
        /// </summary>
        /// <param name="queryString">HSQL语句</param>
        /// <param name="paramName">HSQL语句的参数名称</param>
        /// <param name="value">实例对象</param>
        /// <returns>持久化对象列表</returns>
        public IList FindByNamedParam(string queryString, string paramName, object value)
        {
            return FindByNamedParam(queryString, new string[] { paramName }, new object[] { value });
        }
        /// <summary>
        /// 使用HSQL语句和参数查询对象集合,此集合的索引值从firstResult 到 maxResult
        /// </summary>
        /// <param name="queryString">HSQL语句</param>
        /// <param name="paramName">HSQL语句参数名称</param>
        /// <param name="value">实例对象</param>
        /// <param name="firstResult">起始索引</param>
        /// <param name="maxResult">终止索引</param>
        /// <returns>持久化对象列表</returns>
        public IList FindByNamedParam(string queryString, string paramName, object value, int firstResult, int maxResult)
        {
            return FindByNamedParam(queryString, new string[] { paramName }, new object[] { value }, firstResult, maxResult);
        }

      
        /// <summary>
        ///   使用HSQL语句和参数集合查询所有对象集合
        /// </summary>
        /// <param name="queryString">HSQL语句</param>
        /// <param name="paramNames">HSQL语句参数名称集合</param>
        /// <param name="values">实例对象集合</param>
        /// <returns>持久化对象列表</returns>
        public IList FindByNamedParam(string queryString, string[] paramNames, object[] values)
        {
            try
            {
                if (paramNames.Length != values.Length)
                {
                    throw new deep.exceptions.DaoException("Length of paramNames array must match Length of values array");
                }
                IQuery queryObject = session.CreateQuery(queryString);

                //PrepareQuery(queryObject);
                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        ApplyNamedParameterToQuery(queryObject, paramNames[i], values[i]);
                    }
                }
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find with muti param and objects.", ex);
            }
            finally
            {
               
            }
        }

        /// <summary>
        /// 使用HSQL语句和多个参数查询对象集合,此集合的索引值从firstResult 到 maxResult
        /// </summary>
        /// <param name="queryString">HSQL语句</param>
        /// <param name="paramNames">HSQL语句参数名称集合</param>
        /// <param name="values">实例对象集合</param>
        /// <param name="firstResult">起始索引</param>
        /// <param name="maxResult">终止索引</param>
        /// <returns></returns>
        public IList FindByNamedParam(string queryString, string[] paramNames, object[] values,
          int firstResult, int maxResult)
        {
            if (paramNames.Length != values.Length)
            {
                throw new deep.exceptions.DaoException("Length of paramNames array must match Length of values array");
            }
            try
            {
               
                IQuery queryObject = session.CreateQuery(queryString);
                queryObject.SetFirstResult(firstResult);
                queryObject.SetMaxResults(maxResult);
                //PrepareQuery(queryObject);
                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        ApplyNamedParameterToQuery(queryObject, paramNames[i], values[i]);
                    }
                }
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find by named params ", ex);
            }
            finally
            {
               
            }
        }
        /// <summary>
        /// 由查询名称和参数对象集合查找持久化对象集合，并用列表的形式将结果集返回
        /// </summary>
        /// <param name="queryName">查询名称</param>
        /// <param name="values">参数对象</param>
        /// <returns>结果集</returns>
        public IList FindByNamedQuery(string queryName, object[] values)
        {
           
            try
            {
                IQuery queryObject = session.GetNamedQuery(queryName);
                //PrepareQuery(queryObject);
                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        queryObject.SetParameter(i, values[i]);
                    }
                }
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find by Named query", ex);
            }
            finally
            {
            }
        }
        /// <summary>
        /// 由查询名称和参数对象查找持久化对象集合，并用列表的形式将结果集返回
        /// </summary>
        /// <param name="queryName">HSQL查询名称</param>
        /// <param name="value">参数对象</param>
        /// <returns>持久化对象集合列表</returns>
        public IList FindByNamedQuery(string queryName, object value)
        {
            return FindByNamedQuery(queryName, new object[] { value });
        }
        /// <summary>
        /// 由查询名称查找持久化对象集合，并用列表的形式将结果集返回
        /// </summary>
        /// <param name="queryName">HSQL查询名称</param>
        /// <returns>持久化对象集合列表</returns>
        public IList FindByNamedQuery(string queryName)
        {
            return FindByNamedQuery(queryName, (object[])null);
        }

        /// <summary>
        /// 由查询名称返回所有持久化对象列表，需要参数名称和实例对象 
        /// </summary>
        /// <param name="queryName">查询名称</param>
        /// <param name="paramName">相对应的参数名称</param>
        /// <param name="value">实例对象</param>
        /// <returns>持久化对象列表</returns>
        public IList FindByNamedQueryAndNamedParam(string queryName, string paramName, object value)
        {
            return FindByNamedQueryAndNamedParam(queryName, new string[] { paramName }, new object[] { value });
        }


        /// <summary>
        /// 由查询名称返回所有持久化对象列表，需要参数名称和实例对象 。
        /// 此方法和
        /// </summary>
        /// <param name="queryName">查询名称</param>
        /// <param name="paramNames">多个参数组成的集合</param>
        /// <param name="values">参数值</param>
        /// <returns>持久化对象列表</returns>
        public IList FindByNamedQueryAndNamedParam(string queryName, string[] paramNames, object[] values)
        {
            if (paramNames != null && values != null && paramNames.Length != values.Length)
            {
                throw new deep.exceptions.DaoException("Length of paramNames array must match Length of values array");
            }         
            try
            {
                IQuery queryObject = session.GetNamedQuery(queryName);
                //PrepareQuery(queryObject);
                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        ApplyNamedParameterToQuery(queryObject, paramNames[i], values[i]);
                    }
                }
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find by Named query and Named Param", ex);
            }
            finally
            {
                
            }
        }

        /// <summary>
        /// Get all instances according to the query name, and example instances.
        /// </summary>
        public IList FindByNamedQueryAndValueBean(string queryName, object valueBean)
        {
           try
            {
                IQuery queryObject = session.GetNamedQuery(queryName);
                //PrepareQuery(queryObject);
                queryObject.SetProperties(valueBean);
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find by Named query and value bean", ex);
            }
            finally
            {
                
            }
        }
        /// <summary>
        /// 返回指定类型的符合查询条件的持久化对象列表
        /// </summary>
        /// <param name="type">指定的类型</param>
        /// <param name="restriction">条件</param>
        /// <returns>持久化对象列表</returns>
        public IList FindByProperty(Type type, ICriterion restriction)
        {
            IList objs = new ArrayList();
            try
            {
                            
                objs = session.CreateCriteria(type).Add(restriction).List();
               
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to find objects by property", ex);
            }
            finally
            {
                
            }
            return objs;
        }
        /// <summary>
        /// 由NativeSQL语句返回对象列表
        /// </summary>
        /// <param name="sqlQuery">NativeSQL语句</param>
        /// <param name="aliasName">别名</param>
        /// <param name="type">返回的对象类型</param>
        /// <returns>持久化对象列表</returns>
        public IList FindBySQLQuery(string sqlQuery, string aliasName, Type type)
        {
            IList result = new ArrayList();
            try
            {
              
                result = session.CreateSQLQuery(sqlQuery).AddEntity(aliasName, type).List();
               
            }
            catch (HibernateException ex)
            {
               
                throw new deep.exceptions.DaoException("Fail to execute query", ex);
            }
            finally
            {
                
            }
            return result;
        }

        /// <summary>
        /// 由HQL查询语句和实例对象查询出对象集合，并以列表的形式返回。
        /// </summary>
        /// <param name="queryString">HQL查询语句</param>
        /// <param name="valueBean">实例对象</param>
        /// <returns>对象列表</returns>
        public IList FindByValueBean(string queryString, object valueBean)
        {
          
            try
            {
                IQuery queryObject = session.CreateQuery(queryString);
                //PrepareQuery(queryObject);
                queryObject.SetProperties(valueBean);
                return queryObject.List();
            }
            catch (HibernateException ex)
            {
                throw new deep.exceptions.DaoException("Fail to find by valueBeam", ex);
            }
            finally
            {
                
            }
        }
        #endregion

        #region IQueryable 接口实现
        public IList<T> Query<T>(string filter)
        {
            IList<T> list;
           
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("from {0}  {1} ", typeof(T).Name, filter);
                NHibernate.IQuery qObj = session.CreateQuery(builder.ToString());

                list = qObj.List<T>();
            }
            catch (HibernateException exception)
            {
                throw new DaoException("Fail to find by query string", exception);
            }
            finally
            {
               
            }
            return list;
        }

        public IList Query(string filter)
        {
            IList list;
         
            try
            {
                list = session.CreateQuery(filter).List();
            }
            catch (HibernateException exception)
            {
                throw new DaoException("Fail to find by query string", exception);
            }
            finally
            {
              
            }
            return list;
        }

        public IList<T> Query<T>(string filter, int firstResult, int maxResult)
        {
            IList<T> list;
          
            try
            {
                StringBuilder builder = new StringBuilder().AppendFormat("from {0}  {1} ", typeof(T).Name, filter);
                var q = session.CreateQuery(builder.ToString()).SetFirstResult(firstResult).SetMaxResults(maxResult);

                list = q.List<T>();
            }
            catch (HibernateException exception)
            {
                throw new DaoException("Fail to find by query string", exception);
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
            finally
            {
              
            }
            return list;
        }

        public IList Query(Type clazz, int firstResult, int maxResult)
        {
            IList list;
           
            try
            {

                ICriteria criteria = session.CreateCriteria(clazz).SetFirstResult(firstResult).SetMaxResults(maxResult);

                list = criteria.List();
            }
            catch (HibernateException exception)
            {
                throw new DaoException("Fail to load all", exception);
            }
            catch (Exception exception2)
            {
                throw exception2;
            }
            finally
            {
               
            }
            return list;
        }

        public IList<T> Query<T>(string filter, object[] values, int firstResult, int maxResult)
        {
            IList<T> list;
         
            try
            {
                StringBuilder builder = new StringBuilder().AppendFormat("from {0}  {1} ", typeof(T).Name, filter);
                NHibernate.IQuery qObj = session.CreateQuery(builder.ToString()).SetFirstResult(firstResult).SetMaxResults(maxResult);

                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        qObj.SetParameter(i, values[i]);
                    }
                }
                list = qObj.List<T>();
            }
            catch (HibernateException exception)
            {
                throw new DaoException("Fail to find by query string", exception);
            }
            finally
            {
                
            }
            return list;
        }

        public IList<T> Query<T>(int pageIndex, int pageSize, string filter, string sort, out long count)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" FROM {0} ", typeof(T).Name);
            if (!string.IsNullOrEmpty(filter))
            {
                builder.Append(" " + filter + " ");
            }
            if (!string.IsNullOrEmpty(sort))
            {
                builder.Append(" " + sort + " ");
            }
            string queryString = builder.ToString();
            builder.Length = 0;
            builder.AppendFormat(" SELECT COUNT(*) FROM {0} {1} ", typeof(T).Name, filter);
            List<T> list = new List<T>();
            long num = 0L;
          
            try
            {

                NHibernate.IQuery query = session.CreateQuery(queryString);
                query.SetFirstResult((pageIndex - 1) * pageSize).SetMaxResults(pageSize);
                NHibernate.IQuery query2 = session.CreateQuery(builder.ToString());
                IMultiQuery query3 = session.CreateMultiQuery();
                query3.Add(query);
                query3.Add(query2);
                IList list2 = query3.List();
                foreach (object obj2 in (IList)list2[0])
                {
                    list.Add((T)obj2);
                }
                num = (long)((IList)list2[1])[0];
            }
            catch (DaoException exception)
            {
                throw exception;
            }
            catch (Exception exception2)
            {
                throw new DaoException("执行多个查询时失败! 请检查filter或sort", exception2);
            }
            finally
            {
               
            }
            count = num;
            return list;
        }
        #endregion

        /// <summary>
        /// 析构完成session的关闭
        /// </summary>
        ~Dao()
        {
            if (session != null)
            {
                if(session.IsOpen)
                    session.Close();
                session = null;
            }
        }
    }
}
