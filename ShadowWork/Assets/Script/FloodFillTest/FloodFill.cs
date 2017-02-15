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

		floodFill(tex2D.width/2,tex2D.height/2);
		tex2D.Apply();
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
		
		if(tex2D.GetPixel(pixel_x+1,pixel_y) != targetColor && pixel_x < tex2D.width)
		{
			floodFill(pixel_x+1,pixel_y);
		}
		if(tex2D.GetPixel(pixel_x,pixel_y+1) != targetColor && pixel_y < tex2D.width)
		{
			floodFill(pixel_x,pixel_y+1);
		}
		if(tex2D.GetPixel(pixel_x-1,pixel_y) != targetColor && pixel_x > 0)
		{
			floodFill(pixel_x-1,pixel_y);
		}
		if(tex2D.GetPixel(pixel_x,pixel_y-1) != targetColor && pixel_y > 0)
		{
			floodFill(pixel_x,pixel_y-1);
		}
	}

	private void floodFill_ScanLine(int pixel_x, int pixel_y)
	{
		
	}
}
