using System.ComponentModel.DataAnnotations;

namespace ExtendFieldDemo.Models
{
    public class ExtendFieldModel
    {
        [Key]
        public int Id { get; set; }

        public string? TableName { get; set; }

        public string? ModelType { get; set; }

        public string? FieldName { get; set; }

        public FieldType FieldType { get; set; }
    }
}
