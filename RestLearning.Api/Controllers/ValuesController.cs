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

        // GET values
        [Route("values")]
        public List<UserDto> Get() {
            return UsersService.GetUsers();
        }

        // GET values/5
        [Route("values/{id}")]
        public UserDto Get(string id) {
            Guid userId = Guid.Parse(id);
            return UsersService.GetUser(userId);
        }

        [Route("values/new")]
        // POST values
        public void Post([FromBody]UserDto user) {
            if(user != null) {
                UsersService.CreateUser(user);
            }
        }

        [Route("values/{id}")]
        public void Put(string userId, [FromBody]UserDto user) {

        }

        // DELETE api/values/5
        public void Delete(int id) {
        }
    }
}
