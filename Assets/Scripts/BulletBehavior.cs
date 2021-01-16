using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody RB;

    public bool isMoving = false;

    public float Damage;

    public Transform Home;

    public float Speed;

    public float Range;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if(RB.useGravity == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if(Vector3.Distance(transform.position, Home.transform.position) > Range)
        {
            Reset();
        }
    }

    public void Shoot(Transform Origin, float Damage)
    {
        this.Damage = Damage;

        var angle = Origin.transform.forward;

        RB.transform.position = Origin.position;

        Vector3 move = angle * Speed;

        RB.useGravity = true;
        RB.AddForce(move);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(other.gameObject.GetComponent<TargetScript>() != null)
            {
                other.gameObject.GetComponent<TargetScript>().Yell();
            }
            else
            {
                other.gameObject.GetComponent<EnemyScript>().affectHealth(-Damage);
            }
        }

        Reset();
    }

    private void Reset()
    {
        RB.useGravity = false;
        RB.velocity = new Vector3();
        RB.transform.position = Home.position;
    }
}
