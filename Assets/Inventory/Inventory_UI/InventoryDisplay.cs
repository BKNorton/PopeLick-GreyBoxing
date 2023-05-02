using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<Slot_UI, Slot> slotDictionary;

    public InventorySystem InventorySystem => InventorySystem;
    public Dictionary<Slot_UI, Slot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(Slot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    private void SwapSlots(Slot_UI clickedUISlot)
    {
        // Clone mouseItem Slot and clear mouseItem
        var clonedSlot = new Slot(mouseItem.AssignedSlot.ItemData, mouseItem.AssignedSlot.StackSize);
        mouseItem.ClearSlot();

        // Update mouseItem Slot to clickedUISlot and clear clickedUISlot
        mouseItem.UpdateMouseSlot(clickedUISlot.AssignedSlot);
        clickedUISlot.ClearSlot();

        // Update clickedUISlot to clonedSlot and update the Slot_UI
        clickedUISlot.AssignedSlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }

    public void SlotClick(Slot_UI clickedUISlot)
    {

        // Clicked slot has an item - mouse doesn't have an item - pick up that item
        if (clickedUISlot.AssignedSlot.ItemData != null && mouseItem.AssignedSlot.ItemData == null)
        {
            // If player is holding shift key? Split the stack
            mouseItem.UpdateMouseSlot(clickedUISlot.AssignedSlot);
            clickedUISlot.ClearSlot();

            return;
        }

        // Clicked slot doesn't have an item - Mouse does have an item - place the mouse item the empty slot.
        if (clickedUISlot.AssignedSlot.ItemData == null && mouseItem.AssignedSlot.ItemData != null)
        {
            clickedUISlot.AssignedSlot.AssignItem(mouseItem.AssignedSlot);
            clickedUISlot.UpdateUISlot();

            mouseItem.ClearSlot();
        }

        // Both slots have an item - decide what to do...
        if (clickedUISlot.AssignedSlot.ItemData != null && mouseItem.AssignedSlot.ItemData != null)
        {
            // If items are the same, combine them
            if (clickedUISlot.AssignedSlot.ItemData == mouseItem.AssignedSlot.ItemData)
            {
                // If theres room left in stack of the clickedUISlot, add mouseItem stackSize to clickedUISlot stackSize.
                if (clickedUISlot.AssignedSlot.RoomLeftInStack(mouseItem.AssignedSlot.StackSize, out int leftInStack))
                {
                    clickedUISlot.AssignedSlot.AssignItem(mouseItem.AssignedSlot);
                    clickedUISlot.UpdateUISlot();
                    // Clear mouseItem
                    mouseItem.ClearSlot();
                }
                // Else if the clickedUISlot stackSize is >= MaxStackSize, swap items.
                else
                {
                    // Stack is full so swap
                    if (leftInStack == 0) SwapSlots(clickedUISlot);
                    else
                    {
                        int remainingOnMouse = mouseItem.AssignedSlot.StackSize - leftInStack;
                        clickedUISlot.AssignedSlot.AddToStack(leftInStack);
                        clickedUISlot.UpdateUISlot();

                        var newItem = new Slot(mouseItem.AssignedSlot.ItemData, remainingOnMouse);
                        mouseItem.ClearSlot();
                        mouseItem.UpdateMouseSlot(newItem);
                    }
                }
            }
            // If different item, then swap the items.
            else SwapSlots(clickedUISlot);
        }

        
        // Is the slot stack size + mouse stack size > the slot Max Stack size? if so, take from mouse.

        

    }
}
