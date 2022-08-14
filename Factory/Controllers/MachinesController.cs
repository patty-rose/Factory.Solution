using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
    return View(_db.Machines.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine, int EngineerId)
    {
      var dupe = _db.Machines.Where(a => a.Model == machine.Model).SingleOrDefault();
      if(dupe == null)
      {
        _db.Machines.Add(machine);
        _db.SaveChanges();
        if (EngineerId !=0)
        {
          _db.EngineerMachine.Add(new EngineerMachine() {
            EngineerId = EngineerId, MachineId = machine.MachineId
          });
          _db.SaveChanges();
        }
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");

        ViewBag.Duplicate = 1;

        return View();
      }
    }

    public ActionResult Details(int id)
    {
      var thisMachine = _db.Machines
        .Include(machine => machine.JoinEngineerMachine)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(machine => machine.
        MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine, int EngineerId)
    {
      if (EngineerId != 0)
        {
          _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
        }
        _db.Entry(machine).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddEngineer(int id)
    { 
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int EngineerId)
    {
      var dupe = _db.EngineerMachine.Where(a => a.MachineId == machine.MachineId && a.EngineerId == EngineerId).SingleOrDefault();
      
      if(dupe == null && EngineerId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machine.MachineId, EngineerId = EngineerId });
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.Duplicate = 1;
        ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
        return View(machine);
      }
    }

    public ActionResult Delete(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteEngineer(int joinId)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}