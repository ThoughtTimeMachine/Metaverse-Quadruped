using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using UnityEngine.InputSystem;
using Oculus.Voice;

[RequireComponent(typeof(AppVoiceExperience))]
public class WitVoiceInteraction : MonoBehaviour
{
    [SerializeField]
    private AppVoiceExperience _appVoiceExperience;

    private void Start()
    {
        _appVoiceExperience = GetComponent<AppVoiceExperience>();
    }
    public void TriggerPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            WitActivate();
            print("wit activated");
        }
    }

   private void WitActivate()
    {
       _appVoiceExperience.Activate();
    }
}
