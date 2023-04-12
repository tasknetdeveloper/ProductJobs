using Redis.OM.Modeling;
namespace Model
{
    [Document(IndexName = "OrderItem", StorageType = StorageType.Json)]

    public class OrderItem
    {
        [RedisIdField]
        [Indexed(Sortable = true)]
        public int Id { get; set; }
        [Indexed(Sortable = true)]
        public int IdProduct { get; set; }
        [Indexed(Sortable = true)]
        public string OrderName { get; set; } = "";
        [Indexed(Sortable = true)]
        public OperationStatus Status { get; } = OperationStatus.Empty;
        public int InstanceNumber { get; set; } = -1;
    }
}
