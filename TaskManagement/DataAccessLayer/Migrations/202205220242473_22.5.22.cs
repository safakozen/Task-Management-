namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22522 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            AddColumn("dbo.Tasks", "CreatedUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "User_Id", c => c.Int());
            CreateIndex("dbo.Tasks", "CreatedUserId");
            CreateIndex("dbo.Tasks", "User_Id");
            AddForeignKey("dbo.Tasks", "CreatedUserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tasks", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "CreatedUserId", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "User_Id" });
            DropIndex("dbo.Tasks", new[] { "CreatedUserId" });
            DropColumn("dbo.Tasks", "User_Id");
            DropColumn("dbo.Tasks", "CreatedUserId");
            AddForeignKey("dbo.Tasks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
