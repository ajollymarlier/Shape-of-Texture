using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOverrideLogic : MonoBehaviour
{
    private bool triggered;
    public FirstPersonController player;
    public GameObject staticScreen;
    public Light flashlight;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }


    void Update(){
        if (triggered){
            flashlight.intensity += 0.01f;
            //Vector3.MoveTowards(player.transform.position, staticScreen.transform.position, moveSpeed * Time.deltaTime);
            if (player.transform.position.z < staticScreen.transform.position.z){
                player.transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
            }
            else if (player.transform.position.x < staticScreen.transform.position.x - 1){
                player.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else{
                flashlight.GetComponent<FlashlightTimeout>().losingBattery = false;
            }
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        triggered = true;
        player.playerCanMove = false;
        player.enableHeadBob = false;
    }
}
