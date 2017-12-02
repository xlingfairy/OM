namespace OM.AppServer.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CDRs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50),
                        CallID = c.Int(nullable: false),
                        VisitorID = c.Int(nullable: false),
                        OuterID = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        Route = c.Byte(nullable: false),
                        TimeStar = c.DateTime(),
                        TimeEnd = c.DateTime(),
                        From = c.String(maxLength: 20),
                        To = c.String(maxLength: 20),
                        Duration = c.Int(nullable: false),
                        TrunkNumber = c.String(maxLength: 20),
                        Recording = c.String(maxLength: 500),
                        RecCodec = c.String(maxLength: 10),
                        RecoredOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CDRs");
        }
    }
}
