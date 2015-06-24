namespace NaturalDateTime.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                        Question = c.String(),
                        Note = c.String(),
                        UtcTime = c.DateTime(nullable: false),
                        UnderstoodQuestion = c.Boolean(nullable: false),
                        AnsweredQuestion = c.Boolean(nullable: false),
                        Client = c.String(),
                        Version = c.String(),
                        IsBot = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QuestionLogs");
        }
    }
}
