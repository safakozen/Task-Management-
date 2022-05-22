using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFTaskDAL : GenericRepository<EntityLayer.Concrete.Task>, ITaskDAL
    {
    }
}
