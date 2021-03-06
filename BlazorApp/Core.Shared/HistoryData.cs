﻿using Core.Shared.Enums;
using Core.Shared.Interfaces;
using System;

namespace Core.Shared
{
    public class HistoryData : IContainHistory
    {
        public Int64 Id { get; set; }
        public DateTime Date { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public ActivityLogAction Operation { get; set; }
        public Guid? UserId { get; set; }
        public string NewVal { get; set; }
        public string OldVal { get; set; }
        public Guid RecordId { get; set; }
    }
}
