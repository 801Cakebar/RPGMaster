using UnityEngine;
using System.Collections;

public class Warrior : MonoBehaviour {

	bool isFocused;

	// Use this for initialization
	void Start () {
	
	}

	void beingFocused(bool b){
		isFocused = b;
	}

	void skill0(GameObject target){
		SendMessageUpwards ("actionOccured", gameObject.GetInstanceID () + ":" + target.GetInstanceID () + ":Stab:" + Time.time);
	}
	void skill1(GameObject target){
		SendMessageUpwards ("actionOccured",gameObject.GetInstanceID () + ":" + target.GetInstanceID () + ":Slice:" + Time.time);
	}
	void skill2(){
		SendMessageUpwards ("actionOccured",gameObject.GetInstanceID () + ":" + gameObject.GetInstanceID () + ":Guard:" + Time.time);
	}

	// Update is called once per frame
	void Update () {
		if (isFocused) {
			if (Input.GetKeyDown("q")){
				SendMessageUpwards ("skillSelected", new object[2] {0, true});
				Debug.Log (gameObject.GetInstanceID () + " skill 0 ready");
			}
			if (Input.GetKeyDown("w")){
				SendMessageUpwards ("skillSelected", new object[2] {1, true});
				Debug.Log (gameObject.GetInstanceID () + " skill 1 ready");
			}
			if (Input.GetKeyDown("e")){
				SendMessageUpwards ("skillSelected", new object[2] {2, false});
				Debug.Log (gameObject.GetInstanceID () + " skill 2 ready");
			}
		}
	}
}
