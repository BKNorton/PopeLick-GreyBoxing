using UnityEngine;

public class PauseMenu : Menu
{
    public static PauseMenu instance;

    void Awake()
    {
        pauseTimeScale = 0f;
        instance = this;
    }
}
