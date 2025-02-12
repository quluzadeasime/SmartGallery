using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smart.API.Controllers.Commons
{
    [Authorize]
    public class BaseAuditableAPIController : BaseAPIController { }
}
