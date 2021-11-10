using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tabtale.TTPlugins;
using UnityEngine.UI;
using TMPro;


public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    public TMP_Text CounterText;
    public float timeCounter;

	public GameObject AssaultRifleMain, rayCastobj, fireBoy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        TTPCore.Setup();

        Debug.Log("CLIK done");
    }
    

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 15;
    }

    // Update is called once per frame
    void Update()
	{
        if (timeCounter >= 0)
        {
            timeCounter -= 1 * Time.deltaTime;
        }

        CounterText.text = Mathf.RoundToInt(timeCounter).ToString();

        if (RayCastScript.instance.GameOver)
        {
            StartCoroutine(ExecuteAfterTime(10));
            
        }

        if (timeCounter <= 0 && !RayCastScript.instance.FireOffBoy)
        {
            
            RayCastScript.instance.timeCounter.SetActive(false);
            //CounterText.enabled = false;
            fireBoy.GetComponent<Animator>().enabled = true;
            RayCastScript.instance.HelpText .SetActive(false);

        }
        
		//Debug.Log("Time:"+ Time.time);
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