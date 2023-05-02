
using UnityEngine;

public class FilmRoll : InteractableObject 
{
    public ScriptableItem itemData;
    public Film[] Roll;

    public FilmRoll GetFilmRoll()
    {
        return this;
    }

    // make film inactive
    public Film GetFilm(int i)
    {
        return Roll[i];
    }

    public void DestroyFilmRoll()
    {
        Destroy(this);
    }

    protected override void Interaction()
    {
        //pick up item
        if (PlayerInventory.Instance.InventorySystem.AddToInventory(itemData, 1))
        {
            Destroy(gameObject);
        }
    }
}
