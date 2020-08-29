using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.User.Custom;

namespace UserGroups.Code.Company
{
    public class InformationOfStaff
    {
        public InformationOfStaff(Employee emp, string @event, int Index)
        {
            EMP = emp;
            Event = @event;
            this.Index = Index;
            AddingTime = DateTime.Now;
        }
        public Employee EMP { get; set; }
        public string Event { get; set; }
        public int Index { get; set; }
        public DateTime AddingTime { get; set; }
    }
}
