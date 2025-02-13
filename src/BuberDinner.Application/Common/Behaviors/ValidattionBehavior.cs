using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;
        public ValidationBehavior(IValidator<TRequest>? validator=null)
        {
            _validator = validator;
        }
        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (_validator is null) 
                return await next();
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
                return await next();
            
            
                var errors = validationResult.Errors
                    .ConvertAll(validateFail => Error.Validation(validateFail.PropertyName,
                    validateFail.ErrorMessage));
                return (dynamic)errors;
            
            //before the handler
            //var result=await next();
            //after the handler
            //return result;
        }
    }
}
