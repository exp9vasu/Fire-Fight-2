using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] fire;
    public int counter;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1)
        {
            //for (int i=0; i< 20; i++)
            //{
            //    fire[i].SetActive(true);
            
            //}
            
            fire[counter].SetActive(true);
            counter++;

            timer = 0;
        }
    }
}
