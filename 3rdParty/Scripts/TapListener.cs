using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class TapListener : MonoBehaviour {

    GestureRecognizer recognizer;
    float raycastRadius = 15f; 
    // Use this for initialization
    void Start () {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap) ;
        recognizer.TappedEvent += Recognizer_TappedEvent;
       
        recognizer.StartCapturingGestures(); 

    }

 

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        RaycastHit hitInfo; 
        if ( Physics.Raycast(Camera.main.transform.position,
                          Camera.main.transform.forward,                      
                          out hitInfo, raycastRadius ))
        {
            if ( hitInfo.collider != null  && hitInfo.collider.gameObject != null )
            {
                // TODO: DEMO PURPOSES Shortcut (delegating behaviors to SceneManager) .. 
                SceneManager.Instance.OnTapped(hitInfo.collider.gameObject);   
            }
        } 
    }

    private void OnDestroy()
    {
        recognizer.TappedEvent -= Recognizer_TappedEvent;        
    }
}
