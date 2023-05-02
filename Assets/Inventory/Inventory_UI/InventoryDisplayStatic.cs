
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayStatic : InventoryDisplay
{
    [SerializeField] private InventoryContainer inventoryContainer;
    [SerializeField] private Slot_UI[] slots_UI;

    public void Start()
    {
        base.Start();

        if (inventoryContainer != null)
        {
            inventorySystem = inventoryContainer.InventorySystem;
            inventorySystem.OnSlotChange += UpdateSlot;
        }
        else Debug.Log("warning");

        AssignSlot(inventorySystem);
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<Slot_UI, Slot>();

        if (slots_UI.Length != inventorySystem.InventorySize) Debug.Log("slots out of sync");

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots_UI[i], inventorySystem.Slots[i]);
            slots_UI[i].Init(inventorySystem.Slots[i]);
        }
    }
}