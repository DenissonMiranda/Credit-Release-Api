using CreditRelease.Utility.DTOs;
using CreditRelease.Utility.Enums;
using FluentValidation;

namespace CreditRelease.Application.Validators
{
    public class LinhaDeCreditoValidators : AbstractValidator<LinhaDeCreditoDTO>
    {
        public LinhaDeCreditoValidators()
        {
            RuleFor(x => x.ValorCredito)
                .NotNull()
                .WithMessage("Valor Obrigatorio.")
                .LessThanOrEqualTo(1000000)
                .WithMessage("O valor máximo a ser liberado para qualquer tipo de empréstimo é de R$ 1.000.000,00");
            
            RuleFor(x => x.QtdParcelas)
              .NotNull()
              .WithMessage("Valor Obrigatorio.")
              .InclusiveBetween(5, 72)
              .WithMessage("A quantidade mínima de parcelas é de 5x e a máxima é de 72x");

            RuleFor(x => x.TipoCredito)
                .NotEmpty()
                .WithMessage("Valor Obrigatorio.")
                .Must(x => Enum.IsDefined(typeof(TiposDeCreditosEnum), x.ToUpper()))
                .WithMessage("Tipo de Credito Informado Invalido")
                .Equal("CREDITO_PESSOA_JURIDICA")
                .When(x => x.ValorCredito >= 15000)
                .WithMessage("Somente é permitido crédito para pessoa jurídica com valor mínimo de R$ 15.000,00.");

            RuleFor(x => x.DataPrimeiroVencimento)
                .NotEmpty()
                .WithMessage("Valor Obrigatorio.")
                .GreaterThanOrEqualTo(DateTime.Now.AddDays(15))
                .WithMessage("A data do primeiro vencimento deve ser no mínimo 15 dias a partir da data atual.")
                .LessThanOrEqualTo(DateTime.Now.AddDays(40))
                .WithMessage("A data do primeiro vencimento deve ser no máximo 40 dias a partir da data atual.");
        }
    }
}
