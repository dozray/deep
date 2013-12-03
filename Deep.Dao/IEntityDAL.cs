using System;
using System.Collections;
using deep.entity;
namespace deep.dao
{
    public interface IEntityDAL : IDao
    {
        IList FindByAbbrName(Type type, string abbrName);
        IList FindByAttributeAndValue(Type type, string attribName, string value);
        IList FindByAttributeAndValue(Type type, string attribName, bool value);
        IList FindByAttributeAndValue(Type type, string attribName, object obj);
        IList FindByCode(Type type, string code);
        IList FindByDesciption(Type type, string description);
        IList FindByName(Type type, string name);
        IList FindByQuickCode(Type type, string quickCode);
        IList GetEntityList(Type type, int firstResult, int lastResult);
        IList GetEntityList(Type type);
        IEntity Load(ISequence seq);
        IList QueryByInstance(object obj, int firstResult, int lastResult);
        IList QueryByInstance(object obj);
       
       
    }
}
