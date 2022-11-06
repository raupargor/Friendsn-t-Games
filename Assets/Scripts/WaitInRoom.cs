using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class WaitInRoom : MonoBehaviour
{
    public GameObject playerPrefab;
    public PhotonView view;
    public Text roomName;
    private float timer;

    private List<Vector2> puestosLibres=new List<Vector2>();
    Vector2 position1 = new Vector2(-1.15f, 6f);
    Vector2 position2 = new Vector2(0.23f, 3.41f);
    Vector2 position3 = new Vector2(2.5f, 2.42f);
    Vector2 position4 = new Vector2(4.5f, 2.42f);
    Vector2 position5 = new Vector2(7.77f, 5.53f);
    Vector2 position6 = new Vector2(10f, 6.55f);
    Memory memory;


    void Start()
    {   
        puestosLibres.Add(position1);        puestosLibres.Add(position2);        puestosLibres.Add(position3);
        puestosLibres.Add(position4);        puestosLibres.Add(position5);        puestosLibres.Add(position6);

        memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
        timer += Time.deltaTime;
        
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        GameObject armature = PhotonNetwork.Instantiate("Armatures/A"+memory.Color, puestosLibres[PhotonNetwork.CurrentRoom.PlayerCount-1], Quaternion.identity, 0);

        int parentViewID = armature.transform.GetChild(1).GetComponent<PhotonView>().ViewID;
        armature.transform.GetChild(1).GetComponent<PhotonView>().Owner.NickName = memory.nickname;
        object[] myCustomInitData = new object[3];
        Debug.Log(parentViewID);
        myCustomInitData[0] = parentViewID;
        GameObject hat = PhotonNetwork.Instantiate("Hats/H"+memory.Hat, position1, Quaternion.identity, 0,myCustomInitData);
    }

    void Update()
    {
        timer += Time.deltaTime;
        try{
            if (timer <= 15f)
            {
                GameObject p = GameObject.Find("Animator-movement");
                p.GetComponentInChildren<Movement>().canShoot = false;
                p.GetComponentInChildren<Movement>().canAttackPlayer = false;
                
            }
        }catch{}

    }

    public void Empezar(){ 
        PhotonNetwork.PhotonServerSettings.RpcList.Clear();
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.LoadLevel("Atardecer (Sala)");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }else{
            
        }

    
    }
    public void Salir(){ 
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Lobby");
    }


}
