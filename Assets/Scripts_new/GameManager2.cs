using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager2 : MonoBehaviour
{
	public GameObject AssaultRifleMain, rayCastobj;
	
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (RayCastScript.instance.GameOver)
        {
            StartCoroutine(ExecuteAfterTime(10));
            
        }  	
	}
    

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        AssaultRifleMain.SetActive(false);
    }
}