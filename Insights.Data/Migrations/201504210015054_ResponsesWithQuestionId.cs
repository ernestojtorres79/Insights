namespace Insights.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponsesWithQuestionId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responses", "QuestionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Responses", "QuestionId");
            AddForeignKey("dbo.Responses", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Responses", new[] { "QuestionId" });
            DropColumn("dbo.Responses", "QuestionId");
        }
    }
}
