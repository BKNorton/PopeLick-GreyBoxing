using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;


public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public Slot AssignedSlot;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = ""; 
    }
    
    public void UpdateMouseSlot(Slot invSlot)
    {
        AssignedSlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.ItemData.Icon;
        if (invSlot.StackSize == 1) ItemCount.text = "";
        else ItemCount.text = invSlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    public void ClearSlot()
    {
        AssignedSlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    private void Update()
    {
        if (AssignedSlot.ItemData != null)
            transform.position = Mouse.current.position.ReadValue();
    }
}
