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
        [HttpGet]
        public List<UserDto> Get() {
           var result = UsersService.GetUsers();

            return result;
        }

        // GET values/5
        [Route("values/{userId}")]
        [HttpGet]
        public UserDto Get(string userId) {
            Guid userGuid = Guid.Parse(userId);
            return UsersService.GetUser(userGuid);
        }

        [Route("values/{userId}/new")]
        [HttpPost]
        public void Post(string userId, [FromBody]UserDto user) {
            Guid userGuid;

            if(user != null && Guid.TryParse(userId, out userGuid)) {
                UsersService.CreateUser(user);
            }
        }

        [Route("values/{userId}")]
        [HttpPut]
        public void Put(string userId, [FromBody] UserDto user) {

            Guid userGuid;

            if(user != null && Guid.TryParse(userId, out userGuid)) {
                UsersService.UpdateUser(user);
            }
        }
    }
}
