using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
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