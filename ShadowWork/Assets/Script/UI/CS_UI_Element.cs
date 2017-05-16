using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_UI_Element : MonoBehaviour {
	public bool ifActivated{get; private set;}
	protected SpriteRenderer sprite;
	protected Task_Manager taskManager;
	protected Lit_Task litTask;
	protected Off_Task offTask;
	protected virtual void Start () {
		litTask = new Lit_Task(this);
		offTask = new Off_Task(this);
		taskManager = new Task_Manager();
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(1,1,1,0);
		ifActivated = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		taskManager.Update();
	}

	public void LitUp(){
		if(taskManager.FindTask<Off_Task>() != null)
			taskManager.FindTask<Off_Task>().SetStatus(Task.TaskStatus.Aborted);
		if(taskManager.FindTask<Lit_Task>() == null)
			taskManager.AddTask(new Lit_Task(this));
	}
	public void Off(){
		if(taskManager.FindTask<Lit_Task>() != null)
			taskManager.FindTask<Lit_Task>().SetStatus(Task.TaskStatus.Aborted);
		if(taskManager.FindTask<Off_Task>() == null)
			taskManager.AddTask(new Off_Task(this));
	}


	public class Lit_Task: Task{
		private readonly CS_UI_Element Context;
		private float timer;
		public Lit_Task(CS_UI_Element _Context){
			Context = _Context;
		}
		protected override void Init(){
			timer = 0.0f;
			Context.ifActivated = true;
			if(Context.sprite.color == Color.white){
				SetStatus(TaskStatus.Success);
			}
		}
		internal override void TUpdate(){
			Color clear = new Color(1,1,1,0);
			timer += Time.deltaTime;
			Context.sprite.color = Color.Lerp(clear,Color.white,timer * 2);
			if(Context.sprite.color == Color.white){
				SetStatus(TaskStatus.Success);
			}

		}
	}
	public class Off_Task: Task{
		private readonly CS_UI_Element Context;
		private float timer;
		public Off_Task(CS_UI_Element _Context){
			Context = _Context;
		}
		protected override void Init(){
			timer = 0.0f;
			Color clear = new Color(1,1,1,0);
			Context.ifActivated = false;
			if(Context.sprite.color == clear){
				SetStatus(TaskStatus.Success);
			}
		}
		internal override void TUpdate(){
			Color clear = new Color(1,1,1,0);
			timer += Time.deltaTime;
			Context.sprite.color = Color.Lerp(Color.white,clear, timer*2);
			if(Context.sprite.color == clear){
				SetStatus(TaskStatus.Success);
			}
		}
	}
}
