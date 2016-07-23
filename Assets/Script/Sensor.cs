using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sensor : Basic 
{
	//list off all shuips in trigger area
	public List<GameObject> ships;

	// Use this for initialization
	void Start () 
	{
		super ();
		team = transform.parent.GetComponent<Basic>().team;
		ships = new List<GameObject> ();
	}
	
	//When ships with type Fighter in list Die they may become null so remove them from list
	void Update () 
	{
		for (int i = 0; i < ships.Count; i++) 
		{
			if(ships[i] == null) 
			{
				ships.RemoveAt(i);
				i--;
			}
		}
	}
	public void clean()
	{
		Update ();
	}


	//When ships if type fighter enter triggre add them from list 
	void OnTriggerEnter2D(Collider2D other) 
	{
		Basic basic = other.gameObject.GetComponent<Basic> ();
		if (basic != null) 
		{
			if((basic.isType(Types.Fighter) || basic.isType(Types.TopLevelNoCol))&& basic.team != team)
			{
				ships.Add(other.gameObject);
			}
		}

	}

	//When ships if type fighter leave remove them from list 
	void OnTriggerExit2D(Collider2D other) 
	{
		Basic basic = other.gameObject.GetComponent<Basic> ();
		if (basic != null) 
		{
			if(basic.isType(Types.Fighter) && basic.team != team)
			{
				ships.Remove(other.gameObject);
			}
		}
	}
}
