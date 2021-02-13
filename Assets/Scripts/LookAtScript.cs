using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    public Transform Target;

    private void Awake()
    {
        Target = FindObjectOfType<Camera>().transform;
    }

    void Update()
    {
        transform.LookAt(Target.position);
    }
}
