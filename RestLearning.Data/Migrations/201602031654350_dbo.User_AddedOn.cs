namespace RestLearning.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dboUser_AddedOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AddedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AddedOn");
        }
    }
}
