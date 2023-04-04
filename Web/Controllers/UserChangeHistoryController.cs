using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Web.Infrastructure;

namespace Web.Controllers
{
    [Authorize]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class UserChangeHistoryController : BaseController
    {
        private readonly IMapper _mapper; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserChangeHistoryController(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor; 
        }

      

        public IActionResult Detail(ChangeType changeType, Guid RootId, Guid NodeId, Guid RootTranslationId, Guid NodeTranslationId)
        {

            switch (changeType)
            {
                 
                case ChangeType.NodeTranslation:
                    {
                        return RedirectToAction("UserChangeHistory", "Index");
                    }
                default: { return RedirectToAction("UserChangeHistory", "Index"); }
            }

        }
    }
}
