namespace BJN.Services.Connectors.Lti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addltimeeting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "LtiProvider.LtiMeetings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        MeetingId = c.Int(nullable: false),
                        CourseId = c.String(nullable: false, maxLength: 64),
                        Organization = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("LtiProvider.LtiMeetings");
        }
    }
}
