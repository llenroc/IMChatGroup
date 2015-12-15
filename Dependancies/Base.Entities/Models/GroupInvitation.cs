using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Base.Entities.Models
{
   // [TrackChanges]
    public class GroupInvitation
    {
        #region Properties

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string InviteeEmail { get; set; }

        public DateTime InviteDate { get; set; }

        public bool Withdrawn { get; set; }
        
        public bool Confirmed { get; set; }
        
        public Guid Token { get; set; }

        public bool Admin { get; set; }
        #endregion

        #region Foreign keys
        public int InviterUserId { get; set; }
        public int GroupID { get; set; }
        public int? InviteeUserId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User Inviter { get; set; }
        public virtual Group Group { get; set; }
        public virtual User Invitee { get; set; }
        #endregion

    }
}