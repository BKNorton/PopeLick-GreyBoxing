
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<Slot> slots;
    
    public List<Slot> Slots => slots;
    public int InventorySize => slots.Count;
    public UnityAction<Slot> OnSlotChange; 
    public InventorySystem(int size)
    {
        slots = new List<Slot>(size);

        for (int i = 0; i < size; i++)
        {
            slots.Add(new Slot());
        }
    }

    public bool AddToInventory(ScriptableItem itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<Slot> slots))
        {
            foreach (var invSlot in slots)
            {
                if (invSlot.RoomLeftInStack(amountToAdd))
                {
                    invSlot.AddToStack(amountToAdd);
                    OnSlotChange?.Invoke(invSlot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out Slot freeSlot))
        {
            freeSlot.UpdateSlot(itemToAdd, amountToAdd);
            OnSlotChange?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    public bool ContainsItem(ScriptableItem itemToAdd, out List<Slot> slots)
    {
        slots = Slots.Where(i => i.ItemData == itemToAdd).ToList();
        return slots == null ? false : true;
    }

    public bool HasFreeSlot(out Slot freeSlot)
    {
        freeSlot = Slots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
