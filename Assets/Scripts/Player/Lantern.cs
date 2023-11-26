using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lantern : MonoBehaviour
{

    public Light lanternLight;

	private bool IsLightActive;

	public Transform lanternSlot;
	public Transform inactiveLanternSlot;


	public void HandleLantern()
	{
        if(lanternLight != null)
        {
            if (!IsLightActive)
            {

                lanternLight.transform.SetParent(lanternSlot, false);

                lanternLight.intensity += 19.5f;
            }
            else
            {

                lanternLight.transform.SetParent(inactiveLanternSlot, false);

                lanternLight.intensity = 0.5f;
            }
            IsLightActive = !IsLightActive;

        }

    }

}
