using UnityEngine;

public class Menu : MonoBehaviour
{
    public float pauseTimeScale;
    public GameObject menu;
    public GameObject firstPauseButton;
    public bool isPaused;

    private void Start()
    {
        menu.SetActive(false);
    }
}
