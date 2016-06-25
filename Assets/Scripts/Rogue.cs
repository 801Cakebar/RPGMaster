using UnityEngine;
using System.Collections;

public class Rogue : MonoBehaviour {

	bool isFocused;

	// Use this for initialization
	void Start () {
	
	}

	void beingFocused(bool b){
		isFocused = b;
	}

	void skill0(GameObject target){

	}
	void skill1(){
		SendMessageUpwards ("actionOccured",gameObject.GetInstanceID () + ":" + gameObject.GetInstanceID () + ":Parry:" + Time.time);
	}
	void skill2(){
		SendMessageUpwards ("actionOccured",gameObject.GetInstanceID () + ":" + gameObject.GetInstanceID () + ":Dodge:" + Time.time);
	}
	
	// Update is called once per frame
	void Update () {
		if (isFocused) {
			if (Input.GetKeyDown("q")){
				SendMessageUpwards ("skillSelected", new object[2] {0, true});
			}
			if (Input.GetKeyDown("w")){
				SendMessageUpwards ("skillSelected", new object[2] {1, false});
			}
			if (Input.GetKeyDown("e")){
				SendMessageUpwards ("skillSelected", new object[2] {2, false});
			}
		}
	}
}
