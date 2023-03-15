using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorWallCollider : MonoBehaviour
{
    private int rising;
    public int maxHeight;
    public int riseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rising = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (rising == 0 && gameObject.transform.position.y < maxHeight){
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnTriggerEnter(Collider other){
        rising = 0;
        Light playerLight = GameObject.FindGameObjectsWithTag("Player Light")[0].GetComponent<Light>();
        playerLight.intensity = 0;
    }
}
