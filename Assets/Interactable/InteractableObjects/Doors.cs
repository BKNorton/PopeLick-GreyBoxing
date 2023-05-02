using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Doors : InteractableObject
{
    [Header("Door Interactable object")]
    public string PromptMessageOn = "Close";
    public string PromptMessageOff = "Open";
    public bool IsLocked;
    public bool IsOpen;
    public float CoolDown = 1f;

    private float interactionTime;
    private float lastInteraction;
    private InteractableObject interactable;
    public Animator animator;

    /*
    void Awake()
    {
        animator = GetComponent<Animator>();
        interactable = GetComponent<InteractableObject>();
        if (animator == null)
        {
            animator = GetComponentInParent<Animator>();
        }
    }
    */

    protected override void Interaction()
    {
        interactionTime = Time.time;
        if (interactionTime - lastInteraction > CoolDown)
        {
            IsOpen = !IsOpen;
            animator.SetBool("IsOpen", IsOpen);
            lastInteraction = interactionTime;
        }
    }

    public void ShowPrompt()
    {
        interactable.PromptMessage = IsOpen ? PromptMessageOn : PromptMessageOff;
    }
}

