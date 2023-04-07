using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanicalEmitterManager : MonoBehaviour
{
    private bool started;
    private bool triggered;

    public GameObject[] firstLocks;
    public GameObject[] secondLocks;

    public GameObject[] firstBatteries;
    public GameObject[] secondBatteries;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            started = true;

            foreach (GameObject lck in firstLocks)
                lck.GetComponent<BotanicalWingInit>().Play();

            foreach (GameObject battery in firstBatteries)
                battery.GetComponent<Occlusion>().Play();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Second half active");
        if (!triggered)
        {
            triggered = true;

            foreach (GameObject lck in secondLocks)
                lck.GetComponent<BotanicalWingInit>().Play();

            foreach (GameObject battery in secondBatteries)
                battery.GetComponent<Occlusion>().Play();
        }
    }
}
