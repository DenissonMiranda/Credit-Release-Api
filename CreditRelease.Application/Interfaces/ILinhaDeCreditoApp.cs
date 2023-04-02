using CreditRelease.Utility.DTOs;

namespace CreditRelease.Application.Interfaces
{
    public interface ILinhaDeCreditoApp
    {
        Task<ResultDTO> ObterLinhaDeCredito(LinhaDeCreditoDTO credito);
    }
}
