using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour {

	public GameObject cubePrefab;

	public float radius = 100f;
	public float minScale = 2f;
	public float maxScale = 10f;
	public float cubeSize = 5f;

	private GameObject[] cubes;

	// Use this for initialization
	void Start () {
		cubes = new GameObject[AudioPeer.numSamples];

		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

		for (int i = 0; i < cubes.Length; i++) {
			GameObject cubeInstance = Instantiate (cubePrefab) as GameObject;
			cubeInstance.transform.position = this.transform.position;
			cubeInstance.transform.parent = this.transform;
			cubeInstance.name = "Music Cube " + i;
			this.transform.eulerAngles = new Vector3 (0, -(360f / cubes.Length) * i, 0);
			cubeInstance.transform.position = Vector3.forward * radius;
			cubes [i] = cubeInstance;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < cubes.Length; i++) {
			cubes [i].transform.localScale = new Vector3 (cubeSize, (AudioPeer.samples [i] * maxScale) + minScale, cubeSize);
		}
	}
}
