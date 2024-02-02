namespace WebApiCrudCodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalaryD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Salary", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Department");
            DropColumn("dbo.Employees", "Salary");
        }
    }
}
