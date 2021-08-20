using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class MainCamera : MonoBehaviour
{
    public Portal[] portals;
    private Portal currentPortal;
    private int portalLength;
    public static RenderPipeline currentPipeline;

    void Start()
    {
        portals = FindObjectsOfType<Portal>();
        portalLength = portals.Length;
        //RenderPipelineManager.endFrameRendering += RenderPortal;
    }


    void LateUpdate()
    {
        //Need tp find something that calls the same time as OnPreCull does in the standard pipeline
        // StartCoroutine(RenderPortal());
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


        void RenderPortal(ScriptableRenderContext context,
            Camera[] camera) //maybe a coroutine that waits until the end of frame?
        {
            /**yield return new WaitForEndOfFrame(); //was a coroutine, now isn't
            for (int i = 0; i < portalLength; i++)
            {
                currentPortal = portals[i];
                PrePortalRenderFunc(currentPortal);
                PortalRenderFunc(currentPortal);
                PostPortalRenderFunc(currentPortal);
            }**/

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
}