using Model;

namespace OrderApi
{
    public class AcceptingPaymentsOnOrderImplementation: IImplementer
    {
        public AcceptingPaymentsOnOrderImplementation(IWebApiSettings? settings) { 
            //todo
        }
        public PipelineItemResponse Exec() {
            PipelineItemResponse result = new();
            //todo
            return result;
        }

        public OrderOperationType OrderOperationType { get; } = OrderOperationType.AcceptingPaymentsOnOrder;
    }
}
