﻿using Microsoft.EntityFrameworkCore;
using T065_EntityFramework.Models;

namespace T065_EntityFramework.Data
{
	public class DataContextEF(IConfiguration config) : DbContext
	{
		private readonly IConfiguration _config = config;

		public virtual DbSet<UserModel> Users { get; set; }
		public virtual DbSet<UserSalaryModel> UserSalarys { get; set; }
		public virtual DbSet<UserJobInfoModel> UserJobInfos { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
					.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
						optionsBuilder => optionsBuilder.EnableRetryOnFailure());
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("TutorialAppSchema");

			modelBuilder.Entity<UserModel>()
				.ToTable("Users", "TutorialAppSchema")
				.HasKey(u => u.UserId);

			modelBuilder.Entity<UserSalaryModel>()
				.HasKey(u => u.UserId);
			modelBuilder.Entity<UserJobInfoModel>()
				.HasKey(u => u.UserId);
		}
	}
}
