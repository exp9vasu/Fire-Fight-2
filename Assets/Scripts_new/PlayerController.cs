using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject Camera;
    public float time;
    public bool prine;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteMoveUp(5));
    }

    // Update is called once per frame
    void Update()
    {
        if (!prine)
            return;        

        transform.Translate(0, Mathf.Lerp(0, 30, time), 0);
        Camera.transform.Translate(0, Mathf.Lerp(0,30,time), 0);

        if (transform.position.y < 190)
        {
            time += 0.0008f * Time.deltaTime;
        }
        else if(transform.position.y > 190)
        {
            time += 0.0002f * Time.deltaTime;
        }
    }

    IEnumerator ExecuteMoveUp(float Time)
    {
        yield return new WaitForSeconds(Time);

        prine = true;
        Camera.GetComponent<Animator>().enabled = false;
        
        StartCoroutine(ExecuteMoveUp(5));
    }
}
