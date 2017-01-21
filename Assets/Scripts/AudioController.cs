using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
public class AudioController : MonoBehaviour {

    private AudioSource micAudio;

    public int sampleSize = 1024;
    public float threshHold = 0.02f; //min amplitude to extract pitch
    private float[] spectrumData;
    private float fSample;

    public float averageTime;

    List<float > pitches = new List<float>();

	// Use this for initialization
	void Start () {
        micAudio = GetComponent<AudioSource>();
        setUpMicrophone();
        spectrumData = new float[sampleSize];
        fSample = AudioSettings.outputSampleRate;

        StartCoroutine(updatePitch());
    }

    private IEnumerator updatePitch() {
        while (true) {
            pitches.Clear();
            float currentTime = 0;
            while (currentTime < averageTime) {
                currentTime += Time.fixedDeltaTime;
                pitches.Add(getPitch());
                yield return null;
            }
            Debug.Log(pitches.Average());
        }
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
