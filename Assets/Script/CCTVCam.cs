using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVCam : MonoBehaviour
{
    public static CCTVCam instance;
	public Transform currentTargetRotation,right_TargetRotation,left_TargetRotation;
	public float speed, time;

	public Color normal_fov_col;
	public Color target_fov_col;
	public Color empty_brench_col;
	//	Color current_target_fov_col;
	public MeshRenderer fov_mesh;
	
	public GameObject OnEmptyChair_Expression, OnStudentLeaving_Expresion;
	
	
	int i;
	
    void Start()
	{
        instance = this;
		speed = 0;
		OnTargetChange();
    }


	void Update()
	{

		Vector3 relativePos = currentTargetRotation.position - transform.position;

		// the second argument, upwards, defaults to Vector3.up
		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
		rotation.x = 0;
		rotation.z = 0;
		speed = Time.deltaTime /time;
		transform.rotation =  Quaternion.Lerp(transform.rotation, rotation,  speed);
	
	

	}
	
	public void OnTargetChange()
	{
		speed = 0; 
		switch (i)
		{
		case 0:
			TowardRightPosition();
			break;
		case 1:
			TowardLeftPosition();
			break;
	
		default :
			i= 0;
			TowardRightPosition();
			break;
		
		
		}
	}
	

	
	public void TowardRightPosition()
	{
		//speed = 0;
		currentTargetRotation =right_TargetRotation;
		i++;
		Invoke("OnTargetChange",time);
		
	}
	public void TowardLeftPosition()
	{
		//	speed = 0;
		currentTargetRotation = left_TargetRotation;
		i++;
		Invoke("OnTargetChange",time);
	}
	

	public void FaceStudent( Transform target)
	{	
	
		StartCoroutine(Blink(target_fov_col));
		OnStudentLeaving_Expresion.GetComponent<Expression>().EnableScaleUp();
		Invoke("FaceStudentEnd",1f);
	
	}
	
	public void FaceStudentEnd ()
	{	
		
		OnStudentLeaving_Expresion.GetComponent<Expression>().EnableScaleDownUP();
	
	}
	
	
	public void FaceEmptyBrench (Transform target)
	{	
	
		OnEmptyChair_Expression.GetComponent<Expression>().EnableScaleUp();
		StartCoroutine(Blink(empty_brench_col));
	
	}
	
	public void FaceEmptyBrenchEnd ()
	{	
	  
		OnEmptyChair_Expression.GetComponent<Expression>().EnableScaleDownUP();
		
	}
	
	
	public IEnumerator Blink(  Color target_col)
	{
	
		fov_mesh.material.color = target_col;
		yield return new WaitForSeconds(0.15f);
		fov_mesh.material.color = normal_fov_col;
		yield return new WaitForSeconds(0.15f);
		
		fov_mesh.material.color = target_col;
		yield return new WaitForSeconds(0.15f);
		fov_mesh.material.color = normal_fov_col;
		yield return new WaitForSeconds(0.15f);
	 
	
		fov_mesh.material.color = target_col;
		yield return new WaitForSeconds(0.1f);
		fov_mesh.material.color = normal_fov_col;
	
	}

}
