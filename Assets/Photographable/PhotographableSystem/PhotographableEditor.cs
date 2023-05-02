using UnityEditor;

/*[CustomEditor(typeof(PhotographableObject), true)]
public class PhotographableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PhotographableObject photographable = (PhotographableObject)target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            if (photographable.GetComponent<PhotographEvent>() == null)
            {
                photographable.UseEvents = true;
                photographable.gameObject.AddComponent<PhotographEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (photographable.UseEvents)
            {
                if (photographable.GetComponent<PhotographEvent>() == null)
                {
                    photographable.gameObject.AddComponent<PhotographEvent>();
                }
            }
            else
            {
                if (photographable.GetComponent<PhotographEvent>() != null)
                {
                    DestroyImmediate(photographable.GetComponent<PhotographEvent>());
                }
            }
        }
    }
}*/