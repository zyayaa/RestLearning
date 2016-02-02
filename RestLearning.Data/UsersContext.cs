using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestLearning.Data.Models;

namespace RestLearning.Data {
    public class UsersContext : DbContext {

        public UsersContext() : base("RestLearning") {
        }

        DbSet<Users> Users { get; set; }
    }
}
