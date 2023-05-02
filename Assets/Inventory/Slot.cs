using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot  
{
    [SerializeField] private ScriptableItem itemData;
    [SerializeField] private int stackSize;

    public ScriptableItem ItemData  => itemData;
    public int StackSize => stackSize;

    public Slot(ScriptableItem item, int amount)
    {
        itemData = item;
        stackSize = amount;
    }

    public Slot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public bool IsClear()
    {
        return itemData = null;
    }

    public void AssignItem(Slot invSlot)
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize);
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateSlot(ScriptableItem item, int amount)
    {
        itemData = item;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = itemData.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}
