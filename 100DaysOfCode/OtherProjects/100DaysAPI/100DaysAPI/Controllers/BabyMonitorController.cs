using System;
using _100DaysAPI.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace _100DaysAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BabyMonitorController : ControllerBase
    {
        private readonly BabyDbContext _babyDbContext;

        private readonly ILogger<BabyMonitorController> _logger;

        public BabyMonitorController(ILogger<BabyMonitorController> logger, BabyDbContext babyDbContext)
        {
            _logger = logger;
            _babyDbContext = babyDbContext;
        }
            
        [HttpPost]
        [Route("AddBreastMilk/{ml}")]
        public string AddBreastMilk(int ml)
        {
            return $"{ml}ml of breast milk added to database"; 
        }
    }
}

