namespace AsteriaChallenger.Models;

public class SalesData
{
  public int Id { get; set; }  // PK (Identity)

  public DateTime DataVenda { get; set; }
  public string CodigoCliente { get; set; }
  public string Categoria { get; set; }
  public string SKU { get; set; }
  public string Produto { get; set; }
  public decimal Valor { get; set; }
  public int Quantidade { get; set; }
}