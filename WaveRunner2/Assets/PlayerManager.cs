using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Hellmade.Sound;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private AudioClip deathSound;



    private static bool canMove;

    [SerializeField] public bool movable;

    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private MeshRenderer boatMesh;


    private int obstacleIdNumber;
    [SerializeField]private List<Transform> respawnLocations;

    [SerializeField] private GameObject[] respawnObjects;

    private Transform placeToSpawn;


    [SerializeField] private Transform startingPosition;


    private void Awake()
    {
        placeToSpawn = startingPosition;
        respawnObjects = GameObject.FindGameObjectsWithTag("RespawnPositions");
        respawnLocations = new List<Transform>(); // Initialize the List<Transform> here

        for (int i = 0; i < respawnObjects.Length; i++)
        {
            respawnLocations.Add(respawnObjects[i].transform); // Use Add to add the Transform to the List
        }
    }


    private void Start()
    {
        deathParticle = this.GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        canMove = movable;
    }

    public void Sink()
    {
        Debug.Log("Collider hit!");
        StartCoroutine(DeathSequence());
    }


    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            obstacleIdNumber = other.gameObject.GetComponent<ObstacleTracker>().idNumber;
            Sink();
        }
    }


    private void Respawn()
    {
        switch (obstacleIdNumber)
        {
            case 0:
                placeToSpawn = respawnLocations[0];
                break;
            case 1:
                placeToSpawn = respawnLocations[1];
                break;
            case 2:
                placeToSpawn = respawnLocations[2];
                break;
            case 3:
                placeToSpawn = respawnLocations[2];
                break;
            case 4:
                placeToSpawn = respawnLocations[3];
                break;
            case 5:
                placeToSpawn = respawnLocations[4];
                break;
            case 6:
                placeToSpawn = respawnLocations[6];
                break;
          
        }

        transform.position = placeToSpawn.position;
        boatMesh.enabled = true;
        movable = true;
        isDead = false;
    }


    bool isDead = false;

    private IEnumerator DeathSequence()
    {
        if (!isDead)
        {
            EazySoundManager.PlaySound(deathSound);
            isDead = true;
            boatMesh.enabled = false;
            movable = false;
            deathParticle.Play();
            yield return new WaitForSeconds(2f);
            Respawn();
        }
    

    }
}
