using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class AudioController : MonoBehaviour {

    private AudioSource micAudio;

    public int sampleSize = 1024;
    public float threshHold = 0.02f; //min amplitude to extract pitch
    private float[] spectrumData;
    private float fSample;

	// Use this for initialization
	void Start () {
        micAudio = GetComponent<AudioSource>();
        setUpMicrophone();
        spectrumData = new float[sampleSize];
        fSample = AudioSettings.outputSampleRate;
    }

    private IEnumerator updatePitch() {
        List<int> pitches = new ArrayList<int>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(getPitch());
	}

    private void setUpMicrophone() {
        micAudio.clip = Microphone.Start("Built-in Mic", true, 10, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
        micAudio.Play();
    }

    public float getPitch() {
        float frequency = 0;
        float frequencyN = 0;

        micAudio.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);
        float currentMaxV = 0;
        int currentMaxN = 0;
        for (int i = 0; i<sampleSize; i++) {
            if (spectrumData[i] > currentMaxV && spectrumData[i]> threshHold) {
                currentMaxV = spectrumData[i];
                currentMaxN = i;
            }
        }
        frequencyN = currentMaxN;
        if (currentMaxN>0 && currentMaxN < sampleSize - 1) {
            float dL = spectrumData[currentMaxN - 1] / spectrumData[currentMaxN];
            float dR = spectrumData[currentMaxN + 1] / spectrumData[currentMaxN];
            frequencyN += 0.5f * (dR * dR - dL * dL);
        }
        return frequencyN * (fSample / 2) / sampleSize;
    }
}
