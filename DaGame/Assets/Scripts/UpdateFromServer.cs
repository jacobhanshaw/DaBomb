using UnityEngine;
using System.Collections;

public class UpdateFromServer : MonoBehaviour
{
		public bool usesCharacterController;
		private CharacterController controller;

		private float lastSynchronizationTime = 0f;
		private float syncDelay = 0f;
		private float syncTime = 0f;
		private Vector3 syncStartPosition = Vector3.zero;
		private Vector3 syncEndPosition = Vector3.zero;

		void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
		{
				Vector3 syncPosition = Vector3.zero;
				Vector3 syncVelocity = Vector3.zero;
				if (stream.isWriting) {
						syncPosition = gameObject.transform.position;
						stream.Serialize (ref syncPosition);

						if (usesCharacterController)
								syncVelocity = controller.velocity;
						else
								syncVelocity = rigidbody.velocity;
						stream.Serialize (ref syncVelocity);
				} else {
						stream.Serialize (ref syncPosition);
						stream.Serialize (ref syncVelocity);

						syncTime = 0f;
						syncDelay = Time.time - lastSynchronizationTime;
						lastSynchronizationTime = Time.time;

						syncEndPosition = syncPosition + syncVelocity * syncDelay;
						syncStartPosition = gameObject.transform.position;
				}
		}

		void Awake ()
		{
				if (usesCharacterController)
						controller = gameObject.GetComponent<CharacterController> ();

				lastSynchronizationTime = Time.time;
		}

		void Start ()
		{

		}

		void Update ()
		{
				if (!networkView.isMine)
						SyncedMovement ();
		}

		private void SyncedMovement ()
		{
				syncTime += Time.deltaTime;
				gameObject.transform.position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
		}

}
