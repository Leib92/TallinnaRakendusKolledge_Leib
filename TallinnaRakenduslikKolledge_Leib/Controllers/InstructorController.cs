using Microsoft.AspNetCore.Mvc;

namespace TallinnaRakenduslikKolledge_Leib.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
