using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class IGChatManager : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;
    public GameObject bubbleSpeechObject;
    public Text speechText;
    public int characterLimit = 40;

    private InputField chatInputField;
    private bool disableSend;
    private bool isActive;
    private PlayerController pc;
    

    private void Awake()
    {
        chatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
        isActive = false;
        chatInputField.placeholder.color = Color.black;
        chatInputField.characterLimit = characterLimit;
        pc = gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (!isActive)
            {
                chatInputField.enabled = false;
                chatInputField.placeholder.color = Color.black;
                pc.enabled = true;
            }

            if (isActive)
            {
                chatInputField.enabled = true;
                chatInputField.ActivateInputField();
                chatInputField.Select();
                pc.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.T) && !isActive)
            {
                isActive = true;
                chatInputField.text = "";
                chatInputField.placeholder.color = Color.clear;
            }
            if (!disableSend && chatInputField.isActiveAndEnabled)
            {
                if (chatInputField.text != string.Empty && chatInputField.text.Length > 0 && Input.GetKeyUp(KeyCode.Return))
                {
                    photonView.RPC("SendText", RpcTarget.All, chatInputField.text);
                    isActive = false;
                }
            }
        }
    }

    [PunRPC]
    private void SendText(string message)
    {
        speechText.text = message;
        bubbleSpeechObject.SetActive(true);
        chatInputField.text = "";
        disableSend = true;
        StartCoroutine(RemoveBubble());
    }

    IEnumerator RemoveBubble()
    {
        yield return new WaitForSeconds(2.5f);
        bubbleSpeechObject.SetActive(false);
        disableSend = false;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bubbleSpeechObject.active);
        }
        else if(stream.IsReading)
        {
            bubbleSpeechObject.SetActive((bool)stream.ReceiveNext());           
        }
    }
}
