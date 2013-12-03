namespace deep.dao
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IQueryable
    {
        IList<T> Query<T>(string filter);
        IList Query(string filter);
        IList<T> Query<T>(string filter, int firstResult, int maxResult);
        IList Query(Type clazz, int firstResult, int maxResult);
        IList<T> Query<T>(string filter, object[] values, int firstRecord, int maxRecord);
        IList<T> Query<T>(int pageIndex, int pageSize, string filter, string sort, out long count);
    }
}

