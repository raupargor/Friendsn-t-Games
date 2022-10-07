using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
 public class Memory : MonoBehaviour//, IPunObservable
{
    public string nickname;
    public int posicion;
    public Color32 Color;
    public Sprite Hat;
    public int Points;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         // We own this player: send the others our data
    //         stream.SendNext(Color);
    //     }
    //     else
    //     {
    //         // Network player, receive data
    //         this.Color = (int)stream.ReceiveNext();
    //     }
    // }
}
