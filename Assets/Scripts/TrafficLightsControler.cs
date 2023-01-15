using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsControler : MonoBehaviour
{
    public GameObject tl_down, tl_right, tl_up, tl_left;
    RaycastHit2D traffic_down, traffic_right, traffic_up, traffic_left;

    bool horizontal_cars = false;
    bool vertical_cars = false;

    float cycleTime = 2f;
    float midLightChangeTime = 0.5f;

    void Start()
    {
        StartCoroutine("StartLightsWork");
    }


    void Update()
    {
        SetTrafficRaycasts();
        CheckVericalCars();
        CheckHoriontalCars();
    }

    IEnumerator StartLightsWork()
    {
        yield return new WaitForSeconds(1f);
        //vertical
        tl_down.GetComponent<TrafficLightBehavior>().currentLight = "red";
        tl_up.GetComponent<TrafficLightBehavior>().currentLight = "red";

        //horizontal
        tl_right.GetComponent<TrafficLightBehavior>().currentLight = "green";
        tl_left.GetComponent<TrafficLightBehavior>().currentLight = "green";

        StartCoroutine("NoTraffic");
    }

    void SetTrafficRaycasts()
    {
        //vertical raycasts
        Vector3 positionDown = FindObjectOfType<GameManager>().spawnpointDown.position;
        positionDown.y = -2.5f;
        traffic_down = Physics2D.Raycast(positionDown, Vector2.down);

        Vector3 positionUp = FindObjectOfType<GameManager>().spawnpointUp.position;
        positionUp.y = 2.5f;
        traffic_up = Physics2D.Raycast(positionUp, Vector2.up);


        //horizontal reycasts
        Vector3 positionRight = FindObjectOfType<GameManager>().spawnpointRight.position;
        positionRight.x = 2.5f;
        traffic_right = Physics2D.Raycast(positionRight, Vector2.right);

        Vector3 positionLeft = FindObjectOfType<GameManager>().spawnpointLeft.position;
        positionLeft.x = -2.5f;
        traffic_left = Physics2D.Raycast(positionLeft, Vector2.left);
    }

    void CheckVericalCars()
    {
        bool carsDown = false;
        bool carsUp = false;
        
        if(traffic_down.collider != null)
        {
            if(traffic_down.collider.gameObject.tag == "Car")
            {
                if(Mathf.Abs(traffic_down.collider.gameObject.transform.position.y - traffic_down.transform.position.y) < 5)
                {
                    carsDown = true;
                }
            }
        }
        if (traffic_up.collider != null)
        {
            if (traffic_up.collider.gameObject.tag == "Car")
            {
                if (Mathf.Abs(traffic_up.collider.gameObject.transform.position.y - traffic_up.transform.position.y) < 5)
                {
                    carsUp = true;
                }
            }
        }

        if(carsDown || carsUp)
        {
            vertical_cars = true;
            Debug.Log("S¹ auta vertykalnie");
        }
        else
        {
            vertical_cars = false;
        }
    }

    void CheckHoriontalCars()
    {
        bool carsRight = false;
        bool carsLeft = false;

        if (traffic_right.collider != null)
        {
            if (traffic_right.collider.gameObject.tag == "Car")
            {
                if (Mathf.Abs(traffic_right.collider.gameObject.transform.position.y - traffic_right.transform.position.y) < 10)
                {
                    carsRight = true;
                }
            }
        }
        if (traffic_left.collider != null)
        {
            if (traffic_left.collider.gameObject.tag == "Car")
            {
                if (Mathf.Abs(traffic_left.collider.gameObject.transform.position.y - traffic_left.transform.position.y) < 10)
                {
                    carsLeft = true;
                }
            }
        }

        if (carsRight || carsLeft)
        {
            horizontal_cars = true;
            Debug.Log("S¹ auta horyztontalnie");
        }
        else
        {
            horizontal_cars = false;
        }
    }

    IEnumerator NoTraffic()
    {
        while(!vertical_cars && !horizontal_cars)
        {
            yield return new WaitForSeconds(cycleTime);

            //vertical
            tl_down.GetComponent<TrafficLightBehavior>().currentLight = "red";
            tl_up.GetComponent<TrafficLightBehavior>().currentLight = "red";

            //horizontal
            tl_right.GetComponent<TrafficLightBehavior>().currentLight = "green";
            tl_left.GetComponent<TrafficLightBehavior>().currentLight = "green";
        }
        if(horizontal_cars)
        {
            StartCoroutine("HorizontalDrive");
        }
        else if(vertical_cars)
        {
            tl_down.GetComponent<TrafficLightBehavior>().NextLight();
            tl_up.GetComponent<TrafficLightBehavior>().NextLight();
            tl_right.GetComponent<TrafficLightBehavior>().NextLight();
            tl_left.GetComponent<TrafficLightBehavior>().NextLight();
            yield return new WaitForSeconds(midLightChangeTime);
            tl_right.GetComponent<TrafficLightBehavior>().currentLight = "red";
            tl_left.GetComponent<TrafficLightBehavior>().currentLight = "red";
            yield return new WaitForSeconds(midLightChangeTime);
            StartCoroutine("VerticalDrive");
        }
        else
        {
            StartCoroutine("NoTraffic");
        }
    }

    IEnumerator HorizontalDrive()
    {
        //vertical
        tl_down.GetComponent<TrafficLightBehavior>().currentLight = "red";
        tl_up.GetComponent<TrafficLightBehavior>().currentLight = "red";

        //horizontal
        tl_right.GetComponent<TrafficLightBehavior>().currentLight = "green";
        tl_left.GetComponent<TrafficLightBehavior>().currentLight = "green";
        
        yield return new WaitForSeconds(cycleTime);
        
        if(horizontal_cars)
        {
            yield return new WaitForSeconds(cycleTime);
        }

        while(!vertical_cars)
        {
            yield return new WaitForSeconds(cycleTime);
        }

        if(vertical_cars)
        {
            tl_down.GetComponent<TrafficLightBehavior>().NextLight();
            tl_up.GetComponent<TrafficLightBehavior>().NextLight();
            tl_right.GetComponent<TrafficLightBehavior>().NextLight();
            tl_left.GetComponent<TrafficLightBehavior>().NextLight();
            yield return new WaitForSeconds(midLightChangeTime);
            tl_right.GetComponent<TrafficLightBehavior>().currentLight = "red";
            tl_left.GetComponent<TrafficLightBehavior>().currentLight = "red";
            yield return new WaitForSeconds(midLightChangeTime);
            StartCoroutine("VerticalDrive");
        }
    }

    IEnumerator VerticalDrive()
    {
        //vertical
        tl_down.GetComponent<TrafficLightBehavior>().currentLight = "green";
        tl_up.GetComponent<TrafficLightBehavior>().currentLight = "green";

        //horizontal
        tl_right.GetComponent<TrafficLightBehavior>().currentLight = "red";
        tl_left.GetComponent<TrafficLightBehavior>().currentLight = "red";
        yield return new WaitForSeconds(cycleTime);

        if(!horizontal_cars)
        {
            yield return new WaitForSeconds(cycleTime);
        }

        tl_down.GetComponent<TrafficLightBehavior>().NextLight();
        tl_up.GetComponent<TrafficLightBehavior>().NextLight();
        tl_right.GetComponent<TrafficLightBehavior>().NextLight();
        tl_left.GetComponent<TrafficLightBehavior>().NextLight();
        yield return new WaitForSeconds(midLightChangeTime);
        tl_down.GetComponent<TrafficLightBehavior>().currentLight = "red";
        tl_up.GetComponent<TrafficLightBehavior>().currentLight = "red";
        yield return new WaitForSeconds(midLightChangeTime);
        StartCoroutine("HorizontalDrive");
    }

}
