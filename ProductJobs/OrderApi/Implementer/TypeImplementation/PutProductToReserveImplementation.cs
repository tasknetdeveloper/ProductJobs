using Model;

namespace OrderApi
{
    public class PutProductToReserveImplementation:IImplementer
    {

        public PutProductToReserveImplementation(IWebApiSettings? settings) {
            //todo
        }

        public PipelineItemResponse Exec()
        {
            PipelineItemResponse result = new();
            //todo
            return result;
        }

        public OrderOperationType OrderOperationType { get; } = OrderOperationType.PutProductToReserve;

    }
}
