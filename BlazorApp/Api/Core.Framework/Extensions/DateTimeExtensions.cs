﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TimeZoneConverter;

namespace Core.Framework.Extensions
{
    public static class DateTimeExtensions
    {
        public static string DateTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd hh:mm tt", CultureInfo.InvariantCulture);
        }

        public static string DateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static DateTime SpecifyUtcKind(this DateTime date)
        {
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }
        public static DateTime? SpecifyUtcKind(this DateTime? date)
        {
            return date.HasValue ? (DateTime?)SpecifyUtcKind(date.Value) : null;
        }

        public static double ConvertMinutesToDays(this int minutes)
        {
            return Math.Round(TimeSpan.FromMinutes(minutes).TotalDays);
        }

        public static DateTime FromUtcToEst(this DateTime date)
        {
            var easternZoneId = "Central European Standard Time";
            var easternZone = TZConvert.GetTimeZoneInfo(easternZoneId);
            var estDate = TimeZoneInfo.ConvertTimeFromUtc(date, easternZone);
            return estDate;
        }

        public static string To12HourDateFormat(this DateTime date)
        {
            return date.ToString("MM/dd/yyyy hh:mm tt");
        }

        public static IEnumerable<string> GetTimezones()
	    {
		    var list = TimeZoneInfo.GetSystemTimeZones().Select(x => new
			{
				x.DisplayName,
				x.Id
			});

            return list.Select(x => x.DisplayName);
	    }
    }
}
