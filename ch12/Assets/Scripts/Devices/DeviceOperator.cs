using UnityEngine;
using System.Collections;

public class DeviceOperator : MonoBehaviour {
	public float radius = 1.5f;

	void Update() {
		if (Input.GetButtonDown("Fire3")) {
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
			foreach (Collider hitCollider in hitColliders) {
				Vector3 direction = hitCollider.transform.position - transform.position;
				if (Vector3.Dot(transform.forward, direction) > .5f) {
					hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
