using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.WebExceptionsPresenter
{
    public class ApiExceptionFilterAttribute: ExceptionFilterAttribute
    {
        readonly IDictionary<Type, IExeptionHandler>
            ExceptionHandlers;

        public ApiExceptionFilterAttribute(IDictionary<Type,
            IExeptionHandler>
            exceptionHandlers)=>ExceptionHandlers
            =exceptionHandlers;

        public override void OnException(ExceptionContext context)
        {
            Type ExceptionType= context.Exception.GetType();
            if (ExceptionHandlers.ContainsKey(ExceptionType))
            {
                ExceptionHandlers[ExceptionType].Handle(context);
            }
            else
            {
                new ExceptionHandlerBase().SetResult(context, StatusCodes
                    .Status500InternalServerError, "Ha ocurrido un error al procesar" +
                    "la respuesta.","");
            }
            base.OnException(context);
        }


    }
}
