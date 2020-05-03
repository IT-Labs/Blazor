using Core.Shared.Response;

namespace Core.Shared.Managers
{
    public interface IAwsSecretManager
    {
        Response<string> GetSecret(string secretId);
    }
}
