using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public LogicSkript logic;

    public BirdScript bird;
    // Start is called before the first frame update
    void Start()
    {
        logic= GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicSkript>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out");
        if (other.gameObject.layer == 3)
        {
            Debug.Log("Out of Bounds2");
            logic.GameOver();
            bird.birdIsAlive = false;
        }
    }
}
