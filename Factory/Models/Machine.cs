using System.Collections.Generic;
using System;

namespace Factory.Models
{
  public class Machine
  {

    public Machine()
      {
          this.JoinEngineerMachine = new HashSet<EngineerMachine>();
      }
      
    // auto implemented properties
    public int MachineId { get; set; }
    public string Model { get; set; }
    public virtual ICollection<EngineerMachine> JoinEngineerMachine { get; }
  }
}