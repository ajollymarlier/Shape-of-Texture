using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFloorInit : MonoBehaviour
{
    public Material material;

    private BoxCollider collider;
    private GameObject leftFloor;
    private GameObject rightFloor;
    
    // Start is called before the first frame update
    void Start()
    {
        leftFloor = transform.GetChild(0).gameObject;
        rightFloor = transform.GetChild(1).gameObject;
        
        leftFloor.GetComponent<Renderer>().material = material;
        rightFloor.GetComponent<Renderer>().material = material;

        collider = GetComponent<BoxCollider>();
        collider.size = leftFloor.transform.localScale + new Vector3(rightFloor.transform.localScale.x, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("YO!");
    }
}
