using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
//The purpose of this script is to track which obstacle the player (Or enemy) hits
public class ObstacleTracker : MonoBehaviour
{
    private static int id;

    [SerializeField] private AudioClip deathSound;

    [SerializeField] private ParticleSystem collisionParticle;

    public enum ObstacleType
    {
        Penguin
    }

    [SerializeField] private ObstacleType obstacleType;

    public int idNumber;


    private void Awake()
    {
        id = idNumber;
    }



    public void Die()
    {
        switch (obstacleType)
        {
            case ObstacleType.Penguin:
                EazySoundManager.PlaySound(deathSound);
                collisionParticle.Play();
                Destroy(this.gameObject);
                break;
        }
    }

}
