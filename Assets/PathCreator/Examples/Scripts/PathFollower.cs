
  
using UnityEngine;

namespace PathCreation.Examples
{
	// Moves along a path at constant speed.
	// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
	public class PathFollower : MonoBehaviour
	{
		public PathCreator pathCreator;
		public EndOfPathInstruction endOfPathInstruction;
		public float speed = 5;
		public float distanceTravelled;
		public bool onFOV,onPointerUP,finish;
		
		
		public Color normal_col,running_col;
		public GameObject  Object;
		public int vertices;
		public LineRenderer path;
		
		
		public Animator anim;
		public Vector3 initail_pos;
		public TeacherController teacher;
		public float movementSpeed;
		GameObject temp_canvas;
		void Start() {

            if (pathCreator != null)
			{
				path = gameObject.GetComponent<LineRenderer>();
			
				//	GameObject.fi
				// Subscribed to the pathUpdated event so that we're notified if the path changes during the game
				pathCreator.pathUpdated += OnPathChanged;
			}
			initail_pos  = transform.position;
		
		}

		int current_vertices = 0;
		
		
		void CraetePath(int j)
		{
			vertices = j+5;
			while( current_vertices != j)
			{
				path.SetPosition(current_vertices,new Vector3 (0,-50,0));
				current_vertices ++;
			}
		}
		void Update()
		{

            if (finish)
            {
                anim.SetBool("Run", false);
                if (!GameManager.instance.student.Contains(gameObject))
                {
                    GameManager.instance.student.Add(gameObject);
                }
                return;
            }
				
			//	path.SetPosition(0,gameObject.transform.localPosition);
			if (pathCreator != null)
			{
				
			
				if(pathCreator.path.next_point != vertices && speed > 3)
				{
					CraetePath(pathCreator.path.next_point);
				}
				
				
				distanceTravelled += speed * Time.deltaTime;
				if(speed != 0)
				{
					transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
					Object.transform.localPosition =  Vector3.zero;
				}
				
				
				Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
				rotation.x = 0;
				rotation.z = 0;
				//
				//	Debug.Log(rotation.y);
				if(speed <0  &&  rotation.y < 0.9f)
				{
				
					if(rotation.y < 0.1f )
					rotation.y = 180 -rotation.y;
					else
						rotation.y = -rotation.y;
				}
				
				else if( speed == 0 && !onFOV && !finish)
				{
					//	Time.timeScale = 0;
					
					rotation.y =0;
					transform.localEulerAngles = Vector3.zero;
				}
				
				transform.rotation =  Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 20);
			
	          
			}
			if(onFOV)
			{
				if(distanceTravelled <= 0.3)
				{
					if(teacher != null)
					{
					if(teacher.currentTargetRotation.name == transform.name)
						teacher.FaceStudentEnd();
					}
					
					teacher = null;
					//Stop();
					onFOV = false;
                    anim.SetBool("Run", false);
                    distanceTravelled = 0;
					gameObject.layer = 8;
					speed = 0;
					transform.position = initail_pos;
					Object.transform.localPosition =  new Vector3 ( 0,0.2f,0);
					gameObject.transform.parent.gameObject.tag = "Untagged";
					gameObject.transform.parent.gameObject.layer = 0;
					//	anim.SetBool("IDLE",true);
					//	SetFalse("IDLE");
					//	gameObject.GetComponent<PathFollower>().enabled = false;
				}
			}
			else if ( onPointerUP && (distanceTravelled < 0.3f))
			{
				gameObject.layer = 8;
				onPointerUP = false;
				Object.GetComponent<MeshRenderer>().material.color = normal_col;
			
			}
			else
			{
				if(distanceTravelled  >= pathCreator.path.length -0.1f)
				{
					
					//	score = GameObject.FindGameObjectWithTag("SCORE");

////////////////////       if we reach to door just spwan +10 score & dissapear after 1 sec       ///////////////////////////////
					temp_canvas = Instantiate(GameManager.instance.scoreObj,initail_pos,Quaternion.identity);
                    Destroy(temp_canvas, 2.5f);
					/*temp_canvas.transform.SetParent(gameObject.transform.GetChild(0).transform);
					temp_canvas.transform.localPosition = new Vector3 (0,7,0);
					temp_canvas.transform.localScale = new Vector3(1,1,1);
					temp_canvas.transform.localEulerAngles = Vector3.zero;
					temp_canvas.SetActive(true);*/
					
					
					gameObject.layer = 0;
					onPointerUP = false;
					gameObject.GetComponent<LineRenderer>().enabled = false;
					pathCreator.gameObject.SetActive(false);
				
					//	anim.SetBool("REACHED",true);
					//	SetFalse("REACHED");
					
					//	GameObject.FindGameObjectWithTag("Player").GetComponent<StudentSelector>().NextStudent();
					finish = true;
				}
			}
			
			
			//anim.SetFloat("SPEED",speed);
			
		}
        
		
		// If the path changes during the game, update the distance travelled so that the follower's position on the new path
		// is as close as possible to its position on the old path
		void OnPathChanged() {
			distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
			//	vertices ++;
		}
		
		
		public void Stop()
		{
            gameObject.GetComponent<LineRenderer>().enabled = false;
			pathCreator.gameObject.SetActive(false);
			speed = -movementSpeed;//-14;
			//	Object.GetComponent<MeshRenderer>().material.color = normal_col;
			onFOV = true;
			if(teacher != null)
			teacher.speed = 50;
			vertices = 0;
			current_vertices = 0;
            
            //	anim.SetBool("STOP",true);
            //	SetFalse("STOP");
        }
		
		public void OnClick()
		{

            //gameObject.GetComponent<LineRenderer>().enabled = true;
            //call animation for run
            anim.SetBool("Run", true);



			pathCreator.gameObject.SetActive(true);
			//	pathCreator.gameObject.GetComponent<Path>().Showpath();
			gameObject.transform.parent.gameObject.tag = "EMPTY";
			gameObject.transform.parent.gameObject.layer = 9;
			//	Object.GetComponent<MeshRenderer>().material.color = running_col;
			gameObject.layer = 9;
			speed = movementSpeed ; // 14
			//	anim.SetBool("RUN",true);
			//	SetFalse("RUN");
		}
		
		
		public void SetFalse(string clipname)
		{
			if(clipname != "RUN")
			{
				anim.SetBool("RUN",false);
			}
			if(clipname != "STOP")
			{
				anim.SetBool("STOP",false);
			}
			if(clipname != "REACHED")
			{
				anim.SetBool("REACHED",false);
			}
			if(clipname != "IDLE")
			{
				anim.SetBool("IDLE",false);
			}
		}

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Door"))
            {
                Debug.Log("Door Detected");
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                Debug.Log("Door Detected");
            }
        }

        //public void OnpointerDown()
        //{
        //	Object.GetComponent<MeshRenderer>().material.color = running_col;
        //	gameObject.layer = 9;
        //	speed = 5;

        //}

        //public void OnPointerUp()
        //{
        //	onPointerUP = true;
        //	speed = -5;
        //}

    }
    
}
