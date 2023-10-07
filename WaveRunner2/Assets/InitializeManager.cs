using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeManager : MonoBehaviour
{
    // Spawn Transforms
    [SerializeField] private Transform[] startingPositions;

    private static Transform[] spawns;


    [SerializeField] private Animator CountDownAnim;
    void Awake()
    {
        spawns = startingPositions;
    }

    // Start is called before the first frame update
    void Start()
    {
        CountDownAnim.SetTrigger("Count");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
