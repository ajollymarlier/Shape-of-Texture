using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Occlusion : MonoBehaviour
{
    public string SelectAudio;
    private FMOD.Studio.EventInstance Audio;
    private FMOD.Studio.PARAMETER_ID VolumeParameterId, LPFParameterId;
    
    public FMOD.Studio.EventInstance instance;
    public StudioEventEmitter Emitter;

    private Transform SlLocation;

    [Range(0f, 1f)]
    public float VolumeValue = 0.75f;
    [Range(0f, 1f)]
    public float WholeVolumeValue = 1f;
    [Range(10f, 22000f)]
    public float LPFCutoff = 10000f;
    [Range(10f, 22000f)]
    public float WholeCutoff = 22000f;
    public LayerMask OcclusionLayer = 1;
    
    private bool active;

    void Awake()
    {
        SlLocation = GameObject.FindObjectOfType<StudioListener>().transform;
        Emitter = gameObject.GetComponent<StudioEventEmitter>();
    }

    void Start()
    {
        instance = Emitter.EventInstance;
        // instance.start();
        VolumeValue = 0.75f;
        WholeVolumeValue = 1f;
        LPFCutoff = 11500f;
        WholeCutoff = 22000f;
        active = false;
    }

    public void Play()
    {
        if (!active)
        {
            instance.start();
            active = true;
        }
    }

    void Update()
    {
        if (active)
        {
            RaycastHit hit;
            Physics.Linecast(transform.position, SlLocation.position, out hit, OcclusionLayer);

            if (hit.collider.name == "FirstPersonController")
            {
                // Debug.Log("not occluded");
                NotOccluded();
                Debug.DrawLine (transform.position, SlLocation.position, Color.blue);
            }
            else
            {
                // Debug.Log("occluded");
                Occluded();
                Debug.DrawLine (transform.position, hit.point, Color.red);
            }
        }

    }

    void NotOccluded()
    {
        instance.setParameterByName("Volume", WholeVolumeValue);
        instance.setParameterByName("LPF", WholeCutoff);
    }

    void Occluded()
    {
        instance.setParameterByName("Volume", VolumeValue);
        instance.setParameterByName("LPF", LPFCutoff);
    }
}
