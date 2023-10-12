using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UseCases.CreateOrder
{
    public class CreateOrderValidator :
        AbstractValidator<CreateOrderInputPort>   
    {
        public CreateOrderValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty()
                .WithMessage("Debe Porporcionar el identificador del cliente.");
            RuleFor(c => c.ShipAddress).NotEmpty()
                .WithMessage("Debe proporcionar la dirección de envio.");
            RuleFor(c => c.ShipCity).NotEmpty().MinimumLength(3)
                .WithMessage(
                "Debe proporcionar " +
                "al menos 3 caracteres del nombre de la ciudad.");
            RuleFor(c => c.ShipCountry).NotEmpty().MinimumLength(3)
                .WithMessage(
                "Debe proporcionar " +
                "al menos 3 caracteres del nombre del País.");
            RuleFor(c=>c.OrderDetails)
                .Must(d=>d!=null&&d.Any())
                .WithMessage("Deben de especificarse los productos de la orden.");

        }
    }
}
