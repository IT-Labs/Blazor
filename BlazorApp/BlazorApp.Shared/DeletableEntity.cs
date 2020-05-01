using System;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared
{
    public class DeletableEntity : AuditableEntity, IDeleted, IHaveIsActive
    {
        public virtual DateTime? DeletedAt { get; set; }
        public long? DeletedBy { get; set; }

        public bool IsActive { get; set; } = true;

        public override void UpdateAuditableProperties(AuditableAction action = AuditableAction.Insert,
             long? userId = null)
        {
            DeletedAt = action == AuditableAction.Delete
                ? DateTime.UtcNow
                : action == AuditableAction.Reactivate
                    ? null
                    : DeletedAt;

            DeletedBy = action == AuditableAction.Delete
                ? userId
                : action == AuditableAction.Reactivate
                    ? null
                    : DeletedBy;

            base.UpdateAuditableProperties(action, userId);
        }


    }
}
