using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Shared.ConfigurationValues
{
    public class ScheduleJobSettings
    {
        public List<ScheduleJobSetting> JobSettings { get; set; } = new List<ScheduleJobSetting>();
        public int GetJobTime(string jobClass)
        {
            var jobSetting = JobSettings.FirstOrDefault(x => x.JobName == jobClass);
            if (jobSetting != null)
            {
                return jobSetting.RunEveryMinutes;
            }
            return 5;
        }
        public List<string> GetJobEmails(string jobClass)
        {
            var jobSetting = JobSettings.FirstOrDefault(x => x.JobName == jobClass);
            if (jobSetting != null)
            {
                return jobSetting.Email;
            }
            return new List<string>();
        }
    }

    public class ScheduleJobSetting
    {
        public string JobName { get; set; }
        public int RunEveryMinutes { get; set; }
        public List<string> Email { get; set; }
    }
}