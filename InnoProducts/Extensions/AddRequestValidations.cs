using FluentValidation.AspNetCore;
using InnoProducts.Validators;

namespace HalfbitZadanie.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddRequestValidations(this IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());

            return services;
        }
    }
}