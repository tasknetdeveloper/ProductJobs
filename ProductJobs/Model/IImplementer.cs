
namespace Model
{
    public interface IImplementer
    {
        PipelineItemResponse Exec();
        OrderOperationType OrderOperationType { get; }

    }
}
