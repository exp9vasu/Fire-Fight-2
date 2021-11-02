using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//ity

public class StudentSelector : MonoBehaviour
{
	public LayerMask mask;
	
	//public List<GameObject> students;
	//public GameObject current_students;
	//int current_student_no;
	
	//public int control_type;
	//public Text control_txt;
    // Start is called before the first frame update
    void Start()
	{
	
		//current_student_no = 0;
		//current_students = students[current_student_no];
		
		//if(control_type == 2 || control_type == 3)
		//	current_students.GetComponent<LineRenderer>().enabled = true;
		
		//current_student_no ++;
		
		//if(control_type == 1)
		//	control_txt.text = "TAP on  player for player  moving";
		//if(control_type == 2)
		//	control_txt.text = "TAP for  player moving";
		//if(control_type == 3)
		//	control_txt.text = "TAP and HOLD   player moving";
    }

    // Update is called once per frame
    void Update()
	{
		//if(control_type == 1)
		//{
		    if (Input.GetMouseButtonDown(0))
		    {
			    RaycastHit hitInfo;
			    ReturnClickedObject(out hitInfo);  
			   
		    }
		//	}
		//else if(control_type == 2)
		//{
		//	if (Input.GetMouseButtonDown(0))
		//	{
		//		if(current_students.GetComponent<PathCreation.Examples.PathFollower>().onFOV)
		//			return;
					
		//		current_students.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
		//		current_students.GetComponent<PathCreation.Examples.PathFollower>().OnClick();
		//	}
		//}
		//else if (control_type == 3)
		//{
		//	if (Input.GetMouseButtonDown(0))
		//	{
		//		if(current_students.GetComponent<PathCreation.Examples.PathFollower>().onFOV)
		//			return;
			
		//		current_students.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
		//		current_students.GetComponent<PathCreation.Examples.PathFollower>().OnpointerDown();
		//	}
			
			
		//	if (Input.GetMouseButtonUp(0))
		//	{
		//		if(current_students.GetComponent<PathCreation.Examples.PathFollower>().onFOV)
		//			return;
					
		//		current_students.GetComponent<PathCreation.Examples.PathFollower>().OnPointerUp();
			
		//	}
		//}
    }
    
	GameObject ReturnClickedObject(out RaycastHit hit)
	{
	
		GameObject targetObject = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction * 10, out hit,Mathf.Infinity , mask))
		{
			//	Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hit.distance, Color.yellow);
			//Debug.Log(hit.transform.name);
			if(hit.transform.gameObject.layer == 8)
			{
				hit.transform.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
				hit.transform.GetComponent<PathCreation.Examples.PathFollower>().OnClick();
				//	hit.transform.GetComponent<LineRenderer>().enabled = true;
			}
			
			
		}
		return targetObject;
	}
	
	//public void NextStudent()
	//{	if(control_type == 1)
	//	return;
	
	
	//	current_students.GetComponent<LineRenderer>().enabled = false;
	//	if(current_student_no > students.Count - 1)
	//		return;
			
	//	current_students = students[current_student_no];
	//	current_students.GetComponent<LineRenderer>().enabled = true;
	//	current_student_no ++; 
	//}
	//public void ChangeControl()
	//{
		
	//	control_type = control_type +1;
	//	if(control_type >3)
	//	{
	//		control_type = 1;
	//	}
		
	//	if(control_type == 1)
	//		control_txt.text = "TAP on  player for player  moving";
	//	if(control_type == 2)
	//		control_txt.text = "TAP for  player moving";
	//	if(control_type == 3)
	//	control_txt.text = "TAP and HOLD   player moving";
		
	//	if(control_type == 1)
	//		return;
	//	current_students.GetComponent<LineRenderer>().enabled = true;
	//	}
	
}
