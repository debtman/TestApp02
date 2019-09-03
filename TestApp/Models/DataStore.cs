using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace TestApp.Models
{
    public class DataStore
    {
        private LiteDatabase db;
        private LiteCollection<ClassTask> col;

        public DataStore()
        {
            db = new LiteDatabase(@"TaskStorage");
        }

        public void SaveTask(ClassTask _task)
        {
            col = db.GetCollection<ClassTask>("tasks");
            var result = col.Find(x=>x.TaskName.Equals(_task.TaskName, StringComparison.OrdinalIgnoreCase)).DefaultIfEmpty(null);
            if (result.First()==null)
            {
                col.Insert(_task);                
            } else
            {
                col.Update(_task);
            }            
        }

        public IEnumerable<ClassTask> GetTaskList(string fValue="ALL")
        {
            List<ClassTask> t_list = new List<ClassTask>();

            col = db.GetCollection<ClassTask>("tasks");     
            IEnumerable<ClassTask> result;
            if (fValue == "EXE")
            {
                result = col.Find(x => x.EndDate!=null).OrderBy(x => x.StartDate);
            }
            else if ( fValue == "PLAN")
            {
                result = col.Find(x => x.EndDate==null).OrderBy(x => x.StartDate);
            }
            else 
            {
                result = col.FindAll().OrderBy(x => x.StartDate);
            }

            return result;            
        }

        public bool TaskExists(string tName)
        {
            bool exists = false;
            col = db.GetCollection<ClassTask>("tasks");
            var result = col.Find(x => x.TaskName.Equals(tName, StringComparison.OrdinalIgnoreCase)).DefaultIfEmpty(null);
            if (result.First() != null)
            {
                exists = true;
            }
            return exists;
        }

        public void ChangeTaskStatus(string tName)
        {
            col = db.GetCollection<ClassTask>("tasks");
            var result = col.Find(x => x.TaskName.Equals(tName, StringComparison.OrdinalIgnoreCase)).DefaultIfEmpty(null);
            
            if (result.First() != null)
            {
                ClassTask _task = result.First();
                _task.Completed = !_task.Completed;
                col.Update(_task);
            }            
        }

        public void DeleteTask(string tName)
        {
            col = db.GetCollection<ClassTask>("tasks");
            col.Delete(x => x.TaskName.Equals(tName));

        }
    }
}
