using UnityEngine;

public class InventoryMenu : Menu
{
    public static InventoryMenu instance;

    void Awake()
    {
        pauseTimeScale = 0.3f;
        instance = this;
    }
}
