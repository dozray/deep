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
namespace deep.dao
{
    /// <summary>
    /// 定义的所有查询的协议
    /// </summary>
    public interface IFind
    {
        System.Collections.IList Find(string queryString, object[] values);
        System.Collections.IList Find(string queryString, object value);
        System.Collections.IList Find(string queryString, object[] values, int firstResult, int maxResult);
        System.Collections.IList Find(string queryString, int firstResult, int maxResult);
        System.Collections.IList Find(string queryString);
        System.Collections.IList FindAll(Type type);
        System.Collections.IList FindByCriterions(Type type, System.Collections.IList restrictions);
        System.Collections.IList FindByCriterions(Type type, System.Collections.IList restrictions, int firstResult, int maxResult);
        System.Collections.IList FindByHQLQuery(string hqlQuery);
        System.Collections.IList FindByNamedParam(string queryString, string[] paramNames, object[] values);
        System.Collections.IList FindByNamedParam(string queryString, string paramName, object value, int firstResult, int maxResult);
        System.Collections.IList FindByNamedParam(string queryString, string[] paramNames, object[] values, int firstResult, int maxResult);
        System.Collections.IList FindByNamedParam(string queryString, string paramName, object value);
        System.Collections.IList FindByNamedQuery(string queryName);
        System.Collections.IList FindByNamedQuery(string queryName, object[] values);
        System.Collections.IList FindByNamedQuery(string queryName, object value);
        System.Collections.IList FindByNamedQueryAndNamedParam(string queryName, string[] paramNames, object[] values);
        System.Collections.IList FindByNamedQueryAndNamedParam(string queryName, string paramName, object value);
        System.Collections.IList FindByNamedQueryAndValueBean(string queryName, object valueBean);
        System.Collections.IList FindByProperty(Type type, NHibernate.Criterion.ICriterion restriction);
        System.Collections.IList FindBySQLQuery(string sqlQuery, string aliasName, Type type);
        System.Collections.IList FindByValueBean(string queryString, object valueBean);
        System.Collections.IList QueryByExample(Type entityType, object persistentObj, int firstResult, int maxResult);
        System.Collections.IList QueryByExample(Type entityType, object persistentObj);
        System.Collections.Generic.IList<T> UseQueryByExample<T>(T obj);
    }
}
