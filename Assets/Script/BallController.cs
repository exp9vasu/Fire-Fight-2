using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 15;

	public  Color solveColor;
	public List<GameObject> point;
	public 	GameObject prev_Gameobject;
	public GameObject player;
	int pos;
	public bool move;
	public LayerMask mask;
    private void Start()
	{
		prev_Gameobject = gameObject;
	 
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up/2), .05f);
        int i = 0;
        while (i < hitColliders.Length)
        {
        	if(hitColliders[i].transform.gameObject.tag == "GROUND")
        	{
	            GroundPiece ground = hitColliders[i].transform.GetComponent<GroundPiece>();
				
		        if (!ground.isColored )
	            {
		            ground.Colored(solveColor);
			        prev_Gameobject = ground.gameObject;
			        point.Add(ground.gameObject);
		            // gamemanager.LightVib();
	            }

	        
        	}
	        i++;
        }
	
	    if(move)
	    {
	    	player.transform.position = Vector3.MoveTowards(player.transform.position,point[pos].transform.position,speed*Time.deltaTime);
		    if(0.2f > Vector3.Distance(player.transform.position,point[pos].transform.position))
		    {
		    	pos = pos +1;
		    	if(pos > point.Count-1)
		    	{
		    		move = false;
		    	}
		    }
	    }
	    else
	    {
		    if (Input.GetMouseButtonDown(0))
		    {
			    RaycastHit hitInfo;
			    GameObject temp_target =   ReturnClickedObject(out hitInfo);
			    
			    if(temp_target != null)
				    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,new Vector3(temp_target.transform.position.x,gameObject.transform.position.y,temp_target.transform.position.z),30);
			   
		    }
		    if (Input.GetMouseButton(0))
		    {
		    	
		    	RaycastHit hitInfo;
			    GameObject temp_target =   ReturnClickedObject(out hitInfo);
			    
			    if(temp_target != null)
			    {
			    
				    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,new Vector3(temp_target.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z),30);
				    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,new Vector3( gameObject.transform.position.x,gameObject.transform.position.y,temp_target.transform.position.z),30);
	
			    }
		    }
		    if (Input.GetMouseButtonUp(0))
		    {
			    Debug.Log("UP");
			    move = true;
		    }
	    }
	
	
    }
	GameObject ReturnClickedObject(out RaycastHit hit)
	{
	
		GameObject targetObject = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction * 10, out hit,Mathf.Infinity ))
		{
			
			if(hit.transform.gameObject.layer == 10)
			{
				targetObject = hit.transform.gameObject;
			}
			
			
		}
		return targetObject;
	}
}
