using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
	public  PathCreation.Examples.PathFollower path;
	public LineRenderer line;
	public Vector3[] pos;
	public GameObject prefab;
	public bool done;
    void Start()
	{
	
		Showpath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void Showpath()
	{
		line.positionCount = 0;
		pos = new Vector3[0];
		
		pos = new Vector3[path.pathCreator.path.bezier_pos.Count];
		for (int i = 0; i < 	path.pathCreator.path.NumPoints; i++) {
                        	
			int nextI = i + 1;
			
			pos[i] = 	path.pathCreator.path.GetPoint (i);
			if(i %2 == 0  && !done)
			{
				Instantiate(prefab,pos[i],Quaternion.identity).gameObject.transform.SetParent(this.transform);
				//done = true;
			}
		}
		line.positionCount = pos.Length;
		line.SetPositions(pos);
		done = true;
		gameObject.SetActive(false);
	}

    internal static string Combine(string result, string p)
    {
        throw new NotImplementedException();
    }

    internal static string GetDirectoryName(string zipFileName)
    {
        throw new NotImplementedException();
    }

    internal static string GetFileName(string relativeDirectory)
    {
        throw new NotImplementedException();
    }
}
