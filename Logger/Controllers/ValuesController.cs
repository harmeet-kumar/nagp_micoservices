using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.Model;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Log>> GetAllLogs()
        {
            return LogList.logs;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Log>> GetLogForAPI(string id)
        {
            List<Log> selectedLogs = new List<Log>();
            foreach(var log in LogList.logs)
            {
                if(log.apiName.Equals(id))
                {
                    selectedLogs.Add(log);
                }
            }
            return selectedLogs;
        }

        // POST api/values
        [HttpPost]
        public void AddLog([FromBody] Log value)
        {
            LogList.logs.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateLog(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteLog(int id)
        {
        }
    }
}
