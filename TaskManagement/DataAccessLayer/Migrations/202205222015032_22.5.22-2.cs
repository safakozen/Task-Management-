namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _225222 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "DocumentFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "DocumentFile");
        }
    }
}
