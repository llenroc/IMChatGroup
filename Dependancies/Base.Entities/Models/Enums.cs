using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.Entities.Models
{
    public enum Shift
    {
        Morning,
        Evning,
        Night
    }
    public enum Status
    { 
        Active=1,
        Deactive=2,
        Closed=3            
    }

    public enum Gender
    {
        Male=1,
        Female=2
    }
    public enum Role
    {
        Admin=1,
        Accountant=2,
        Librarian=3,
        Instructor=4,
        Student=5,
        Guest=10,
        Staff=8
    }

    public enum CourseType
    {
        Yearly=1,
        Quaterly=3,
        HalfYearly=2,
        Weekly=7
    }
    public enum ExamStatus
    {
        PASS=1,
        FAIL=2,
        PROMOTED=3
    }
    public enum AttendenceStatus
    {
        Holiday=0,
        Present=1,
        Absent=3,
        HalfDay=2,
        ShortLeave=4,
        OnLeave=5,
        CasualLeave=6,
        PersonalLeave=8,
        AppliedLeave=9
    }
}
