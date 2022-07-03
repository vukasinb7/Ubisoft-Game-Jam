using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class TileCatch : MonoBehaviour
{
    public  KeyCode catchKey;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float scaleMax;
    [SerializeField] private float baseScale;
    [SerializeField] private Transform lightningSpawn;
    [SerializeField] private GameObject lightningPrefab;
    private Animator anim;
    private AudioSource _source;
    private bool spawnedLight;
    private bool canBeHurt = true;
    public Animator ezioAnim;
    public RhytmManager rhythm;
    private void Start()
    {
        anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(catchKey))
        {
            hitEffect.SetActive(true);
            hitEffect.transform.DOScale(scaleMax,0.2f);
            anim.SetBool("Shoot", true);
            if (!spawnedLight)
            {
                Instantiate(lightningPrefab, lightningSpawn.position, quaternion.identity);
                spawnedLight = true;
            }
        }
        else if(Input.GetKeyUp(catchKey))
        {
            hitEffect.transform.DOScale(baseScale, 0.2f);
            hitEffect.SetActive(false);
            anim.SetBool("Shoot", false);
            spawnedLight = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Tile") && Input.GetKey(catchKey))
        {
            canBeHurt = false;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Tile") &&canBeHurt)
        {
            anim.SetTrigger("Hurt");
            if(ezioAnim!=null)
                ezioAnim.SetTrigger("Shoot");
            rhythm.misses++;
        }

        canBeHurt = true;
    }
}
