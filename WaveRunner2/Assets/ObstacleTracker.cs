using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The purpose of this script is to track which obstacle the player (Or enemy) hits
public class ObstacleTracker : MonoBehaviour
{
    private static int id;

    public int idNumber;


    private void Awake()
    {
        id = idNumber;
    }

}
