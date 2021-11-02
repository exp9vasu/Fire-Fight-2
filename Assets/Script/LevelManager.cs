using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
	public bool  test;
	public int level;
    public float transitionSeed = 1f;


    public int[] NoStudent;
	
	public bool[] secondTeacher;

	public bool[] enableCCTV;

    [Header("Camera Position")]
    public Vector3[] CCTVPos;

	[Header("STUDENTS")]
	public float[] speed;
	
	[Header("MAIN TEACHER")]
	public bool[] staticTeacher;
	public float[] radius;
	public float[] movementSpeed;
	
	[Header("SECOND TEACHER")]
	public bool[]  second_staticTeacher;
	public float[] second_radius;
	public float[] second_movementSpeed;
	
	[Header("GAMEOBJECT")]
	public GameObject[] Students;
	public GameObject MainTeacher;
	public GameObject SecondTeacher;
	public GameObject CCTV;

    public Transform mainTeacherCamPos;
    public Transform mainTeacherLookAt;

    public List<int> inRoomStudents = new List<int>();
    public bool levelFinised = false, lv = false;
    // Start is called before the first frame update
    void Awake()
	{
        levelFinised = false;

        if (!test)
			level = PlayerPrefs.GetInt("LEVEL",1);
		
	    if(level > NoStudent.Length)
	    {
	    	level = 1;
	    }
	    PlayerPrefs.SetInt("LEVEL",level+1);
	    ActiveStudent();
		if(secondTeacher[level-1] == true)
	    {
	    	SecondTeacher.GetComponent<TeacherController>().movement_speed = second_movementSpeed[level - 1];
		    SecondTeacher.GetComponent<FieldOfView>().viewRadius = second_radius[level-1]; 
		    SecondTeacher.GetComponent<TeacherController>().StaticTeacher = second_staticTeacher[level-1];
	    	SecondTeacher.SetActive(true);
	    }
		if(enableCCTV[level-1] == true)
	    {
            int cctvPos = Random.Range(0, 3);

            if(cctvPos == 0)
            {
                CCTV.transform.position = CCTVPos[cctvPos];
                CCTV.SetActive(true);
            }
            if(cctvPos == 1)
            {
                CCTV.transform.position = CCTVPos[cctvPos];
                CCTV.transform.rotation = Quaternion.Euler(0,90,0);
                CCTV.SetActive(true);
            }
            if (cctvPos == 2)
            {
                CCTV.transform.position = CCTVPos[cctvPos];
                CCTV.transform.rotation = Quaternion.Euler(0, -90, 0);
                CCTV.SetActive(true);
            }

        }
	    
	    MainTeacher.GetComponent<TeacherController>().movement_speed = movementSpeed[level - 1];
	    //MainTeacher.GetComponent<FieldOfView>().viewRadius = radius[level-1]; 
	    MainTeacher.GetComponent<TeacherController>().StaticTeacher = staticTeacher[level-1];
	    
	    
	    for(int i = 0; i<Students.Length;i++)
	    {
	    	Students[i].transform.GetChild(0).GetComponent<PathCreation.Examples.PathFollower>().movementSpeed = speed[level-1];
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
        //when all student escaped focus camera toward students and then apply Confetti effect
        if (GameManager.instance.student.Count == inRoomStudents.Count && !levelFinised)
        {
            lv = true;
            Instantiate(GameManager.instance.completeLevelEffectParticale, transform.position, Quaternion.identity);
            MainTeacher.gameObject.GetComponent<MeshRenderer>().enabled = false;
            SecondTeacher.gameObject.GetComponent<MeshRenderer>().enabled = false;

            

            if (enableCCTV[level - 1] == true)
            {
                CCTVCam.instance.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            levelFinised = true;
        }
        if (lv)
        {
            TeacherController.instance.movement_speed = 0;
            TeacherController.instance.speed = 0;
            TeacherController.instance.anime.SetBool("Angry", true);
            //TeacherController.instance.currentTargetRotation = null;
        }
        
    }

    private void LateUpdate()
    {
        if (GameManager.instance.student.Count == inRoomStudents.Count)
        {
            StartCoroutine(cameraTransition(1));
        }
    }
    IEnumerator cameraTransition(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.instance.mainCamera.transform.position = Vector3.Lerp(GameManager.instance.mainCamera.transform.position, mainTeacherCamPos.position, Time.deltaTime * transitionSeed);
        //if(GameManager.instance.mainCamera.transform.position== mainTeacherCamPos.position)
            GameManager.instance.mainCamera.transform.LookAt(mainTeacherLookAt);
    }



    public void ActiveStudent()
	{
	
		int active  = NoStudent[level -1];
		GameObject[] temp_GameObject = new GameObject[active];
		
		for(int i=0;i< active;i++)
		{
			bool found = false;
			int temp_no = Random.Range(0,Students.Length);
			for(int j =0 ; j< inRoomStudents.Count ;j++)
			{	//Debug.Log("qqqqqqqq");
				if(temp_no == inRoomStudents[j])
				{
					found = true;
					i--;
				}
			}
			if (!found)
			{
				inRoomStudents.Add(temp_no);
				Students[temp_no].SetActive(true);
			}
		}
		
	}
    
}
