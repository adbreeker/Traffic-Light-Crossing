using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform spawnpointDown, spawnpointRight, spawnpointUp, spawnpointLeft;
    public int carsDown = 0, carsRight = 0, carsUp = 0, carsLeft = 0;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    //buttons
    public void spawnDownButton()
    {
        for (int i = 0; i <= 20; i++)
        {
            GameObject car = Instantiate(carPrefab, Vector3Extension.Translate(spawnpointDown.position, 0, carsDown * -3.5f, 0), spawnpointDown.rotation);
            car.GetComponent<CarBehavior>().carFrom = "Down";
            carsDown++;
        }
    }

    public void spawnRightButton()
    {
        for (int i = 0; i <= 20; i++)
        {
            GameObject car = Instantiate(carPrefab, Vector3Extension.Translate(spawnpointRight.position, carsRight * 3.5f, 0, 0), spawnpointRight.rotation);
            car.GetComponent<CarBehavior>().carFrom = "Right";
            carsRight++;
        }
    }

    public void spawnUpButton()
    {
        for (int i = 0; i <= 20; i++)
        {
            GameObject car = Instantiate(carPrefab, Vector3Extension.Translate(spawnpointUp.position, 0, carsUp * 3.5f, 0), spawnpointUp.rotation);
            car.GetComponent<CarBehavior>().carFrom = "Up";
            carsUp++;
        } 
    }

    public void spawnLeftButton()
    {
        for(int i = 0; i<=20; i++)
        {
            GameObject car = Instantiate(carPrefab, Vector3Extension.Translate(spawnpointLeft.position, carsLeft * -3.5f, 0, 0), spawnpointLeft.rotation);
            car.GetComponent<CarBehavior>().carFrom = "Left";
            carsLeft++;
        }
    }
}
