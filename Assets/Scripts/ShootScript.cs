using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
using System;
using System.Linq;

public class ShootScript : MonoBehaviour
{
    public GameObject[] SmlBullets;

    public GameObject[] BigBullets;

    public Transform Origin;

    int[] milestones = { 5, 15, 50, 80, 120 };
    float[] cooldowns = { 0.5f, 0.4f, 0.3f, 0.2f, 0.1f };
    float[] pullCooldowns = { 1.25f, 1.05f, 0.75f, 0.60f, 0.45f };

    bool isPlaying0 = false;
    bool isPlaying1 = false;

    int shotCount = 0;

    float minDmg = 15;
    float cooldown = 0;
    float maxCooldown = 0.5f;
    float arrowDmg = 0;
    float arrowDmgMax = 75;
    float arrowPull = 0;
    float arrowPullMax = 1.25f;

    public VFXScript vfxScript;

    private void Awake()
    {
        shotCount = 0;

        vfxScript = gameObject.GetComponent<VFXScript>();

        vfxScript.Reset();
    }

    void Update()
    {
        Shoot();
        UpdateSpeed();
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

                arrowDmg = (int)(arrowPull * arrowDmgMax) / arrowPullMax;

                if (arrowPull >= arrowPullMax)
                {
                    vfxScript.FX3Play();

                    foreach (GameObject Bullet in BigBullets)
                    {
                        if (Bullet.GetComponent<BulletBehavior>().isMoving == false)
                        {
                            Bullet.GetComponent<BulletBehavior>().Shoot(Origin.transform, arrowDmg);

                            shotCount += 3;

                            break;
                        }
                    }

                    vfxScript.UpdatePos3(Origin);
                }
                else
                {
                    if(arrowDmg < minDmg)
                    {
                        arrowDmg = minDmg;
                    }

                    foreach (GameObject Bullet in SmlBullets)
                    {
                        if (Bullet.GetComponent<BulletBehavior>().isMoving == false)
                        {
                            Bullet.GetComponent<BulletBehavior>().Shoot(Origin.transform, arrowDmg);

                            shotCount += 1;

                            break;
                        }
                    }
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

        arrowDmg = 0;

        arrowPull = 0;
    }

    void UpdateSpeed()
    {
        int tempShots = shotCount;

        if(milestones.Contains<int>(tempShots))
        {
            for (int i = 0; i >= milestones.Count(); i++)
            {
                if(tempShots == milestones[i])
                {
                    maxCooldown = cooldowns[i];
                    arrowPullMax = pullCooldowns[i];
                }
            }
        }
    }
}
