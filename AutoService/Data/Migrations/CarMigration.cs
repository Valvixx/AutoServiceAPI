using FluentMigrator;

namespace AutoService.Data.Migrations
{
    [Migration(1)]
    public class CarMigration : Migration
    {
        public override void Up()
        {
            Create.Table("cars")
            .WithColumn("brand").AsString(255)
            .WithColumn("model").AsString(255)
            .WithColumn("releaseYear").AsString(255)
            .WithColumn("vin").AsString(255).Unique().PrimaryKey();

            Insert.IntoTable("cars")
                .Row(new { brand = "BMW", model = "X5", releaseYear = "2015", vin = "VIN1" });

        }
        public override void Down() 
        {
            Delete.Table("cars");
        }
    }
}
