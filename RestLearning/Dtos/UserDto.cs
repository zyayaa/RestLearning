using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestLearning.Dtos {

    [DataContract(Name = "user")]
    public class UserDto {
		[DataMember(Name="userId")]
		public Guid UserId{ get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "age")]
        public int Age { get; set; }

		[DataMember(Name = "addedOn")]
		public DateTime AddedOn{ get; set; }
    }
}
