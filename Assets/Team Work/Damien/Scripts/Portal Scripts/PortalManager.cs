using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien
{
    public class PortalManager : MonoBehaviour
    {
        public Portal[] portals;

        // Start is called before the first frame update
        void Awake()
        {
            portals = FindObjectsOfType<Portal>();
            foreach (Portal portal in portals)
            {
                //do something here
                //It's 5am, I'm tired I'll do it in the morning
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
