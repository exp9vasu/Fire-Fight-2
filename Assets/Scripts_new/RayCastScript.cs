using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    public static RayCastScript instance;

    public GameObject Spray, Confetti, boss, boss2, HelpText;
    public RaycastHit hit;
    public GameObject Splash_Prefab;
    public bool GameOver;
	public GameObject GameOverPanel, AssaultRifleMain;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAfterTime(50));
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (GameOver)
        {
            Confetti.SetActive(true);
            boss.GetComponent<Animator>().SetBool("isHit", true);
            HelpText.SetActive(false);
	        //GameOverPanel.SetActive(true);
	       
}

        //Debug.Log(PlayerPrefs.GetInt("WaterShot"));
    }

    public void Shoot()
    {
        //RaycastHit hit;
        if(Physics.Raycast(Spray.transform.position, Spray.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            if (!hit.transform.CompareTag("EMPTY")) 
            { 
                hit.transform.GetComponent<ParticleSystem>().Stop();
                //PlayerPrefs.SetInt("WaterShot", PlayerPrefs.GetInt("WaterShot") + 1);
                
            }

            //Instantiate(Splash_Prefab, hit.transform.position, Quaternion.identity);
            
            //StartCoroutine(ExecuteAfterTime(5));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        GameOver = true;
    }
}
