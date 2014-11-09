using UnityEngine;
using System.Collections;

public class AddRift : MonoBehaviour {

	public GameObject riftPrefab;

	void Start () {
		if (networkView.isMine)
		{
			GameObject rift = Instantiate(riftPrefab, transform.position + new Vector3(0.0f,0.0f,0.0f), transform.rotation) as GameObject;
			Debug.Log("RIFT: " + rift);
			Debug.Log("Trans: " + gameObject.transform);
			rift.transform.parent = gameObject.transform;
		}
	}

}
