using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestLearning.Dtos;

namespace RestLearning.Api.Controllers {
    public class ValuesController : ApiController {

        private List<UserDto> Users = new List<UserDto> {
            new UserDto {Name= "Joao" , Age =21 },
            new UserDto {Name= "Filipe" , Age =22 }
        };

        // GET api/values
        public List<UserDto> Get() {
            return Users;
        }

        // GET api/values/5
        public UserDto Get(int id) {
            return new UserDto();
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
