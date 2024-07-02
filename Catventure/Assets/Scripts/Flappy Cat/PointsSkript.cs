using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsSkript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public LogicSkript logic;


    // Start is called before the first frame update
    void Start()
    {
        logic= GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicSkript>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.AddScore(1);
        }
    }
}
