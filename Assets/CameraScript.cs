using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject target;
	private Camera cam;
	// Use this for initialization
	void Start()
	{
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if (target)
		{
			transform.position = new Vector3(target.transform.position.x, target.transform.position.y,-1);
			
		}
		if (Input.GetKey("e"))
		{
			cam.orthographicSize -= 25 * Time.deltaTime;
		}
		else if (Input.GetKey("q"))
		{
			cam.orthographicSize += 25 * Time.deltaTime;
		}
		if (cam.orthographicSize < 5)
			cam.orthographicSize = 5;
		if (cam.orthographicSize > 55)
			cam.orthographicSize = 55;
	}
}
