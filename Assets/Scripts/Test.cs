using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public AudioSource aud;

	void Start () {
		aud = GetComponent<AudioSource>();
		aud.clip = Microphone.Start("Built-in Mic",true,10,44100);
		aud.loop = true;
		while (!(Microphone.GetPosition(null) > 0)) {}
		aud.Play();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("audio pitch: " + aud.pitch);
	}
}
