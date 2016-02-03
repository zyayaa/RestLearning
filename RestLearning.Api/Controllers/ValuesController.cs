using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestLearning.Dtos;
using RestLearning.Services;

namespace RestLearning.Api.Controllers {
    public class ValuesController : ApiController {

        // GET api/values
        public List<UserDto> Get() {
            return UsersService.GetUsers();
        }

        // GET api/values/5
        public UserDto Get(string id) {
            Guid userId = Guid.Parse(id);
            return UsersService.GetUser(userId);
        }

        // POST api/values
        public void Post([FromBody]string value) {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        public void Delete(int id) {
        }
    }
}
