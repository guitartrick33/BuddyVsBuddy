using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    public BadWordsFilter badWordsFilter;

    /* Room listing - disabled at the moment */
    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;
    public Dropdown dropdown;
    private int maxPlayers;
    
    /* Used for OnRoomListUpdate function, precautionary check for updating the rooms list */
    public float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;
    public InputField searchTerm;
    public Text errorCreationText;
    public Text errorJoinText;

    public Image chatImage;
    private ChatManager chatManager;

    private PanelManager panelManager;

    public GameObject playButton;

    public Color normalMapColor;
    public Color highlightMapColor;
    public GameObject mapPanel;
    public List<GameObject> maps;
    

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        chatManager = GetComponent<ChatManager>();
        panelManager = GetComponent<PanelManager>();
        chatManager.enabled = false;
        chatImage.gameObject.SetActive(false);
        errorCreationText.text = "";
        errorJoinText.text = "";
        mapPanel.SetActive(false);
    }

    /* Creates a room with the name you've selected in the input field */
    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            bool isProhibited = false;
            foreach (string word in badWordsFilter.badWords)
            {
                if (roomInputField.text.ToLower() == word.ToLower() || roomInputField.text.ToLower().Contains(word.ToLower()))
                {
                    // usernameInput.text = "";
                    errorCreationText.text = "Room name prohibited";
                    isProhibited = true;
                    StartCoroutine(WaitErrorText());
                }
            }
            if (!isProhibited)
            {
                foreach (GameObject g in maps)
                {
                    if (g.GetComponent<MapManager>().isSelected)
                    {
                        PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = (byte)maxPlayers, BroadcastPropsChangeToAll = true});
                        errorCreationText.text = "";
                        break;
                    }
                    else
                    {
                        errorCreationText.text = "Choose a map!"; 
                    }
                }
            }
        }
        else
        {
            errorCreationText.text = "Enter a name";
            StartCoroutine(WaitErrorText());
        }
    }
    
    /* Switches between the lobby/room panels upon joining/creating a room */
    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        chatManager.enabled = true;
        chatImage.gameObject.SetActive(true);
        UpdatePlayerList();
    }

    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    /* Clears the list of rooms and creates a new one with the current room listings */
    public void UpdateRoomList(List<RoomInfo> list)
    {
        foreach(RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach(RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    /* Join a random room out of the rooms lists */
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    

    /* Check for errors */
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        errorJoinText.text = message;
        roomInputField.text = "";
        StartCoroutine(WaitErrorText());
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        errorJoinText.text = message;
        roomInputField.text = "";
        StartCoroutine(WaitErrorText());
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorCreationText.text = message;
        roomInputField.text = "";
        StartCoroutine(WaitErrorText());
    }
    
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        chatManager.enabled = false;
        chatImage.gameObject.SetActive(false);
        chatManager.chatMessages.Clear();
        panelManager.Close();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    /* Clears the list first and initializes player banners in the already created rooms */
    void UpdatePlayerList()
    {
        foreach(PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }
    
    /* Search function for the rooms */
    public void FindAndJoinRoom()
    {
        bool foundRoom = false;
        for (int i = 0; i < roomItemsList.Count; i++)
        {
            if (roomItemsList[i].roomName.text == searchTerm.text)
            {
                PhotonNetwork.JoinRoom(roomItemsList[i].roomName.text);
                foundRoom = true;
                break;
            }
        }

        if (!foundRoom)
        {
            errorJoinText.text = "No room found";
            roomInputField.text = "";

        }
        StartCoroutine(WaitErrorText());
    }

    public void OpenMapPanel()
    {
        mapPanel.SetActive(true);
    }
    
    public void CloseMapPanel()
    {
        mapPanel.SetActive(false);
    }
    
    
    
    IEnumerator WaitErrorText()
    {
        yield return new WaitForSeconds(1.5f);
        errorJoinText.text = "";
        errorCreationText.text = "";
    }
    

    /* Goes back to nickname selection screen - disconnects the player from the lobby */
    public void GoBack()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("ConnectToServer");
    }

    private void Update()
    {
        maxPlayers = dropdown.value + 2;
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount <= maxPlayers)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        playButton.SetActive(false);
        foreach (GameObject g in maps)
        {
            if (g.GetComponent<MapManager>().isSelected)
            {
                PhotonNetwork.LoadLevel(g.GetComponent<MapManager>().mapName);
            }
        }
    }
}
