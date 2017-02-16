using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateColor : MonoBehaviour {

	public SpriteRenderer render_1;
	public SpriteRenderer render_2;
	private Color color_1;
	private Color color_2;
	// Use this for initialization
	void Start () {
		color_1 = render_1.color;
		color_2 = render_2.color;
	}
	
	// Update is called once per frame
	void Update () {
		color_1 = render_1.color;
		color_2 = render_2.color;

		Vector3 VelColor1 = new Vector3(color_1.r, color_1.g, color_1.b);
		Vector3 VelColor2 = new Vector3(color_2.r, color_2.g, color_2.b);

		VelColor2 = Vector3.Cross(VelColor1,VelColor2).normalized;
		Color tarColor = new Color(VelColor2.x,VelColor2.y,VelColor2.z,1.0f);

		GetComponent<SpriteRenderer>().color = tarColor;
	}
}
