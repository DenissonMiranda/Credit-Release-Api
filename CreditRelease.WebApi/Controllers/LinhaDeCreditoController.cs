using CreditRelease.Application.Interfaces;
using CreditRelease.Application.Validators;
using CreditRelease.Utility.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CreditRelease.WebApi.Controllers
{
    [Route("api/linhaDeCredito")]
    public class LinhaDeCreditoController : ControllerBase
    {
        private readonly ILinhaDeCreditoApp _app;

        public LinhaDeCreditoController(ILinhaDeCreditoApp app)
        {
            _app = app;
        }


        [HttpPost("obterLinhaDeCredito")]
        public async Task<IActionResult> ObterLinhaDeCredito([FromBody] LinhaDeCreditoDTO credito)
        {
            try
            {
                if (credito == null) throw new Exception("Dados não informados.");

                var validator = new LinhaDeCreditoValidators().Validate(credito);

                if (validator.IsValid)
                {
                    return Ok(await _app.ObterLinhaDeCredito(credito));
                }
                else
                {
                    throw new Exception(validator?.Errors?.FirstOrDefault()?.ErrorMessage);
                }
            } 
            catch (Exception ex)
            {
                return Ok(new ResultDTO()
                { 
                    StatusCredito = "REPROVADO",
                    ValorDosJuros = 0,
                    ValorTotalComJuros = 0                                  
                });

            }
        }
    }
}
