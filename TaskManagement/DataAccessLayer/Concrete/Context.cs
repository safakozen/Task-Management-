using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Concrete
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UrgencyStatus> UrgencyStatus { get; set; }
        public DbSet<EntityLayer.Concrete.Task> Tasks { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
