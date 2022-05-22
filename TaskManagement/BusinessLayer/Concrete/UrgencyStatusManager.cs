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
    public class UrgencyStatusManager : IUrgencyStatus
    {
        IUrgencyStatusDAL _urgencyStatusDAL;

        public UrgencyStatusManager(IUrgencyStatusDAL urgencyStatusDAL)
        {
            _urgencyStatusDAL = urgencyStatusDAL;
        }

        public List<UrgencyStatus> GetUrgencyStatusList()
        {
            return _urgencyStatusDAL.List();
        }
    }
}
