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
    [Header("UI Elements")]

    public Text statsDisplay;
    
    [Space(10)]

    public GameObject[] SmlBullets;

    public GameObject[] BigBullets;

    public Transform Origin;

    int[] milestones = { 5, 10, 25, 45, 70 };
    float[] cooldowns = { 0.5f, 0.4f, 0.3f, 0.2f, 0.1f };
    float[] pullCooldowns = { 1.25f, 1.05f, 0.75f, 0.60f, 0.45f };
    float[] critChances = { 0.1f, 0.25f, 0.45f, 0.80f, 1f };

    bool isPlaying0 = false;
    bool isPlaying1 = false;

    int shotCount = 0;

    float minDmg = 15;
    float maxDmg = 75;
    float arrowDmg = 0;
    float critChance = 0;
    float cooldown = 0;
    float maxCooldown = 0.5f;
    float arrowPull = 0;
    float arrowPullMax = 1.25f;

    public VFXScript vfxScript;

    private void Awake()
    {
        UpdateSpeed();

        shotCount = 0;

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

                bool crit = false;

                arrowDmg = (int)(arrowPull * maxDmg) / arrowPullMax;

                if (UnityEngine.Random.Range(0f, 1f) < critChance)
                {
                    crit = true;
                    Debug.Log("CRIT!!");
                }

                if (arrowPull >= arrowPullMax)
                {
                    vfxScript.FX3Play();

                    foreach (GameObject Bullet in BigBullets)
                    {
                        if (Bullet.GetComponent<BulletBehavior>().isMoving == false)
                        {
                            if (crit)
                                arrowDmg = arrowDmg * 2;

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
                            if (crit)
                                arrowDmg = arrowDmg * 2;

                            Bullet.GetComponent<BulletBehavior>().Shoot(Origin.transform, arrowDmg);

                            shotCount += 1;

                            break;
                        }
                    }
                }
                Reset();

                UpdateSpeed();
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

        for (int i = 0; i < milestones.Length; i++)
        {
            if (tempShots == milestones[i])
            {
                maxCooldown = cooldowns[i];
                arrowPullMax = pullCooldowns[i];
                critChance = critChances[i];
            }
        }

        statsDisplay.text = "Number of Shots:\t" + shotCount + "\n\nSimple Shot Speed:\t" + maxCooldown + "\n\nSuper Shot Speed:\t" + arrowPullMax + "\n\nCritical Strike Chance:\t" + critChance;
    }
}
