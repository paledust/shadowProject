using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilding : MonoBehaviour {
	public Color rayColor = Color.white;
	public List<Transform> path_Nodes = new List<Transform>();
	Transform[] TransArray;

	void OnDrawGizmos(){
		Gizmos.color = rayColor;
		TransArray = GetComponentsInChildren<Transform>();
		path_Nodes.Clear();

		foreach(Transform path_Node in TransArray){
			if(path_Node != this.transform){
				path_Nodes.Add(path_Node);
			}
		}

		// for(int i = 0; i < path_Nodes.){

		// }
	}
}
