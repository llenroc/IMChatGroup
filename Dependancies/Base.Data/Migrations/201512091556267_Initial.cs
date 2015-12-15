namespace Base.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        User_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.GroupInvitations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InviteeEmail = c.String(nullable: false, maxLength: 100),
                        InviteDate = c.DateTime(nullable: false),
                        Withdrawn = c.Boolean(nullable: false),
                        Confirmed = c.Boolean(nullable: false),
                        Token = c.Guid(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        InviterUserId = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        InviteeUserId = c.Int(),
                        Invitee_Id = c.String(maxLength: 128),
                        Inviter_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Invitee_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Inviter_Id)
                .Index(t => t.GroupID)
                .Index(t => t.Invitee_Id)
                .Index(t => t.Inviter_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedByUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByUser_Id)
                .Index(t => t.CreatedByUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfilePicUrl = c.String(),
                        DateCreated = c.DateTime(),
                        LastLoginTime = c.DateTime(),
                        DOJ = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Activated = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        ContactNo = c.Double(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.GroupRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        fromUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedTimeStamp = c.Binary(),
                        CreatedOn = c.DateTime(nullable: false),
                        group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.group_Id)
                .Index(t => t.group_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SysConfigs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DefaultSundayHours = c.Time(nullable: false, precision: 7),
                        DefaultMondayHours = c.Time(nullable: false, precision: 7),
                        DefaultTuesdayHours = c.Time(nullable: false, precision: 7),
                        DefaultWednesdayHours = c.Time(nullable: false, precision: 7),
                        DefaultThursdayHours = c.Time(nullable: false, precision: 7),
                        DefaultFridayHours = c.Time(nullable: false, precision: 7),
                        DefaultSaturdayHours = c.Time(nullable: false, precision: 7),
                        DefaultWorryLevel = c.Double(nullable: false),
                        EasyTaskCutOff = c.Int(nullable: false),
                        ShortTaskCutOffTicks = c.Long(nullable: false),
                        ImminentDeadlineCutOffTicks = c.Long(nullable: false),
                        DefaultLabels = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupInvitations", "Inviter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupInvitations", "Invitee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupInvitations", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowUsers", "User_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "group_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Groups", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.GroupInvitations", new[] { "Inviter_Id" });
            DropIndex("dbo.GroupInvitations", new[] { "Invitee_Id" });
            DropIndex("dbo.GroupInvitations", new[] { "GroupID" });
            DropIndex("dbo.FollowUsers", new[] { "User_Id1" });
            DropIndex("dbo.FollowUsers", new[] { "User_Id" });
            DropTable("dbo.SysConfigs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.GroupRequests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupInvitations");
            DropTable("dbo.FollowUsers");
        }
    }
}
