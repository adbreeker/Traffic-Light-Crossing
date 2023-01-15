using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    public Sprite car1, car2, car3, car4;
    public string carFrom;
    public float speed;

    RaycastHit2D carsVision, lightsVision;
    Vector2 front = Vector2.up;
    Rigidbody2D rb;

    bool carsAllowToDrive = true;
    bool lightsAllowToDrive = true;


    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = this.GetType().GetField("car" + Random.Range(1, 5)).GetValue(this) as Sprite;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetRaycasts();
        GetObjectsInVision();
    }

    void FixedUpdate()
    {
        front = Vector2Extension.Rotate(Vector2.up, Vector2Extension.UnwrapAngle(transform.localEulerAngles.z));
        if (carsAllowToDrive && lightsAllowToDrive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.MovePosition(rb.position + front * speed);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void SetRaycasts()
    {
        carsVision = Physics2D.Raycast(transform.position, front);
        lightsVision = Physics2D.Raycast(transform.position, Vector2Extension.Rotate(front, -90));
    }

    void GetObjectsInVision()
    {
        GameObject spottedCarProbably, spottedTrafficLightProbably;
        if (carsVision.collider != null)
        {
            spottedCarProbably = carsVision.collider.gameObject;
            if (spottedCarProbably.tag == "Car")
            {
                ManageCarInVision(spottedCarProbably);
            }
        }
        if (lightsVision.collider != null)
        {
            spottedTrafficLightProbably = lightsVision.collider.gameObject;
            if (spottedTrafficLightProbably.tag == "TrafficLight")
            {
                ManageTrafficLightInVistion(spottedTrafficLightProbably);
            }
        }


    }

    void ManageCarInVision(GameObject car)
    {
        if(car.transform.rotation == transform.rotation)
        {
            float rot = Vector2Extension.UnwrapAngle(transform.localEulerAngles.z);
            if (rot == 90f || rot == 270f || rot == -90f)
            {

                if (Mathf.Abs(car.transform.position.x - transform.position.x) < 3)
                {
                    carsAllowToDrive = false;
                }
                else
                {
                    carsAllowToDrive = true;
                }
            }
            if(rot == 180f || rot == 0 || rot == -180f)
            {
                if (Mathf.Abs(car.transform.position.y - transform.position.y) < 3)
                {
                    carsAllowToDrive = false;
                }
                else
                {
                    carsAllowToDrive = true;
                }
            }
        }
    }

    void ManageTrafficLightInVistion(GameObject trafficLight)
    {
        if(Mathf.Abs(Vector2Extension.UnwrapAngle(transform.localEulerAngles.z) - Vector2Extension.UnwrapAngle(trafficLight.transform.localEulerAngles.z)) < 10f)
        {
            if(trafficLight.GetComponent<TrafficLightBehavior>().currentLight == "red" || trafficLight.GetComponent<TrafficLightBehavior>().currentLight == "orange")
            {
                lightsAllowToDrive = false;
            }
            if(trafficLight.GetComponent<TrafficLightBehavior>().currentLight == "green")
            {
                lightsAllowToDrive = true;
            }
        }
    }

    private void OnBecameInvisible()
    {
        if(carFrom == "Down")
        {
            FindObjectOfType<GameManager>().carsDown--;
        }
        if (carFrom == "Right")
        {
            FindObjectOfType<GameManager>().carsRight--;
        }
        if (carFrom == "Up")
        {
            FindObjectOfType<GameManager>().carsUp--;
        }
        if (carFrom == "Left")
        {
            FindObjectOfType<GameManager>().carsLeft--;
        }
        Destroy(gameObject);

    }
}
