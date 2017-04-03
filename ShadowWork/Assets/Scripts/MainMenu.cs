using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Player player;

	public Text scoreLabel;

	private void Awake () {
		Application.targetFrameRate = 1000;
	}

	public void StartGame (int mode) {
		player.StartGame(mode);
		gameObject.SetActive(false);
		Cursor.visible = false;
	}

	public void EndGame (float distanceTraveled) {
		scoreLabel.text = ((int)(distanceTraveled * 10f)).ToString();
		gameObject.SetActive(true);
		Cursor.visible = true;
	}
}