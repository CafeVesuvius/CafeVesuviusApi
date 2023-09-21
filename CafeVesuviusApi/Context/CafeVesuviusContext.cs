﻿using System;
using System.Collections.Generic;
using CafeVesuviusApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeVesuviusApi.Context;

public partial class CafeVesuviusContext : DbContext
{
    public CafeVesuviusContext(DbContextOptions<CafeVesuviusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessUser> AccessUsers { get; set; }

    public virtual DbSet<DiningTable> DiningTables { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationDiningTable> ReservationDiningTables { get; set; }

    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessUser>(entity =>
        {
            entity.ToTable("AccessUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.UserPassword).HasMaxLength(255);
        });

        modelBuilder.Entity<DiningTable>(entity =>
        {
            entity.ToTable("DiningTable");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Number).HasMaxLength(10);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu", tb => tb.HasTrigger("trg_MenuUpdated"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Changed).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Season).HasMaxLength(50);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItem", tb => tb.HasTrigger("trg_MenuItemUpdated"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            //entity.HasOne(d => d.Menu).WithMany(p => p.MenuItems)
            //    .HasForeignKey(d => d.MenuId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_MenuItem_Menu");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order", tb => tb.HasTrigger("trg_OrderCreated"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.ToTable("OrderLine");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Quantity).HasColumnType("Quantity");
            entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            //entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderLines)
            //    .HasForeignKey(d => d.MenuItemId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_OrderLine_MenuItem");

            //entity.HasOne(d => d.Order).WithMany(p => p.OrderLines)
            //    .HasForeignKey(d => d.OrderId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_OrderLine_Order");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation", tb => tb.HasTrigger("trg_ReservationCreated"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Time).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReservationDiningTable>(entity =>
        {
            entity.ToTable("ReservationDiningTable");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiningTableId).HasColumnName("DiningTableID");
            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

            // entity.HasOne(d => d.DiningTable).WithMany(p => p.ReservationDiningTables)
            //     .HasForeignKey(d => d.DiningTableId)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("FK_ReservationDiningTable_DiningTable");

            // entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationDiningTables)
            //     .HasForeignKey(d => d.ReservationId)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("FK_ReservationDiningTable_Reservation");
        });

        modelBuilder.Entity<UserRefreshToken>(entity =>
        {
            entity.ToTable("UserRefreshToken");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(255);
            entity.Property(e => e.RefreshToken).HasMaxLength(255);
            entity.Property(e => e.Token).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            // entity.HasOne(d => d.User).WithMany(p => p.UserRefreshTokens)
            //     .HasForeignKey(d => d.UserId)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("FK_UserRefreshToken_AccessUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}