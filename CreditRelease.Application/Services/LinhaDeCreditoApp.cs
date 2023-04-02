using AutoMapper;
using CreditRelease.Application.Interfaces;
using CreditRelease.Utility.DTOs;
using CreditRelease.Utility.Enums;

namespace CreditRelease.Application.Services
{
    public class LinhaDeCreditoApp : ILinhaDeCreditoApp
    {
        private readonly IMapper _mapper;
        public LinhaDeCreditoApp(IMapper mapper)
        {
            _mapper = mapper; 

        }
        public async Task<ResultDTO> ObterLinhaDeCredito(LinhaDeCreditoDTO credito)
        {
            int valorTaxa = (int)Enum.Parse(typeof(TaxasTiposDeCreditosEnum), credito.TipoCredito);

            decimal valorJuros = valorTaxa / 100m * credito.ValorCredito;

            return new ResultDTO()
            {
                StatusCredito = "APROVADO",
                ValorTotalComJuros = credito.ValorCredito + valorJuros,
                ValorDosJuros = valorJuros
            };
        }
      
    }
}
