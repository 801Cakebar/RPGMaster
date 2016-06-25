using UnityEngine;
using System.Collections;

public class CharTiming : MonoBehaviour {

	public float actionSpd;
	public bool hasAction;
	float timer;

	void countdown(float tick){
		timer -= tick;
		if (timer <= 0 && !hasAction) {
			hasAction = true;
			Debug.Log (gameObject.GetInstanceID () + " action ready");
			SendMessageUpwards ("actionReady", gameObject.GetInstanceID ());
		}
	}

	void resetCountdown(){
		timer = actionSpd;
		hasAction = false;
	}

	// Use this for initialization
	void Start () {
		timer = actionSpd;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
