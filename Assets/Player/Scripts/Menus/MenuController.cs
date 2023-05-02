using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    private InputManager input;
    private Menu menu;

    private void Start()
    {
        input = InputManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // If the pause button is pressed 
        if (input.pause)
        {
            menu = PauseMenu.instance;
            Pause();
        }
        // If the start button is pressed
        else if (input.start)
        {
            menu = InventoryMenu.instance;
            Pause();
        }
        if (menu.isPaused)
        {
            if (!input.pause && !input.start)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        // Activate UI
        menu.gameObject.SetActive(true); 
        // Pause Game
        Time.timeScale = menu.pauseTimeScale;
        // EventSystem
        EventSystem.current.SetSelectedGameObject(null); // clear system
        EventSystem.current.SetSelectedGameObject(menu.firstPauseButton); // set first button
        input.SetCursorState(false);

        menu.isPaused = true;
    }

    public void Resume()
    {
        // Deactivate UI
        menu.gameObject.SetActive(false);
        // Resume Game
        Time.timeScale = 1f;
        input.SetCursorState(true);

        menu.isPaused = false;
    }
}
