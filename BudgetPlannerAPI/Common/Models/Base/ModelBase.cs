using System.ComponentModel.DataAnnotations;

namespace Common.Models.Base
{
    public class ModelBase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
