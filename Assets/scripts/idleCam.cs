using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class idleCam : MonoBehaviour
{
	public float lerpValPos;
	float timeToMove, timeToDrop;
	public float idleTimeToMove, idleTimeToDrop;
	Vector3 targetRot, originRot;
	bool setPos = true;
	bool leaveScreen, returningToDrop, dropping, sureBool = false;
	public Transform screenObj, playerObj;

	public GameObject spaceToDrop, youSure, screenWorldObj, ukeObj;

	Vector3 screenOriginPos, screenOriginRot;

	public AnimationCurve tweenCurve;
	float t1 =0;

//	float tim =0;

	public AudioSource lcdScreenAudio;
	// Use this for initialization
	void Start ()
	{


	}
	
	// Update is called once per frame
	void Update ()
	{
		if(lcdScreenAudio.gameObject.activeSelf)
			timeToMove += Time.deltaTime;
		lerpValPos = Mathf.Clamp01 (lerpValPos);
		t1 = Mathf.Clamp01(t1);
	
			if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0 || 
				Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0) {
				timeToMove = 0;
				timeToDrop = 0;
			}
		
		if (screenObj.gameObject.activeSelf) {
			if (timeToMove >= idleTimeToMove) {
				leaveScreen = true;

				if(!returningToDrop){
					//lerpValPos += (1 - lerpValPos) * 0.5f * Time.deltaTime;
					t1 += Time.deltaTime/(idleTimeToDrop -5);
					lerpValPos = tweenCurve.Evaluate(t1);
				}

				setPos = false;

				timeToDrop += Time.deltaTime;
			} else {
				lerpValPos -= Time.deltaTime * 4f;

			}
		}

		if (setPos) {
			originRot = new Vector3 (transform.localEulerAngles.x, 0, 0);

			float randY;
			if (Random.Range (-10, 10) >= 0)
				randY = Random.Range (60, 90);
			else
				randY = Random.Range (-60, -90);
				
			targetRot = originRot + new Vector3 (Random.Range (5, -25), randY, 0);

			screenOriginPos = screenObj.position;
			screenOriginRot = screenObj.eulerAngles;

			t1 = 0;

		}



		if (lerpValPos > 0)
			transform.localEulerAngles = Vector3.Lerp (originRot, targetRot, lerpValPos);
		else {
			setPos = true;
			transform.localEulerAngles = originRot;
			leaveScreen = false;
		}

		if (leaveScreen) {
			//screenObj.SetParent (playerObj);
			screenObj.position = screenOriginPos;
			screenObj.eulerAngles = screenOriginRot;

		} else {
			screenObj.SetParent (this.transform);
		}

		if (timeToDrop >= idleTimeToDrop) {
			returningToDrop = true;
		}else
			returningToDrop = false;

		if(returningToDrop){
			if(!dropping){
				t1 -= Time.deltaTime/(idleTimeToDrop-5);
				lerpValPos = tweenCurve.Evaluate(t1);
			}

			if(sureBool){
				spaceToDrop.SetActive(false);
				youSure.SetActive(true);
//				Debug.Log(lerpValPos);
				if(Input.GetKeyDown(KeyCode.Space) ){
					lerpValPos = 0;
					dropping = true;
				}
			}else{
				spaceToDrop.SetActive(true);

				if(Input.GetKeyDown(KeyCode.Space)){
					sureBool = true;
				}
			}
		}else{
			spaceToDrop.SetActive(false);
			youSure.SetActive(false);
			sureBool = false;
		}

		if(dropping){
			print("dropping");
			screenObj.gameObject.SetActive(false);

			//with this you can pick it back up (change screenObj for it to work):
//			screenWorldObj.SetActive(true);
//			screenWorldObj.transform.position = new Vector3(transform.position.x + Random.Range(-2.0f,2.0f), 
//				transform.position.y - 1.75f, transform.position.z + Random.Range(1.0f,4.0f));
//			screenObj.transform.rotation = Random.rotation;

			//with this you can't pick it back up
			GameObject instantiatedScreen;
			instantiatedScreen = Instantiate(screenWorldObj, 
				new Vector3(transform.position.x,transform.position.y,transform.position.z),transform.rotation);

			instantiatedScreen.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(-50,50),Random.Range(750,1250),Random.Range(750,1500));
			instantiatedScreen.GetComponent<Rigidbody>().AddTorque(Random.Range(-10,10),Random.Range(-10,10),Random.Range(-10,10));

			Instantiate(ukeObj);

			dropping = false;

			//FIX THIS SO IT SPAWNS STRAIGHT FROM THE PLAYER!
		}


		lcdScreenAudio.volume = 1-lerpValPos;

	}
}
