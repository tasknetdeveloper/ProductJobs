
namespace Model
{
    public enum OperationStatus
    { 
        Empty,
        InWork,        
        ErrorResult,
        SuccessResult
    }

    public enum OrderOperationType
    {
        Empty=0,
        CreateOrder=1,
        PutProductToReserve=2,
        AcceptingPaymentsOnOrder=3
    }
}
