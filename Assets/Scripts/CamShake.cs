using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	public bool shaketrue = false;

	// How long the object should shake for.
	public float shakeDuration = 0.1f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.2f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos = new Vector3(0, 1, -10);

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shaketrue)
		{
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0.1f;
				camTransform.localPosition = originalPos;
				shaketrue = false;
			}
		}
	}

	public void shakecamera()
	{
		shaketrue = true;
	}
}
