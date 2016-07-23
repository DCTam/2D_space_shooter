using UnityEngine;
using System.Collections;

public class Turret: Destructible 
{
	public GameObject explosion;
	public GameObject Target;
	public float TurnRate;
	public Gun State;
	public GameObject Projectile;
	public float cooldown;
	private float timer;
	public float strayAngle;

	// Use this for initialization
	void Start () 
	{
		super ();
		timer = cooldown;
		addType (Types.TopLevelNoCol);
		addType (Types.BottomLevel);
	}

	public void Fire()
	{
		/*
		GameObject projectile  = Instantiate(Projectile, gameObject.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
		projectile.transform.rotation = gameObject.transform.rotation;
		projectile.GetComponent<Basic> ().team = team;
		timer = 0;
*/

		var randomNumberX = Random.Range(-strayAngle, strayAngle);
		var randomNumberY = Random.Range(-strayAngle, strayAngle);
		var randomNumberZ = Random.Range(-strayAngle, strayAngle);
		GameObject bullet = Instantiate(Projectile, transform.position, transform.rotation) as GameObject;
		bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
		bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.forward * 10000);
		bullet.GetComponent<Basic> ().team = team;
		timer = 0;

	}
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= cooldown) 
		{
			timer = cooldown;
			if(Target!=null)
			Fire ();
		}

		if (health <= 0)
			endLife ();
		if (Target != null) 
		{
			Vector3 vectorToTarget = Target.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + -90f;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * TurnRate);

		}
	}

	private void endLife()
	{
		GameObject temp  = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as GameObject;
		temp.transform.rotation = gameObject.transform.rotation;
		temp.transform.parent = transform.parent;
		temp.GetComponent<Detonator> ().size = 1f;
		gameObject.transform.parent.gameObject.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		this.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Destructible> ().health--;
		Destroy (this.gameObject);
	}
}
