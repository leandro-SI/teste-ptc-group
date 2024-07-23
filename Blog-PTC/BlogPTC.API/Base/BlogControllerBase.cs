using Microsoft.AspNetCore.Mvc;

namespace BlogPTC.API.Base
{
    public abstract class BlogControllerBase : ControllerBase
    {
        [NonAction]
        public virtual ObjectResult StatusCodeResponse(int statusCode, string message)
        {
            return this.StatusCode(statusCode, (object)new
            {
                message = new
                {
                    status = statusCode,
                    message = message
                }
            });
        }
    }
}
