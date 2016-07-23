using UnityEngine;
using System.Collections;

public class Fighter : Destructible 
{
	public float speed;
	public float turnSpeed;
	public float cooldown;
	public bool altFire;
	private float timer;
	public GameObject Projectile;
	public GameObject explosion;
	private bool leftfire;
	public GameObject dyingSound;

	// Use this for initialization
	void Start () 
	{
		super ();
		addType (Types.TopLevel);
		addType (Types.Fighter);
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
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)
			endLife ();
		timer += Time.deltaTime;
		if (timer >= cooldown) 
		{
			timer = cooldown;
			if(Input.GetAxis ("Joystick" + (int)controller + "RightTrigger") >= .5f)
				if(altFire)
					AltFire();
				else 
					Fire();
		}
		if (controller != Controller.None && controller != Controller.AI) 
		{
			if (Input.GetAxis ("Joystick" + (int)controller + "LeftStickY") > .1 || Input.GetAxis ("Joystick" + (int)controller + "LeftStickY") < -.1 || 
				Input.GetAxis ("Joystick" + (int)controller + "LeftStickX") > .1 || Input.GetAxis ("Joystick" + (int)controller + "LeftStickX") < -.1) {
				transform.Translate (Vector3.up * Input.GetAxis ("Joystick" + (int)controller + "LeftStickY") * Time.deltaTime * speed, Space.World);
				transform.Translate (Vector3.left * Input.GetAxis ("Joystick" + (int)controller + "LeftStickX") * Time.deltaTime * speed, Space.World);
			}

			if (Input.GetAxis ("Joystick" + (int)controller + "RightStickX") > .1 || Input.GetAxis ("Joystick" + (int)controller + "RightStickX") < -.1 || 
				Input.GetAxis ("Joystick" + (int)controller + "RightStickY") > .1 || Input.GetAxis ("Joystick" + (int)controller + "RightStickY") < -.1) {
				float angle = Mathf.Atan2 (Input.GetAxis ("Joystick" + (int)controller + "RightStickX"), Input.GetAxis ("Joystick" + (int)controller + "RightStickY")) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * turnSpeed);
			} else if (Input.GetAxis ("Joystick" + (int)controller + "LeftStickY") > .1 || Input.GetAxis ("Joystick" + (int)controller + "LeftStickY") < -.1 || 
				Input.GetAxis ("Joystick" + (int)controller + "LeftStickX") > .1 || Input.GetAxis ("Joystick" + (int)controller + "LeftStickX") < -.1) {
				float angle = Mathf.Atan2 (Input.GetAxis ("Joystick" + (int)controller + "LeftStickX"), Input.GetAxis ("Joystick" + (int)controller + "LeftStickY")) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * speed);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
	}

	public void Fire()
	{
		GameObject projectile = Instantiate (Projectile, gameObject.transform.position, Quaternion.identity) as GameObject;
		projectile.transform.rotation = gameObject.transform.rotation;
		projectile.GetComponent<Basic> ().team = team;
		timer = 0;
	}
	private void endLife()
	{
		GameObject temp  = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as GameObject;
		temp.transform.rotation = gameObject.transform.rotation;
		temp.transform.parent = transform.parent;
		temp.GetComponent<Detonator> ().size = 1f;
		Instantiate (dyingSound, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
