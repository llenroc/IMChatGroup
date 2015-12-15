using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Base.Entities.Models
{
    //[TrackChanges]
    public class SysConfig
    {
        [Key]
        public int ID { get; set; }

        public TimeSpan DefaultSundayHours { get; set; }
        public TimeSpan DefaultMondayHours { get; set; }
        public TimeSpan DefaultTuesdayHours { get; set; }
        public TimeSpan DefaultWednesdayHours { get; set; }
        public TimeSpan DefaultThursdayHours { get; set; }
        public TimeSpan DefaultFridayHours { get; set; }
        public TimeSpan DefaultSaturdayHours { get; set; }

        public double DefaultWorryLevel { get; set; }

        //Used for 'get me started' task selection
        public int EasyTaskCutOff { get; set; }

        public Int64 ShortTaskCutOffTicks { get; set; }
        [NotMapped]
        public TimeSpan ShortTaskCutOff
        {
            get { return TimeSpan.FromTicks(ShortTaskCutOffTicks); }
            set { ShortTaskCutOffTicks = value.Ticks; }
        }

        public Int64 ImminentDeadlineCutOffTicks { get; set; }
        [NotMapped]
        public TimeSpan ImminentDeadlineCutOff
        {
            get { return TimeSpan.FromTicks(ImminentDeadlineCutOffTicks); }
            set { ImminentDeadlineCutOffTicks = value.Ticks; }
        }

        public string DefaultLabels { get; set; }
    }
}