using UnityEngine;

public class Drawers : InteractableObject
{
    [Header("Door Interactable object")]
    public string PromptMessageOn = "Close";
    public string PromptMessageOff = "Open";
    public bool IsLocked;
    public bool IsOpen;
    public float TimeToOpen = 2f;
    public float CoolDown = 0.5f;

    private float interactionTime;
    private float lastInteraction;
    private InteractableObject interactable;
    private Vector3 startPos;
    private Vector3 openPos;

    // Start is called before the first frame update
    void Start()
    {
        //interactable = GetComponent<InteractableObject>();
        startPos = transform.position;
        openPos = startPos + transform.up * -0.31f;   
    }

    protected override void Interaction()
    {
        interactionTime = Time.time;
        if (interactionTime - lastInteraction > CoolDown)
        {
            IsOpen = !IsOpen;
            if (IsOpen)
            {
                transform.position = openPos;
            }
            else
            {
                transform.position = startPos;
            }
            lastInteraction = interactionTime;
        }
    }

    public void ShowPrompt()
    {
        interactable.PromptMessage = IsOpen ? PromptMessageOn : PromptMessageOff;
    }
}
