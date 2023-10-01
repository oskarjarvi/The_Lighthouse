using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lantern : MonoBehaviour
{
	public PlayerControls input;

    public Light lanternLight;

	private bool IsLightActive;

	public Transform lanternSlot;
	public Transform inactiveLanternSlot;


    private void Awake()
    {
        input = new PlayerControls();

        input.PlayerInput.Lighting.performed += ctx => HandleLantern();
    }

    private void OnEnable()
	{
		input.PlayerInput.Enable();
	}
	private void OnDisable()
	{
		input.PlayerInput.Disable();
	}
	private void HandleLantern()
	{

            if (!IsLightActive)
            {

                lanternLight.transform.SetParent(lanternSlot, false);

                lanternLight.intensity += 0.5f;
            }
            else
            {

                lanternLight.transform.SetParent(inactiveLanternSlot, false);

                lanternLight.intensity = 0.5f;
            }
            IsLightActive = !IsLightActive;
        
    }

}
