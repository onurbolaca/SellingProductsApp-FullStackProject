using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SatisSimilasyon.Web.Models
{
  public class ApprovalModel
  {
    public string Simulation1ID { get; set; }
    public string Simulation2ID { get; set; }
    public string SelectedSimulationID { get; set; }
    public string Definition { get; set; }
    public string ProductGroupID { get; set; }
    public string LocalOrExport { get; set; }

  }
}