using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StatusManager : IStatusService
    {
        IStatusDAL _statusDAL;

        public StatusManager(IStatusDAL statusDAL)
        {
            _statusDAL = statusDAL;
        }

        public List<Status> GetStatusList()
        {
            return _statusDAL.List();
        }
    }
}
