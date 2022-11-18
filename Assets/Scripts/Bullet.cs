using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour, IPunInstantiateMagicCallback
{
    public float Speed;
    
    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;
    PhotonView view;

    // Movement stickmanAsesino;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

    }


    void Update()
    {
        Rigidbody2D.velocity = Direction*Speed ;

    }

    public void SetDirection(Vector3 direction){
        Direction = direction;
    }

    public void DestroyBullet() {
        Destroy(gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision){
        Movement stickman=collision.collider.GetComponent<Movement>();
            Destroy(gameObject);
        if(stickman!=null){
            // view.RPC("Hit", RpcTarget.All, 1,new Vector2(Direction.x,Direction.y),collision.transform.GetComponent<PhotonView>().ViewID);
            GameObject.FindWithTag("PlayerController").GetComponent<PlayerController>().addPoints(10);
            stickman.ReceiveDamage(1,Direction);
        }
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        Vector3 direction = (Vector3)instantiationData[0];
        float angle = (float)instantiationData[1];
        // stickmanAsesino=instantiationData[2];
        this.GetComponent<Bullet>().SetDirection(direction);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
