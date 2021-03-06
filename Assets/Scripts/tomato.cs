﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomato : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;
	public float throwForce = 1.5f;
	public GameObject splatObject;
	public float tomatoLifetime;
	//public bool isSplatted = false;
	private Material myMat;
	float lerpTime = 5; //Takes 5 seconds



	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		Material myMat = GetComponent<MeshRenderer>().material;

	}

	void onTriggerStay(Collider col){
		if (GetComponent<Collider> ().gameObject.CompareTag ("tomato")) {
			if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
				ThrowTomato (col);
			} else if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				GrabTomato (col);
			}
		}
	}



	void GrabTomato(Collider coli){
		coli.transform.SetParent (gameObject.transform);
		coli.GetComponent<Rigidbody> ().isKinematic = true;
		device.TriggerHapticPulse (2000);
		Debug.Log ("You are holding a tomato.");
	}

	void ThrowTomato(Collider coli){
		coli.transform.SetParent (null);
		Rigidbody rigidbody = coli.GetComponent<Rigidbody> ();
		rigidbody.isKinematic = false;
		rigidbody.velocity = device.velocity * throwForce;
		rigidbody.angularVelocity = device.angularVelocity;
		Debug.Log ("You threw the tomato!");
	}


	void OnCollisionEnter(Collision collision){
		splatObject = (GameObject) Instantiate (splatObject, transform.position, transform.rotation);
		Debug.Log ("Splat Called");
			Destroy (gameObject);
		Destroy(splatObject, tomatoLifetime);

		Debug.Log ("Splat Destroyed");


		//}
	} 


	void FadeOut(){
		
				//Define this in Start();

		//Color matColor = myMat.color;
		myMat.SetColor("_Color", Color.clear);

		for(float i=0; i<1; i+=Time.deltaTime/lerpTime){

			myMat.SetColor(Color.Lerp(matColor, Color.clear, i));

		}
	}
		/*public void FadeOut(){
		//iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", 0.0f, "time", 1.5f, "easetype", "linear", "onupdate", "setAlpha"));
			}*/
	// Update is called once per frame
	void Update () {
		

	}

}
