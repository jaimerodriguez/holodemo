
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRenderCameraFollow : MonoBehaviour {

    LineRenderer line;
    // Use this for initialization     
    Vector3 lineOffset; 


    void Start () {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        lineOffset = new Vector3(0f, -.2f, 0f);     
    }

    // Update is called once per frame
    void Update () {
        if ( true) 
        //if (GazeManager.Instance != null &&
        //   GazeManager.Instance.enabled && (GazeManager.Instance.HitObject != null)  )
        {
            line.enabled = true; 
            if (!line.useWorldSpace)
            { 
                line.SetPosition(0, transform.InverseTransformPoint(Camera.main.transform.position + lineOffset));
                Vector3 point = Camera.main.transform.position + (Camera.main.transform.forward * .5f);
                line.SetPosition(1, transform.InverseTransformPoint(point));
            } 
            else
            {                 
                line.SetPosition(0, Camera.main.transform.position + lineOffset );
                Vector3 point = Camera.main.transform.position + (Camera.main.transform.forward * .5f);
                line.SetPosition(1,  point);
            }
        }
        else
        {
            // no hit, hide our line.. 
            line.enabled = false;  
        }
    }
}
