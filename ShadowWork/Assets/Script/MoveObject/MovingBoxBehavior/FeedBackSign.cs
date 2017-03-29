using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;

public class FeedBackSign : MonoBehaviour {
	[SerializeField] DirectionOption dirOption;
	[SerializeField] Color DeactColor;
	[SerializeField] Color actColor;
	[SerializeField] Color ErrorColor;
	[SerializeField] DragObjectScript dragObjectScript;
	private bool ifError = false;
	private Task_Manager taskManager = new Task_Manager();
	private BlinkColorTask blinkTask;
	private LightColorTask lightTask;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = DeactColor;
		dragObjectScript = GetComponentInParent<DragObjectScript>();
		directionRegister();
		blinkTask = new BlinkColorTask(this);
		lightTask = new LightColorTask(this, this.actColor);
	}
	
	// Update is called once per frame
	void Update () {
		taskManager.Update();
	}

	private void directionRegister(){
		EventManager.Instance.Register<DirectionCheckEvent>(Active);
		// switch (dirOption)
		// {
		// 	case DirectionOption.left:
		// 		EventManager.Instance.Register<ButtonEvent_Left>(Error);
		// 		break;
		// 	case DirectionOption.right:
		// 		EventManager.Instance.Register<ButtonEvent_Right>(Error);
		// 		break;
		// 	case DirectionOption.up:
		// 		EventManager.Instance.Register<ButtonEvent_Up>(Error);
		// 		break;
		// 	case DirectionOption.down:
		// 		EventManager.Instance.Register<ButtonEvent_Down>(Error);
		// 		break;
		// 	case DirectionOption.forward:
		// 		EventManager.Instance.Register<ButtonEvent_Forward>(Error);
		// 		break;
		// 	case DirectionOption.back:
		// 		EventManager.Instance.Register<ButtonEvent_Back>(Error);
		// 		break;
		// 	default:
		// 		break;
		// }
	}

	//Call When DirectionCheckEvent Fire
	private void lightDirection(){
		if(lightTask.ifDetached){
			lightTask = null;
			lightTask = new LightColorTask(this, this.actColor);
			taskManager.AddTask(lightTask);
		}
		else{
			lightTask.ResetColor(this.actColor);
		}
	}

	//Call When DirectionCheckEvent Fire
	private void OfflightDirection(){
		if(lightTask.ifDetached){
			lightTask = null;
			lightTask = new LightColorTask(this, this.DeactColor);
			taskManager.AddTask(lightTask);
		}
		else{
			lightTask.ResetColor(this.DeactColor);
		}
	}
	public void Active(Event e)
	{
		DirectionCheckEvent tempEvent = e as DirectionCheckEvent;
		if(CheckDirection(tempEvent.dirOption))
		{
			if(tempEvent.ifLight)
				lightDirection();
			else
				OfflightDirection();
		}
	}
	public void Error(Event e){
		if(blinkTask.ifDetached)
		{
			taskManager.AddTask(blinkTask);
		}
	}
	private bool CheckDirection(DirectionOption m_dirOption)
	{
		if(dirOption == m_dirOption)
			return true;
		// else if(m_dirOption == DirectionOption.x && (dirOption == DirectionOption.right || dirOption == DirectionOption.left))
		// 	return true;
		// else if(m_dirOption == DirectionOption.y && (dirOption == DirectionOption.up || dirOption == DirectionOption.down))
		// 	return true;
		// else if(m_dirOption == DirectionOption.z && (dirOption == DirectionOption.forward || dirOption == DirectionOption.back))
		// 	return true;
		else
			return false;
	}


	public class BlinkColorTask: Task{
		FeedBackSign feedBackSign;
		private float timer= 0.0f;
		private Color originalColor;
		public BlinkColorTask(FeedBackSign _feedBackSign)
		{	
			feedBackSign = _feedBackSign;
		}
		protected override void Init()
		{
			timer = 0.0f;
			originalColor = feedBackSign.GetComponent<Renderer>().material.color;
		}
		internal override void TUpdate()
		{
			timer += Time.deltaTime;
			feedBackSign.GetComponent<Renderer>().material.color = Color.Lerp(originalColor, feedBackSign.ErrorColor,Mathf.Sin(timer*2*Mathf.PI));
			if(timer >= 0.5f)
			{
				SetStatus(TaskStatus.Success);
			}
		}
	}
	public class LightColorTask: Task{
		FeedBackSign feedBackSign;
		private float timer= 0.0f;
		private Color targetColor;
		private Color originalColor;
		public LightColorTask(FeedBackSign _feedBackSign, Color _TargetColor)
		{	
			targetColor = _TargetColor;
			feedBackSign = _feedBackSign;
		}
		protected override void Init()
		{
			timer = 0.0f;
			originalColor = feedBackSign.GetComponent<Renderer>().material.color;
		}
		internal override void TUpdate()
		{
			timer += Time.deltaTime;
			feedBackSign.GetComponent<Renderer>().material.color = Color.Lerp(targetColor, targetColor,timer/2.0f);
			if(timer >= 2f)
			{
				SetStatus(TaskStatus.Success);
			}
		}
		public void ResetColor(Color newTargetColor)
		{
			targetColor = newTargetColor;
		}
	}
}
