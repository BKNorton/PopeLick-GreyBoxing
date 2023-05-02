using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [Header("Interactable Object")]
    public string PromptMessage;
    public bool UseEvents;

    public void BaseInteraction()
    {
        if (UseEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interaction();
    }

    protected abstract void Interaction();
}

