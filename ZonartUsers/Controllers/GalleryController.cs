using Microsoft.AspNetCore.Mvc;

namespace ZonartUsers.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
