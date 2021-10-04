using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    private Vector3 originalSize;

    private EnemyScript parent;

    public string targetTag { get; private set; }

    private bool isGrowing = false;

    private float damage = 20;

    public float speed = 15;

    public float maxSize = 10;

    void Awake()
    {
        originalSize = gameObject.transform.localScale;
    }

    void Update()
    {
        Grow();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == targetTag)
        {
            other.gameObject.GetComponent<PlayerHealth>().affectHealth(-damage);
        }
    }

    public void Ignition(string tag, EnemyScript parent)
    {
        targetTag = tag;
        isGrowing = true;
        this.parent = parent;
    }

    private void Grow()
    {
        if(isGrowing)
        {
            if (gameObject.transform.localScale.x < maxSize)
                gameObject.transform.localScale += gameObject.transform.localScale * speed * Time.deltaTime;
            else
                Reset();
        }
    }

    private void Reset()
    {
        parent.Death();
        isGrowing = false;
        gameObject.transform.localScale = originalSize;
    }
}
