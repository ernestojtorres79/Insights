namespace Insights.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "SurveyClientControlId", "dbo.SurveyClientControls");
            DropIndex("dbo.Questions", new[] { "SurveyClientControlId" });
            CreateTable(
                "dbo.SurveyClientControlQuestions",
                c => new
                    {
                        SurveyClientControl_SurveyClientControlId = c.Int(nullable: false),
                        Question_QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SurveyClientControl_SurveyClientControlId, t.Question_QuestionId })
                .ForeignKey("dbo.SurveyClientControls", t => t.SurveyClientControl_SurveyClientControlId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId, cascadeDelete: true)
                .Index(t => t.SurveyClientControl_SurveyClientControlId)
                .Index(t => t.Question_QuestionId);
            
            DropColumn("dbo.Questions", "SurveyClientControlId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "SurveyClientControlId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SurveyClientControlQuestions", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.SurveyClientControlQuestions", "SurveyClientControl_SurveyClientControlId", "dbo.SurveyClientControls");
            DropIndex("dbo.SurveyClientControlQuestions", new[] { "Question_QuestionId" });
            DropIndex("dbo.SurveyClientControlQuestions", new[] { "SurveyClientControl_SurveyClientControlId" });
            DropTable("dbo.SurveyClientControlQuestions");
            CreateIndex("dbo.Questions", "SurveyClientControlId");
            AddForeignKey("dbo.Questions", "SurveyClientControlId", "dbo.SurveyClientControls", "SurveyClientControlId", cascadeDelete: true);
        }
    }
}
