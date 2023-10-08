using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class InitializeManager : MonoBehaviour
{
    [SerializeField] private Transform[] startingPositions;
    [SerializeField] private GameObject[] aiPathObject;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Animator CountDownAnim;


    [SerializeField] private RaceManager rm;

    [SerializeField] private TimerController timerController;


    [SerializeField] private MeshFilter[] enemyMeshChoices;


    [SerializeField] private Transform finishPoint;

    private GameManager _gm;
    private bool[] spawnLocationUsed;

    void Awake()
    {
        
        
        
            _gm = GameObject.Find("GAMEMANAGER").GetComponent<GameManager>();
        
      
        if(_gm == null)
        {
            Debug.Log("Game Manager not set");
        }



    }

    void Start()
    {
        CountDownAnim.SetTrigger("Count");

        // Initialize the array to keep track of used spawn locations.
        spawnLocationUsed = new bool[startingPositions.Length];

        // Call the SpawnEnemies method when the script starts.
        SpawnEnemies();
        aiPathObject = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i<aiPathObject.Length; i++)
        {
            aiPathObject[i].GetComponent<AIDestinationSetter>().target = finishPoint;
            aiPathObject[i].GetComponent<MeshFilter>().sharedMesh = enemyMeshChoices[Random.Range(0, enemyMeshChoices.Length)].sharedMesh;
        }

        rm.SetEnemyPositions();

        //DisableAI();
    }

    void Update()
    {

    }

    public void EnableAI()
    {
        // Disable AI movement
        if (aiPathObject != null)
        {
            for (int i = 0; i < aiPathObject.Length; i++)
            {
                AIPath aiPath = aiPathObject[i].GetComponent<AIPath>();
                if (aiPath != null)
                {
                    aiPath.canMove = true; // Set to true or false as needed
                }
                else
                {
                    Debug.LogError("AIPath component not found on the GameObject.");
                }
            }
        }

        timerController.StartTimer();

       
    }

  

    private void SpawnEnemies()
    {
        for (int i = 0; i < _gm.enemyCount; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, startingPositions.Length);
            } while (spawnLocationUsed[randomIndex]);

            spawnLocationUsed[randomIndex] = true;
            Vector3 spawnPosition = startingPositions[randomIndex].position;
            Instantiate(enemyPrefab, spawnPosition, transform.rotation * Quaternion.Euler(0f, 90f, 0f));
        }
    }
}
