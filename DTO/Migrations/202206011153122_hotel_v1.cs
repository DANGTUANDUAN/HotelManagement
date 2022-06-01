namespace DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotel_v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accounts",
                c => new
                    {
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.employees",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Phonenumber = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bills",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        ReservationId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.reservations", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.bill_details",
                c => new
                    {
                        BillId = c.String(nullable: false, maxLength: 128),
                        ServiceId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BillId, t.ServiceId })
                .ForeignKey("dbo.bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.BillId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.reservations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        RoomId = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.RoomId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        CustomerId = c.String(nullable: false),
                        Phonenumber = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.rooms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        KindOfRoomId = c.String(nullable: false, maxLength: 128),
                        RoomStatusId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.kind_of_rooms", t => t.KindOfRoomId, cascadeDelete: true)
                .ForeignKey("dbo.room_status", t => t.RoomStatusId, cascadeDelete: true)
                .Index(t => t.KindOfRoomId)
                .Index(t => t.RoomStatusId);
            
            CreateTable(
                "dbo.kind_of_rooms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Max = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.room_status",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.rooms", "RoomStatusId", "dbo.room_status");
            DropForeignKey("dbo.reservations", "RoomId", "dbo.rooms");
            DropForeignKey("dbo.rooms", "KindOfRoomId", "dbo.kind_of_rooms");
            DropForeignKey("dbo.reservations", "EmployeeId", "dbo.employees");
            DropForeignKey("dbo.reservations", "CustomerId", "dbo.customers");
            DropForeignKey("dbo.bills", "Id", "dbo.reservations");
            DropForeignKey("dbo.bills", "EmployeeId", "dbo.employees");
            DropForeignKey("dbo.bill_details", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.bill_details", "BillId", "dbo.bills");
            DropForeignKey("dbo.accounts", "EmployeeId", "dbo.employees");
            DropIndex("dbo.rooms", new[] { "RoomStatusId" });
            DropIndex("dbo.rooms", new[] { "KindOfRoomId" });
            DropIndex("dbo.reservations", new[] { "CustomerId" });
            DropIndex("dbo.reservations", new[] { "RoomId" });
            DropIndex("dbo.reservations", new[] { "EmployeeId" });
            DropIndex("dbo.bill_details", new[] { "ServiceId" });
            DropIndex("dbo.bill_details", new[] { "BillId" });
            DropIndex("dbo.bills", new[] { "EmployeeId" });
            DropIndex("dbo.bills", new[] { "Id" });
            DropIndex("dbo.accounts", new[] { "Username" });
            DropIndex("dbo.accounts", new[] { "EmployeeId" });
            DropTable("dbo.room_status");
            DropTable("dbo.kind_of_rooms");
            DropTable("dbo.rooms");
            DropTable("dbo.customers");
            DropTable("dbo.reservations");
            DropTable("dbo.Services");
            DropTable("dbo.bill_details");
            DropTable("dbo.bills");
            DropTable("dbo.employees");
            DropTable("dbo.accounts");
        }
    }
}
