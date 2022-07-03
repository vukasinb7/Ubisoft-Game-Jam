using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class RhytmManager : MonoBehaviour
{
    public Song currentSong;
    private Stopwatch _stopwatch;
    private Queue<String> lines;
    public Transform[] spawnPoints;
    public GameObject notePrefab;
    private AudioSource _source;
    [FormerlySerializedAs("fallSpeed")] [SerializeField] private float fallTime;
    [SerializeField] private float yEnd;
    [SerializeField] private GameObject sliderPrefab;
    [SerializeField] private GameObject[] fireballPrefabs;
    public int misses = 0;
    public TMPro.TextMeshProUGUI accText;
    private float accuracy;
    private int numOfTiles;

    private float speed;
    private bool checkForWin = false;
    
    private void Start()
    {
        StartCoroutine(startWait());
    }

    IEnumerator startWait()
    {
        yield return new WaitForSeconds(3f);
        
        _source = GetComponent<AudioSource>();
        _source.Play();
        checkForWin = true;
        lines = new Queue<string>(currentSong.songData.Split('\n'));
        speed = (spawnPoints[0].transform.position.y - yEnd) / fallTime;
        numOfTiles = lines.Count;
        
        while (lines.Count>0)
        {
            if (lines.Peek() != String.Empty)
            {
                String[] currentLine = lines.Dequeue().Split(',');
                int lane = xToLane(int.Parse(currentLine[0]));
                Vector3 _spawnPos = new Vector3(spawnPoints[lane].position.x,
                    spawnPoints[lane].position.y - speed * (fallTime - int.Parse(currentLine[2])), 0);

                if (int.Parse(currentLine[3]) == 1)
                {
                    GameObject tile = Instantiate(fireballPrefabs[lane], _spawnPos, quaternion.identity);
                    tile.transform.DOMoveY(yEnd, int.Parse(currentLine[2]) / 1000.0f).SetEase(Ease.Linear).OnComplete(()=>tile.transform.DOMoveY(yEnd-10,4));
                }
                else if (int.Parse(currentLine[3]) == 128)
                {
                    Vector3 _spawnPosEnd = _spawnPos;
                    _spawnPosEnd.y = spawnPoints[lane].position.y - speed * (fallTime - int.Parse(currentLine[5].Split(':')[0]));
                    GameObject slider = Instantiate(sliderPrefab, _spawnPosEnd, quaternion.identity);
                    Transform[] children = slider.GetComponentsInChildren<Transform>();
                    children[2].position = _spawnPos;
                    children[2].GetComponent<SpriteRenderer>().color = Color.red;
                    var lr = children[1].GetComponent<LineRenderer>();
                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, new Vector3(0,-_spawnPosEnd.y+_spawnPos.y,0));
                    slider.transform.DOMoveY(yEnd, int.Parse(currentLine[5].Split(':')[0]) / 1000.0f).SetEase(Ease.Linear).OnComplete(()=>slider.transform.DOMoveY(yEnd-10,3));
                }
            }
        }
    }

    private void Update()
    {
        if (numOfTiles != 0)
        {
            accuracy = (numOfTiles - misses) / (1.0f * numOfTiles) * 100;
            accText.SetText(accuracy.ToString("0.00") + "%");
            
            if (accuracy < 85)
            {
                SceneManager.LoadScene("LostScene");
            }
        }

        if (checkForWin && !_source.isPlaying)
        {
            SceneManager.LoadScene("WonScene");
        }

        
    }

    private int xToLane(int x)
    {
        return (x == 64) ? 0 : ((x == 192) ? 1 : ((x == 320) ? 2 : ((x == 448) ? 3 : -1)));
    }
}
