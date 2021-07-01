using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHRAgency
{
    public class AppState
    {
        public int EmployeeId { get; set; }
        public Action AppStateUpdated { get; set; }
    }
}
