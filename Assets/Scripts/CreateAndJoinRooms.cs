using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    Memory memory;
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;

        if (!string.IsNullOrEmpty(createInput.text))
        {
            memory= GameObject.FindWithTag("Memory").GetComponent<Memory>();
            memory.Points=0;
            PhotonNetwork.CreateRoom(createInput.text, roomOptions, null); //,new RoomOptions(){ MaxPlayers = 6 }, null);
        }
    }

    public void JoinRoom()
    {
        memory= GameObject.FindWithTag("Memory").GetComponent<Memory>();
        memory.Points=0;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log(player.NickName);
        }
        // SceneManager.LoadScene("ZonaPruebas");
        //  SceneManager.LoadScene("ChooseColor");
        Debug.Log("USUARIOS:"+PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Sala");
        // PhotonNetwork.LoadLevel("Sala");
        //PhotonNetwork.LoadLevel("ZonaPruebas");
    }
}
