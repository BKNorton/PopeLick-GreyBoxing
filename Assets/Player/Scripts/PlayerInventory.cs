
public class PlayerInventory : InventoryContainer
{
    public static PlayerInventory Instance;

    public void Start()
    {
        Instance = this;
    }
}
