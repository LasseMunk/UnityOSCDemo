using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using NaughtyAttributes;
using TMPro;

public class DisplayConnectionInfo : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI txt_IP;
    // [SerializeField] private TMPro.TextMeshProUGUI txt_Subnet;
    [SerializeField] private TMPro.TextMeshProUGUI txt_PortReceiver;
    [SerializeField] private TMP_InputField tmpInputField;
    
    public event Action<int> OnPortChanged;

    private void Start()
    {
        SetIPText();
    }

    [Button]
    private void SetIPText()
    {
        string ipv4 = GetLocalIPAddress();
        txt_IP.text = $"IP: {ipv4}";
    } 

    private void SetPortReceiverText(string newPort)
    {
        txt_PortReceiver.text = $"Port: {newPort}";
    }
    
    public void ChangePortNumber()
    {
        if (tmpInputField.text != null)
        {
            txt_PortReceiver.text = tmpInputField.text;
            int newPort = int.Parse(tmpInputField.text);
            OnPortChanged?.Invoke(newPort);
        }
    }
    
    private static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}


