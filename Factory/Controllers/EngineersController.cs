using Microsoft.AspNetCore.Mvc;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}