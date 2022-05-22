using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITaskService
    {
        List<EntityLayer.Concrete.Task> GetAllTask();
        void CreateTask(EntityLayer.Concrete.Task task);
        void UpdateTask(EntityLayer.Concrete.Task task);
        void DeleteTask(EntityLayer.Concrete.Task task);
        EntityLayer.Concrete.Task GetTaskById(int id);
    }
}
