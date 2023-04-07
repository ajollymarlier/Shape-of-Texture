using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MovementOverrideLogic : MonoBehaviour
{
    private bool triggered;
    private bool teleported;
    public FirstPersonController player;
    public GameObject staticScreen;
    public Light flashlight;
    public GameObject teleportMarker;
    public GameObject structure;
    public float moveSpeed;
    public GameObject tvWithEmitter;

    private BotanicalWingInit[] inits;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        teleported = false;
        inits = GameObject.FindObjectsOfType<BotanicalWingInit>();
    }


    void Update(){
        StartCoroutine(handleForcedMovement());
    }

    IEnumerator handleForcedMovement(){
        if (triggered)
        {
            flashlight.GetComponent<FlashlightTimeout>().losingBattery = false;
            flashlight.intensity = flashlight.GetComponent<FlashlightTimeout>().flashingIntensity;
            tvWithEmitter.GetComponent<StudioEventEmitter>().enabled = true;
        }
        if (triggered && !teleported){
            if (player.transform.position.z < staticScreen.transform.position.z){
                player.transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
            }
            else if (player.transform.position.x < staticScreen.transform.position.x - 1){
                player.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else{
                yield return new WaitForSeconds(4);
                player.transform.position = teleportMarker.transform.position;
                structure.SetActive(false);
                staticScreen.GetComponent<MeshRenderer>().enabled = false;
                teleported = true;
            }
        }
        else if (triggered && teleported){
            if (GameObject.Find("Final Sequence Tele Location").transform.position.x - player.transform.position.x < 10){
                player.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else{
                yield return new WaitForSeconds(3);
                PauseMenu.GamePaused = true;
                player.playerCanMove = true;
                Cursor.lockState = CursorLockMode.None;
                foreach (BotanicalWingInit init in inits)
                    init.Stop();
                tvWithEmitter.GetComponent<StudioEventEmitter>().Stop();
                SceneManager.LoadScene("MainMenu_sketch");
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
