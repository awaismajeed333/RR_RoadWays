using System;
using System.Collections.Generic;

namespace RR_RoadWays_Services.Models
{
    public partial class Department
    {
        public Department()
        {
            Maintenance = new HashSet<Maintenance>();
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Maintenance> Maintenance { get; set; }
    }
}
