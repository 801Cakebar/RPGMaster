using UnityEngine;
using System.Collections;

public class CharStats : MonoBehaviour {

	public float hp;
	public float armor;
	public float attack;

	float currentHp;

	// Use this for initialization
	void Start () {
		currentHp = hp;
		gameObject.name = gameObject.GetInstanceID ().ToString();
	}

	public void takeDamage(float d){
		currentHp -= d;
		if (currentHp <= 0) {
			Debug.Log (gameObject.GetInstanceID () + " died");
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
