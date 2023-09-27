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
        protected readonly AuthIdentity authIdentity;

        public BaseController(IServiceManager serviceManager, ILoggerManager loggerManager)
        {
            this.serviceManager = serviceManager;
            this.loggerManager = loggerManager;
            authIdentity = HttpContext.GetAuthIdentity();
        }
    }
}
