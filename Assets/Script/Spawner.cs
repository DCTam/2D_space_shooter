using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float spawnCoolDown;
	public GameObject EnemyAI;
	private float timer;
	public GameObject[] Points;
	// Use this for initialization
	void Start () 
	{
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if(timer >= spawnCoolDown){
			GameObject temp = Instantiate(EnemyAI, transform.position, transform.rotation) as GameObject;
			temp.GetComponent<AIFighter>().primaryPoint = Points[(int)Random.Range(0,Points.Length-1)];
			temp.GetComponent<AIFighter>().point = Points[(int)Random.Range(0,Points.Length-1)];
			timer = 0;
		}
	
	
	}
}
