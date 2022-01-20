using System;
using Microsoft.AspNetCore.Mvc;

namespace _100DaysAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BabyMonitorController : ControllerBase
    {
        private readonly ILogger<BabyMonitorController> _logger;

        public BabyMonitorController(ILogger<BabyMonitorController> logger)
        {
            _logger = logger;
        }
            
        [HttpPost]
        [Route("AddBreastMilk/{ml}")]
        public string AddBreastMilk(int ml)
        {
            return $"{ml}ml of breast milk added to database"; 
        }
    }
}

