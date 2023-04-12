
namespace Model
{
    public class PipelineItemResponse
    {
        public OperationStatus Status { get; set; } = OperationStatus.Empty;
        public OrderOperationType OrderOperationType { get; set; } = OrderOperationType.Empty;
        public string Response { get; set; } = string.Empty;
        // public int Cost { get; set; }  //может приодиться в будущем, тут можно проставить оценку стоимости выполнения запроса
        public int ExecNumber { get; set; }
    }
}
