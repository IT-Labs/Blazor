using Sotsera.Blazor.Toaster;
using Sotsera.Blazor.Toaster.Core.Models;

namespace BlazorApp.Client.Services
{
    public class ToasterService
    {
        private readonly IToaster _service;

        private readonly int _maximumOpacity;
        private readonly bool _escapeHtml;
        private readonly bool _requireInteraction;
        private readonly bool _showProgressBar;
        private readonly bool _showCloseIcon;
        private readonly int _showTransitionDuration;
        private readonly int _visibleStateDuration;
        private readonly int _hideTransitionDuration;

        public ToasterService(IToaster service)
        {
            _service = service;
            _maximumOpacity = _service.Configuration.MaximumOpacity;
            _escapeHtml = _service.Configuration.EscapeHtml;
            _requireInteraction = _service.Configuration.RequireInteraction;
            _showProgressBar = _service.Configuration.ShowProgressBar;
            _showCloseIcon = _service.Configuration.ShowCloseIcon;
            _showTransitionDuration = _service.Configuration.ShowTransitionDuration;
            _visibleStateDuration = _service.Configuration.VisibleStateDuration;
            _hideTransitionDuration = _service.Configuration.HideTransitionDuration;
        }

        public void ShowError(string message)
        {
            _service.Add(ToastType.Error, message, "Error", config => ConfigureOptions(config));
        }

        public void ShowSucess()
        {
            _service.Add(ToastType.Success, "Action completed", "Success", config => ConfigureOptions(config));
        }

        private ToastOptions ConfigureOptions(ToastOptions options)
        {
            options.MaximumOpacity = _maximumOpacity;
            options.EscapeHtml = _escapeHtml;
            options.RequireInteraction = _requireInteraction;
            options.ShowProgressBar = _showProgressBar;
            options.ShowCloseIcon = _showCloseIcon;
            options.ShowTransitionDuration = _showTransitionDuration;
            options.VisibleStateDuration = _visibleStateDuration;
            options.HideTransitionDuration = _hideTransitionDuration;
            return options;
        }
    }
}
