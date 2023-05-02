using UnityEngine;

public class Film : MonoBehaviour
{
    [SerializeField] private Texture2D texture;
    [SerializeField] private GameObject picture;

    private bool isEmpty;

    void Update()
    {
        //CheckIfIsEmpty();
    }
    private void CheckIfIsEmpty()
    {
        if (texture == null)
        {
            isEmpty = true; 
        }

        else
        {
            isEmpty = false;  
        }
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public Texture getTexture()
    {
        return texture;
    }

    public void setTexture(Texture2D texture)
    {
        this.texture = texture;
        setPicture(texture);
    }

    public void InstantiatePicture()
    {

    }

    public void setPicture(Texture texture)
    {
        // This game object is a Picture which is a prefab, it has one child(the face of the object)
        GameObject face = picture.transform.GetChild(0).gameObject;
        face.GetComponent<Renderer>().material.mainTexture = texture;
    }
}
