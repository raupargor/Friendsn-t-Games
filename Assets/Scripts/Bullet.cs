using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    
    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

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
        
        if(stickman!=null)stickman.Hit(1,Direction);

        DestroyBullet();
    }
}
