using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedDoorInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject leftDoor = transform.Find("Door/Left Door").gameObject;
        leftDoor.SetActive(false);
    }
}
