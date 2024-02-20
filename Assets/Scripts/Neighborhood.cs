﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    [Header("Set Dynamically")]
    public List<Boid> neighbors;
    private SphereCollider coll;
    public List<GameObject> obstacles;

    void Start()
    {
        neighbors = new List<Boid>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.S.neighborDist / 2;
        obstacles = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (coll.radius != Spawner.S.neighborDist / 2)
        {
            coll.radius = Spawner.S.neighborDist / 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if enter trigger, add to neighborhood
        Boid b = other.GetComponent<Boid>();
        GameObject go = other.gameObject;
        if (b != null)
        {
            if (neighbors.IndexOf(b) == -1)
            {
                neighbors.Add(b);
            }
        }
        else
        {
            if(obstacles.IndexOf(go) == -1)
            {
                obstacles.Add(go);
            }
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        //if exit trigger, add to neighborhood
        Boid b = other.GetComponent<Boid>();
        GameObject go = other.gameObject;
        if (b != null)
        {
            if (neighbors.IndexOf(b) != -1)
            {
                neighbors.Remove(b);
            }
        }
        else
        {
            obstacles.Remove(go);
        }
    }


    public Vector3 avgPos
    {
        get
        {
            Vector3 avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;

            for (int i = 0; i < neighbors.Count; i++)
            {
                avg += neighbors[i].pos;
            }
            avg /= neighbors.Count;

            return avg;

        }
    }

    public Vector3 avgVel
    {
        get
        {
            Vector3 avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;

            for (int i = 0; i < neighbors.Count; i++)
            {
                avg += neighbors[i].rigid.velocity;
            }
            avg /= neighbors.Count;

            return avg;
        }
    }

    public Vector3 avgClosePos
    {
        get
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for (int i = 0; i < neighbors.Count; i++)
            {
                delta = neighbors[i].pos - transform.position;
                if (delta.magnitude <= Spawner.S.collDist)
                {
                    avg += neighbors[i].pos;
                    nearCount++;
                }
            }

            // If there were no neighbors too close, return Vector3.zero
            if (nearCount == 0) return avg;

            // Otherwise, averge their locations
            avg /= nearCount;
            return avg;
        }
    }
    public Vector3 ClosePosObstacles
    {
        get
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for (int i = 0; i < obstacles.Count; i++)
            {
                delta = obstacles[i].transform.position - transform.position;
                if (delta.magnitude <= Spawner.S.collDist)
                {
                    avg += obstacles[i].transform.position;
                    nearCount++;
                }
            }

            if (nearCount == 0) return avg;

            avg /= nearCount;
            return avg;
        }
    }
}
