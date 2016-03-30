using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace service.procalculo.Helpers
{
    public class EnableCorsAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(
            HttpActionExecutedContext context
            )
        {
            context.Response.Headers
                .Add("Access-Control-Allow-Origin", "*");
        }
    }
}
