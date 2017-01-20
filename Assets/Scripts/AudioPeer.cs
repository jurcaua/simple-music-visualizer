using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {

	public static int numSamples = 512;
	public static float[] samples;

	public static int numBands = 8;
	public static float[] freqBand;
	public static float[] bandBuffer;
	private float[] bufferDecrease;
	private float[] freqBandHighest;

	public static float[] audioBand;
	public static float[] audioBandBuffer;

	public static float amplitude, amplitudeBuffer;
	private float amplitudeHighest;

	public float audioProfile;

	public float startBufferDecrease = 0.005f;
	public float bufferDecreaseMultiplier = 1.2f;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		samples = new float[numSamples];

		freqBand = new float[numBands];
		bandBuffer = new float[numBands];
		bufferDecrease = new float[numBands];
		freqBandHighest = new float[numBands];

		audioBand = new float[numBands];
		audioBandBuffer = new float[numBands];

		source = GetComponent<AudioSource> ();

		AudioProfile (audioProfile);
	}
	
	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
		MakeFrequencyBands ();
		BandBuffer ();
		CreateAudioBands ();
		GetAmplitude ();
	}

	void GetSpectrumAudioSource(){
		source.GetSpectrumData (samples, 0, FFTWindow.Blackman);
	}

	void MakeFrequencyBands(){
		int count = 0;

		for (int i = 0; i < numBands; i++) {
			int sampleCount = (int)Mathf.Pow (2, i + 1);

			float average = 0;
			for (int j = 0; j < sampleCount; j++) {
				average += samples [count] * (count + 1);
				count++;
			}
			average /= count;

			freqBand [i] = average * 10;
		}
	}

	void BandBuffer(){
		for (int i = 0; i < numBands; i++) {
			if (freqBand [i] > bandBuffer [i]) {
				bandBuffer [i] = freqBand [i];
				bufferDecrease [i] = startBufferDecrease;
			}
			if (freqBand [i] < bandBuffer [i]) {
				bandBuffer [i] -= bufferDecrease [i];
				bufferDecrease [i] *= bufferDecreaseMultiplier;
			}
		}
	}

	void CreateAudioBands(){
		for (int i = 0; i < numBands; i++) {
			if (freqBand [i] > freqBandHighest [i]) {
				freqBandHighest [i] = freqBand [i];
			}

			audioBand [i] = freqBand [i] / freqBandHighest [i];
			audioBandBuffer [i] = bandBuffer [i] / freqBandHighest [i];
		}
	}

	void GetAmplitude(){
		float currentAmplitude = 0;
		float currentAmplitudeBuffer = 0;

		for (int i = 0; i < numBands; i++) {
			currentAmplitude += audioBand [i];
			currentAmplitudeBuffer += audioBandBuffer [i];
		}

		if (currentAmplitude > amplitudeHighest) {
			amplitudeHighest = currentAmplitude;
		}

		amplitude = currentAmplitude / amplitudeHighest;
		amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
	}

	void AudioProfile(float audioProfile){
		amplitudeHighest = audioProfile;
		for (int i = 0; i < numBands; i++) {
			freqBandHighest [i] = audioProfile;
		}
	}
}
