using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Portal[] portals;

    void Awake()
    {
        portals = FindObjectsOfType<Portal>();
    }

    void LateUpdate()
    {
        //Debug.Log("PreCull");
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].PrePortalRender();
        }

        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].Render();
        }

        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].PostPortalRender();
        }
    }
}