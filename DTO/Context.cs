using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;

using DTO.Entities;
using DTO.Maps;

namespace DTO
{
    public class Context : DbContext
    {
        public Context() : base("name=hoteldata1")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<KindOfRoom> KindOfRooms { get; set; }
        public DbSet<RoomStatus> RoomStatuses { get; set; }

        // Inject Constraints
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new KindOfRoomMap());
            modelBuilder.Configurations.Add(new RoomStatusMap());
            modelBuilder.Configurations.Add(new RoomMap());
            modelBuilder.Configurations.Add(new ReservationMap());
            modelBuilder.Configurations.Add(new BillMap());
            modelBuilder.Configurations.Add(new BillDetailMap());
            modelBuilder.Configurations.Add(new CustomerMap());
        }

    }
}
