using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Speed;
    
    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;
    float timer = 0.0f;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.AddForce(new Vector3(0,1f,0));

    }


    void Update()
    {
        timer += Time.deltaTime;
        float seconds = timer % 60;
        if(seconds>=2.7f){
            gameObject.GetComponent<Animator>().SetBool("EXPLODE",true);
            timer=0f;
            }

    }

    public void SetDirection(Vector3 direction){
        Direction = direction;
    }

    public void DestroyBomb() {
        Destroy(gameObject);

    }

    public void Explode() {
        
        Collider2D[] colisionesBomba= Physics2D.OverlapCircleAll(gameObject.GetComponentInChildren<Transform>().position,2.5f);
        foreach(Collider2D stickman in colisionesBomba){ 
            if(stickman.tag=="Enemy" || stickman.tag=="Player"){

                stickman.GetComponent<Movement>().Hit(1,Vector2.up);
            }
        }
        DestroyBomb();
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag=="Enemy"){
            gameObject.GetComponent<Animator>().SetBool("EXPLODE",true);
        }
    }
}
