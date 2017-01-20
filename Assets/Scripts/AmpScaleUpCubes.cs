using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpScaleUpCubes : MonoBehaviour {

	public float cubeSize = 10f;
	public float startScale = 2f;
	public float scaleMultiplier = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 (transform.localScale.x, (AudioPeer.amplitudeBuffer * scaleMultiplier) + startScale, transform.localScale.z);
	}
}
