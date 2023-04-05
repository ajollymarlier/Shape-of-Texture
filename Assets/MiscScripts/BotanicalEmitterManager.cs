using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanicalEmitterManager : MonoBehaviour
{
    private bool started;
    private bool triggered;

    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;
    public GameObject lock5;
    public GameObject lock6;
    public GameObject lock7;
    public GameObject lock8;
    public GameObject lock9;

    public GameObject battery1;
    public GameObject battery2;
    public GameObject battery3;
    public GameObject battery4;
    public GameObject battery5;
    public GameObject battery6;
    public GameObject battery7;

    private GameObject[] firstLocks;
    private GameObject[] secondLocks;

    private GameObject[] firstBatteries;
    private GameObject[] secondBatteries;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        started = false;
        firstLocks = new GameObject[] {lock1, lock2, lock3};
        secondLocks = new GameObject[] {lock4, lock5, lock6, lock7, lock8, lock9};

        firstBatteries = new GameObject[] {battery1, battery2, battery3};
        secondBatteries = new GameObject[] {battery4, battery5, battery6, battery7};
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
