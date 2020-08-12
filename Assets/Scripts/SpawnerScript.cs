using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] SpawnObjects;
    public float SpawnTime = 2.0f;

    private GameObject SpawnObject;

    // Use this for initialization
    void Start()
    {
        SpawnObject = SpawnObjects[Random.Range(0, SpawnObjects.Length)];
        Spawn();
    }

    void Spawn()
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            this.SpawnTime = GameLauncher.SpawnerScript_SpawnTime;
            //random y position
            float y = Random.Range(-0.5f, 1f);
            Instantiate(SpawnObject, this.transform.position + new Vector3(0, y, 0), Quaternion.identity);
        }
        Invoke("Spawn", SpawnTime + Random.Range(-0.5f, 1));
    }

    
    
}
