﻿using System;
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

        [HttpGet]
        [Route("GetMilk")]
        public IActionResult GetMilk()
        {
            var milkLogs = _babyDbContext.MilkLogs.ToList();
            return Ok(milkLogs);
        }

        [HttpGet]
        [Route("GetMilk/{id}")]
        public IActionResult GetMilk(int id)
        {
            var milkLog = _babyDbContext.MilkLogs.Where(x => x.ID == id).First();
            return Ok(milkLog);
        }

        [HttpGet]
        [Route("GetExercise")]
        public IActionResult GetExercise()
        {
            var exerciseLogs = _babyDbContext.ExerciseLogs.ToList();
            return Ok(exerciseLogs);
        }

        [HttpGet]
        [Route("GetExercise/{id}")]
        public IActionResult GetExercise(int id)
        {
            var exerciseLog = _babyDbContext.ExerciseLogs.Where(x => x.ID == id).First();
            return Ok(exerciseLog);
        }

        [HttpGet]
        [Route("GetPoo")]
        public IActionResult GetPoo()
        {
            var pooLogs = _babyDbContext.PooLogs.ToList();
            return Ok(pooLogs);
        }

        [HttpGet]
        [Route("GetPoo/{id}")]
        public IActionResult GetPoo(int id)
        {
            var pooLog = _babyDbContext.PooLogs.Where(x => x.ID == id).First();
            return Ok(pooLog);
        }

        [HttpGet]
        [Route("GetSleep")]
        public IActionResult GetSleep()
        {
            var sleepLogs = _babyDbContext.SleepLogs.ToList();
            return Ok(sleepLogs);
        }

        [HttpGet]
        [Route("GetSleep/{id}")]
        public IActionResult GetSleep(int id)
        {
            var sleepLog = _babyDbContext.SleepLogs.Where(x => x.ID == id).First();
            return Ok(sleepLog);
        }

        [HttpPost]
        [Route("AddMilk")]
        public IActionResult AddMilk([FromBody] MilkLog milkLog)
        {
            try
            {
                _babyDbContext.MilkLogs.Add(milkLog);
                _babyDbContext.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddExercise")]
        public IActionResult AddExercise([FromBody] ExerciseLog exerciseLog)
        {
            _babyDbContext.ExerciseLogs.Add(exerciseLog);
            _babyDbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("AddPoo")]
        public IActionResult AddPoo([FromBody] PooLog pooLog)
        {
            _babyDbContext.PooLogs.Add(pooLog);
            _babyDbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("AddSleep")]
        public IActionResult AddSleep([FromBody] SleepLog sleepLog)
        {
            _babyDbContext.SleepLogs.Add(sleepLog);
            _babyDbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateMilk/{id}")]
        public IActionResult UpdateMilk(int Id, [FromBody] MilkLog milkLog)
        {
            var log = _babyDbContext.MilkLogs.Where(x => x.ID == Id).First();
            log.ID = milkLog.ID;
            log.Type = milkLog.Type;
            log.EstimatedAmount = milkLog.EstimatedAmount;
            log.Amount = milkLog.Amount;
            log.MeasurementType = milkLog.MeasurementType;
            log.Comment = milkLog.Comment;
            log.StartTime = milkLog.StartTime;
            log.FinishTime = milkLog.FinishTime;
            
            _babyDbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateExercise")]
        public IActionResult UpdateExercise([FromBody] ExerciseLog exerciseLog)
        {
            var log = _babyDbContext.ExerciseLogs.Where(x => x.ID == exerciseLog.ID).First();
            log.ID = exerciseLog.ID;
            log.Type = exerciseLog.Type;
            log.Comment = exerciseLog.Comment;
            log.StartTime = exerciseLog.StartTime;
            log.FinishTime = exerciseLog.FinishTime;

            _babyDbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("UpdatePoo")]
        public IActionResult UpdatePoo([FromBody] PooLog pooLog)
        {
            var log = _babyDbContext.PooLogs.Where(x => x.ID == pooLog.ID).First();
            log.ID = pooLog.ID;
            log.Type = pooLog.Type;
            log.Comment = pooLog.Comment;
            log.Colour = pooLog.Colour;
            log.OccurrenceTime = pooLog.OccurrenceTime;

            _babyDbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateSleep")]
        public IActionResult UpdateSleep([FromBody] SleepLog sleepLog)
        {
            var log = _babyDbContext.SleepLogs.Where(x => x.ID == sleepLog.ID).First();

            log.ID = sleepLog.ID;
            log.Comment = sleepLog.Comment;
            log.StartTime = sleepLog.StartTime;
            log.FinishTime = sleepLog.FinishTime;

            _babyDbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteMilk")]
        public IActionResult DeleteMilk([FromBody] MilkLog milkLog)
        {
            var log = _babyDbContext.MilkLogs.Where(x => x.ID == milkLog.ID).First();
            _babyDbContext.Remove(log);
            _babyDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteExercise")]
        public IActionResult DeleteExercise([FromBody] ExerciseLog exerciseLog)
        {
            var log = _babyDbContext.ExerciseLogs.Where(x => x.ID == exerciseLog.ID).First();
            _babyDbContext.Remove(log);
            _babyDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("DeletePoo")]
        public IActionResult DeletePoo([FromBody] PooLog pooLog)
        {
            var log = _babyDbContext.PooLogs.Where(x => x.ID == pooLog.ID).First();
            _babyDbContext.Remove(log);
            _babyDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteSleep")]
        public IActionResult DeleteSleep([FromBody] SleepLog sleepLog)
        {
            var log = _babyDbContext.SleepLogs.Where(x => x.ID == sleepLog.ID).First();
            _babyDbContext.Remove(log);
            _babyDbContext.SaveChanges();

            return Ok();
        }

    }
}

