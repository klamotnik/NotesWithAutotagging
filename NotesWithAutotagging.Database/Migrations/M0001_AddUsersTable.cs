using FluentMigrator;

namespace NotesWithAutotagging.Database.Migrations
{
    [Migration(2024_01_25__12_31)]
    public class M0001_AddUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}