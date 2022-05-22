using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }
        public int StatusTypeId { get; set; }
        public virtual StatusType StatusType { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public ICollection<User> Users { get; set; }
        
    }
} 
