using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This is what shows the circle and lines in the editor showing the detection radius and angle

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor {//this is in Editor and not MonoBehavior, so this stuff only shows up in editor (and uses a slightly different API)

	void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target; //reference to the FieldOfView script
        Handles.color = Color.white;//sets circle color to white
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);//creates the circle around agent in the editor
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);//these are the angle lines
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);//drawing the angle lines in editor
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        Handles.color = Color.red;//setting color of detection line to red in editor
        foreach(Transform visibleTarget in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);//draws a red line from agent to each target detect. targets are assigned in the target layer in the editor
        }
    }

}
