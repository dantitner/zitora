using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiConnector : MonoBehaviour
{
    public bool UIOpened = true;
    public RectTransform connectionUI;

    public NetworkManager NetworkManager;
    public UNetTransport UNetTransport;

    public void SetIp(string ip)
    {
        UNetTransport.ConnectAddress = ip;
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(UIOpened)
            {
                connectionUI.gameObject.SetActive(false);
                UIOpened = false;
            }
            else
            {
                connectionUI.gameObject.SetActive(true);
                UIOpened = true;
            }
        }
    }

    public void Host()
    {
        NetworkManager.StartHost();
    }
    public void Connect()
    {
        NetworkManager.StartClient();
    }

    public void Discontect()
    {
        if(NetworkManager.IsClient)
            NetworkManager.DisconnectClient(NetworkManager.LocalClientId);
        if(NetworkManager.IsHost)
            NetworkManager.Shutdown();
    }

    private void SetActiveMainCamera(bool isActive)
    {
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = isActive;
    }
}
