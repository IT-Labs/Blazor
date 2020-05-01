using System;
using BlazorApp.Shared.Interfaces;

namespace BlazorApp.Shared
{
    public class AuditableEntity : Entity, ICreated, IUpdated
    {
        public enum AuditableAction
        {
            Insert,
            Update,
            Delete,
            Reactivate
        }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }

        public virtual void UpdateAuditableProperties(AuditableAction action = AuditableAction.Insert, long? userId = null)
        {
            UpdatedAt = action != AuditableAction.Delete && action != AuditableAction.Reactivate
                ? DateTime.UtcNow
                : UpdatedAt;

            UpdatedBy = action != AuditableAction.Delete && action != AuditableAction.Reactivate
                ? userId
                : UpdatedBy;

            CreatedAt = action == AuditableAction.Insert
                ? DateTime.UtcNow
                : CreatedAt;

            CreatedBy = action == AuditableAction.Insert
                ? userId
                : CreatedBy;
        }
    }
}
