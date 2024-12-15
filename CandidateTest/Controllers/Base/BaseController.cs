using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CandidateTest.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        protected static string GetContentType(string fileName)
        {
            var provide = new FileExtensionContentTypeProvider();

            if (!provide.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
