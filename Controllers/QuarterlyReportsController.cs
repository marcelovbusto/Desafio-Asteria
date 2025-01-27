using AsteriaChallenger.Data;
using AsteriaChallenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsteriaChallenger.Controllers
{
  public class QuarterlyReportsController : Controller
  {
    private readonly AppDbContext _context;

    public QuarterlyReportsController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      // Agrupar as vendas por (Ano, Trimestre)
      var result = await _context.Sales
          .GroupBy(s => new
          {
            Ano = s.DataVenda.Year,
            Tri = (s.DataVenda.Month - 1) / 3 + 1
          })
          .Select(g => new
          {
            Ano = g.Key.Ano,
            Trimestre = g.Key.Tri,
            ValorTotal = g.Sum(x => x.Valor),
            QuantidadeTotal = g.Sum(x => x.Quantidade)
          })
          .OrderBy(x => x.Ano)
          .ThenBy(x => x.Trimestre)
          .ToListAsync();

      // Retorna a View "Index" (Views/RelatoriosTrimestres/Index.cshtml), 
      // passando esse "result" como Model (IEnumerable de tipo anônimo).
      return View(result);
    }
  }
}
