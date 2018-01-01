using UnityEngine;

public class TimeHandler : MonoBehaviour {

	public float dayLength = 300.0f;
	public float startTime = 45.0f;
	public float sunIntensitySmoothing = 0.1f;

	private float worldTime;

	private Vector3 euler;
	private float pitch;
	private float yaw;
	private float roll;
	private float vel;

	private Light sun;

	void Start() {
		worldTime = startTime;

		pitch = transform.rotation.eulerAngles.y;
		roll = transform.rotation.eulerAngles.z;

		sun = GetComponent<Light>();
	}

	void Update() {
		worldTime += Time.deltaTime;
		SetSun();
	}

	private void SetSun() {
		yaw = GetTimeOfDay() / dayLength * 360.0f;
		transform.rotation = Quaternion.Euler(yaw, pitch, roll);

		if (yaw >= 180.0f) {
			sun.intensity = Mathf.SmoothDamp(sun.intensity, 0.0f, ref vel, sunIntensitySmoothing);
		} else {
			sun.intensity = Mathf.SmoothDamp(sun.intensity, 1.0f, ref vel, sunIntensitySmoothing);
		}
	}

	public float GetTime() {
		return worldTime;
	}

	public float GetTimeOfDay() {
		return worldTime % dayLength;
	}

}