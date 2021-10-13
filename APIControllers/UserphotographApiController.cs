using Instagram.Helpers;
using Instagram.InstagramContext;
using Instagram.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Controllers
{
    
    [ApiController]
    public class UserphotographApiController : ControllerBase
    {

        UserManager usermanager = new UserManager();
        instagramContext _context = ContextManager.GetContext();
        private readonly ILogger<UserphotographApiController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        SessionHelper sessionHelper;

        public UserphotographApiController(ILogger<UserphotographApiController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);

        }



      

    }

 
}
