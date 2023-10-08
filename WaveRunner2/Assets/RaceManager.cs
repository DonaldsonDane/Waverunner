using System.Collections.Generic;
using UnityEngine;
using System.Collections;

using System;
using TMPro;
using Pathfinding;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private Transform finishLine;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemies;
    public int playerPosition;

    [SerializeField] private PlayerManager pm;


    [SerializeField] private int maxPenguins;
    [SerializeField] private Transform[] penguinSpawnerPositions;

    [SerializeField] private TMP_Text positionText;

    private List<KeyValuePair<float, GameObject>> distanceList;


    [SerializeField] private GameObject penguinPrefab;

    private bool raceOver = false;


    private void Awake()
    {
        BeginSpawningPenguins();
    }

    public void SetEnemyPositions()
    {
        // Find all objects with the tag 'Enemy' and add them to the 'enemies' list
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            enemies.Add(enemy);
        }


       
    }

    private void Update()
    {
        CalculateDistances();
        SortDistances();
        DeterminePlayerPosition();
    }

    private void CalculateDistances()
    {
        distanceList = new List<KeyValuePair<float, GameObject>>();

        float playerDistance = Vector3.Distance(player.transform.position, finishLine.position);
        distanceList.Add(new KeyValuePair<float, GameObject>(playerDistance, player));

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(enemy.transform.position, finishLine.position);
            distanceList.Add(new KeyValuePair<float, GameObject>(enemyDistance, enemy));
        }
    }

    private void SortDistances()
    {
        distanceList.Sort((a, b) => a.Key.CompareTo(b.Key));
    }

    private void DeterminePlayerPosition()
    {
        int playerIndex = distanceList.FindIndex(item => item.Value == player);
         playerPosition = playerIndex + 1; // Add 1 to make it human-readable (1st, 2nd, 3rd, etc.)

        //Debug.Log("Player position: " + playerPosition);
        
        switch(playerPosition)
        {
            case 1:
                positionText.text = "1st";
                break;
            case 2:
                positionText.text = "2nd";
                break;
            case 3:
                positionText.text = "3rd";
                break;
            case 4:
                positionText.text = "4th";
                break;
            case 5:
                positionText.text = "5th";
                break;
                case 6:
                positionText.text = "6th";
                break;
                case 7:
                positionText.text = "7th";
                break;
        }
    }



    private void BeginSpawningPenguins()
    {
        // BeginSpawningPenguins()
        StartCoroutine(PenguinTicker());

    }
    private GameObject spawnedPenguin; // Declare a variable to store the spawned penguin
    [SerializeField] private Transform startingPosition;
    private void SpawnPenguin()
    {
        for(int i = 0; i < 1; i++)
        {
            int randomIndex;
         
            
                randomIndex = UnityEngine.Random.Range(0, penguinSpawnerPositions.Length);
          

           
            Vector3 spawnPosition = penguinSpawnerPositions[randomIndex].position;
            spawnedPenguin =  Instantiate(penguinPrefab, spawnPosition, transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            spawnedPenguin.gameObject.GetComponent<AIDestinationSetter>().target = startingPosition;
            
        }
    }

    private System.Collections.IEnumerator PenguinTicker()
    {
        if (!raceOver)
        {
            yield return new WaitForSeconds(3f);
            SpawnPenguin();
            StartCoroutine(PenguinTicker());
        }
       
       
    }


    public void RaceOver()
    {
        Debug.Log("Game Over");
        pm.movable = false;
        GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>().ShowEndGame();
    }

}
