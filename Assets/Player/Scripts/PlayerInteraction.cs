using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Player Interactions System")]
    [Tooltip("Distance you can use an interactable object")]
    [SerializeField] private float Distance = 3f;
    [SerializeField] private Sprite DefaultCrosshair;
    [SerializeField] private Sprite InteractableCrosshair;

    private Camera cam;
    private Ray ray;
    private RaycastHit hit;
    private PlayerUI playerUI;
    private InteractableObject interactable;
    private InputManager input;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerUI = GetComponent<PlayerUI>();
        input = InputManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset
        ChangeCrosshair(DefaultCrosshair);
        ResetText();
        ResetInteractable();

        // Check for interactable and interaction method, animator,display prompt, change crosshair
        DrawRay();
        if (!CheckForHit()) return;
        if (IsInteractable())
        {
            ChangeCrosshair(InteractableCrosshair);
            DisplayPromptMessage();
            Interact();
        }
    }

    private void Interact()
    {
        if (input.interact)
            interactable.BaseInteraction();
    }


    private void DrawRay()
    {
        ray = new Ray(cam.transform.position, cam.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * Distance);
    }

    private bool CheckForHit()
    {
        return (Physics.Raycast(ray, out hit, Distance));
    }

    private bool IsInteractable()
    {
        if (hit.collider.GetComponent<InteractableObject>() != null)
        {
            interactable = hit.collider.GetComponent<InteractableObject>();
            return true;
        }
        return false;
    }

    private void DisplayPromptMessage()
    {
        playerUI.UpdateText(interactable.PromptMessage);
    }

    private void ResetText()
    {
        playerUI.UpdateText(string.Empty);
    }

    private void ChangeCrosshair(Sprite sprite)
    {
        playerUI.UpdateCrosshair(sprite);
    }

    private void ResetInteractable()
    {
        interactable = null;
    }
}
