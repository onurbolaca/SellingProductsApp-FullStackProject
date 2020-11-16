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
	public class Simulation : BaseObject
	{
		public string SimulationName { get; set; }
		public float ResaleMevcutFiyat { get; set; }
		public float ResaleYeniFiyat { get; set; }

		public float NonResaleMevcutFiyat { get; set; }
		public float NonResaleYeniFiyat { get; set; }

		public float MevcutEnflasyon { get; set; }
		public float YeniEnflasyon { get; set; }

		public float ArtisMevcutResale { get; set; }
		public float ArtisYeniResale { get; set; }

		public float ArtisMevcutNonResale { get; set; }
		public float ArtisYeniNonResale { get; set; }

		public float MevcutLTA { get; set; }
		public float YeniLTA { get; set; }

		public float MevcutArtisExportResale { get; set; }
		public float YeniArtisExportResale { get; set; }
		public float MevcutExportNonResale { get; set; }
		public float YeniArtisExportNonResale { get; set; }

		public Department? SimulationType { get; set; }

		public virtual List<SimulationLine> SimulationLines { get; set; }

		public int ProductGroupId { get; set; }
		public virtual ProductGroup ProductGroups { get; set; }
		public LocalOrExport LocalOrExport { get; set; }

		public SimulationStatus? SimulationStatus { get; set; }

		public List<ApprovedSimulation> ApprovedSimilations { get; set; }
	}
}
