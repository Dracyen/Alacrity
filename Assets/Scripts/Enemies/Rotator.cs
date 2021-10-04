using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    bool gonnaBlow = false;

    public float rotSpeed = 10;

    public float multiplier = 3;

    void Update()
    {
        if(gonnaBlow)
        {
            transform.Rotate(new Vector3(0, multiplier * rotSpeed * Time.deltaTime, 0), Space.World);
        }
        else
        {
            transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0), Space.World);
        }
    }

    public void SpeedUp()
    {
        gonnaBlow = true;
    }

    public void Reset()
    {
        gonnaBlow = false;
    }
}
