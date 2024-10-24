using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataScience.Controllers
{
    [Authorize(Policy = "RequireAuthorRole")]
    public class PostController : Controller
    {
        public IActionResult AddNewPost()
        {
            return View();
        }
    }
}
