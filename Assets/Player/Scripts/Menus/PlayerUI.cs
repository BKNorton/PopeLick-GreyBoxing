
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Player HUD")]
    [Tooltip("Displayed text")]
    [SerializeField] private TextMeshProUGUI PromptText;
    [Tooltip("Default Crosshair")]
    [SerializeField] private GameObject Crosshair;
    [Tooltip("Default Crosshair")]
    [SerializeField] private GameObject CameraUI;

    public void UpdateText(string promptMessage)
    {
        PromptText.text = promptMessage;
    }

    public void UpdateCrosshair(Sprite sprite)
    {
        Crosshair.SetActive(true);
        Crosshair.GetComponent<Image>().sprite = sprite;
    }

    public void CameraUIEnable()
    {
        CameraUI.SetActive(true);
    }

    public void CameraUIDisable()
    {
        CameraUI.SetActive(false);
    }

    public void HideCrosshair()
    {
        Crosshair.SetActive(false);
    }
}


