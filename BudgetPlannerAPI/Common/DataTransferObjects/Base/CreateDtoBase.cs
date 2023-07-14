namespace Common.DataTransferObjects.Base
{
    public class CreateDtoBase
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}
