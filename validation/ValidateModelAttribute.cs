using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api8.validation
{
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid) 
            {
                context.Result = BadRequestResult();
            }
            
        }

        private IActionResult? BadRequestResult()
        {
            throw new NotImplementedException();
        }
    }
}
