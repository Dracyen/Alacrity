using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Rotator rotator;
    public ExplosionControl explosion;
    public float minDistance = 0;
    public float timerTotal = 2;
    public GameController parent;
    public Slider healthBar;

    public float Health { get; private set; }
    private bool isExploding = false;
    private PlayerHealth player;
    private float timer;

    public Transform Home;

    public bool onChase { get; private set; }

private void Awake()
    {
        Health = 50;

        timer = timerTotal;

        Death();

        player = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if(isExploding)
        {
            BlowUp();
        }
        else
        {
            if(onChase)
            {
                if (Vector3.Distance(gameObject.transform.position, player.transform.position) > minDistance)
                {
                    Follow();
                }
                else
                {
                    navMeshAgent.isStopped = true;
                    navMeshAgent.ResetPath();
                    isExploding = true;
                }
            }
        }
    }

    public void affectHealth(float Quantity)
    {
        Health += Quantity;

        healthBar.value = Health;

        if (Health < 0)
        {
            Death();
        }
    }

    void Follow()
    {
        if(Vector3.Distance(navMeshAgent.destination, player.transform.position) > 2)
        {
            navMeshAgent.destination = player.transform.position;
        }
    }

    void BlowUp()
    {
        rotator.SpeedUp();

        if (timer <= 0)
        {
            explosion.Ignition("Player", this);
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    public void Death()
    {
        Health = 50;
        navMeshAgent.enabled = false;
        transform.position = Home.position;
        onChase = false;
        isExploding = false;
        timer = timerTotal;
        rotator.Reset();
        parent.UpdateEnemies();
    }

    public void UpdatePos(Vector3 position)
    {
        healthBar.value = Health;

        navMeshAgent.enabled = true;
        transform.position = position;
        onChase = true;
    }
}
