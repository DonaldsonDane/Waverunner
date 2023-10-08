using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Hellmade.Sound;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private AudioClip deathSound;

    [SerializeField] private RaceManager rm;

    private static bool canMove;

    [SerializeField] public bool movable;

    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private MeshRenderer boatMesh;


    private int obstacleIdNumber;
    [SerializeField]private List<Transform> respawnLocations;

    [SerializeField] private GameObject[] respawnObjects;

    private Transform placeToSpawn;


    [SerializeField] private Transform startingPosition;


    [SerializeField] private Transform resetPosition;


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


        //Emergency Respawn
        if (Input.GetKey(KeyCode.P))
        {
            transform.position = resetPosition.position;
        }
    }

    public void Sink()
    {
        Debug.Log("Collider hit!");
        StartCoroutine(DeathSequence());
    }

    private bool isSpinning = false;

    public void OnCollisionEnter(Collision other)
    {
        ObstacleTracker obstacleTracker = other.gameObject.GetComponent<ObstacleTracker>();

        if (obstacleTracker != null)
        {
            obstacleIdNumber = obstacleTracker.idNumber;
            if (other.gameObject.tag == "Penguin")
            {
                Debug.Log("Penguin hit");
                other.gameObject.GetComponent<ObstacleTracker>().Die();
                SpinOnSpit(); // Call the method for spinning when collided with a penguin.
            }



            else
            {
                Sink();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            rm.RaceOver();
        }else if(other.gameObject.tag == "Speed")
        {
            SpeedCollected();
        }
    }

    private void SpinOnSpit()
    {
        if (!isSpinning)
        {
            isSpinning = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetTrigger("Spin");
            StartCoroutine(ResetSpin(5.0f));
        }
    }

    private IEnumerator ResetSpin(float duration)
    {
        float elapsed = 0f;
     
    

        while (elapsed < duration)
        {
           
            elapsed += Time.deltaTime;
            yield return null;
        }

    

        isSpinning = false;
        canMove = true; // Re-enable player movement.
        GetComponent<Animator>().enabled = false;
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
            GetComponentInChildren<BoxCollider>().enabled = false;
            EazySoundManager.PlaySound(deathSound);
            isDead = true;
            boatMesh.enabled = false;
            movable = false;
            deathParticle.Play();
            yield return new WaitForSeconds(2f);
            Respawn();
            yield return new WaitForSeconds(2f);
            GetComponentInChildren<BoxCollider>().enabled = true;
        }
    

    }

    bool speedBoostEnabled = false;

    private void SpeedCollected()
    {
        if (!speedBoostEnabled)
        {
            speedBoostEnabled = true;
            Debug.Log("Speed Collected");
            StartCoroutine(SpeedBoostSequence());
        }
    
    }

    private IEnumerator SpeedBoostSequence()
    {
        GetComponent<Rigidbody>().mass = 400.0f;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().mass = 3000.0f;
        speedBoostEnabled = false;
    }
}
