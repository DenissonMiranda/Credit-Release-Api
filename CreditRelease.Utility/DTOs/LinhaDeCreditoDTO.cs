namespace CreditRelease.Utility.DTOs
{
    public class LinhaDeCreditoDTO
    {
        public decimal ValorCredito { get; set; }
        public string? TipoCredito { get; set; }
        public int QtdParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }

    }
}
