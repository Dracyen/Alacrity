using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public TextMesh textMesh;

    public string[] bits = { "Ouch!", "No!", "Stop it!", "Ahh!", "How dare you?!", "U Cun..!", "Dick Move!"};

    public void Yell()
    {
        textMesh.text = bits[Random.Range(0, bits.Length)];
    }
}
