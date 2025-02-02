﻿using UnityEngine;

namespace Damien
{
    public class PortalTextureSetup : MonoBehaviour
    {
        public Camera camera1;
        public Camera camera2;

        public Material cameraMat1;
        public Material cameraMat2;

        void Start()
        {
            if (camera2.targetTexture != null)
            {
                camera2.targetTexture.Release();
            }

            if (camera1.targetTexture != null)
            {
                camera1.targetTexture.Release();
            }

            camera1.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameraMat1.mainTexture = camera1.targetTexture;

            camera2.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameraMat2.mainTexture = camera2.targetTexture;
        }
    }
}