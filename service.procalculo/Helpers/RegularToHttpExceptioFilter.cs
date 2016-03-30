using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web;
using System.Web.Http.Filters;
public class RegularToHttpExceptionFilter
    : ExceptionFilterAttribute
{
    readonly IDictionary<Type, HttpStatusCode> _fromTo;

    public RegularToHttpExceptionFilter()
    {
        _fromTo = new Dictionary<Type, HttpStatusCode>();

        _fromTo.Add(typeof(ArgumentException), HttpStatusCode.BadRequest);
        _fromTo.Add(typeof(SecurityException),HttpStatusCode.Unauthorized);
        _fromTo.Add(typeof(NotImplementedException), HttpStatusCode.NotImplemented);
    }

    public override void OnException(
        HttpActionExecutedContext context
        )
    {
        if (_fromTo.ContainsKey(context.Exception.GetType()))
        {
            context.Response = new HttpResponseMessage(
                _fromTo[context.Exception.GetType()]
                )
            {
                Content = new StringContent(context.Exception.Message)
            };
        }
        else
        {
            base.OnException(context);
        }
    }
}