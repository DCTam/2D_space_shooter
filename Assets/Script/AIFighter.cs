using UnityEngine;
using System.Collections;

public class AIFighter : Destructible 
{
	public GameObject explosion;
	public GameObject point;
	public float TurnRate;
	public float speed;
	public GameObject target;
	public bool altFire;
	public float cooldown;
	private float timer;
	public GameObject Projectile;
	private bool leftfire;
	public GameObject primaryPoint;
	public GameObject dyingSound;

	// Use this for initialization
	void Start () 
	{
		super ();
		addType (Types.TopLevel);
		addType (Types.Fighter);
		leftfire = true;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (health <= 0)
			endLife ();

		gameObject.transform.GetChild (2).GetComponent<Sensor> ().clean ();
		if (gameObject.transform.GetChild (2).GetComponent<Sensor> ().ships.Count >= 1) 
		{
			target = gameObject.transform.GetChild (2).GetComponent<Sensor> ().ships [0];
			if (target.GetComponent<Basic> ().isType (Types.Fighter)) 
			{
				point = target;
			}
		} 
		else 
		{
			point = primaryPoint;
		}

		if (point != null) 
		{
			Vector3 vectorToPoint = point.transform.position - transform.position;
			if(target == null)
			{
				float angle = Mathf.Atan2 (vectorToPoint.y, vectorToPoint.x) * Mathf.Rad2Deg + -90f;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
					transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * speed);
			}
			else
			{
				Vector3 vectorToTarget = target.transform.position - transform.position;
				timer += Time.deltaTime;
				if (timer >= cooldown) 
				{
					timer = cooldown;
						AltFire();

				}
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + -90f;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * TurnRate);
			}
			if(Vector2.Distance(point.transform.position,transform.position) > 1.75f)
				transform.Translate (vectorToPoint.normalized * Time.deltaTime * speed, Space.World);
			else 
			{
				Waypoint waypoint = point.GetComponent<Waypoint>();
				if(waypoint != null)
				{
					point = waypoint.nextWaypoint;
					primaryPoint = waypoint.nextWaypoint;
					if(waypoint.remove) Destroy(waypoint.gameObject);
				}
			}
		}
	
	}
	public void AltFire()
	{
		int temp;
		if (leftfire) 
		{
			temp = 1;
			leftfire = false;
		} 
		else 
		{
			temp = 0;
			leftfire = true;
		}
		GameObject projectile = Instantiate (Projectile, gameObject.transform.GetChild(temp).transform.position, Quaternion.identity) as GameObject;
		projectile.transform.rotation = gameObject.transform.rotation;
		projectile.GetComponent<Basic> ().team = team;
		timer = 0;
	}

	private void endLife()
	{
		GameObject temp  = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as GameObject;
		temp.transform.rotation = gameObject.transform.rotation;
		temp.transform.parent = transform.parent;
		temp.GetComponent<Detonator> ().size = .4f;
		Instantiate (dyingSound, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
