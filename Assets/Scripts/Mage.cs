using UnityEngine;
using System.Collections;

public class Mage : MonoBehaviour {

	bool isFocused;

	// Use this for initialization
	void Start () {
	
	}

	void beingFocused(bool b){
		isFocused = b;
	}

	void skill0(GameObject target){

	}
	void skill1(GameObject target){

	}
	void skill2(GameObject target){

	}
	
	// Update is called once per frame
	void Update () {
		if (isFocused) {
			if (Input.GetKeyDown("q")){
				SendMessageUpwards ("skillSelected", new object[2] {0, true});
			}
			if (Input.GetKeyDown("w")){
				SendMessageUpwards ("skillSelected", new object[2] {1, true});
			}
			if (Input.GetKeyDown("e")){
				SendMessageUpwards ("skillSelected", new object[2] {2, true});
			}
		}
	}
}
