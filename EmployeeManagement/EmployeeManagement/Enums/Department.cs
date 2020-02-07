using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Enums
{
    public enum Department
    {
        [Description("HR")]
        HR,
        [Description("Finance")]
        Finance,
        [Description("Marketing")]
        Marketing,
        [Description("IT")]
        IT,
        [Description("Operations")]
        Operations
    }
}
