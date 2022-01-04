using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One58.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;
        public FeaturesController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeatures()
        {
            var featureNames = new List<string>();
            await foreach (string featureName in _featureManager.GetFeatureNamesAsync().ConfigureAwait(false))
            {
                var enabled = await _featureManager.IsEnabledAsync(featureName);
                featureNames.Add($"{featureName}=>{enabled}");
            }

            return Ok(featureNames);
        }
    }
}
