using ExtendFieldDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtendFieldDemo.DatabaseAccess
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 扩展字段改变标志。
        /// </summary>
        public static bool ExtendFieldChanged { get; set; }


        public DbSet<Book> Books { get; set; }

        public DbSet<Dictionary<string, object>> BookExtendField => Set<Dictionary<string, object>>("BookExtendField");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in ExtendFieldDbContext.ExtendInfo)
            {
                modelBuilder.SharedTypeEntity<Dictionary<string, object>>(item.Key, c =>
                {
                    c.IndexerProperty<int>("Id");
                    c.IndexerProperty<int>("ModelId").HasComment("模型的Id");
                    foreach (var info in item.Value)
                    {
                        switch (info.FieldType)
                        {
                            case FieldType.IntType:
                                c.IndexerProperty<int>(info.FieldName!);
                                break;
                            case FieldType.StringType:
                                c.IndexerProperty<string>(info.FieldName!);
                                break;
                            case FieldType.DateTimeType:
                                c.IndexerProperty<DateTime>(info.FieldName!);
                                break;
                        }
                    }
                });
            }
        }
    }
}
