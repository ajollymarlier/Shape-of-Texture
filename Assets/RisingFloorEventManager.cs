using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFloorEventManager : MonoBehaviour
{
    private RisingFloorInit currentFloor;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                currentFloor.Raise();
            }
        }
    }

    public void ChangeFloor(RisingFloorInit floor)
    {
        currentFloor = floor;
        timeRemaining = 10;
        timerIsRunning = true;
    }
}
