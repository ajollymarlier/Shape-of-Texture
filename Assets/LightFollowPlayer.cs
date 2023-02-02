using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowPlayer : MonoBehaviour
{

    public GameObject target;
    public int xOffset;
    public int yOffset;
    public int zOffset;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
    }
}
