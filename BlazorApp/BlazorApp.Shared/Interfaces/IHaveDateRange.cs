﻿using System;

namespace BlazorApp.Shared.Interfaces
{
    public interface IHaveDateRange: IHaveStartDate, IHaveEndDate
    {
    }

    public interface IHaveStartDate
    {
        DateTime StartDate { get; set; }

    }
    public interface IHaveEndDate
    {
        DateTime EndDate { get; set; }

    }
}
