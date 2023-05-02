
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryContainer : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] private InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequsted;

    public void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }
}
