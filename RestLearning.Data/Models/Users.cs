using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestLearning.Data.Models {
    [Table("Users")]
    public class Users {

        [Key]
        public Guid UserID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
