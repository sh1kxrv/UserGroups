using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroups.Code.User.Custom
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee
    {
        public Employee(string Name, string Position, double Salary)
        {
            this.Name = Name;
            this.Position = Position;
            this.Salary = Salary;
        }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("pos")]
        public string Position { get; set; }
        [JsonProperty("salary")]
        public double Salary { get; set; }
        [JsonProperty("paid")]
        public double ToPaid { get; set; }
        [JsonProperty("tpaid")]
        public double TotalyPaid { get; set; }
    }
}
