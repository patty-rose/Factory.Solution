using System.Collections.Generic;
using System;

namespace Factory.Models
{
  public class Engineer
  {

    public Engineer()
      {
          this.JoinEngineerMachine = new HashSet<EngineerMachine>();
      }
      
    // auto implemented properties
    public int EngineerId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<EngineerMachine> JoinEngineerMachine { get; }
  }
}