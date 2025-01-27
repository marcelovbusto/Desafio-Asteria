using AsteriaChallenger.Data;
using AsteriaChallenger.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace AsteriaChallenger.Controllers
{
  public class SalesController : Controller
  {
    private readonly AppDbContext _context;

    public SalesController(AppDbContext context)
    {
      _context = context;
    }



    [HttpGet]
    public IActionResult Index(
       int? month,
        string? codigoCliente,
        string? categoria,
        string? sku,
        int page = 1,
        int pageSize = 50)
    {
      // Monta a query base
      var query = _context.Sales.AsQueryable();

      // Filtros, se existirem
      if (month.HasValue)
        query = query.Where(s => s.DataVenda.Month == month.Value);

      if (!string.IsNullOrEmpty(codigoCliente))
        query = query.Where(s => s.CodigoCliente == codigoCliente);

      if (!string.IsNullOrEmpty(categoria))
        query = query.Where(s => s.Categoria == categoria);

      if (!string.IsNullOrEmpty(sku))
        query = query.Where(s => s.SKU == sku);

      // Contagem total para paginação
      int totalRegistros = query.Count();

      // Buscando dados paginados
      var salesData = query
          .OrderBy(s => s.Id)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToList();

      // Valores totais (se quiser exibir na tela)
      decimal valorTotal = totalRegistros > 0 ? query.Sum(s => s.Valor) : 0;
      int quantidadeTotal = totalRegistros > 0 ? query.Sum(s => s.Quantidade) : 0;

      // Passar infos para a View via ViewBag
      ViewBag.TotalRegistros = totalRegistros;
      ViewBag.ValorTotal = valorTotal;
      ViewBag.QuantidadeTotal = quantidadeTotal;
      ViewBag.CurrentPage = page;
      ViewBag.PageSize = pageSize;

      // Retorna a View passando a lista
      return View(salesData);
    }

  }
}
