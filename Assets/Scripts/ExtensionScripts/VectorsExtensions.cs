using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extension
{

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public static Vector2 Translate(Vector2 v, float x, float y)
    {
        Vector2 translated = new Vector2(v.x + x, v.y + y);
        return translated;
    }

    public static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }

}

public class Vector3Extension
{
    public static Vector3 Translate(Vector3 v, float x, float y, float z)
    {
        Vector3 translated = new Vector3(v.x + x, v.y + y, v.z + z);
        return translated;
    }
}

