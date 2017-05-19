using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
public class SimplestCursor : MonoBehaviour {

    public float Radius = 10f;

    public Material MaterialOn;
    public Material MaterialOff;
    Renderer _renderer; 
     
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>(); 
	}
	
    
	// Update is called once per frame
	void Update () {
        var cameraTransform = Camera.main.transform; 
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraTransform.position,
                          cameraTransform.forward,
                          out hitInfo, Radius)) 
        {
             _renderer.material = MaterialOn;            
             this.transform.position = hitInfo.point; 
        }
        else
        {
            this.transform.position = Camera.main.transform.forward * 1.5f; 
             _renderer.material = MaterialOff; 
        }
        // this.transform.rotation = Camera.main.transform.rotation;
        Quaternion rotation = Quaternion.LookRotation(this.transform.position);
        transform.rotation = rotation;

       
    }
     
}
