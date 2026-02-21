using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace VentusLibrary.Commons.Behaviors;

/// <summary>
/// Ejecuta validadores FluentValidation antes de los handlers.
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Inicializa el comportamiento con la colección de validadores para el tipo de request.
    /// </summary>
    /// <param name="validators">Validadores registrados para TRequest.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Ejecuta las validaciones y continúa con el siguiente delegado si no hay errores.
    /// </summary>
    /// <param name="request">Solicitud a validar.</param>
    /// <param name="next">Delegado hacia el siguiente componente del pipeline.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Respuesta del handler si la validación es exitosa.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = new List<ValidationFailure>();

            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(context, cancellationToken);
                if (!result.IsValid) failures.AddRange(result.Errors);
            }

            if (failures.Count > 0) throw new ValidationException(failures);
        }

        return await next();
    }
}
