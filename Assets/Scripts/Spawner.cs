using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //This is a Singleton of the BoidSpawner. there is only one instance 
    // of BoidSpawner, so we can store it in a static variable named s.
    static public Spawner       S;
    static public List<Boid>    boids = new List<Boid>();
    static public List<BoidNew> newboids = new List<BoidNew>();

    // These fields allow you to adjust the spawning behavior of the boids
    [Header("Set in Inspector: Spawning")]
    public GameObject           boidPrefabOne;
    public GameObject           boidPrefabTwo;
    public Transform            boidAnchor;
    public int                  numBoids = 100;
    public float                spawnRadius = 100f;
    public float                spawnDelay = 0.1f;

    // These fields allow you to adjust the flocking behavior of the boids
    [Header("Set in Inspector: Boids")]
    public float                velocity = 30f;
    public float                neighborDist = 30f;
    public float                collDist = 4f;
    public float                velMatching = 0.25f;
    public float                flockCentering = 0.2f;
    public float                collAvoid = 2f;
    public float                attractPull = 2f;
    public float                attractPush = 2f;
    public float                attractPushDist = 5f;
    
    void Awake()
    {
        //Set the Singleton S to be this instance of BoidSpawner
        S = this;
        //Start instantiation of the Boids
        boids = new List<Boid>();
        InstantiateBoid();
    }

    public void InstantiateBoid()
    {
        GameObject prefab;
        if (boids.Count  <= newboids.Count)
        {
            prefab = boidPrefabOne;
        }
        else
        {
            prefab = boidPrefabTwo;
        }
        GameObject go = Instantiate(prefab);
        if (go.GetComponent<Boid>()!= null)
        {
            Boid b = go.GetComponent<Boid>();
            b.transform.SetParent(boidAnchor);
            boids.Add(b);
        }
        else
        {
            BoidNew b = go.GetComponent<BoidNew>();
            b.transform.SetParent(boidAnchor);
            newboids.Add(b);
        }
        if (newboids.Count < numBoids)
        {
            Invoke("InstantiateBoid", spawnDelay);
        }
    }
}