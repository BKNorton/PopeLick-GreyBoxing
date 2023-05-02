using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class ScriptableItem : ScriptableObject
{
    public int ID;
    public string Name;
    public int MaxStackSize;
    [TextArea(4,4)]
    public string Description;
    public Sprite Icon;
}
