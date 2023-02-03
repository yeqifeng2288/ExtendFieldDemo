using System.ComponentModel.DataAnnotations;
using ExtendFieldDemo.DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace ExtendFieldDemo.Models
{
    public class Book : BaseExtendModel<int>
    {
        public Book()
        {

        }

        private Book(DefaultDbContext defaultDbContext)
        {
            ExtendTableName = "BookExtendField";
            DbContext = defaultDbContext;
        }

        [Key]
        public override int Id { get; set; }

        [Comment("标题")]
        public string? Title { get; set; }
    }
}
