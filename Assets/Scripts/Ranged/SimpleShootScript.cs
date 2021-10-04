using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
using System;

public class SimpleShootScript : MonoBehaviour
{
    public Transform Origin;

    bool isPlaying0 = false;
    bool isPlaying1 = false;

    float cooldown = 0;
    float maxCooldown = 0.5f;
    float arrowPull = 0;
    float arrowPullMax = 1.25f;

    public VFXScript vfxScript;

    private void Awake()
    {
        vfxScript = gameObject.GetComponent<VFXScript>();

        vfxScript.Reset();
    }

    void Update()
    {
        Shoot();
    }

    private void LateUpdate()
    {
        vfxScript.UpdatePos(Origin); 
    }

    void Shoot()
    {
        if(cooldown <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                vfxScript.Reset();
            }

            if (arrowPull < arrowPullMax)
            {
                if (Input.GetMouseButton(0))
                {
                    arrowPull += Time.deltaTime;

                    if (arrowPull > 0.1f)
                    {
                        if (isPlaying0 == false)
                        {
                            vfxScript.FX1Play();
                            isPlaying0 = true;
                        }
                    }
                }
            }
            else
            {
                vfxScript.FX1Stop();

                if (isPlaying1 == false)
                {
                    vfxScript.FX2Play();
                    isPlaying1 = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                vfxScript.FX1Stop();
                vfxScript.FX2Stop();

                isPlaying0 = false;
                isPlaying1 = false;

                if (arrowPull >= arrowPullMax)
                {
                    vfxScript.FX3Play();

                    vfxScript.UpdatePos3(Origin);
                }

                Reset();
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    void Reset()
    {
        cooldown = maxCooldown;

        arrowPull = 0;
    }
}
