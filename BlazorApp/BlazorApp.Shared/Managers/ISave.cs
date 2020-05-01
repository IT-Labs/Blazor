using BlazorApp.Shared.Repository;
using BlazorApp.Shared.Requests;
using BlazorApp.Shared.Response;
using System;
using System.Collections.Generic;

namespace BlazorApp.Shared.Managers
{
    public interface ISave
    {
        Response<long> Save<T, TRequest>(TRequest request, IQueryInclude<T> includeQuery = null)
            where T : AuditableEntity, new()
            where TRequest : SaveRequest;
    }
    public interface ISave<T>
        where T : AuditableEntity, new()

    {
        Response<long> Save<TSaveRequest>(TSaveRequest request, IQueryInclude<T> includeQuery = null)
            where TSaveRequest : SaveRequest;

        Response<bool> SaveMultiple<TSaveRequest>(List<TSaveRequest> request, IQueryInclude<T> includeQuery = null, bool stopOnFirstInvalidRequest = false) 
          where TSaveRequest : SaveRequest;

    }
}