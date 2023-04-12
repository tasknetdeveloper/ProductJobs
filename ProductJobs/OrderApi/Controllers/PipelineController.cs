using Microsoft.AspNetCore.Mvc;
using Model;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PipelineController : Controller
    {
        private Implementer Impl = new();

        [HttpGet(Name = "RequestWithPipeline")]
        public async Task<PipelineItemResponse[]?> RequestWithPipeline(PipleneItemRequest[]? request)
        {
            if (request == null) return null;
            await Task.Run(() => {
                return Impl.Exec(request);                
            });
            return null;    
        }

        [HttpGet(Name = "RequestWithRulesPipeline")]
        public async Task<PipelineItemResponse[]?> RequestWithRulesPipeline(PipleneItemRequest[]? request)
        {
            if (request == null) return null;
            await Task.Run(() => {
                return Impl.ExecWithRules(request);
            });
            return null;
        }
    }
}
