// 2017 IndieChest All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessLayer))]
public class FocusEye : MonoBehaviour {


	[SerializeField] PostProcessProfile _pp;
	private float _focusDistance = 0.2f;




	private GameObject _focusTo;
	private RaycastHit _hit;


	private DepthOfField _dOF;




	public float _speed;





	void Start () {
        _pp.TryGetSettings(out _dOF);
		_dOF.enabled.value = true;
		_focusTo = new GameObject ("focusTo");

	}
	
	// Update is called once per frame
	void Update () {
		_dOF.focusDistance.value = _focusDistance;

		_focusDistance = Vector3.Distance (this.transform.position, _focusTo.transform.position);
	}

	void FixedUpdate(){
	
		Vector3 fwd = transform.TransformDirection(Vector3.forward);



		if (Physics.Raycast (transform.position, fwd, out _hit, 1000)) {
			
			_focusTo.transform.position = Vector3.MoveTowards (_focusTo.transform.position, _hit.point, _speed * Time.fixedUnscaledDeltaTime);
		}
	
	}
}
