using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2d.velocity = new Vector2(speed*Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Rabbits")
        {
            var anims = col.GetComponentsInChildren<Animator>();
            Debug.Log(anims.Length);
            for(int i = 1;i<anims.Length;i++)
            {
                anims[i].SetTrigger("Hurt");
            }
            anim.SetTrigger("Burst");
            Destroy(gameObject,0.5f);
        }
    }
}
