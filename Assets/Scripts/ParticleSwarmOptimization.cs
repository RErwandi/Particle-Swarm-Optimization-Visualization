﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParticleSwarmOptimization : MonoBehaviour {

    public GameObject swarmPrefab;
    public float terrainSize;
    public Material bestMaterial;
    public Material normalMaterial;
    public int particleCount;
    public float cognitionRate;
    public float socialRate;
    public int maxIteration;
    public float durationIteration;
    public List<Swarm> swarms = new List<Swarm>();
    public Swarm bestSwarm;
    public float bestFitness = 0;
    public TextMeshProUGUI infoText;
    private GameObject startButton;

	void Start () {
		
	}

	void Update () {
		
	}

    public void StartPSO(GameObject startButton)
    {
        this.startButton = startButton;
        this.startButton.SetActive(false);

        foreach(Swarm s in swarms)
        {
            Destroy(s.gameObject);
        }
        swarms.Clear();

        StartCoroutine(PSO());
    }

    IEnumerator PSO()
    {
        // Spawn particles randomly
        for (int i = 0; i < particleCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-terrainSize, terrainSize), 0, Random.Range(-terrainSize, terrainSize));
            GameObject newSwarm = Instantiate(swarmPrefab, randomPosition, Quaternion.identity);
            swarms.Add(newSwarm.GetComponent<Swarm>());
        }

        for (int i = 0; i < maxIteration; i++)
        {
            // Update All Swarms Fitness
            foreach (Swarm s in swarms)
            {
                s.UpdateFitness();
            }
            
            // Get Best Swarms and Best Fitness
            if(bestSwarm != null)
                bestSwarm.GetComponent<Renderer>().material = normalMaterial;
            bestSwarm = GetBestSwarm();
            bestSwarm.GetComponent<Renderer>().material = bestMaterial;
            bestFitness = bestSwarm.best_fitness;

            // Move all Swarms
            foreach (Swarm s in swarms)
            {
                if(s != bestSwarm)
                {
                    s.UpdateVelocity(bestSwarm, cognitionRate, socialRate, terrainSize, durationIteration, false);
                }
                else
                {
                    s.UpdateVelocity(bestSwarm, cognitionRate, socialRate, terrainSize, durationIteration, true);
                }
            }
            infoText.text = "Iteration:" + i + "\nBest Fitness:" + bestFitness;
            yield return new WaitForSeconds(durationIteration);
        }
        this.startButton.SetActive(true);
    }

    Swarm GetBestSwarm()
    {
        Swarm bestSwarm = swarms[0];
        float currentBestFit = bestSwarm.best_fitness;
        foreach (Swarm s in swarms)
        {
            if (currentBestFit < s.best_fitness)
            {
                bestSwarm = s;
                currentBestFit = s.best_fitness;
            }
        }
        return bestSwarm;
    }
}
