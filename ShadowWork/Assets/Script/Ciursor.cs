using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciursor : MonoBehaviour {
	public Texture2D texture;
	// Use this for initialization
	void Start () {
		Cursor.SetCursor(texture, new Vector2(0.0f,0.0f), CursorMode.Auto);
	}
}
