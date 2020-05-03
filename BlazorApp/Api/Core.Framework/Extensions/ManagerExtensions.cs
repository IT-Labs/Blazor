using Core.Shared;
using Core.Shared.Enums;
using Core.Shared.Interfaces;
using Core.Shared.Requests;
using Core.Shared.Response;
using System;
using System.Threading.Tasks;

namespace Core.Framework.Extensions
{
    public static class ManagerExtensions
    {
        public static TResult HandleFilePost<TRequest, TResult>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "", FileParameter file = null) where TRequest : IRequest, new()
            where TResult : IResponse, new()
        {
            return manager.HttpClient.HandlePost<TRequest, TResult>(ApiModel(manager, request, controllerName, route), manager.Logger, file).Result;
        }

        public static TResult HandlePost<TRequest, TResult>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()
            where TResult : IResponse, new()
        {
            return manager.HttpClient.HandlePost<TRequest, TResult>(ApiModel(manager, request, controllerName, route), manager.Logger).Result;
        }
        public static Response<Guid> HandlePost<TRequest>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()

        {
            return manager.HttpClient.HandlePost(ApiModel(manager, request, controllerName, route), manager.Logger).Result;
        }

        public static TResult HandlePut<TRequest, TResult>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()
            where TResult : IResponse, new()
        {
            return manager.HttpClient.HandlePut<TRequest, TResult>(ApiModel(manager, request, controllerName, route), manager.Logger).Result;
        }

        public static Response<Guid> HandlePut<TRequest>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()

        {
            return manager.HttpClient.HandlePut(ApiModel(manager, request, controllerName, route), manager.Logger).Result;
        }
        public static Task<TResult> HandleGetTask<TRequest, TResult>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()
            where TResult : IResponse, new()
        {
            if (string.IsNullOrEmpty(route) && request is IdRequest)
            {
                var data = request as IdRequest;
                route = $"{data.Id}";
            }

            return manager.HttpClient.HandleGet<TRequest, TResult>(ApiModel(manager, request, controllerName, route), manager.Logger);
        }

        public static TResult HandleGet<TRequest, TResult>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()
            where TResult : IResponse, new()
        {
            return HandleGetTask<TRequest, TResult>(manager, request, controllerName, route).Result;
        }

        public static TResult HandleDelete<TRequest, TResult>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()
            where TResult : IResponse, new()
        {
            return manager.HttpClient.HandleDelete<TRequest, TResult>(ApiModel(manager, request, controllerName, route), manager.Logger).Result;
        }
        public static Response<bool> HandleDelete<TRequest>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()

        {
            return manager.HttpClient.HandleDelete(ApiModel(manager, request, controllerName, route), manager.Logger).Result;
        }

        public static Task<Response<bool>> HandleDeleteTask<TRequest, TResult>(this IHaveServiceUrlsClient manager, TRequest request, ControllerName controllerName, string route = "") where TRequest : IRequest, new()

        {
            return manager.HttpClient.HandleDelete(ApiModel(manager, request, controllerName, route), manager.Logger);
        }

        private static ApiModel<TRequest> ApiModel<TRequest>(this IHaveServiceUrlsClient manager, TRequest request,
            ControllerName controllerName, string route = "") where TRequest : IRequest, new()
        {
            var hostType = controllerName.GetAttribute<HostUrlAttribute>().HostType;
            Uri.TryCreate($"http://localhost", UriKind.Absolute, out Uri referrer);

            return new ApiModel<TRequest>
            {
                Referrer = referrer,
                Request = request,
                Route = route,
                ApiUrl = $"http://localhost",
                Controller = controllerName.ToString().ToLower(),
                Token = manager.ContextInfo.Token
            };
        }
    }
}