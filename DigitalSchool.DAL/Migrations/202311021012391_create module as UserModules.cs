namespace DS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createmoduleasUserModules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        ClassOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassID);
            
            CreateTable(
                "dbo.UserModules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        ParentID = c.Int(nullable: false),
                        Url = c.String(),
                        PhysicalLocation = c.String(),
                        Ordering = c.Int(nullable: false),
                        IconClass = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserModules");
            DropTable("dbo.Classes");
        }
    }
}
