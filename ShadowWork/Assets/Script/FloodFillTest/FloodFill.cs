using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour {
	public Color targetColor;
	public RenderTexture renderTex;
	public Texture2D tex2D;
	public Texture2D targetTex;
	private Camera mainCamera;
	private bool ifFill;
	private int count;

	private int i = 0;
	// Use this for initialization
	void Start () {
		count = 0;
		mainCamera = Camera.main;
		ifFill = false;
		// tex2D = new Texture2D(renderTex.width,renderTex.height);

		// for(int i = 0; i<= tex2D.height ; i++)
		// {
		// 	floodFill(0,i);
		// }
		floodFill(0,0);
		//Graphics.Blit(tex2D,renderTex);
		// tex2D.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height),0,0);
		// tex2D.Apply();
		tex2D.Apply();
		Debug.Log(count);

	}
	
	// Update is called once per frame
	void Update () {
		if(!ifFill)
		{
			//floodFill(tex2D.width/2,tex2D.height/2);
			ifFill = true;
		}
	}

	private void floodFill(int pixel_x,int pixel_y)
	{
		// i ++;
		//Debug.Log(i);
		// if(i <= 250)
		// {
		// 	floodFill(pixel_x,pixel_y);
		// }
		tex2D.SetPixel(pixel_x,pixel_y,targetColor);
		
		if(tex2D.GetPixel(pixel_x+1,pixel_y) != targetColor && pixel_x <= tex2D.width-1)
		{
			floodFill(pixel_x+1,pixel_y);
		}
		if(tex2D.GetPixel(pixel_x,pixel_y+1) != targetColor && pixel_y <= 499)
		{
			floodFill(pixel_x,pixel_y+1);
		}
		if(tex2D.GetPixel(pixel_x-1,pixel_y) != targetColor && pixel_x >= 4)
		{
			floodFill(pixel_x-1,pixel_y);
		}
		if(tex2D.GetPixel(pixel_x,pixel_y-1) != targetColor && pixel_y >= 1)
		{
			floodFill(pixel_x,pixel_y-1);
		}
	}
}
