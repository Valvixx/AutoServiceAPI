using FluentMigrator;

namespace AutoService.Data.Migrations
{
    [Migration(3)]
    public class OrderMigrator : Migration
    {
        public override void Up()
        {
            Create.Table("orders")
            .WithColumn("id").AsString(255).PrimaryKey().Unique()
            .WithColumn("car").AsString(255).ForeignKey("cars", "vin")
            .WithColumn("customer").AsString(255).ForeignKey("customers", "phone")
            .WithColumn("date").AsString(255)
            .WithColumn("description").AsString(255)
            .WithColumn("status").AsString(255);

            Insert.IntoTable("orders")
                .Row(new {id = "1", car = "VIN1", customer = "89051234560", date = "11.10.2023", description = "engine repair", status = "Completed"});
        }
        public override void Down()
        {
            Delete.Table("orders");
        }
    }
}