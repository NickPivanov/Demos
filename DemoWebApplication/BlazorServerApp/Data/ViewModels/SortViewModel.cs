using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Data.ViewModels
{
    public class SortViewModel
    {
        public SortState DateSort { get; private set; }
        public SortState MotorSort { get; private set; }
        public SortState PropSort { get; private set; }
        public SortState ValueSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            MotorSort = sortOrder == SortState.MotorAsc ? SortState.MotorDesc : SortState.MotorAsc;
            PropSort = sortOrder == SortState.PropAsc ? SortState.PropDesc : SortState.PropAsc;
            ValueSort = sortOrder == SortState.ValueAsc ? SortState.ValueDesc : SortState.ValueAsc;
            Current = sortOrder;
        }
    }
}
