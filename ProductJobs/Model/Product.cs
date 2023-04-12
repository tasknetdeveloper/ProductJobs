using Redis.OM.Modeling;
namespace Model
{
    [Document(IndexName = "Product", StorageType = StorageType.Json)]
    public class Product
    {
        [RedisIdField]
        [Indexed(Sortable = true)]
        public int Id { get; set; }
        [Indexed(Sortable = true)]
        public string Name { get; set; } = "Empty";
        [Indexed(Sortable = true)]
        public float Price { get; set; }
        [Indexed(Sortable = true)]
        public string Description { get; set; } = "";

        [Indexed(Sortable = true)]
        public OperationStatus Status { get; } = OperationStatus.Empty;
        public int InstanceNumber { get; set; } = -1;

    }
}