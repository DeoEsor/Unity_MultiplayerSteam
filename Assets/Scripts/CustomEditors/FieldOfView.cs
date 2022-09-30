using Core;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            var fov = (FieldOfView)target;
            Handles.color = Color.white;
            var position = fov.transform.position;
            Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, fov.radius);

            var viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
            var viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(position, position + viewAngle01 * fov.radius);
            Handles.DrawLine(position, position + viewAngle02 * fov.radius);

            if (!fov.canSeePlayer) 
                return;
            
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.targetRef.transform.position);
        }

        private static Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
    
}