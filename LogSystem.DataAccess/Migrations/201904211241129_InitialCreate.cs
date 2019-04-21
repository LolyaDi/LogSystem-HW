namespace LogSystem.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Fullname = c.String(nullable: false, maxLength: 150),
                        CertificateNumber = c.Long(nullable: false),
                        EnterTime = c.DateTime(nullable: false),
                        QuitTime = c.DateTime(nullable: false),
                        EnterPurpose = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Visitors");
        }
    }
}
