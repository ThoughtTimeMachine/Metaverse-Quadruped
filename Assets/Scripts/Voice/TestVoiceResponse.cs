using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi.Json;
using Oculus.Interaction.Deprecated;
using Meta.WitAi;
using static UnityEngine.EventSystems.EventTrigger;

public class TestVoiceResponse : MonoBehaviour
{
    [SerializeField] public VoiceService Voice;
    void Start()
    {
        if (!Voice) Voice = FindObjectOfType<VoiceService>();
        Voice.VoiceEvents.OnResponse.AddListener(HandleResponse);
    }
    public  void HandleResponse(WitResponseNode response)
    {
        var entity = WitResultUtilities.GetFirstEntityValue(response, "animation:animation");
       // print("entity: " + entity);
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
