using UnityEngine;
using System.Collections;
using Ovr;

public class NoCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(Network.isServer)
			gameObject.camera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
