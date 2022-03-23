namespace PickPoint.Lib.Domain.Enums;

public enum EOrderStatus
{
    None = 0,
    
    Registered = 1,
    
    InStock = 2,
    
    IssuedToCourier = 3,
    
    DeliveredToMachine = 4,
    
    DeliveredToRecipient = 5,
    
    Canceled = 6,
}