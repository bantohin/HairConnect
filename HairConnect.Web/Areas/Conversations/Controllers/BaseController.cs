
namespace HairConnect.Web.Areas.Conversations.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Conversations")]
    [Authorize]
    public class BaseController : Controller
    {
    }
}
