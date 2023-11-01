using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Service.Database;

public partial class TestAppDbContext : DbContext // I used scaffolding database, because it safe a lot of time
{
    public TestAppDbContext()
    {
    }

    public TestAppDbContext(DbContextOptions<TestAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SampleCabDatum> SampleCabData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=<type your>;Initial Catalog=TestAppDB;Integrated Security=SSPI;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SampleCabDatum>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DOlocationId).HasColumnName("DOLocationID");
            entity.Property(e => e.FareAmount).HasColumnName("fare_amount");
            entity.Property(e => e.PassengerCount).HasColumnName("passenger_count");
            entity.Property(e => e.PUlocationId).HasColumnName("PULocationID");
            entity.Property(e => e.StoreAndFwdFlag).HasColumnName("store_and_fwd_flag");
            entity.Property(e => e.TipAmount).HasColumnName("tip_amount");
            entity.Property(e => e.TpepDropoffDatetime)
                .HasColumnType("datetime")
                .HasColumnName("tpep_dropoff_datetime");
            entity.Property(e => e.TpepPickupDatetime)
                .HasColumnType("datetime")
                .HasColumnName("tpep_pickup_datetime");
            entity.Property(e => e.TripDistance).HasColumnName("trip_distance");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
