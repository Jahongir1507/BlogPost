using Microsoft.AspNetCore.Mvc;

namespace WeebApp.Controllers
{
    public class PostsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
