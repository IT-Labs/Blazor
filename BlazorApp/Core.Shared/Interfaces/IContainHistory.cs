using Core.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Shared.Interfaces
{
    public interface IContainHistory
    {
        Int64 Id { get; set; }
        DateTime Date { get; set; }
        string SchemaName { get; set; }
        string TableName { get; set; }
        ActivityLogAction Operation { get; set; }
        Guid? UserId { get; set; }
        [Column(TypeName = "jsonb")]
        string NewVal { get; set; }
        [Column(TypeName = "jsonb")]
        string OldVal { get; set; }
        Guid RecordId { get; set; }
    }
}
