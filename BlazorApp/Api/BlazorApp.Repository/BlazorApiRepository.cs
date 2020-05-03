using Core.Shared.Repository;
using Core.Framework.Repository;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Repository
{
    public class BlazorApiRepository : DomainRepository
    {
        public BlazorApiRepository(IContext dataContext, ILogger<BlazorApiRepository> logger)
            : base(dataContext, logger)
        {
        }
    }
}
