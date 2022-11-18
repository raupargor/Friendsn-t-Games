using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
 public class Memory : MonoBehaviour//, IPunObservable
{
    public string nickname;
    public int posicion;
    public string Color;
    public string Hat;
    public int Points;
    public int photonID;
    private PhotonView view;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // void Start() {
    //     view = GetComponent<PhotonView>();
    //     AddObservable();
    // }
    // private void AddObservable()
    // {
    //     if (!view.ObservedComponents.Contains(this))
    //     {
    //         view.ObservedComponents.Add(this);
    //     }
    // }

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {   
    //     try{
    //         if (stream.IsWriting)
    //         {
    //             stream.SendNext(Points);
    //         }
    //         else
    //         {
    //             Points = (int) stream.ReceiveNext();
    //         }
    //     }
            
    //     catch{}
    // }
}
