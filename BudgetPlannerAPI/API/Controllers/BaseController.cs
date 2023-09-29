using Common.DataTransferObjects.Authentication;
using Common.Utils;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IServiceManager serviceManager;
        protected readonly ILoggerManager loggerManager;


        private readonly IHttpContextAccessor _contextAccessor;

        public BaseController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor)
        {
            this.serviceManager = serviceManager;
            this.loggerManager = loggerManager;
            _contextAccessor = contextAccessor;
        }

        protected AuthIdentity AuthIdentity
        {
            get
            {
                return _contextAccessor?.HttpContext?.GetAuthIdentity() ?? new AuthIdentity();
            }
        }
    }
}
