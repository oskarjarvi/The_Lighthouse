using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lantern : MonoBehaviour
{
	public PlayerControls input;

    public Light light;

	private bool IsLightActive;

	private void Awake()
	{
		input = new PlayerControls(); 
	}
	private void OnEnable()
	{
		input.PlayerInput.Lighting.Enable();
	}
	private void OnDisable()
	{
		input.PlayerInput.Lighting.Disable();
	}
	void Update()
    {
		if (input.PlayerInput.Lighting.triggered)
		{
			if (!IsLightActive)
			{
				light.transform.position = new Vector3(-0.84f, 0, 1.17f);
				light.intensity += 0.5f;
			}
			else
			{
				light.intensity = 0.5f;
			}
			IsLightActive = !IsLightActive;

		}
    }
}
