using UnityEngine;
using CS_Kevin;

public class PullBox : MonoBehaviour {
	void OnTriggerStay(Collider m_collider){
		Debug.Log("Collider");
		if(m_collider.gameObject.name == "MovingBox" && m_collider.transform.position == transform.position){
			Debug.Log("Inside");
			m_collider.gameObject.GetComponent<MoveObject>().SetStatus(MOVESTATE.PULLING);
			m_collider.gameObject.GetComponent<MoveObject>().moveTo(transform.parent.position);
		}
	}
}
