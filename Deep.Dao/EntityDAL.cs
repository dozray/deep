using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using deep.entity;
namespace deep.dao
{
    public class EntityDAL:Dao , IEntityDAL
    {
        #region 增删改查
        public IEntity Load(ISequence seq)
        {
            //throw new NotImplementedException();
            IList cs = new ArrayList();
            cs.Add(Expression.Eq("Id", seq.Id));
            IList list = base.FindByCriterions(typeof(IEntity), cs);
            return (((list != null) && (list.Count > 0)) ? (IEntity)list[0] : null);            
        }
    
        public IList GetEntityList(Type type)
        {
            return base.LoadAll(type);
        }
        
        public IList GetEntityList(Type type,int firstResult, int lastResult)
        {
            //throw new NotImplementedException();
            return base.Load(type,firstResult,lastResult);
        }

        public IList QueryByInstance(Object obj)
        {
            //throw new NotImplementedException();
            return base.QueryByExample(obj.GetType(), obj);
        }

        public IList QueryByInstance(Object obj, int firstResult, int lastResult)
        {
            //throw new NotImplementedException();
            return base.QueryByExample(obj.GetType(),obj,firstResult,lastResult);
        }      
      
        #endregion


        #region 属性查询
        public IList FindByCode(Type type,string code)
        {
            //throw new NotImplementedException();
            IList lst = new ArrayList();
            lst.Add(Expression.Eq("Code", code));
            return base.FindByCriterions(type, lst);
        }

        public IList FindByName(Type type, string name)
        {
            //throw new NotImplementedException();
            IList cs = new ArrayList();
            cs.Add(Expression.Eq("Name", name));
            return base.FindByCriterions(type, cs);
        }

        public IList FindByAbbrName(Type type, string abbrName)
        {
            //throw new NotImplementedException();
            IList lst = new ArrayList();
            lst.Add(Expression.Eq("FullName", abbrName));
            return base.FindByCriterions(type, lst);
        }

        public IList FindByQuickCode(Type type, string quickCode)
        {
            //throw new NotImplementedException();
            IList lst = new ArrayList();
            lst.Add(Expression.Eq("QuickCode", quickCode));
            return base.FindByCriterions(type, lst);
        }

        public IList FindByDesciption(Type type, string description)
        {
            //throw new NotImplementedException();
            IList lst = new ArrayList();
            lst.Add(Expression.Eq("Description", description));
            return base.FindByCriterions(type, lst);
        }
        ///// <summary>
        ///// 扩展的通用方法
        ///// </summary>
        ///// <param name="attribName">POCO类的属性名称</param>
        ///// <param name="value">POCO类的属性值</param>
        ///// <returns>符合条件的对象集合列表</returns>
        public IList FindByAttributeAndValue(Type type, string attribName, string value)
        {
            IList lst = new ArrayList();
            lst.Add(Expression.Eq(attribName, value));
            return base.FindByCriterions(type, lst);
        }
        public IList FindByAttributeAndValue(Type type, String attribName, Boolean value)
        {
            IList lst = new ArrayList();
            lst.Add(Expression.Eq(attribName, value));
            return base.FindByCriterions(type, lst);
        }
        public IList FindByAttributeAndValue(Type type, String attribName, object value)
        {
            IList lst = new ArrayList();
            lst.Add(Expression.Eq(attribName, value));
            return base.FindByCriterions(type, lst);
        }

     
        #endregion 
      
    }
}
