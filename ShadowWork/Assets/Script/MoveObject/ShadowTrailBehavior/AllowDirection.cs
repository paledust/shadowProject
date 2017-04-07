using System.Collections.Generic;
using UnityEngine;
using CS_Kevin;
public class AllowDirection : MonoBehaviour {
	public List<DIRECTION> avaDirection;

	//Get the AvaDirection List
	//If it's the IEnumerable that confusing you, please don't.
	//It means the function return multiple DIRECTION Type values, like return a list,array,arraylist of this type.
	public IEnumerable<DIRECTION> GET_DIRECTION_LIST(){return avaDirection;}
}
