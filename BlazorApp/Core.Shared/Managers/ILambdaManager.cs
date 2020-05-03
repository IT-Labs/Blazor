using Core.Shared.Response;

namespace Core.Shared.Managers
{
    public interface ILambdaManager
    {
        Response<T> InvokeLambda<TRequest, T>(string functionName, TRequest requestPayload);
    }
}