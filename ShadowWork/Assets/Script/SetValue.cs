using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetValue : MonoBehaviour {
	private Slider m_slider;
	[SerializeField] AnimTimeControl animeTimeControl;
	// Use this for initialization
	void Start () {
		m_slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	public void setValue()
	{
		animeTimeControl.SetAnimeFrame(m_slider.value);
	}
}
