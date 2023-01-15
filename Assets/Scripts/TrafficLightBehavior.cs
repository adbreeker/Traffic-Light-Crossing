using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightBehavior : MonoBehaviour
{
    public Sprite light_no, light_red, light_yellow, light_green, light_orange;
    public string currentLight = "no";
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = this.GetType().GetField("light_" + currentLight).GetValue(this) as Sprite;
    }

    public void NextLight()
    {
        switch(currentLight)
        {
            case "red":
                currentLight = "orange";
                break;
            case "orange":
                currentLight = "green";
                break;
            case "green":
                currentLight = "yellow";
                break;
            case "yellow":
                currentLight = "red";
                break;
        }
    }
}
