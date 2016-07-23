using UnityEngine;
using System.Collections;

public class Projectile : Basic 
{
	public float speed;
	public int dmg;
	public float lifeTime;
	public GameObject explosion;
	public float explosionSize;
	public Color color;
	public GameObject sound;
	// Use this for initialization
	void Start ()
	{
		Instantiate (sound, transform.position, transform.rotation);
		super ();
		Destroy(this.gameObject,lifeTime);
		//addType (Types.TopLevel);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.up * speed * Time.deltaTime, Space.Self); 
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		Basic basic = other.GetComponent<Basic> ();
		if(basic == null) 
			return;
		if (basic.team == team)
			return;
		if (basic.isType (Types.TopLevel) || basic.isType (Types.TopLevelNoCol)) 
		{
			Destructible destructible = other.GetComponent<Destructible> ();
			if (destructible == null)
				return;
			destructible.takeDamage (dmg);
			transform.parent = other.gameObject.transform;
			endLife ();
		}
	}

	private void endLife()
	{
		GameObject temp  = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as GameObject;
		temp.GetComponent<Detonator> ().size = explosionSize;
		temp.GetComponent<Detonator> ().color = color;
		temp.transform.parent = transform.parent;

		Destroy(this.gameObject);
	}
}
