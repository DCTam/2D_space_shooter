using UnityEngine;
using System.Collections;

public class SmokingShips : MonoBehaviour {

	public GameObject SmokeLv1;
	public GameObject SmokeLv2;
	private float smokeCooldown;
	private float timer;
	private int maxHealth;
	public int currentHealth;
	private Fighter fighterScript;

	// Use this for initialization
	void Start () {
		fighterScript = GetComponent<Fighter>();
		timer = 0;
		smokeCooldown = .2F;
		maxHealth = fighterScript.health;
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		currentHealth = fighterScript.health;

		if(fighterScript.health <= .33 * maxHealth && timer >= smokeCooldown){
			GameObject smoke2 = Instantiate(SmokeLv2, new Vector3(this.gameObject.transform.Find("Engine").position.x, this.gameObject.transform.Find("Engine").position.y, this.gameObject.transform.Find("Engine").position.z) , transform.rotation) as GameObject;
			Destroy (smoke2, 1.1f);
			
			timer = 0;
		}

		 if (fighterScript.health <= .66 * maxHealth && timer >= smokeCooldown) {

			GameObject smoke1 = Instantiate(SmokeLv1, new Vector3(this.gameObject.transform.Find("Engine").position.x, this.gameObject.transform.Find("Engine").position.y, this.gameObject.transform.Find("Engine").position.z) , transform.rotation) as GameObject;
			Destroy (smoke1, .21F);

			timer = 0;
		}
	
	}
}
