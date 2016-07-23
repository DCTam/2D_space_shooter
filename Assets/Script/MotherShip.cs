using UnityEngine;
using System.Collections;

public class MotherShip : Destructible 
{
	public GameObject Turret;
	public GameObject []Turrets;
	public GameObject explosion;
	public GameObject point;
	public float TurnRate;
	public float speed;
	public GameObject dyingSound;

	// Use this for initialization
	void Start () 
	{
		super ();
		addType (Types.BottomLevel);
		for(int i = 0; i < Turrets.Length; i++)
		{
			Turrets[i] = Instantiate(Turret, Turrets[i].transform.position, Quaternion.identity) as GameObject;
			Turrets[i].transform.rotation = gameObject.transform.rotation;
			Turrets[i].transform.parent = gameObject.transform.GetChild(2);
			Turrets[i].transform.GetChild(0).GetComponent<Basic>().team = team;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)
			endLife ();

		for(int i = 0; i < Turrets.Length; i++)
		{
			if(Turrets[i] != null && Turrets[i].transform.GetChild(0) != null)
			{
				if(gameObject.transform.GetChild(3).GetComponent<Sensor>().ships.Count >= 1)
				{
					Turret turret = Turrets[i].transform.GetChild(0).GetComponent<Turret>();
					if(turret != null)
					{
						Turrets[i].transform.GetChild(0).GetComponent<Turret>().Target = gameObject.transform.GetChild(3).GetComponent<Sensor>().ships[0];
					}
				}
			}
		}

		if (point != null) {
			Vector3 vectorToPoint = point.transform.position - transform.position;
			float angle = Mathf.Atan2 (vectorToPoint.y, vectorToPoint.x) * Mathf.Rad2Deg + -90f;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * TurnRate);
			if(Vector2.Distance(point.transform.position,transform.position) > 1.75f)
				transform.Translate (vectorToPoint.normalized * Time.deltaTime * speed, Space.World);
			else 
			{
				Waypoint waypoint = point.GetComponent<Waypoint>();
				if(waypoint != null)
				{
					point = waypoint.nextWaypoint;
					if(waypoint.remove) Destroy(waypoint.gameObject);
				}
			}
			transform.Translate (Vector3.up * Time.deltaTime * speed, Space.Self);
			transform.GetChild (1).transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = true;
			transform.GetChild (1).transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = true;
			transform.GetChild (1).transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = true;
		}
		else
		{
			transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
			transform.GetChild(1).transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		Basic basic = other.GetComponent<Basic> ();
		if (basic.isType (Types.Fighter) && basic.team != team) 
		{
			for(int i = 0; i < Turrets.Length; i++)
			{
				if(Turrets[i] != null && Turrets[i].transform.GetChild(0).GetComponent<Turret>().Target == null)
				{
					Turrets[i].transform.GetChild(0).GetComponent<Turret>().Target = other.gameObject;
				}
			}
		}
	}

	private void endLife()
	{
		GameObject temp  = Instantiate(explosion, gameObject.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
		temp.transform.rotation = gameObject.transform.rotation;
		temp.transform.parent = transform.parent;
		Instantiate (dyingSound, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
