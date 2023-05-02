using System.Collections;
using UnityEngine;

public class Photography : MonoBehaviour
{
    [Header("InGame Camera")]
    [SerializeField] private float distance;
    [SerializeField] private FilmRoll filmRoll;
    [SerializeField] private float coolDownTime;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private Camera cam;

    private Ray ray;
    private RaycastHit hit;
    private PhotographableObject photographable;
    private InputManager input;
    private Texture2D texture;
    private float photographTime;
    private float lastPhotographTime;

    void Start()
    {
        cam = Camera.main;
        //playerUI = GetComponent<PlayerUI>();
        input = InputManager.instance;
    }

    void LateUpdate()
    {
        // Reset
        HideCameraUI();
        if (photographable != null) 
            photographable.OutOfView();

        // Check for Photographable clue
        DrawRay();
        CheckForPhotoObject();

        // if can take photo 
        Photograph();
    }

    private void HideCameraUI()
    {
        playerUI.CameraUIDisable();
    }

    private void Photograph()
    {
        // Check for input
        if (input.ads && input.actionTrigger)
        {

            // returns if there is no film
            //if (!HasFilm()) return;
            
            // Check cooldown
            photographTime = Time.time;
            if (photographTime - lastPhotographTime <= coolDownTime) return;
            
            // Take and save screenshot
            TakeScreenshot(); 
            lastPhotographTime = photographTime;
            if (photographable == null) return;
            if (!WithinDistance()) return;
            photographable.Photograph();
        }
    }

    private void DrawRay()
    {
        ray = new Ray(cam.transform.position, cam.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * distance);
    }

    private bool CheckForHit()
    {
        return (Physics.Raycast(ray, out hit, distance));
    }

    public float CheckDistance()
    {
        return hit.distance;
    }

    private bool IsPhotographable()
    {
        photographable = null;
        if (hit.collider.GetComponent<PhotographableObject>() != null)
        {
            photographable = hit.collider.GetComponent<PhotographableObject>();
            return true;
        }
        return false;
    }

    public bool HasFilm()
    {
        if (filmRoll != null)
        {
            for (int i = 0; i < filmRoll.Roll.Length; i++)
            {
                if (filmRoll.Roll[i].IsEmpty()) return true;
            }
        }
        return false;
    }

    public void TakeScreenshot()
    {
        StartCoroutine(ScreenShot());
    }

    public bool SaveScreenshot()
    {
        // Searches for an empty film texture and sets it to the screenshot
        // Returns true if the texture was saved and false if it was not saved
        for (int i = 0; i < filmRoll.Roll.Length; i++)
        {
            if (filmRoll.Roll[i].IsEmpty())
            {
                filmRoll.Roll[i].setTexture(texture);
                return true;
            }
        }
        return false;
    }

    public bool CheckForPhotoObject()
    {
        if (!CheckForHit() || !IsPhotographable()) return false;
        photographable.InView();
        return true;
    }

    public void MarkAsPhotographed()
    {
        photographable.WasPhotographed();
    }

    public bool WithinDistance()
    {
        if (photographable.GetMinDistance() - hit.distance > 0f) return false;
        if (photographable.GetMaxDistance() - hit.distance < 0f) return false;
        return true;
    }

    public IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();
        texture = ScreenCapture.CaptureScreenshotAsTexture();
        SaveScreenshot();
    }
}
