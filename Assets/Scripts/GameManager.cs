using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	public float tickRate;
	float anchorTime;
	List<GameObject> p1chars;
	List<GameObject> p2chars;
	Dictionary<int,GameObject> chars;
	GameObject focusedChar;
	int focusedSkill;
	bool isTargeting;
	ArrayList actionBuffer;
	Dictionary<GameObject, List<List<string>>> activeChars;

	//action format is type:effect:amount:startup:delay
	static Dictionary<string,string> AllActions = new Dictionary<string, string> () {
		{"Stab","Attack:Damage:5:1:10"},
		{"Slice","Attack:Damage:10:3:20"},
		{"Guard","Defend:TempHP:10:0:20"},
		{"Dodge","Defend:Negate:-1:0:5"},
		{"Parry","Counter:Damage:20:0:20"},
	};

	// Use this for initialization
	void Start () {
		anchorTime = Time.time;
		p1chars = new List<GameObject>();
		p2chars = new List<GameObject>();
		chars = new Dictionary<int, GameObject>();
		actionBuffer = new ArrayList ();
		activeChars = new Dictionary<GameObject, List<List<string>>> ();
		for (int i = 0; i < 3; i++) {
			GameObject newchar = Instantiate (Resources.Load ("Prefabs/Warrior"), new Vector3(i*2-2,0,0), Quaternion.identity) as GameObject;
			newchar.transform.parent = gameObject.transform;
			chars.Add (newchar.GetInstanceID(), newchar);
			p1chars.Add (newchar);
		}
		for (int i = 0; i < 3; i++) {
			GameObject newchar = Instantiate (Resources.Load ("Prefabs/Rogue"), new Vector3(i*2-2,2,0), Quaternion.identity) as GameObject;
			newchar.transform.parent = gameObject.transform;
			chars.Add (newchar.GetInstanceID(), newchar);
			p2chars.Add (newchar);
		}

	}

	void actionReady(int id){
		chars [id].GetComponent("Halo").GetType().GetProperty("enabled").SetValue(chars [id].GetComponent("Halo"), true, null);
	}

	void skillSelected(object[] ps){
		focusedSkill = (int) ps[0];
		isTargeting = (bool) ps[1];
	}

	//action format is initiator:target:actionCode:time
	void actionOccurred(string action){
		string[] a = action.Split (':');
		actionBuffer.Add (a);
	}

	void resetFocus(){
		focusedChar.SendMessage ("resetCountdown");
		focusedChar.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(focusedChar.GetComponent("Halo"), false, null);
		focusedChar = null;
		focusedSkill = -1;
		isTargeting = false;
		p1chars [0].SendMessage ("beingFocused", false);
		p1chars [1].SendMessage ("beingFocused", false);
		p1chars [2].SendMessage ("beingFocused", false);
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - anchorTime > tickRate) {
			BroadcastMessage ("countdown", tickRate);
			Debug.Log ("tick");
			anchorTime = Time.time;

			foreach (string[] action in actionBuffer) {
				GameObject i = GameObject.Find (action [0].ToString());
				string details = AllActions [action [2]];
				List<string> d = details.Split(':').ToList();
				d.Add (action [1]);
				if (!activeChars.ContainsKey (i)) {
					activeChars.Add (i, new List<List<string>>());
					activeChars [i].Add (d);
				} else {
					activeChars [i].Add(d);
				}
			}
			actionBuffer.Clear ();
			foreach (GameObject actor in activeChars.Keys) {
				List<List<string>> acts = activeChars [actor];
				foreach (List<string> act in acts) {
					GameObject target = GameObject.Find (act [5]);
					if (target!=null && act[0].Equals("Attack")){
						if (activeChars.ContainsKey (target)) {
							foreach (List<string> react in activeChars[target]){
								if (react [0].Equals ("Defend")) {
									act[>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.
								}
							}
						}
					}
				}
			}
		}
		//p1 controls
		if (Input.GetKeyDown("a")){
			if (p1chars [0].GetComponent<CharTiming> ().hasAction) {
				focusedChar = p1chars [0];
				p1chars [0].SendMessage ("beingFocused", true);
				p1chars [1].SendMessage ("beingFocused", false);
				p1chars [2].SendMessage ("beingFocused", false);
				Debug.Log (gameObject.GetInstanceID () + " selected");
			}
		}
		if (Input.GetKeyDown("s")){
			if (p1chars [1].GetComponent<CharTiming> ().hasAction) {
				focusedChar = p1chars [1];
				p1chars [0].SendMessage ("beingFocused", false);
				p1chars [1].SendMessage ("beingFocused", true);
				p1chars [2].SendMessage ("beingFocused", false);
				Debug.Log (gameObject.GetInstanceID () + " selected");
			}
		}
		if (Input.GetKeyDown("d")){
			if (p1chars [2].GetComponent<CharTiming> ().hasAction) {
				focusedChar = p1chars [2];
				p1chars [0].SendMessage ("beingFocused", false);
				p1chars [1].SendMessage ("beingFocused", false);
				p1chars [2].SendMessage ("beingFocused", true);
				Debug.Log (gameObject.GetInstanceID () + " selected");
			}
		}
		if (isTargeting) {
			if (Input.GetKeyDown ("1")) {
				focusedChar.SendMessage ("skill" + focusedSkill, p2chars [0]);
				resetFocus ();
			}
			if (Input.GetKeyDown ("2")) {
				focusedChar.SendMessage ("skill" + focusedSkill, p2chars [1]);
				resetFocus ();
			}
			if (Input.GetKeyDown ("3")) {
				focusedChar.SendMessage ("skill" + focusedSkill, p2chars [2]);
				resetFocus ();
			}
		} else if (focusedSkill > 0) {
			focusedChar.SendMessage ("skill" + focusedSkill);
			resetFocus ();
		}

		//p2 controls
		if (Input.GetKeyDown("j")){
			if (p2chars [0].GetComponent<CharTiming> ().hasAction) {
				p2chars [0].SendMessage ("skill1");
				p2chars [0].SendMessage ("resetCountdown");
				p2chars [0].GetComponent("Halo").GetType().GetProperty("enabled").SetValue(focusedChar.GetComponent("Halo"), false, null);
			}
		}
		if (Input.GetKeyDown("k")){
			if (p2chars [1].GetComponent<CharTiming> ().hasAction) {
				p2chars [1].SendMessage ("skill1");
				p2chars [1].SendMessage ("resetCountdown");
				p2chars [1].GetComponent("Halo").GetType().GetProperty("enabled").SetValue(focusedChar.GetComponent("Halo"), false, null);
			}
		}
		if (Input.GetKeyDown("l")){
			if (p2chars [2].GetComponent<CharTiming> ().hasAction) {
				p2chars [2].SendMessage ("skill2");
				p2chars [2].SendMessage ("resetCountdown");
				p2chars [2].GetComponent("Halo").GetType().GetProperty("enabled").SetValue(focusedChar.GetComponent("Halo"), false, null);
			}
		}
	}
}
