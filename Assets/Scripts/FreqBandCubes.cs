using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreqBandCubes : MonoBehaviour {

	public GameObject[] cubes;

	public float cubeSize = 10f;
	public float startScale = 2f;
	public float scaleMultiplier = 10f;
	public bool useBuffer = true;

	// Use this for initialization
	void Start () {
		cubes = new GameObject[AudioPeer.numBands];
		for (int i = 0; i < cubes.Length; i++) {
			cubes [i] = transform.GetChild (i).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (useBuffer) {
			for (int i = 0; i < cubes.Length; i++) {
				cubes [i].transform.localScale = new Vector3 (cubeSize, (AudioPeer.bandBuffer [i] * scaleMultiplier) + startScale, cubeSize);
			}
		} else {
			for (int i = 0; i < cubes.Length; i++) {
				cubes [i].transform.localScale = new Vector3 (cubeSize, (AudioPeer.freqBand [i] * scaleMultiplier) + startScale, cubeSize);
			}
		}
	}
}
