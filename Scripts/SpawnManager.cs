using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject obstaclePrefab;
	public float startDelay = 2;
	// public float repeatRate = 2;
	public Vector3 spawnPos = new Vector3(15, 0.25f, 0);
	
	private PlayerController playerControllerScript;
	private bool isSpawn= false;
    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = 
        	GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
    	if(!isSpawn)
    	{
    		SpawnObstacle();
    		isSpawn = true;
    		StartCoroutine("SpawnDelay");
    	}
    }

    // Update is called once per frame
    void SpawnObstacle()
    {
    	if(playerControllerScript.gameOver == false)
    	{
    		Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    	}
    }

    IEnumerator SpawnDelay()
    {
    	float spawnInterval = Random.Range(0.75f,1);
    	yield return new WaitForSeconds(spawnInterval);
    	isSpawn = false;
    }
}
