using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    
	public Transform target;

	public float Damping = 3.0f;

	Vector3 TargetPos;

	Quaternion TargetRot;

	[AddComponentMenu("Camera-Control/Smooth Follow")]


	void FixedUpdate(){

		if (!target) return;

		transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * Damping);

		TargetRot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, Time.deltaTime * Damping);

	}
}