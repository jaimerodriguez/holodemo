using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoInteractable : MonoBehaviour {

    private bool isWalking = false;
    public float TapForceStrength = 10f; 

    //Saved so we can "stand-up on TAP" 
    Quaternion initialRotation; 

	// Use this for initialization
	void Start () {
        initialRotation = this.transform.rotation;         
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PerformWalk ( )
    {
        isWalking = true;
        float totalTime = 5f; 
        float currentTime = 0f;
        Quaternion startRotation, endRotation;        
        Vector3 startingPosition = this.transform.position;
        Vector3 targetPosition = startingPosition;
        targetPosition.x = startingPosition.x - 1.5f;
        startRotation =  Quaternion.Euler (0f, 0f, 0f);
        endRotation =  Quaternion.Euler (0, 179f, 0f);

        while (currentTime < totalTime )
        {
            currentTime += Time.deltaTime;
            float percent = currentTime / totalTime;
            this.transform.position = Vector3.Lerp(startingPosition, targetPosition, percent);
            float twiceSpeed = (percent * 2f) % 1f; 
            this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, twiceSpeed);             
            yield return null;
        }
        isWalking = false; 
        Debug.Log("Walk completed"); 
    }

    
    public void Walk ( )
    {
        if (!isWalking)
        {           
            StartCoroutine( PerformWalk());            
        } 
    }


    public void OnTapped ( object targetObject   )
    {
        if (this.gameObject == targetObject)
        {              
            if (this.transform.rotation == initialRotation)
            {
                Vector3 v = Camera.main.transform.forward * TapForceStrength;
                this.GetComponent<Rigidbody>().AddForce(v);
            }
            else
                this.transform.rotation = initialRotation;
        } 
    }
}
