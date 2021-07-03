using System;

namespace BlazorHRAgency
{
    public class AppState
    {
        public int EmployeeId { get; set; }
        public Action AppStateUpdated { get; set; }
    }
}
