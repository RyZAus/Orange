using UnityEditor;
using UnityEngine;


namespace Damien
{
    [CustomEditor(typeof(PlayerFOV))]
    public class PlayerFOVEditor : Editor
    {
        private Vector3 targetPos;

        void OnSceneGUI()
        {
            PlayerFOV fov = (PlayerFOV) target;

            Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
            Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);


            Handles.color = Color.gray;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
            Handles.color = Color.blue;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

            foreach (GameObject playerTarget in fov.listOfTargets)
            {
                Handles.color = Color.clear;
                targetPos = playerTarget.transform.position;
                Handles.DrawLine(fov.transform.position, new Vector3(targetPos.x + .25f, targetPos.y, targetPos.z));
                Handles.DrawLine(fov.transform.position, new Vector3(targetPos.x, targetPos.y + 2.75f, targetPos.z));
                Handles.DrawLine(fov.transform.position,
                    new Vector3(targetPos.x, targetPos.y + 1.5f, targetPos.z + .75f));
                Handles.DrawLine(fov.transform.position,
                    new Vector3(targetPos.x, targetPos.y + 1.5f, targetPos.z - .75f));

            }
        }
    }
}