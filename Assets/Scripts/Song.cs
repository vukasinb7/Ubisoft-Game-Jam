using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Rhythm/Song", order = 1)]
public class Song : ScriptableObject
{
    [TextArea]
    public string songData;
}
