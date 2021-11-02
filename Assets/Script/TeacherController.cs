using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    public static TeacherController instance;
	public Transform board_target;
	public Transform classroom_target,left_target,right_target,middle_target;
	public Transform student_target; 
	public bool findStudent;
	
	public Transform currentTargetRotation,prev_TargetRotation;
	public float speed, movement_speed;



	public bool timer;
	public float duration;
	
	public FieldOfView fov;
	float fov_target_angle;
	float fob_target_radius;
	float initial_fob_target_radius;
	int i = 0;

     
    public Color normal_fov_col;
	public Color target_fov_col;
	public Color empty_brench_col;
	Color current_target_fov_col;
	public MeshRenderer fov_mesh;
	
	
	
	
	public bool faceEmptyBrench;
	public Vector3 brenchPos;
	public Vector3 currentTargetPosition;
	public GameObject OnEmptyChair_Expression, OnStudentLeaving_Expresion;
	
	
	public bool backToInitialPos;
	public Vector3 initialPosition;
	public GameObject EmptyGameObject;
	
	public bool initialmovement;
	public GameObject rightPos;
	public GameObject leftPos;
	int posNo;
	
	public bool StaticTeacher;
    public Animator anime;
	//public string status;
	//public Mesh mesh;
    // Start is called before the first frame update
    void Start()
	{
        instance = this;
        findStudent = true;
		posNo =0;
		i =0;
		OnTargetPositionChange();
	    FaceBoard();
	    currentTargetPosition = gameObject.transform.position;
	    initialPosition = gameObject.transform.position;
		// movement_speed = speed;
	    
	    initial_fob_target_radius =  fov.viewRadius;
		fob_target_radius = 10;
		
		
    }


    void Update()
    {

        if (timer)
        {
            duration -= 1 * Time.deltaTime;
            if (duration <= 0)
            {
                timer = false;
                OnTargetChange();
            }

        }

        
        else
        {
            anime.SetBool("Walk", false);
        }
        /*
        if(currentTargetPosition == new Vector3(brenchPos.x, gameObject.transform.position.y, gameObject.transform.position.z))
        {
            anime.SetBool("Walk", true);
            
        }
        else
        {
            anime.SetBool("Angry", false);
        }*/


        //Color lerpedColor = Color.Lerp(	fov_mesh.material.color, current_target_fov_col,  movement_speed *Time.deltaTime);
        //fov_mesh.material.color = lerpedColor;


        ///////////////////////////////////////////////////////////////////////////////
        //fov.viewAngle = Mathf.Lerp(fov.viewAngle,fov_target_angle,movement_speed *Time.deltaTime);
        //fov.viewRadius = Mathf.Lerp (fov.viewRadius,fob_target_radius,movement_speed*Time.deltaTime);
        //////////////////////////////////////////////////////////////////////////////


        Vector3 relativePos = currentTargetRotation.position - transform.position;

		// the second argument, upwards, defaults to Vector3.up
		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
		rotation.x = 0;
		rotation.z = 0;
		transform.rotation =  Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed);
		
		if(!initialmovement )
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position , new Vector3(currentTargetPosition.x,gameObject.transform.position.y,currentTargetPosition.z) ,(movement_speed*2)*Time.deltaTime );
				
		if(faceEmptyBrench)
		{
            anime.SetBool("Walk", true);
            fov_target_angle = 20;
			fob_target_radius = Vector3.Distance(gameObject.transform.position,currentTargetRotation.position);
			//	Debug.Log(Vector3.Distance(gameObject.transform.position,brenchPos));
			if( 0.2f > Vector3.Distance(gameObject.transform.position,currentTargetPosition))
			{
				currentTargetPosition.z = brenchPos.z;
			}
			if(0.5f > Vector3.Distance(gameObject.transform.position,brenchPos) )
			{
                //	Debug.Log("EMPTY BRENCH ");
                anime.SetBool("Angry", true);
                Invoke("FaceEmptyBrenchEnd",3f);
				faceEmptyBrench = false;
			}
            
        }
		else if (backToInitialPos)
		{
            anime.SetBool("Angry",false);
            anime.SetBool("Walk", true);
            EmptyGameObject.transform.position = currentTargetPosition;
			currentTargetRotation = EmptyGameObject.transform;
			//	fob_target_radius =0;// Vector3.Distance(gameObject.transform.position,currentTargetRotation.position);
			if( 0.2f > Vector3.Distance(gameObject.transform.position,currentTargetPosition))
			{
				currentTargetPosition.x = initialPosition.x;
				
				if(1f > Vector3.Distance(gameObject.transform.position,initialPosition))
				{
					
					backToInitialPos = false;
					//timer = true;
					Invoke("OnTargetChange",0.2f);
				}
			}
			
		}
		else if (initialmovement )
		{
			if(!StaticTeacher)
			{	
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position , new Vector3(currentTargetPosition.x,gameObject.transform.position.y,currentTargetPosition.z) ,(movement_speed)*Time.deltaTime );
			
				if( 0.2f > Vector3.Distance(gameObject.transform.position,currentTargetPosition))
				{
					initialmovement = false;
					Invoke("OnTargetPositionChange",0.5f);
				}
			}
			else
			{
				currentTargetRotation = board_target;
			}
		}


        
	}
	
	public void OnTargetChange()
	{	speed = 3;
		CancelInvoke("OnTargetPositionChange");
		if(duration >0)
		{
			timer = true;
		
			if(i<=1)
			{
				fob_target_radius =3;
				OnEnableInitialMovememnt();
			}
			else
			{
				fob_target_radius =initial_fob_target_radius;
				currentTargetRotation = prev_TargetRotation;
			}
			return;
		}
		
		fov_mesh.material.color = normal_fov_col;
		switch (i)
		{
		case 0:
			FaceBoard();
			break;
		case 1:

			LookTargetLeft();
			break;
		case 2:
			LookTargetMiddle();
			break;
		case 3:
			LookTargetRight();
			break;
		//case 4:
		//	LookTargetRight();
		//	break;
		default :
			i= 0;
			FaceBoard();
			break;
		
		
		}
		if(i!=0)
		{
			fob_target_radius =initial_fob_target_radius;
			speed = 3;
			findStudent = true;
			//	initialmovement = false;
		}
		
	}
	
	public void OnTargetPositionChange()
	{
		//if()
		OnEnableInitialMovememnt();
		findStudent = true;
		switch (posNo)
		{
		case 0:
            anime.SetBool("Walk", true);
			TowardRightPosition();
			break;
		case 1:
            anime.SetBool("Walk", true);
            TowardLeftPosition();
			break;
		default :
			posNo= 0;
            anime.SetBool("Walk", true);
            TowardRightPosition();
			break;
		}
		
	}
	
	public void TowardRightPosition()
	{
		currentTargetPosition = rightPos.transform.position;
		currentTargetRotation = rightPos.transform;
		posNo = posNo +1;
	}
	public void TowardLeftPosition()
	{
		currentTargetPosition = leftPos.transform.position;
		currentTargetRotation = leftPos.transform;
		posNo = posNo +1;
	}
	
	public void FaceBoard()
	{
        anime.SetBool("Walk", false);
        fov_target_angle = 10;
		//	currentTargetRotation = board_target;
		i++;
		duration = 5.0f;
		timer = true;
		OnEnableInitialMovememnt();
		
	}
    
	public void FaceClassroom()
	{
        anime.SetBool("Walk", false);
        OnDisableInitialMovememnt();
		current_target_fov_col = normal_fov_col;
		fov_target_angle = 60;
		currentTargetRotation = classroom_target;
		i++;
		duration = 3.0f;
		timer = true;
		
	}
	
	public void LookTargetLeft()
	{
        anime.SetBool("Walk", false);
        OnDisableInitialMovememnt();
		current_target_fov_col = normal_fov_col;
		fov_target_angle = 20;
		currentTargetRotation = left_target;
		i++;
		duration = 3.0f;
		timer = true;
	
	}
	
	public void LookTargetMiddle()
	{
        anime.SetBool("Walk", false);
        OnDisableInitialMovememnt();
		current_target_fov_col = normal_fov_col;
		fov_target_angle = 20;
		i++;
		currentTargetRotation = middle_target;
		duration = 3.0f;
		timer = true;
		OnDisableInitialMovememnt();
	}
	
	public void LookTargetRight()
	{
        anime.SetBool("Walk", false);
        OnDisableInitialMovememnt();
		current_target_fov_col = normal_fov_col;
		fov_target_angle = 20;
		i++;
		currentTargetRotation = right_target;
		duration = 3.0f;
		timer = true;
		OnDisableInitialMovememnt();
	}
	
	public void OnDisableInitialMovememnt()
	{
		currentTargetPosition = gameObject.transform.position;
		initialmovement = false;
	}
	
	public void OnEnableInitialMovememnt()
	{
		
		
		fob_target_radius = 5;
		fov_target_angle = 15;
		if(!StaticTeacher)
			initialmovement = true;
		else
			initialmovement = false;
		
	}
	
	public void FaceStudent( Transform target)
	{	
		prev_TargetRotation = currentTargetRotation;
		OnDisableInitialMovememnt();
		fov_target_angle = 10;
		StartCoroutine(Blink(target_fov_col));
		OnStudentLeaving_Expresion.GetComponent<Expression>().EnableScaleUp();
		currentTargetRotation =  target;
		timer = false;
	}
	
	public void FaceStudentEnd ()
	{	
		
		OnStudentLeaving_Expresion.GetComponent<Expression>().EnableScaleDownUP();
		//speed = 3;
		timer = true;
		OnTargetChange();
	}
	
	public void FaceEmptyBrench (Transform target)
	{	
		prev_TargetRotation = currentTargetRotation;
		OnDisableInitialMovememnt();
		findStudent = false;
		timer = false;
		OnEmptyChair_Expression.GetComponent<Expression>().EnableScaleUp();
		StartCoroutine(Blink(empty_brench_col));
		currentTargetPosition = new Vector3 ( brenchPos.x,gameObject.transform.position.y , gameObject.transform.position.z );
		currentTargetRotation = target;
		faceEmptyBrench = true;
    }
	
	public void FaceEmptyBrenchEnd ()
	{	
		timer = false;
		OnEmptyChair_Expression.GetComponent<Expression>().EnableScaleDownUP();
		current_target_fov_col = normal_fov_col;
		faceEmptyBrench = false;
		currentTargetPosition = new Vector3 ( gameObject.transform.position.x,gameObject.transform.position.y , initialPosition.z );
		currentTargetRotation = gameObject.transform;
		backToInitialPos = true;
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
