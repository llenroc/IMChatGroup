using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entities.Models
{
   public class Group
   {
       public Group()
       {
           this.GroupUsers = new HashSet<User>();
       }

       public int Id { get; set; }
       public string Name { get; set; }
       public DateTime? CreatedOn { get; set; }


       #region Foreign keys
       public int CreatedByUserId { get; set; }
      #endregion

       #region Navigation Properties
       public virtual User CreatedByUser { get; set; }

       public virtual ICollection<User> GroupUsers { get; set; }
              
       #endregion


    }
}
