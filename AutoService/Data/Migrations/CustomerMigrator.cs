using FluentMigrator;

namespace AutoService.Data.Migrations
{
    [Migration(2)]
    public class CustomerMigrator : Migration
    {
        public override void Up()
        {
            Create.Table("customers")
            .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
            .WithColumn("name").AsString(255)
            .WithColumn("adress").AsString(255)
            .WithColumn("phone").AsString(255).Unique();

            Insert.IntoTable("customers")
                .Row(new { name = "Dan", adress = "Moscow, Pushkin Street, 33", phone = "89051234560" });

        }
        public override void Down()
        {
            Delete.Table("customers");
        }
    }
}