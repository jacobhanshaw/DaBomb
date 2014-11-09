using UnityEngine;
using System.Collections;

public class AddRift : MonoBehaviour {

	public GameObject riftPrefab;

	void Start () {
		if (networkView.isMine)
		{
			GameObject rift = Instantiate(riftPrefab, transform.position + new Vector3(0.0f,0.0f,0.0f), transform.rotation) as GameObject;
			rift.transform.parent = gameObject.transform;
		}
	}

}
