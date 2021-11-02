using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    public bool isColored = false;
	public List<Color> col;
	
	
    public void Colored(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;

	    //FindObjectOfType<GameManager>().CheckComplete();
    }
    
	void Start()
	{
		//GetComponent<MeshRenderer>().material.color
	}
}
