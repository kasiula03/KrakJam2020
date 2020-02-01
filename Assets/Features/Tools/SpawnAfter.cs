using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfter : MonoBehaviour
{
    public GameObject prefab;
    public float afterSeconds = 1f;

    IEnumerator Start()
    {
        if (!prefab) yield break;
        
        yield return new WaitForSeconds(afterSeconds);
        GameObject.Instantiate(prefab, this.transform);
    }
    
}
