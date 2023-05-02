using UnityEngine;

public class PhotographableObject : MonoBehaviour
{
    [Header("Photographable Object")]
    [SerializeField] private string Name;
    [SerializeField] private bool IsVisible;
    [SerializeField] private bool wasPhotographed;
    [SerializeField] public bool UseEvents;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    //private Renderer _renderer;
    //[SerializeField] private PhotographEvent OnPhotographed;

    private void Start()
    {
        //OnPhotographed = new PhotographEvent();
    }

    private void LateUpdate()
    {
        //IsVisible = false;
    }
    public void Photograph()
    {
        //OnPhotographed.Photographed.Invoke();
        wasPhotographed = true;
    }

    public string GetName()
    {
        return Name;
    }    

    public bool WasPhotographed()
    {
        return wasPhotographed;
    }

    public void InView()
    {
        IsVisible = true;
    }

    public void OutOfView()
    {
        IsVisible = false;
    }

    public float GetMinDistance()
    {
        return minDistance;
    }

    public float GetMaxDistance()
    {
        return maxDistance;
    }
}