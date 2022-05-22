using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
        public bool IsDelete{ get; set; }

        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
        [StringLength(50)]
        public string CreatedUserName { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}
