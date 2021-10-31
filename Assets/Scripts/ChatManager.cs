using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPun
{
    bool isChatting = false;
    string chatInput = "";
    public Color color;

    [System.Serializable]
    public class ChatMessage
    {
        public string sender = "";
        public string message = "";
        public float timer = 0;
    }

    public List<ChatMessage> chatMessages = new List<ChatMessage>();

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Photon View
        if(gameObject.GetComponent<PhotonView>() == null)
        {
            PhotonView photonView = gameObject.AddComponent<PhotonView>();
            photonView.ViewID = 1;
        }
        else
        {
            photonView.ViewID = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && !isChatting)
        {
            isChatting = true;
            chatInput = "";
        }

        //Hide messages after timer is expired
        for (int i = 0; i < chatMessages.Count; i++)
        {
            if (chatMessages[i].timer > 0)
            {
                chatMessages[i].timer -= Time.deltaTime;
            }
        }
    }

    void OnGUI()
    {
        GUIStyle sayStyle = new GUIStyle();
        sayStyle.fontSize = 16;
        sayStyle.normal.textColor = color;
        if (!isChatting)
        {
            GUIStyle chatStyle = new GUIStyle();
            chatStyle.fontSize = 12;
            chatStyle.normal.textColor = color;
            GUI.Label(new Rect(26, Screen.height - 45, 250, 30), "Press 'T' to chat", chatStyle);
        }
        else
        {
            
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                isChatting = false;
                if(chatInput.Replace(" ", "") != "")
                {
                    //Send message
                    photonView.RPC("SendChat", RpcTarget.All, PhotonNetwork.LocalPlayer, chatInput);
                }
                chatInput = "";
            }

            GUI.SetNextControlName("ChatField");
            
            // GUI.Label(new Rect(20, Screen.height - 50, 200, 25), "Say:", sayStyle);
            GUIStyle inputStyle = GUI.skin.GetStyle("box");
            inputStyle.alignment = TextAnchor.MiddleLeft;
            inputStyle.fontSize = 15;
            inputStyle.normal.textColor = color;
            chatInput = GUI.TextField(new Rect(10, Screen.height - 50, 500, 35), chatInput, 50, inputStyle);

            GUI.FocusControl("ChatField");
        }
        
        //Show messages
        for(int i = 0; i < chatMessages.Count; i++)
        {
            if(chatMessages[i].timer > 0 || isChatting)
            {
                GUI.Label(new Rect(25, Screen.height - 90 - 25 * i, 300, 30), chatMessages[i].sender + ": " + chatMessages[i].message, sayStyle);
            }
        } 
    }

    [PunRPC]
    void SendChat(Player sender, string message)
    {
        ChatMessage m = new ChatMessage();
        m.sender = sender.NickName;
        m.message = message;
        m.timer = 15.0f;

        chatMessages.Insert(0, m);
        if(chatMessages.Count > 8)
        {
            chatMessages.RemoveAt(chatMessages.Count - 1);
        }
    }
}