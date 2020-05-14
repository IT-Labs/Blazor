using BlazorApp.Client.Extensions;
using BlazorApp.Client.Interfaces;
using BlazorApp.Client.Services;
using BlazorApp.Shared.Entities;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Response;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages
{
    public partial class PremiereCalendar
    {
        [Inject] public IMoviesService MoviesService { get; set; }

        private PagedResponse<Movie> _moviesResponse;
        private DxSchedulerDataStorage _dataStorage = new DxSchedulerDataStorage();
        private readonly DxSchedulerTimeSpanRange _visibleTime = new DxSchedulerTimeSpanRange(TimeSpan.FromHours(8), TimeSpan.FromHours(23));
        private readonly DxSchedulerTimeSpanRange _workTime = new DxSchedulerTimeSpanRange(TimeSpan.FromHours(8), TimeSpan.FromHours(23));
        private GetMoviesRequest _request;

        protected async override Task OnInitializedAsync()
        {
            _request = new GetMoviesRequest { All = true };
            _moviesResponse = await MoviesService.GetMultiple(_request);
            if (_moviesResponse.Ok)
            {
                _dataStorage = new DxSchedulerDataStorage()
                {
                    AppointmentsSource = _moviesResponse.Payload.Select(x => x.ToAppointment()),
                    AppointmentMappings = new DxSchedulerAppointmentMappings()
                    {
                        Type = "AppointmentType",
                        Start = "StartDate",
                        End = "EndDate",
                        Subject = "Caption",
                        AllDay = "AllDay",
                        Location = "Location",
                        Description = "Description",
                        LabelId = "Label",
                        StatusId = "Status",
                        RecurrenceInfo = "Recurrence"
                    }
                };

            }
        }
    }
}
