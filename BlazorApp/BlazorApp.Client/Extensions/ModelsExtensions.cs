using BlazorApp.Client.Models;
using BlazorApp.Shared.Entities;

namespace BlazorApp.Client.Extensions
{
    public static class ModelsExtensions
    {
        public static Appointment ToAppointment(this Movie movie)
        {
            return new Appointment
            {
                Caption = movie.TitleBrief,
                StartDate = movie.PremiereDate,
                EndDate = movie.PremiereDate.AddHours(2),
                Label = 8,
                Status = 1
            };
        }
    }
}
