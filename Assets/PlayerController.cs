using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public Boundary boundary;
	public GameObject startPos;
	public GameObject endPos;

	public float midPitch;
	public float lowThreshhold;
	public float speed;
	private float currentSpeed;
	public AudioController audioController;
	void FixedUpdate ()
	{
		
	}
	
	
	void Update ()
	{
		float currentFrequency = audioController.getPitch ();
		if (currentFrequency > midPitch) {
			currentSpeed = speed;
			
		} else if (currentFrequency < midPitch && currentFrequency > lowThreshhold) {
			currentSpeed = -speed;
		} else {
			currentSpeed = 0;
		}

		transform.position = transform.position + new Vector3(0, currentSpeed * Time.fixedDeltaTime, 0);

	}
}
