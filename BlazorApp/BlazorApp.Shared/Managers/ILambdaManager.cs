using BlazorApp.Shared.Response;

namespace BlazorApp.Shared.Managers
{
    public interface ILambdaManager
    {
        Response<T> InvokeLambda<TRequest, T>(string functionName, TRequest requestPayload);
    }
}