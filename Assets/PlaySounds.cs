using UnityEngine;
using System.Collections;

public class PlaySounds : MonoBehaviour {
	public AudioSource sound;
	public float soundLength = .5f;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, soundLength);
		sound.Play ();
	}

}
