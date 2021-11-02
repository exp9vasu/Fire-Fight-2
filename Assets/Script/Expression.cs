using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expression : MonoBehaviour
{
	
	//	public bool scaleUp, ScaleDown;
	public Vector3 target;
	float speed;
    // Start is called before the first frame update
    void Start()
    {
	    target = Vector3.zero;
	    speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
	   
	    	gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, target,speed*Time.deltaTime);
	   
    }
    
	public void EnableScaleUp()
	{
		target = new Vector3 (1f,1f,1f);
		speed = 5;
		Invoke("ScaleDown",0.3f);
	}
	public void ScaleDown()
	{
		target = new Vector3 (0.5f,0.5f,0.5f);
		speed = 5;
		Invoke("EnableScaleUp",0.3f);
	}
	
	public void EnableScaleDownUP()
	{
		CancelInvoke();
		speed = 7;
		target = new Vector3 (1.5f,1.5f,1.5f);
		Invoke("EnableScaleDown",0.35f);
	}
	public void EnableScaleDown()
	{  speed = 10;
		target = Vector3.zero;
	}
	
}
