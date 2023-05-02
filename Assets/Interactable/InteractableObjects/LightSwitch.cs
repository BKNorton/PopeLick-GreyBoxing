
using System.Collections;
using UnityEngine;

public class LightSwitch : InteractableObject
{
    [Header("LightSwitch Interactable object")]
    public string PromptMessageOn;
    public string PromptMessageOff;
    public bool IsOn;
    public float CoolDown;
    public Light[] Lights;

    private float interactionTime;
    private float lastInteraction = 0f;
    public InteractableObject interactable;
    public Material material;

    protected override void Interaction()
    {
        interactionTime = Time.time;
        if (interactionTime - lastInteraction > CoolDown)
        {
            StartCoroutine(Wait(0.1f));
        }
    }

    public IEnumerator Wait(float seconds)
    {
        IsOn = !IsOn;
        lastInteraction = interactionTime;
        yield return new WaitForSeconds(seconds);
        foreach (Light light in Lights)
        {
            light.enabled = IsOn;
        }
    }
}

