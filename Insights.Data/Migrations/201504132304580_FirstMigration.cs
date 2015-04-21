namespace Insights.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        CampaignId = c.Int(nullable: false, identity: true),
                        CampaignName = c.String(),
                        CampaignActive = c.Boolean(nullable: false),
                        CampaignKeyword = c.String(),
                        CampaignGift = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionBody = c.String(),
                        QuestionType = c.String(),
                        QuestionActive = c.Boolean(nullable: false),
                        CampaignId = c.Int(nullable: false),
                        SurveyClientControlId = c.Int(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Campaigns", t => t.CampaignId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyClientControls", t => t.SurveyClientControlId, cascadeDelete: true)
                .Index(t => t.CampaignId)
                .Index(t => t.SurveyClientControlId);
            
            CreateTable(
                "dbo.SurveyClientControls",
                c => new
                    {
                        SurveyClientControlId = c.Int(nullable: false, identity: true),
                        SurveyClientPhone = c.String(),
                        SurveyClientId = c.Int(nullable: false),
                        CampaignId = c.Int(nullable: false),
                        UserId = c.String(),
                        BlackList = c.Boolean(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyClientControlId)
                .ForeignKey("dbo.Campaigns", t => t.CampaignId, cascadeDelete: false)
                .ForeignKey("dbo.SurveyClients", t => t.SurveyClientId, cascadeDelete: true)
                .Index(t => t.SurveyClientId)
                .Index(t => t.CampaignId);
            
            CreateTable(
                "dbo.CurrentQuestions",
                c => new
                    {
                        CurrentQuestionId = c.Int(nullable: false, identity: true),
                        CurrentQuestionSwitch = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        SurveyClientControlId = c.Int(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CurrentQuestionId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyClientControls", t => t.SurveyClientControlId, cascadeDelete: false)
                .Index(t => t.QuestionId)
                .Index(t => t.SurveyClientControlId);
            
            CreateTable(
                "dbo.Progresses",
                c => new
                    {
                        ProgressId = c.Int(nullable: false, identity: true),
                        ProgressSwitch = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        SurveyClientControlId = c.Int(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProgressId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyClientControls", t => t.SurveyClientControlId, cascadeDelete: false)
                .Index(t => t.QuestionId)
                .Index(t => t.SurveyClientControlId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        ResponseBody = c.String(),
                        SurveyClientControlId = c.Int(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.SurveyClientControls", t => t.SurveyClientControlId, cascadeDelete: true)
                .Index(t => t.SurveyClientControlId);
            
            CreateTable(
                "dbo.SurveyClients",
                c => new
                    {
                        SurveyClientId = c.Int(nullable: false, identity: true),
                        SurveyClientPhone = c.String(),
                        SurveyClientBlackList = c.Boolean(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyClientId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        profileId = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        businessName = c.String(),
                        address1 = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zip = c.String(),
                        phoneNumber = c.String(),
                        UserId = c.String(maxLength: 128),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.profileId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TwilioMSGs",
                c => new
                    {
                        TwilioMSGId = c.Int(nullable: false, identity: true),
                        TwiilioMSGSid = c.String(),
                        TwilioMSGBody = c.String(),
                        TwilioMSGFrom = c.String(),
                        TwilioMSGTo = c.String(),
                        TwilioMSGDate = c.DateTime(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TwilioMSGId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Campaigns", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SurveyClientControls", "SurveyClientId", "dbo.SurveyClients");
            DropForeignKey("dbo.Responses", "SurveyClientControlId", "dbo.SurveyClientControls");
            DropForeignKey("dbo.Questions", "SurveyClientControlId", "dbo.SurveyClientControls");
            DropForeignKey("dbo.Progresses", "SurveyClientControlId", "dbo.SurveyClientControls");
            DropForeignKey("dbo.Progresses", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.CurrentQuestions", "SurveyClientControlId", "dbo.SurveyClientControls");
            DropForeignKey("dbo.CurrentQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.SurveyClientControls", "CampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.Questions", "CampaignId", "dbo.Campaigns");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Responses", new[] { "SurveyClientControlId" });
            DropIndex("dbo.Progresses", new[] { "SurveyClientControlId" });
            DropIndex("dbo.Progresses", new[] { "QuestionId" });
            DropIndex("dbo.CurrentQuestions", new[] { "SurveyClientControlId" });
            DropIndex("dbo.CurrentQuestions", new[] { "QuestionId" });
            DropIndex("dbo.SurveyClientControls", new[] { "CampaignId" });
            DropIndex("dbo.SurveyClientControls", new[] { "SurveyClientId" });
            DropIndex("dbo.Questions", new[] { "SurveyClientControlId" });
            DropIndex("dbo.Questions", new[] { "CampaignId" });
            DropIndex("dbo.Campaigns", new[] { "UserId" });
            DropTable("dbo.TwilioMSGs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Profiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SurveyClients");
            DropTable("dbo.Responses");
            DropTable("dbo.Progresses");
            DropTable("dbo.CurrentQuestions");
            DropTable("dbo.SurveyClientControls");
            DropTable("dbo.Questions");
            DropTable("dbo.Campaigns");
        }
    }
}
