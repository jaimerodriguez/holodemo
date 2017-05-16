#define FAKESCAN   

// NOTE: Temporarily FAKESCAN if you don't have the "yoda toys" that object scanner finds. 
// NOTE: We will soon replace it for an image scan to make this demo more portable.. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq; // for ToArray 
using Vuforia;
using UnityEngine.VR.WSA;


public class SceneManager : MonoBehaviour   
{

    public GameObject ObjectTrackingReticle;
    public GameObject[] InteractiveElements;
    public GameObject SpatialMappingContainer;
    public GameObject Cursor;
    AudioSource audioSource; 
    

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void Awake()
    {
        Debug.Assert(_instance == null);
        _instance = this;
        audioSource = GetComponent<AudioSource>(); 
    } 

    // Use this for initialization
    void Start () {
        StartVoiceRecognizer();

         
    }

    private void OnDestroy()
    {        
        if (keywordRecognizer != null )
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
            keywordRecognizer = null; 
        }          
    }

    void Acknowledge ()
    {
        if ( audioSource != null )
            audioSource.Play(); 
    }

    public void VuforiaObjectDetected (  GameObject go  )
    {
        if (ObjectTrackingReticle != null)
        {
            var material = Resources.Load<Material>("Materials/Green");
            if (material != null)
            {
                ObjectTrackingReticle.GetComponent<Renderer>().material = material;
            }                               
            Acknowledge();
        }
    }


    void StartVoiceRecognizer()
    { 
        keywords = new Dictionary<string, System.Action>();
        keywords.Add("Laser", ToggleLaser);
        keywords.Add("Gravity", ToggleGravity);        
        keywords.Add("Stop Tracking", StopVuforia);
        keywords.Add("Toggle scan", ToggleScan);
        keywords.Add("Toggle renderer", ToggleRenderer);
        keywords.Add("Walk", OnWalk);
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void OnWalk()
    {
        if (lastSelectedObject != null)
        {
            var interactable = lastSelectedObject.GetComponent<DemoInteractable>();
            if (interactable)
                interactable.Walk();
            Acknowledge();
        }
    } 

   
    public void ToggleRenderer()
    {
        if (SpatialMappingContainer != null)
        {
            var renderer = SpatialMappingContainer.GetComponent<SpatialMappingRenderer>();
            if (renderer != null)
            {
                if (renderer.renderState == SpatialMappingRenderer.RenderState.Occlusion)
                    renderer.renderState = SpatialMappingRenderer.RenderState.Visualization;
                else
                    renderer.renderState = SpatialMappingRenderer.RenderState.Occlusion;
            }
        }

        Acknowledge(); 
    }

    void ToggleScan ()
    {
        if (SpatialMappingContainer != null)
        {
            SpatialMappingContainer.SetActive(!SpatialMappingContainer.activeSelf);
        }
        Acknowledge();
    }

    
    IEnumerator StopVuforiaCoroutine ()
    {
        //TODO: i have not found good documentation on stopping vuforia. 
        // I do want to stop it because for demo purposes when capturing video it consumes resources
        //this is best i can think to stop it. Seems to work 'OK' 
                  
        CameraDevice.Instance.Stop();
        yield  return new WaitForSeconds(2f);

        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker != null)
            objectTracker.Stop();
         
        
        yield return new WaitForSeconds(1f);
        bool deInit = TrackerManager.Instance.DeinitTracker<ObjectTracker>(); 

    }
    void  StopVuforia ()
    {
         StartCoroutine("StopVuforiaCoroutine");  
    }


    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    void ToggleLaser()
    {
        if ( ObjectTrackingReticle != null )
        {
            ObjectTrackingReticle.SetActive(!ObjectTrackingReticle.activeSelf);                        
        }

        if ( Cursor != null )
        {
            Cursor.SetActive(!Cursor.activeSelf); 
        }


#if FAKESCAN 
        if (doOnce)
        {
            StartCoroutine("LaserHack");
            doOnce = false;
        } 
#endif 
    }

#if FAKESCAN  
    bool doOnce = true  ;
#endif 

    void Clear ( )
    {
        lastSelectedObject = null; 
    }

    void ToggleGravity ()
    {
        foreach ( GameObject go in InteractiveElements )
        {
            var rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {                
                rb.useGravity = true;
            } 
        }         
    }

    void ActivateYoda( )
    {         
        foreach (GameObject go in InteractiveElements)
        {
            go.SetActive(true);
        }         
    }

    GameObject lastSelectedObject; 

    public void OnTapped ( object targetObject )
    {
        lastSelectedObject = targetObject as GameObject;    
        foreach ( GameObject go in InteractiveElements )
        {
            if ( go == lastSelectedObject ) 
                go.SendMessage("OnTapped", lastSelectedObject);
        }       
    }


    //TODO: LAZY demo shortcut to keep everything in one place
    static SceneManager _instance; 
    internal static SceneManager Instance
    {
        get
        {             
            Debug.Assert(_instance != null);
            return _instance ;   
        }
    }

#if FAKESCAN  
    IEnumerator LaserHack ()
    {
        yield return new WaitForSeconds(4f);
        VuforiaObjectDetected(null); 
        ActivateYoda(); 
    }
    
#endif 
}
