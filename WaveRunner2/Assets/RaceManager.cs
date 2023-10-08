using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RaceManager : MonoBehaviour
{
    [SerializeField] private Transform finishLine;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private TMP_Text positionText;

    private List<KeyValuePair<float, GameObject>> distanceList;


    private void Awake()
    {
       
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
        int playerPosition = playerIndex + 1; // Add 1 to make it human-readable (1st, 2nd, 3rd, etc.)

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
}
