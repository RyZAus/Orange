using System.Collections;
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
        //Need tp find something that calls the same time as OnPreCull does in the standard pipeline
        for (int i = 0; i < portalLength; i++)
        {
            currentPortal = portals[i];
            PrePortalRenderFunc(currentPortal);
            PortalRenderFunc(currentPortal);
            PostPortalRenderFunc(currentPortal);
        }
        
        // StartCoroutine(RenderPortal());
    }


    IEnumerator RenderPortal() //maybe a coroutine that waits until the end of frame?
    {
        yield return new WaitForEndOfFrame();
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