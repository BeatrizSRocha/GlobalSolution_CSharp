using System.Collections.Generic;

namespace ProjetoGS.Web.Models;

public class StatsViewModel
{
    public int TotalTecnologias { get; set; }
    public int TotalMissoes { get; set; }
    public int TotalSetores { get; set; }
    public List<SetorStatsViewModel> DistribuicaoSetores { get; set; } = new();
}

public class SetorStatsViewModel
{
    public string Setor { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public double Percentual { get; set; }
}
