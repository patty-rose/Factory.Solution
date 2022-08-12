using Microsoft.AspNetCore.Mvc;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}