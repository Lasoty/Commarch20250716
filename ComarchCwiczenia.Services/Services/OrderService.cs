namespace ComarchCwiczenia.Services.Services;
public interface IInventoryService
{
    bool IsProductAvailable(string productId);
}

public class OrderService
{
    private readonly IInventoryService _inventoryService;

    public OrderService(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public bool PlaceOrder(string productId)
    {
        if (productId == null)
            throw new ArgumentNullException(nameof(productId));

        if (!_inventoryService.IsProductAvailable(productId))
        {
            return false;
        }

        // ... kod składania zamówienia (pominięty)
        return true;
    }
}

