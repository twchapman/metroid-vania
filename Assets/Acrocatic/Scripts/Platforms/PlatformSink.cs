﻿using UnityEngine;
using System.Collections;

// This class is used for sinking platforms.
public class PlatformSink : MonoBehaviour {
	// Public variables.
	[Header("General")]
	[Tooltip("Determines if the platform should sink when the player stands on it. If this is enabled, a timer will run. When the timer is completed, the platform will sink.")]
	public bool sinkOnHit = true;
	[Tooltip("Stop the timer when the player leaves the platform. If this is enabled, the platform will not sink when the player leaves the platform before the timer runs out.")]
	public bool stopTimerWhenGone = false;
	[Tooltip("Reset the timer when the player leaves the platform. This resets the timer to the original duration.")]
	public bool resetTimerWhenGone = false;
	[Tooltip("Set the sink timer's duration. When this is 0 and Sink On Hit is enabled, the platform will sink instantly when the player stands on it.")]
	public float time = 1.0f;
	[Header("Camera shake")]
	[Tooltip("Set the duration for the camera shake when the camera should shake on hit.")]
	public float shakeTime = 1.0f;
	[Tooltip("Set the shake's intensity.")]
	public float shakeAmount = 0.1f;
	// NOTE: You can set the platform's speed by changing its rigidbody2D components!

	// Private variables.
	private bool hasBeenStopped = false;			// Determines if the timer has been stopped at least once.
	private bool timerRunning = false;				// Determines if the timer is running.
	private float sinkTimer;						// This is used for the timer.
	private bool sinking = false;					// Determines if the platform is currently sinking.
	private bool shaken = false;					// Determines if the platform has already shaken (not stirred).

	// Use this for initialization.
	void Start () {

	}
	
	// Update is called once per frame.
	void Update () {
		// If the platform isn't sinking...
		if (!sinking) {
			// ... if the timer is running...
			if (timerRunning) {
				// ... run the timer and make the platform sink when it's completed.
				if (sinkTimer > 0) {
					sinkTimer -= Time.deltaTime;
				} else {
					Sink();
				}
			// Or else...
			} else {
				// ... start the timer when the platform should sink automatically.
				if (!sinkOnHit) {
					StartSinkTimer();
				}
			}
		}
	}

	// Function to make the platform sink.
	public void Sink() {
		sinking = true;
		rigidbody2D.isKinematic = false;
	}

	// Function to start the sinking timer.
	public void StartSinkTimer() {
		// If the timer is not yet running and the platform isn't sinking...
		if (!sinking && !timerRunning) {
			// ... only reset the timer if it's the first time the player interacts or when it should reset after jumping off.
			if (!hasBeenStopped || (hasBeenStopped && resetTimerWhenGone)) {
				sinkTimer = time;
			}
			// Start the timer.
			timerRunning = true;
		}
	}
	
	// Function to reset the sinking timer based on the public variables.
	public void ResetSinkTimer() {
		// If the platform isn't sinking and the timer should stop or reset when the player jumps off the platform.
		if (!sinking && (stopTimerWhenGone || resetTimerWhenGone)) {
			// Remember that the platform has been stopped at least once.
			hasBeenStopped = true;
			// Stop the timer.
			timerRunning = false;
		}
	}

	// Shake the camera.
	public void ShakeCamera() {
		// If the platform hasn't shaken and the shake amount and time is higher than 0...
		if (!shaken && shakeAmount > 0 && shakeTime > 0) {
			// Set shaken to true, so this platform doesn't trigger a shake again.
			shaken = true;
			// Get the Camera and CameraShake class.
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			CameraShake shake = camera.GetComponent<CameraShake>();
			// Set the shake amount and shake time.
			shake.shakeAmount = shakeAmount;
			shake.shake = shakeTime;
			// Enable the CameraShake class.
			shake.enabled = true;
		}
	}
}
