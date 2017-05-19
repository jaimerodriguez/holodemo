using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia; 


public class VuforiaTargetBehavior : MonoBehaviour ,  ITrackableEventHandler
{

    public Material TargetHighlight;
    public Material TargetNormal; 
    public Renderer TargetRenderer;
    public GameObject Target;

    #region PRIVATE_MEMBER_VARIABLES

    private TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
         
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }
      
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found at " + this.transform.position.ToString() );


        if ( this.TargetRenderer  != null )
        {            
            this.TargetRenderer.material = TargetHighlight; 
        }

        if (this.Target != null)
        {
            var renderer = this.Target.GetComponent<Renderer>(); 
            renderer.enabled = true;
            renderer.material = TargetHighlight; 
        }

        SceneManager.Instance.VuforiaObjectDetected(this.gameObject); 
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

        if (this.TargetRenderer != null)
        {
            this.TargetRenderer.material = TargetNormal;
        }

        if ( this.Target != null )
        {
            var renderer = this.Target.GetComponent<Renderer>();             
            renderer.material = TargetNormal;
        }
    }

    #endregion // PRIVATE_METHODS

    private void OnDisable()
    {
        mTrackableBehaviour.UnregisterTrackableEventHandler(this); 
    }
}
