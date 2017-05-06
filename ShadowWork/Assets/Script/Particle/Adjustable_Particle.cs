using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Adjustable_Particle : MonoBehaviour {
	[System.SerializableAttribute]
	public struct MaterialPara{
		public float start_FallOff;
		public float Max_FallOff;
		public float Start_Height;
		public float End_Height;
	}
	[SerializeField] MaterialPara matrialPara;
	[SerializeField]float ratio;
	float Start_PosHeight;
	float height_Scale;
	[SerializeField] float Height_offset;
	Material mat;
	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer>().material;
		matrialPara.start_FallOff = mat.GetFloat("_shadowfalloff");
		matrialPara.Start_Height = mat.GetFloat("_shadowheight");
		Start_PosHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Height_offset = (transform.position.y - Start_PosHeight);
		float height = Mathf.Pow(0.5f, Height_offset);
		mat.SetFloat("_shadowheight", Mathf.Clamp(height * ratio,0,10));
	}
}
