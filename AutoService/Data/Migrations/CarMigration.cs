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
            .WithColumn("vin").AsString(255).PrimaryKey().Identity();
        }
        public override void Down() 
        {
            Delete.Table("cars");
        }
    }
}
