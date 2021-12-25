using System;
using extOSC;
using UnityEngine;

public class OSCHandler : MonoBehaviour
{
    public event Action OnRedMaterial;
    public event Action OnGreenMaterial;
    
    private OSCReceiver _receiver;

    [SerializeField] int receivePort = 7000;
    private void Awake()
    {
        var doesOscReceiverExist = gameObject.GetComponent<OSCReceiver>();
        if (doesOscReceiverExist == null)
            _receiver = gameObject.AddComponent<OSCReceiver>();
        else
            _receiver = doesOscReceiverExist;
    }

    private void Start()
    {
        // Set local port.
        _receiver.LocalPort = receivePort;    
        // Bind "MessageReceived" method to special address.
        _receiver.Bind("/redMaterial", MessageReceived);  
        _receiver.Bind("/greenMaterial", MessageReceived);  
    }

    private void MessageReceived(OSCMessage message)
    {
        var value = message.Values[0].FloatValue;
        
        Debug.Log($"value: {value}");
        
        if (value > 0.0f)
        {
            switch (message.Address)
            {
                case "/redMaterial":
                    OnRedMaterial?.Invoke();
                    break;
                case "/greenMaterial":
                    OnGreenMaterial?.Invoke();
                    break;
                default:
                    Debug.Log("other material");
                    break;
            }
        }
    }
}
