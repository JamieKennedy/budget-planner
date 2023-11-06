namespace Common.Models.Base
{
    public class ModifiableBase : ModelBase
    {
        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}
