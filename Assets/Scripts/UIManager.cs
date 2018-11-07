using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public ParticleSwarmOptimization pso;
    public TMP_InputField particleCount;
    public TMP_InputField cognitionRate;
    public TMP_InputField socialRate;
    public TMP_InputField maxIteration;
    public TMP_InputField iterationDelay;

	void Start () {
        particleCount.text = pso.particleCount.ToString();
        cognitionRate.text = pso.cognitionRate.ToString();
        socialRate.text = pso.socialRate.ToString();
        maxIteration.text = pso.maxIteration.ToString();
        iterationDelay.text = pso.durationIteration.ToString();
	}

	void Update () {
		
	}

    public void UpdateParticleCount()
    {
        pso.particleCount = int.Parse(particleCount.text);        
    }

    public void UpdateCognitionRate()
    {
        pso.cognitionRate = float.Parse(cognitionRate.text);
    }

    public void UpdateSocialRate()
    {
        pso.socialRate = float.Parse(socialRate.text);
    }

    public void UpdateMaxIteration()
    {
        pso.maxIteration = int.Parse(maxIteration.text);
    }

    public void UpdateDelayIteration()
    {
        pso.durationIteration = float.Parse(iterationDelay.text);
    }
}
