namespace Model
{
    public class PipleneItemRequest
    {
        public int Cost { get; set; } = 0;
        public string TaskName { get; set; } = "";
        public OrderOperationType OrderOperationType { get; set; } = OrderOperationType.Empty;
        public object? Obj { get; set; } = null;
        public int OrderNumber { get; set; } = 0;
        public OrderOperationType NextOperationWhenSuccessResponse { get; set; } = OrderOperationType.Empty;
        public OrderOperationType NextOperationWhenErrorResponse { get; set; } = OrderOperationType.Empty;

    }
}