using UnityEngine;

public class CameraMove : MonoBehaviour {

	public float moveSpeed = 5.0f;
	public float rotateSpeed = 180.0f;
	public Vector2 minMaxRotation = new Vector2(15.0f, 50.0f);
	public Vector2 minMaxYPos = new Vector2(5.0f, 20.0f);

	void Update() {
		HandleRotation();
		HandleMovement();
		HandleClamping();
	}

	private void HandleRotation() {
		if (Input.GetMouseButton(0)) {
			Vector3 point = GetCameraPoint();
			transform.RotateAround(point, Vector3.up, Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime);
		}
	}

	private void HandleMovement() {
		Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
		movement = transform.TransformDirection(movement.normalized);
		movement.y = 0.0f;
		movement.Normalize();
		movement *= moveSpeed * Time.deltaTime;
		transform.position += movement;
	}

	private void HandleClamping() {
		Vector3 pos = transform.position;
		pos.y = Mathf.Clamp(pos.y, minMaxYPos.x, minMaxYPos.y);
		transform.position = pos;
		Vector3 rotation = transform.rotation.eulerAngles;
		rotation.x = GetRotationX();
		transform.rotation = Quaternion.Euler(rotation);
	}

	private Vector3 GetCameraPoint() {
		Ray ray = new Ray(transform.position, transform.forward);
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		float distance;
		if (plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
		}
		return Vector3.zero;
	}

	private float GetRotationX() {
		float perc = transform.position.y / (minMaxYPos.y - minMaxYPos.x);
		return perc * (minMaxRotation.y - minMaxRotation.x) + minMaxRotation.x;
	}

}