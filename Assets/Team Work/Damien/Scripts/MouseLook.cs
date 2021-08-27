using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 500f;

        public Transform playerBody;

        float xRotation = 0f;

        private bool cursorLocked;
        private bool disabled;


        void Start()
        {
            LockCursor();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(GameManager.paused);
            if (!GameManager.paused)
            {
                LockCursor();
                //Lock and hide the cursor
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

                playerBody.Rotate(Vector3.up * mouseX);
            }
            else
            {
                UnlockCursor();
            }
        }

        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void UnlockCursor()
        {
            //may change this to none later, not sure if I want to lock the cursor to the window or not.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
        }
    }
}