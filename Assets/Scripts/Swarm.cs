using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Swarm : MonoBehaviour {

    public float offsetY;
    public float best_fitness = -1;

	void Start () {
        
	}

	void Update () {
        Vector3 CurrentPos = transform.position;
        CurrentPos.y = Terrain.activeTerrain.SampleHeight(transform.position) + offsetY;
        transform.position = CurrentPos;
    }

    public void UpdateFitness()
    {
        float fitness = Terrain.activeTerrain.SampleHeight(transform.position);
        if(best_fitness < fitness || best_fitness == -1)
            best_fitness = fitness;
    }

    public void UpdateVelocity(Swarm bestSwarm, float cognitiveRate, float socialRate, float bound, float duration)
    {
        Vector3 nextPosition = transform.position;
        for (int i = 0; i < 2; i++)
        {
            float r1 = Random.Range(0f, 1f);
            float r2 = Random.Range(0f, 1f);
            if(i == 0)
            {
                float vCognitive = cognitiveRate * r1 * (bestSwarm.transform.position.x - transform.position.x);
                float sCognitive = socialRate * r2 * (bestSwarm.transform.position.x - transform.position.x);
                nextPosition.x = transform.position.x + vCognitive + sCognitive;
                nextPosition.x = Mathf.Clamp(nextPosition.x, -bound, bound);
            } else
            {
                float vCognitive = cognitiveRate * r1 * (bestSwarm.transform.position.z - transform.position.z);
                float sCognitive = socialRate * r2 * (bestSwarm.transform.position.z - transform.position.z);
                nextPosition.z = transform.position.z + vCognitive + sCognitive;
                nextPosition.z = Mathf.Clamp(nextPosition.z, -bound, bound);
            }
        }
        
        transform.DOMoveX(nextPosition.x, duration);
        transform.DOMoveZ(nextPosition.z, duration);
    }
}
