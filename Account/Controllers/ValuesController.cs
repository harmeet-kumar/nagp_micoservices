using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Account.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ConfigSettings configSetting { get; set; }

        public ValuesController(IOptions<ConfigSettings> settings)
        {
            this.configSetting = settings.Value;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAccounts()
        {
            try
            {
                var result = LogTask("Information", "Get Account");
                return new string[] { "Account - value1", "value2" };
            } catch (Exception ex)
            {
                var log = LogTask("Error", ex.Message);
                return null;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> GetAccountFor(int id)
        {
            try
            {
                var result = LogTask("Information", "Get Account for id : " + id);
                return "Account - " + id  ;
            }
            catch (Exception ex)
            {
                var log = LogTask("Error", ex.Message);
                return null;
            }
        }

        [HttpGet("issueChequeBook/{id}")]
        public ActionResult<IEnumerable<string>> GetCheckBookFor()
        {
            try
            {
                var result = LogTask("Information", "Get Account");
                return new string[] { "Account - value1", "value2" };
            }
            catch (Exception ex)
            {
                var log = LogTask("Error", ex.Message);
                return null;
            }
        }

        [HttpGet("chequeBookBlock/{id}")]
        public ActionResult<String> BlockChequeBook(int id)
        {
            try
            {
                LogTask("Information", "Cheque Book blocked for id " + id);
                return "ChequeBook Blocked";
            }
            catch (Exception ex)
            {
                LogTask("Exception", "Exception in blocking Cheque book");
                return null;
            }

        }

        [HttpGet("{id}/transferAccount/{branchid}")]
        public ActionResult<String> TransferAccount(int id, int branchid)
        {
            try
            {
                LogTask("Information", "Account " + id + " transferred to " + branchid);
                return "Account Transfered";
            }
            catch (Exception ex)
            {
                LogTask("Exception", "Exception in account transfer");
                return null;
            }

        }

        // POST api/values
        [HttpPost]
        public void CreateAccount([FromBody] UserAccount value)
        {
            try
            {
                var log = LogTask("Information", "New Account created for: "+ value.userID);
            }
            catch (Exception ex)
            {
                var log = LogTask("Error", ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateAccount(int id, [FromBody] UserAccount value)
        {
            try
            {
                var log = LogTask("Information", "Update Account created for: " + value.userID);
            }
            catch (Exception ex)
            {
                var log = LogTask("Error", ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteAccount(int userID)
        {
            try
            {
                var log = LogTask("Information", "Account deleted for: " + userID);
            }
            catch (Exception ex)
            {
                var log = LogTask("Error", ex.Message);
            }
        }

        private async Task LogTask(string typ, string value)
        {
            var data = new
            {
                apiName = "Account",
                exception = value,
                type = typ,
                time = DateTime.Now
            };

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://logger/api/values");
            var response = await client.PostAsJsonAsync("", data);
        }
    }
}
