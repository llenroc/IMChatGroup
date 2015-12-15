namespace Base.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityRoomAddedd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Tittle = c.String(),
                        MaxUsers = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                        WelcomeMessage = c.String(),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedByUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByUser_Id)
                .Index(t => t.CreatedByUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Rooms", new[] { "CreatedByUser_Id" });
            DropTable("dbo.Rooms");
        }
    }
}
