using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    public Ball ballScript;
    public GameObject holes;
    public float width = 1.98f ,height = -3.3f, height2 = 4.5f;
    public IEnumerator SpawnObject()
    {
       while (!ballScript.isFinish)
        {Instantiate(holes, new Vector3(Random.Range(-width, width), Random.Range(height, height2), 0) , Quaternion.identity);
          yield return new WaitForSeconds(10f);
        }}
    private void Start(){StartCoroutine(SpawnObject());}}
