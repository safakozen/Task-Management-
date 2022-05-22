using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class TaskStatusVM
    {
        public List<Status> Status { get; set; }
        public List<EntityLayer.Concrete.Task> Task { get; set; }
    }
}
