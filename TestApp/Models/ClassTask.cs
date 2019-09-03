using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class ClassTask
    {
        [BsonId]
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string Executor { get; set; }
        public DateTime StartDate { get; private set; }
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                completed = (value != null);                
            }
        }

        private bool completed=false;
        public bool Completed
        {
            get {
                return completed;
            }
            set
            {
                completed = value;
                if (value==true)
                {
                    EndDate = DateTime.Now;
                }
                else
                {
                    EndDate = null;
                }
            }
        }

        public ClassTask()
        {

        }

        public ClassTask(string _taskName)
        {
            TaskName = _taskName;
            StartDate = DateTime.Now;           
            Completed = false;            
        }
        
    }
}
