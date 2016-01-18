namespace BJN.Services.Connectors.Lti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "LtiProvider.Consumers",
                c => new
                    {
                        ConsumerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Key = c.String(nullable: false),
                        Secret = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ConsumerId);
            
            CreateTable(
                "LtiProvider.Outcomes",
                c => new
                    {
                        OutcomeId = c.Int(nullable: false, identity: true),
                        ConsumerId = c.Int(nullable: false),
                        ContextTitle = c.String(),
                        LisResultSourcedId = c.String(),
                        ServiceUrl = c.String(),
                        Tool_ToolId = c.Int(),
                    })
                .PrimaryKey(t => t.OutcomeId)
                .ForeignKey("LtiProvider.Tools", t => t.Tool_ToolId)
                .Index(t => t.Tool_ToolId);
            
            CreateTable(
                "LtiProvider.ProviderRequests",
                c => new
                    {
                        ProviderRequestId = c.Int(nullable: false, identity: true),
                        LtiRequest = c.String(),
                        Received = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProviderRequestId);
            
            CreateTable(
                "LtiProvider.LtiRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "LtiProvider.LtiUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("LtiProvider.LtiRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("LtiProvider.LtiUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "LtiProvider.Tools",
                c => new
                    {
                        ToolId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ToolId);
            
            CreateTable(
                "LtiProvider.LtiUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "LtiProvider.LtiUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("LtiProvider.LtiUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "LtiProvider.LtiUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("LtiProvider.LtiUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("LtiProvider.LtiUserRoles", "UserId", "LtiProvider.LtiUsers");
            DropForeignKey("LtiProvider.LtiUserLogins", "UserId", "LtiProvider.LtiUsers");
            DropForeignKey("LtiProvider.LtiUserClaims", "UserId", "LtiProvider.LtiUsers");
            DropForeignKey("LtiProvider.Outcomes", "Tool_ToolId", "LtiProvider.Tools");
            DropForeignKey("LtiProvider.LtiUserRoles", "RoleId", "LtiProvider.LtiRoles");
            DropIndex("LtiProvider.LtiUserLogins", new[] { "UserId" });
            DropIndex("LtiProvider.LtiUserClaims", new[] { "UserId" });
            DropIndex("LtiProvider.LtiUsers", "UserNameIndex");
            DropIndex("LtiProvider.LtiUserRoles", new[] { "RoleId" });
            DropIndex("LtiProvider.LtiUserRoles", new[] { "UserId" });
            DropIndex("LtiProvider.LtiRoles", "RoleNameIndex");
            DropIndex("LtiProvider.Outcomes", new[] { "Tool_ToolId" });
            DropTable("LtiProvider.LtiUserLogins");
            DropTable("LtiProvider.LtiUserClaims");
            DropTable("LtiProvider.LtiUsers");
            DropTable("LtiProvider.Tools");
            DropTable("LtiProvider.LtiUserRoles");
            DropTable("LtiProvider.LtiRoles");
            DropTable("LtiProvider.ProviderRequests");
            DropTable("LtiProvider.Outcomes");
            DropTable("LtiProvider.Consumers");
        }
    }
}
