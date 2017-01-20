using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAudio : MonoBehaviour {

	public float minIntensity = 1f;
	public float maxIntensity = 8f;

	private Light[] cubes;

	// Use this for initialization
	void Start () {
		cubes = new Light[AudioPeer.numBands];
		for (int i = 0; i < cubes.Length; i++) {
			cubes [i] = transform.GetChild (i).gameObject.GetComponent<Light> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < cubes.Length; i++) {
			cubes [i].intensity = (AudioPeer.audioBandBuffer [i] * (maxIntensity - minIntensity)) + minIntensity;
		}
	}
}
