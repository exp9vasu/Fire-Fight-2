using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adaptationSlider : MonoBehaviour {
	private FocusEye _focusEye;
	private float _speed;
	// Use this for initialization
	void Start () {
		if (Camera.main.GetComponent<FocusEye> () != null) {
			_focusEye = Camera.main.GetComponent<FocusEye> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.B)){
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
		}
	}


	public void SetSpeed(float s){
	
		_speed = s;
		if(_focusEye !=null)
		_focusEye._speed = _speed;
	}

	public void GeneralScene(){

		UnityEngine.SceneManagement.SceneManager.LoadScene ("General Focus Scene");
	
	}

	public void ObjectScene(){

		UnityEngine.SceneManagement.SceneManager.LoadScene ("Object Focus Scene");

	}


}
