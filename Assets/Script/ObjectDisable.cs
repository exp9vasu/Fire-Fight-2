using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisable : MonoBehaviour
{
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void AnimationDisable()
	{
		gameObject.GetComponent<Animator>().enabled = false;
		
		for(int i =0; i<gameObject.transform.childCount;i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetActive(false);
		}
		gameObject.transform.localScale = new Vector3 (1,1,1);
	}
}
