namespace BJN.Services.Connectors.Lti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyltimeetingtoincludeconsumer : DbMigration
    {
        public override void Up()
        {
            AddColumn("LtiProvider.LtiMeetings", "Consumer_ConsumerId", c => c.Int());
            CreateIndex("LtiProvider.LtiMeetings", "Consumer_ConsumerId");
            AddForeignKey("LtiProvider.LtiMeetings", "Consumer_ConsumerId", "LtiProvider.Consumers", "ConsumerId");
            DropColumn("LtiProvider.LtiMeetings", "Organization");
        }
        
        public override void Down()
        {
            AddColumn("LtiProvider.LtiMeetings", "Organization", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("LtiProvider.LtiMeetings", "Consumer_ConsumerId", "LtiProvider.Consumers");
            DropIndex("LtiProvider.LtiMeetings", new[] { "Consumer_ConsumerId" });
            DropColumn("LtiProvider.LtiMeetings", "Consumer_ConsumerId");
        }
    }
}
