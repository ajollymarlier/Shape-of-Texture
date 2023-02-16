using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFloorInit : MonoBehaviour
{
    public Material material;

    private BoxCollider boxCollider;
    private GameObject leftFloor;
    private GameObject rightFloor;
    private RisingFloorEventManager EventManager;

    private Rigidbody _rigidbody;
    private Transform _transform;

    public bool raising;
    public GameObject toRaise;
    
    // Start is called before the first frame update
    void Start()
    {
        leftFloor = transform.GetChild(1).gameObject;
        rightFloor = transform.GetChild(0).gameObject;
        toRaise = transform.GetChild(Random.Range(0,2)).gameObject;
        
        leftFloor.GetComponent<Renderer>().material = material;
        rightFloor.GetComponent<Renderer>().material = material;

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = leftFloor.transform.localScale + new Vector3(rightFloor.transform.localScale.x, 0, 0);

        EventManager = FindObjectOfType<RisingFloorEventManager>();

        _rigidbody = leftFloor.GetComponent<Rigidbody>();
        _transform = leftFloor.GetComponent<Transform>();
        raising = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (raising){
            // print(_transform.position.y);
            _transform.position += new Vector3(0, 0.01f , 0);   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("YO!");
        EventManager.ChangeFloor(gameObject.GetComponent<RisingFloorInit>());
    }

    public void Raise()
    {
        raising = true;
    }
}
