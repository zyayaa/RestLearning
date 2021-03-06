﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestLearning.Data.Models;

namespace RestLearning.Data.Repositories {
    public class UsersRepository : BaseRepository<Users> {

        public UsersRepository(UsersContext context):base(context) {
           
        }

        public Users GetUser(Guid userId) {

            return (from u in context.Users
                    where u.UserID == userId
                    select u).FirstOrDefault();
        }

        public List<Users> GetUsers() {
            return (from u in context.Users
                    orderby u.AddedOn ascending
                    select u).ToList();
        }
    }
}
