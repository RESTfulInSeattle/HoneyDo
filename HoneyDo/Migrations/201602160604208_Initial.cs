namespace HoneyDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Todoes",
                c => new
                    {
                        TodoId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        TaskName = c.String(),
                        Deadline = c.DateTime(nullable: false),
                        Completed = c.Int(nullable: false),
                        Moredetails = c.String(),
                    })
                .PrimaryKey(t => t.TodoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Todoes");
        }
    }
}
