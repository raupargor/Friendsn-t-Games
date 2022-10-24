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

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;

        if (!string.IsNullOrEmpty(createInput.text))
        {
            PhotonNetwork.CreateRoom(createInput.text, roomOptions, null); //,new RoomOptions(){ MaxPlayers = 6 }, null);
        }
    }

    public void JoinRoom()
    {
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
