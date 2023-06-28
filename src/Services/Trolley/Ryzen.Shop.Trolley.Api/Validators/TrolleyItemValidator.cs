using FluentValidation;
using Ryzen.Shop.Trolley.Api.ViewModel;
using System;

namespace Ryzen.Shop.Trolley.Api.Validators
{
    public class TrolleyItemValidator : AbstractValidator<TrolleyItemViewModel>
    {
        public TrolleyItemValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }

    public class TrolleyViewModelValidator : AbstractValidator<TrolleyViewModel>
    {
        public TrolleyViewModelValidator()
        {
            RuleFor(x => x.Items).NotNull();
            RuleForEach(x => x.Items).SetValidator(new TrolleyItemValidator());
        }
    }


}
