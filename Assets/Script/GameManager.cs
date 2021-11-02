using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Game Objects")]
    public GameObject scoreObj;
    public GameObject completeLevelEffectParticale;
    public GameObject mainCamera;
    

    [Header("Arrays")]
    public List<GameObject> student = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
