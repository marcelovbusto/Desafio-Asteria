using AsteriaChallenger.Data;
using AsteriaChallenger.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;

namespace AsteriaChallenger.Controllers
{
  public class ImportController : Controller
  {
    private readonly AppDbContext _context;

    public ImportController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile file)
    {
      if (file == null || file.Length == 0)
      {
        TempData["Message"] = "Nenhum arquivo selecionado.";
        return View();
      }

      try
      {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var stream = file.OpenReadStream();
        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];
        var rowCount = worksheet.Dimension.Rows;

        var salesList = new List<SalesData>();

        for (int row = 2; row <= rowCount; row++)
        {
          // Colunas do Excel (A..F):
          // A: Código Cliente
          // B: Categoria do Produto
          // C: Sku/Produto
          // D: Data
          // E: Quantidade
          // F: Valor de Faturamento

          // Lê o texto (fazendo Trim para remover espaços extras)
          string codigoClienteText = worksheet.Cells[row, 1].Text?.Trim();
          string categoriaText = worksheet.Cells[row, 2].Text?.Trim();
          string skuProdutoText = worksheet.Cells[row, 3].Text?.Trim();
          string dataText = worksheet.Cells[row, 4].Text?.Trim();
          string quantidadeText = worksheet.Cells[row, 5].Text?.Trim();
          string valorFatText = worksheet.Cells[row, 6].Text?.Trim();

          // 1) Converter Data
          //   Se "3/2/2022" for (mês=3, dia=2), use "M/d/yyyy".
          //   Se for (dia=3, mês=2), use "d/M/yyyy".
          if (!DateTime.TryParseExact(
                  dataText,
                  "M/d/yyyy", // ou "d/M/yyyy" se for dia/mês/ano
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None,
                  out DateTime dataVenda))
          {
            // Se não conseguir converter, defina data default ou gere erro
            dataVenda = DateTime.MinValue;
          }

          // 2) Converter Quantidade (int)
          if (!int.TryParse(quantidadeText, out int quantidade))
          {
            quantidade = 0; // ou tratar erro
          }

          // 3) Converter Valor (decimal)
          //   Usamos NumberStyles.Any e CultureInfo.InvariantCulture
          //   para aceitar ponto como separador decimal
          if (!decimal.TryParse(valorFatText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
          {
            valor = 0;
          }

          // Criar objeto SalesData
          var sale = new SalesData
          {
            CodigoCliente = codigoClienteText,
            Categoria = categoriaText,
            SKU = skuProdutoText,    // Se quiser armazenar no campo "SKU"
            Produto = skuProdutoText,    // Se quiser duplicar no campo "Produto"
            DataVenda = dataVenda,
            Quantidade = quantidade,
            Valor = valor
          };

          salesList.Add(sale);
        }

        // Bulk Insert (para muitos registros)
        var bulkConfig = new BulkConfig
        {
          BatchSize = 100000,
          UseTempDB = true
        };

        await _context.BulkInsertAsync(salesList, bulkConfig);

        TempData["Message"] = $"Foram importadas {salesList.Count} linhas com sucesso!";
        return RedirectToAction("Index", "Sales");
      }
      catch (Exception ex)
      {
        TempData["Message"] = "Erro ao importar: " + ex.Message;
        return View();
      }
    }
  }
}
