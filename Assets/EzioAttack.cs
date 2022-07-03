using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EzioAttack : MonoBehaviour
{
    public Transform shurikenSpawn;
    public GameObject shurikenPrefab;
    
    public void LaunchShuriken()
    {
        Instantiate(shurikenPrefab, shurikenSpawn.position, quaternion.identity);
    }
}
