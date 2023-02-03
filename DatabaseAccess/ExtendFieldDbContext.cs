using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using ExtendFieldDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExtendFieldDemo.DatabaseAccess
{
    public class ExtendFieldDbContext : DbContext
    {
        public static ConcurrentDictionary<string, List<ExtendFieldModel>> ExtendInfo { get; set; } = new();

        public DbSet<ExtendFieldModel> ExtendFieldModels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=xxx.db");

            this.SavedChanges += ExtendFieldDbContext_SavedChanges;
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<ExtendFieldModel>();
            foreach (var entry in entries)
            {
                var sql = "";
                if (entry.State == EntityState.Deleted)
                {
                    sql = $"ALTER TABLE {entry.Entity.TableName}  DROP COLUMN '{entry.Entity.FieldName}'";
                }
                else if (entry.State == EntityState.Modified)
                {

                }
                else if (entry.State == EntityState.Added)
                {
                    var type = string.Empty;
                    switch (entry.Entity.FieldType)
                    {
                        case FieldType.IntType:
                            type = "INTEGER";
                            break;
                        case FieldType.StringType:
                            type = "TEXT";
                            break;
                        case FieldType.DateTimeType:
                            break;
                    }

                    sql = $"ALTER TABLE {entry.Entity.TableName} add COLUMN '{entry.Entity.FieldName}' {type}";

                    this.Database.ExecuteSqlRaw(sql);
                }
            }

            return base.SaveChanges();
        }

        private void ExtendFieldDbContext_SavedChanges(object? sender, SavedChangesEventArgs e)
        {
            ExtendInfo.Clear();
            var list = this
                .ExtendFieldModels
                .ToList()
                .GroupBy(o => o.TableName)
                .Select(o => o);
            foreach (IGrouping<string, ExtendFieldModel> item in list)
            {
                ExtendInfo.TryAdd(item.Key, item.Select(o => o).ToList());
            }

            DefaultDbContext.ExtendFieldChanged = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
