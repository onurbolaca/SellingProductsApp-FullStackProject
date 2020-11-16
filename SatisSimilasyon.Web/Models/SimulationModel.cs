using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SatisSimilasyon.Web.Models
{
  public class SimulationModel
  {
    public string SimulationName { get; set; }
    public string ResaleMevcutFiyat { get; set; }
    public string ResaleYeniFiyat { get; set; }
    public string NonResaleMevcutFiyat { get; set; }
    public string NonResaleYeniFiyat { get; set; }
    public string MevutEnflasyon { get; set; }
    public string YeniEnflasyon { get; set; }
    public string ArtisMevcutResale { get; set; }
    public string ArtisYeniResale { get; set; }
    public string ArtisMevcutNonResale { get; set; }
    public string ArtisYeniNonResale { get; set; }
    public string MevcutLTA { get; set; }
    public string YeniLTA { get; set; }
    public string MevcutArtisExportResale { get; set; }
    public string YeniArtisExportResale { get; set; }
    public string MevcutExportNonResale { get; set; }
    public string YeniArtisExportNonResale { get; set; }
    public string ProductGroupID { get; set; }

    public string LocalOrExport { get; set; }
    public List<SimulationLineModel> Lines { get; set; }
  }
}