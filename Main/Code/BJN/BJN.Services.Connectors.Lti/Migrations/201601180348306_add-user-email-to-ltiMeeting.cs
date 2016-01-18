namespace BJN.Services.Connectors.Lti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduseremailtoltiMeeting : DbMigration
    {
        public override void Up()
        {
            AddColumn("LtiProvider.LtiMeetings", "TeacherEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("LtiProvider.LtiMeetings", "TeacherEmail");
        }
    }
}
