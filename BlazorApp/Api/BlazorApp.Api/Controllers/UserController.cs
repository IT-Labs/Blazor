using Core.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorApp.Shared.Entities;

namespace BlazorApp.Api.Controllers
{
    [ApiController]
    public class UserController : BaseApiController
    {
        public UserController(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {

        }

        [HttpGet]
        public BlazorUser GetUser()
        {
            BlazorUser objBlazorUser = new BlazorUser();
            if (this.User.Identity.IsAuthenticated)
            {
                objBlazorUser.UserName = this.User.Identity.Name;
            }
            else
            {
                objBlazorUser.UserName = ""; // Not logged in
            }
            return objBlazorUser;
        }
    }
}