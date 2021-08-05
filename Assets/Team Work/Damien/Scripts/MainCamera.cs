using UnityEngine;
using UnityEngine.Rendering;

public class MainCamera : MonoBehaviour
{
    public Portal[] portals;
    private Portal currentPortal;
    private int portalLength;

    void Awake()
    {
        portals = FindObjectsOfType<Portal>();
        portalLength = portals.Length;
    }

    void LateUpdate()
    {
        //Debug.Log("PreCull");
        for (int i = 0; i < portalLength; i++)
        {
            currentPortal = portals[i];
            PrePortalRenderFunc(currentPortal);
            PortalRenderFunc(currentPortal);
            PostPortalRenderFunc(currentPortal);
        }
    }

    void PrePortalRenderFunc(Portal portal)
    {
        portal.PrePortalRender();
    }

    void PortalRenderFunc(Portal portal)
    {
        portal.Render();
    }

    void PostPortalRenderFunc(Portal portal)
    {
        portal.PostPortalRender();
    }
}