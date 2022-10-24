using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Linq;
using Photon.Pun;

public class Hats : MonoBehaviourPun, IPunInstantiateMagicCallback//,IPunObservable
{
    public SpriteRenderer spriteRenderer;
    public int newSprite;
    public List<Sprite> spriteArray;

    public PhotonView view;
    public GameObject parent;
    object[] customInstantiateData;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        view = GetComponent<PhotonView>();
        

       // AddObservable();
    }

    void Update()
    {   
            transform.position = Vector3.MoveTowards(
                transform.position,
                transform.parent.Find("Animator-movement").transform.position+ new Vector3(0, 0.95f, -1),10f);
            if (transform.parent.Find("Animator-movement").GetComponent<SpriteRenderer>().flipX == true)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                transform.position = transform.position + new Vector3(-0.2f, 0, 0);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                transform.position = transform.position + new Vector3(0.2f, 0, 0);
            }
        
        // if (Input.GetKeyDown(KeyCode.H) && transform.tag == "Player")
        // {
        //     ChangeSprite(newSprite);
        // }
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        parent = PhotonView.Find((int)instantiationData[0]).gameObject;
        // Debug.Log(parent);
        this.transform.parent=parent.transform.parent.transform;
    }
    // public void ChangeSprite(int somb)
    // {
    //     // if (view.IsMine){
    //         spriteRenderer.sprite = spriteArray.ElementAt(somb);
    //     // }
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
    //             stream.SendNext(transform.position);
    //         }
    //         else
    //         {
    //             transform.position = (Vector3) stream.ReceiveNext();
    //         }
    //     }
            
    //     catch{}
    // }

}
