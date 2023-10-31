using FluentValidation;
using FreeCourse.Web.Models.Discounts;

namespace FreeCourse.Web.Validators
{
    public class DiscountApplyValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim kupon alanı boş olamaz");
        }
    }
}
