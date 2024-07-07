using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float timerate = 3;
    private float timer = 0;
    private float pipeoffset= 3;
    // Start is called before the first frame update
    void Start()
    {
        timer = timerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer<timerate)
        {
            timer += Time.deltaTime;
        }else
        {
            spawnPipe();
            timer = 0;
        }
    }

    //more functions
    void spawnPipe()
    {
        float lowestPoint= transform.position.y - pipeoffset;
        float highestPoint = transform.position.y + pipeoffset;
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), transform.rotation);
    }


}
