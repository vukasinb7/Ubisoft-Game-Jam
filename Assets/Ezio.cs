using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ezio : MonoBehaviour
{
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponentsInChildren<Animator>()[1];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Light")
        {
            anim.SetTrigger("Hurt");
        }
    }
}
