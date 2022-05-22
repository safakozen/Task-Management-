using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string TaskContent { get; set; }

        public bool IsPriority { get; set; }

        public int UrgencyStatusId { get; set; }
        public virtual UrgencyStatus UrgencyStatus { get; set; }

        public DateTime EndDate { get; set; }
        public bool IsDelete { get; set; }

        //public File DocumentFile { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int CreatedUserId { get; set; }
        public virtual User CreatedUser { get; set; }





    }
}
