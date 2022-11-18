using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class quitGame : MonoBehaviour
{
    public void Quit() {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }   
    public void Salir(){ 
            GameObject.FindGameObjectWithTag("JazzAudio").GetComponent<Music>().PlayMusic();
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Lobby");
    }
}
