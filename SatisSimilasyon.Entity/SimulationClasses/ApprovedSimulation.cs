using SatisSimilasyon.Entity.BaseClasses;
using SatisSimilasyon.Entity.Enum;
using SatisSimilasyon.Entity.ProductGroupClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.SimulationClasses
{
	public class ApprovedSimulation : BaseObject
	{

    public int? Simulation1Id { get; set; }
    public virtual Simulation Simulation1 { get; set; }
    public int? Simulation2Id { get; set; }
    public virtual Simulation Simulation2 { get; set; }
    public int? SelectedSimulationId { get; set; }
    public virtual Simulation SelectedSimulation { get; set; }
    public string Definition { get; set; }
    public int? ProductGroupId { get; set; }
    public virtual ProductGroupClasses.ProductGroup ProductGroup { get; set; }
    public string ApprovedDefinition { get; set; }
    public SimulationStatus SimulationStatus { get; set; }
    public LocalOrExport LocalOrExport { get; set; }
  }
}
