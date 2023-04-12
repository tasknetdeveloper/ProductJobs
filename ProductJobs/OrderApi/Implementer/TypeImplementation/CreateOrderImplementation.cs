using Model;

namespace OrderApi
{
    public class CreateOrderImplementation:IImplementer
    {

        public CreateOrderImplementation(IWebApiSettings? settings) { 
            //todo
        }
        public PipelineItemResponse Exec()
        {
            PipelineItemResponse result = new();
            //todo
            return result;
        }

        public OrderOperationType OrderOperationType { get; } = OrderOperationType.CreateOrder;

    }
}
