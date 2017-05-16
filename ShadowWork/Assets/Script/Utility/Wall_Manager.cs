using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Manager : MonoBehaviour {
	[SerializeField] List<GameObject> Walls;
	// Use this for initialization
	void Start () {
		Walls = new List<GameObject>();
		for(int i = 0;i<4;i++)
			Walls.Add(Instantiate(Service.prefebList.Wall));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Put_Wall(Vector3 pos){
		GameObject wall = Walls.Find(x => !x.GetComponent<InviWall_Particle>().IF_ON);
		if(wall != null){
			wall.transform.position = pos;
			wall.GetComponent<InviWall_Particle>().SHOW_UP();
			return;
		}
		Walls[0].transform.position = pos;
		Walls[0].GetComponent<InviWall_Particle>().Clear_And_Show();
		return;

	}
}
