using FluentMigrator;

namespace AutoService.Data.Migrations
{
    [Migration(3)]
    public class OrderMigrator : Migration
    {
        public override void Up()
        {
            Create.Table("orders")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("vin").AsString(255)
            .WithColumn("phone").AsString(255)
            .WithColumn("date").AsString(255)
            .WithColumn("description").AsString(255)
            .WithColumn("status").AsString(255);

            Insert.IntoTable("orders")
                .Row(new { vin = "1", phone = "89051234560", date = "11.10.2023", description = "some", status = "Completed"});
        }
        public override void Down()
        {
            Delete.Table("orders");
        }
    }
}