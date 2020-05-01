using System;

namespace BlazorApp.Shared.Requests
{
    public class SetActiveStatusRequest : IdRequest
    {
        public SetActiveStatusRequest() { }

        public SetActiveStatusRequest(long id)
        {
            Id = id;
        }
        public bool IsActive { get; set; }
        public bool SetParent { get; set; }
    }

    public class SetMultipleActiveStatusRequest : SetActiveStatusRequest
    {
        public SetMultipleActiveStatusRequest(bool isActive) : base(0)
        {
            IsActive = isActive;
        }
        public SetMultipleActiveStatusRequest(bool isActive, bool setParent) : this(isActive)
        {
            SetParent = setParent;
        }
    }

    public class SetActiveStatusRequest<T> : SetActiveStatusRequest
        where T : AuditableEntity
    {
        public SetActiveStatusRequest() { }
        public SetActiveStatusRequest(IdRequest request, bool isActive)
            : this(request.Id, isActive)
        {
        }
        public SetActiveStatusRequest(long id, bool isActive)
        {
            IsActive = isActive;
            Id = id;
        }
    }

    public class SetActiveStatusPairRequest<T> : SetActiveStatusRequest<T>
        where T : AuditableEntity
    {
        public SetActiveStatusPairRequest()
        {

        }
        public long PairId { get; set; }
        public SetActiveStatusPairRequest(IdRequest request, long pairId, bool isActive)
            : this(request.Id, pairId, isActive)
        {
        }
        public SetActiveStatusPairRequest(long id, long pairId, bool isActive)
        {
            IsActive = isActive;
            Id = id;
            PairId = pairId;

        }
    }

    public class SetParentActiveStatusRequest : IdRequest
    {
        public SetParentActiveStatusRequest() { }

        public SetParentActiveStatusRequest(long id)
        {
            Id = id;
        }
        public bool IsParentActive { get; set; }
    }

    public class SetParentActiveStatusRequest<T> : SetParentActiveStatusRequest
        where T : AuditableEntity
    {
        public SetParentActiveStatusRequest() { }
        public SetParentActiveStatusRequest(IdRequest request, bool isParentActive)
            : this(request.Id, isParentActive)
        {
        }
        public SetParentActiveStatusRequest(long id, bool isParentActive)
        {
            IsParentActive = isParentActive;
            Id = id;
        }
    }
}