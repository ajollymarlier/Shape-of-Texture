using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementOverrideLogic : MonoBehaviour
{
    private bool triggered;
    private bool teleported;
    public FirstPersonController player;
    public GameObject staticScreen;
    public Light flashlight;
    public GameObject teleportMarker;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        teleported = false;
    }


    void Update(){
        StartCoroutine(handleForcedMovement());
    }

    IEnumerator handleForcedMovement(){
        if (triggered && !teleported){
            if (player.transform.position.z < staticScreen.transform.position.z){
                player.transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
            }
            else if (player.transform.position.x < staticScreen.transform.position.x - 1){
                player.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else{
                flashlight.GetComponent<FlashlightTimeout>().losingBattery = false;

                 yield return new WaitForSeconds(4);
                player.transform.position = teleportMarker.transform.position;
                teleported = true;
            }
        }
        else if (triggered && teleported){
            if (GameObject.Find("Final Sequence Tele Location").transform.position.x - player.transform.position.x < 10){
                player.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else{
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Main_Menu");
            }
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        triggered = true;
        player.playerCanMove = false;
        player.isWalking = false;
        player.enableHeadBob = false;
    }
}
