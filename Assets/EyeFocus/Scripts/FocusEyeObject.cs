// 2017 IndieChest All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessLayer))]
public class FocusEyeObject : MonoBehaviour {


    [SerializeField] PostProcessProfile _pp;
    private float _focusDistance = 0.2f;




	public GameObject _focusTo;



	private DepthOfField _dOF;



	public float _speed = 10;





	void Start () {
        _pp.TryGetSettings(out _dOF);

		_dOF.enabled.value = true;

	}

	// Update is called once per frame
	void Update () {
		_dOF.focusDistance.value = _focusDistance;

		_focusDistance = Vector3.Distance (this.transform.position, _focusTo.transform.position);
	}
		
}
