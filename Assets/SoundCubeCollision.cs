using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCubeCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("CUBE!");
    }
}
