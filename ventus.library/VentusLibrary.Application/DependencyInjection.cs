using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VentusLibrary.Application.Authors.Services;
using VentusLibrary.Application.Books.Services.BookLimits.Builders;
using VentusLibrary.Application.Books.Services.BookLimits.Interfaces;
using VentusLibrary.Commons.Behaviors;

namespace VentusLibrary.Application;

/// <summary>
/// Registro de dependencias de la capa de aplicación: MediatR y validaciones.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Agrega MediatR y validadores de FluentValidation explorando el ensamblado actual.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<BookLimitRuleFactory>();
        services.AddScoped<IBookReferencesChecker, BookReferencesChecker>();
        services.AddScoped<BookLimitEnforcer>();
        services.AddScoped<AuthorUpsertEnforcer>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
