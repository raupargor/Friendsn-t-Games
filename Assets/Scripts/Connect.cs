using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connect : MonoBehaviourPunCallbacks
{
    public InputField nickname;
    Memory memory;
    // void Awake(){
    //     DontDestroyOnLoad(this.gameObject);
    // }

    // Start is called before the first frame update
    public void EnterNickname(){ 
        if(!string.IsNullOrEmpty(nickname.text)){ 
            PhotonNetwork.NickName = nickname.text;

            memory= GameObject.FindWithTag("Memory").GetComponent<Memory>();
            memory.nickname=nickname.text;
            Debug.Log("Nickname: "+ PhotonNetwork.NickName);
            // SceneManager.LoadScene("Loading");
            PhotonNetwork.LoadLevel("ChooseColor");
        }
    }

}
