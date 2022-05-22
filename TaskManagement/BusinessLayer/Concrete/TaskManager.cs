using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TaskManager : ITaskService
    {
        ITaskDAL _taskdal;
        public TaskManager(ITaskDAL taskdal)
        {
            _taskdal = taskdal;
        }
        public List<EntityLayer.Concrete.Task> GetAllTask()
        {
            return _taskdal.List();
        }
        public EntityLayer.Concrete.Task GetTaskById(int id)
        {
            return _taskdal.List(x => x.Id == id).SingleOrDefault();
        }
        public void CreateTask(EntityLayer.Concrete.Task task)
        {
            _taskdal.Insert(task);
        }

        public void UpdateTask(EntityLayer.Concrete.Task task)
        {
            _taskdal.Update(task);
        }
        public void DeleteTask(EntityLayer.Concrete.Task task)
        {
            _taskdal.Delete(task);
        }
    }
}
