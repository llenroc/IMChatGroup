using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entities.Models
{
    public class User : IdentityUser
    {
        public User() //:base()
        {
            DateCreated = DateTime.Now;
           // base.Id = new Guid().ToString();
        }

      

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicUrl { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime? DOJ { get; set; }
        public Status Status { get; set; }

        public bool Activated { get; set; }

        public Role Role { get; set; }

        //public virtual ICollection<Goal> Goals { get; set; }

         public virtual ICollection<FollowUser> FollowFromUser { get; set; }

         public virtual ICollection<FollowUser> FollowToUser { get; set; }

        //public virtual ICollection<GroupRequest> GroupRequests { get; set; }        
        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }      

        public double? ContactNo { get; set; }

     //   public string UserId { get; set; }
        public string DisplayName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}
