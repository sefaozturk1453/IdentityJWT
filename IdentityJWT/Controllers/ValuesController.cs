using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityJWT.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize] //yetkisiz kişi bu controller ın içindeki action ları göremez
    public class ValuesController : ControllerBase
    {




        private AppIdentityDbContext context; //bağlantı sağladık.

        public ValuesController(AppIdentityDbContext context)
        {
            this.context = context;
        }








        // GET api/values
        [HttpGet]
        [Authorize(Roles ="admin, editor")] //action a sadece admin ve editör ulaşa bilir.
        public ActionResult<IEnumerable<string>> Get()
        {
            return context.Users.Select(x => x.UserName).ToArray();

            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
