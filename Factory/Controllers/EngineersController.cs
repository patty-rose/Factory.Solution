using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
    return View(_db.Engineers.ToList());
    }

    public ActionResult Create()
    {

      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Model");
      
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer, int MachineId)
    {
      var dupe = _db.Engineers.Where(a => a.Name == engineer.Name).SingleOrDefault();
      if(dupe == null)
      {
        _db.Engineers.Add(engineer);
        _db.SaveChanges();
        if (MachineId !=0)
        {
          _db.EngineerMachine.Add(new EngineerMachine() {
            EngineerId = engineer.EngineerId, MachineId = MachineId
          });
          _db.SaveChanges();
        }
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Model");

        ViewBag.Duplicate = 1;
        
        return View();
      }
    }

    public ActionResult Details(int id)
    {
      var thisEngineer = _db.Engineers
        .Include(engineer => engineer.JoinEngineerMachine)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.
        EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult Edit(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Model");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer, int MachineId)
    {
      if (MachineId != 0)
        {
          _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
        }
        _db.Entry(engineer).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    { 
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);

      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Model");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int MachineId)
    {
      var dupe = _db.EngineerMachine.Where(a => a.MachineId == MachineId && a.EngineerId == engineer.EngineerId).SingleOrDefault();
      
      if(dupe == null && MachineId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.Duplicate = 1;
        ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Model");
        return View(engineer);
      }
    }


    public ActionResult Delete(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteMachine(int joinId)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}