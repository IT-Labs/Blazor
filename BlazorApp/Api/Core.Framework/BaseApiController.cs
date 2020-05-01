using Core.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Framework
{
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        public readonly IHttpContextAccessor HttpContextAccessor;

        protected BaseApiController()
        {

        }

        protected BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected UserContextInfo GetUserContext(IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.GetUserContext();
        }

        protected bool UserIdClaimMatch(long id)
        {
            return HttpContextAccessor.UserIdClaimMatch(id);
        }

        protected bool IdNotMatch(long routeId, long? requestId)
        {
            return requestId.HasValue && routeId != requestId;
        }
    }

}
