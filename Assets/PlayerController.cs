using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {
	public Boundary boundary;

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
	}
	
	
	void Update () {
		
	}
}
