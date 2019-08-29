namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDo11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dones",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dones", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Dones", new[] { "User_Id" });
            DropTable("dbo.Dones");
        }
    }
}
