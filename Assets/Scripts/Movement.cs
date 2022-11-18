using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour, IPunObservable
{
    // public Memory memory;
    public GameObject BulletPrefab;
    public GameObject BombPrefab;
    public float Speed;
    public float JumpForce;
    public bool canMove;
    public int vidas = 3;

    // public List<Color> colores = new List<Color>();
    // public string nickname;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private float Vertical;
    private bool Grounded;
    private Animator anim;

    private float LastAttack;
    private float LastShot;
    private float LastDash;

    // private Vector3 positionEnemigo;
    private Vector3 positionPlayer;
    // private float distanciaEnemigo;

    public bool canHeal;
    public bool canDash;
    public bool canBomb;
    public bool canAttack;//particulas de atacar x2
    private int fuerzaAtaque = 1;
    public bool infiniteDash = false;

    public bool canShoot = true;
    public bool canAttackPlayer = true;//poder atacar

    public bool canReceiveDamage = true;

    public Transform mira;
    private Vector2 direcionNormalizada;

    public PhotonView view;
    private SpriteRenderer spriteRenderer;
    public bool isme=false;

    public PlayerController pc;
    public float segundero=0;
    public int puntos;

    void Start()
    {
        anim = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        try{pc = GameObject.FindWithTag("PlayerController").GetComponent<PlayerController>();}catch{}
        
        //Ponemos el Photon View como el que instancia
        view = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        AddObservable();
    }

    private void AddObservable()
    {
        if (!view.ObservedComponents.Contains(this))
        {
            view.ObservedComponents.Add(this);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {   
        try{
            if (stream.IsWriting)
            {
                stream.SendNext(spriteRenderer.flipX);
                stream.SendNext(puntos);
                stream.SendNext(canReceiveDamage);
            }
            else
            {
                spriteRenderer.flipX = (bool) stream.ReceiveNext();
                puntos=(int) stream.ReceiveNext();
                canReceiveDamage =(bool)stream.ReceiveNext();
            }
        }
            
        catch{}
    }


    void Update()
    {   

        if (view.IsMine)
        {   
            segundero+=Time.deltaTime;
            if(segundero>=1 && (canAttackPlayer || canShoot)){ 
                segundero = 0f;
                pc.addPoints(1);
            }
            isme=true;
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            // if (Input.GetKeyDown(KeyCode.C))
            // {
            //     ChangeColor(12);
            // }

            //DETECTAR EL RATON Y PONER LA MIRA
            mira.position = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    -Camera.main.transform.position.z
                )
            );
            Vector3 mousePos = mira.position;
            Vector3 position = gameObject.transform.position;
            Vector3 direction = (mousePos - position);

            //ANIMACION DE CORRER
            if (Horizontal > 0 && canMove)
            {
                gameObject.transform.Translate(Speed * Time.deltaTime, 0, 0);
                spriteRenderer.flipX = true;
                anim.SetBool("Moving", true);
            }
            else if (Horizontal < 0 && canMove)
            {
                spriteRenderer.flipX = false;
                gameObject.transform.Translate(-Speed * Time.deltaTime, 0, 0);
                anim.SetBool("Moving", true);
            }
            else
            {
                anim.SetBool("Moving", false);
            }

            //ANIMACION DE SALTAR
            if (Input.GetKeyDown(KeyCode.W) && Grounded && canMove )
            {
                Jump();
                GameObject.FindGameObjectWithTag("JumpAudio").GetComponent<Music>().PlayMusic();
            }

            if (Physics2D.Raycast(transform.position, Vector3.down, 1f))
            {
                Grounded = true;
                anim.SetBool("Jumping", false);
            }
            else
            {
                if (Vertical > 0)
                {
                    anim.SetBool("Jumping", true);
                }
                Grounded = false;
            }

            //ANIMACION DE ATACAR
            if (
                (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K))
                && Time.time > LastAttack + 0.3f
                && canMove 
                && canAttackPlayer
            )
            {
                // anim.SetBool("Attack", true);
                // anim.SetBool("Attack", true);
                //view.RPC("Attack",RpcTarget.All,fuerzaAtaque);
                LastAttack = Time.time;
                Attack(fuerzaAtaque);
                GameObject.FindGameObjectWithTag("AttackAudio").GetComponent<Music>().PlayMusic();

                fuerzaAtaque = 1;
            }
            else
            {
                anim.SetBool("Attack", false);
            }

            // ANIMACION DE DISPARAR
            if (
                (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L))
                && Time.time > LastShot + 0.5f
                && canMove
                && canShoot
            )
            {
                
                LastShot = Time.time;
                anim.SetBool("Shot", true);
                Shot();
                GameObject.FindGameObjectWithTag("ShotAudio").GetComponent<Music>().PlayMusic();
            }
            else
            {
                anim.SetBool("Shot", false);
            }

            // ANIMACION DE DASH INFINITO
            if (
                Input.GetKeyDown(KeyCode.LeftShift)
                && canMove
                && Time.time > LastDash + 0.4f
                && infiniteDash
            )

            {
                if (Horizontal > 0)
                {
                    anim.SetBool("Dash", true);
                    Dash(Vector2.right);
                    LastDash = Time.time;
                }
                else if (Horizontal < 0)
                {
                    anim.SetBool("Dash", true);
                    Dash(Vector2.left);
                    LastDash = Time.time;
                }
                else if (Horizontal == 0)
                {
                    anim.SetBool("Dash", true);
                    if (direction.x >= 0.1f)
                    {
                        Dash(Vector2.right);
                    }
                    if (direction.x < -0.1f)
                    {
                        Dash(Vector2.left);
                    }
                    LastDash = Time.time;
                }
                else
                {   
                    anim.SetBool("Dash", false);
                }
            }

            //USAR UN OBJETO
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDash && canMove && Time.time > LastDash + 0.4f)
                {
                    if (Horizontal > 0)
                    {
                        anim.SetBool("Dash", true);
                        Dash(Vector2.right);
                        view.RPC("useObject", RpcTarget.All);
                        LastDash = Time.time;
                    }
                    else if (Horizontal < 0)
                    {
                        anim.SetBool("Dash", true);
                        Dash(Vector2.left);
                        view.RPC("useObject", RpcTarget.All);                        
                        LastDash = Time.time;
                    }
                    else if (Horizontal == 0)
                    {
                        anim.SetBool("Dash", true);
                        if (direction.x >= 0.1f)
                        {
                            Dash(Vector2.right);
                            view.RPC("useObject", RpcTarget.All);                        
                        }
                        if (direction.x < -0.1f)
                        {
                            Dash(Vector2.left);
                            view.RPC("useObject", RpcTarget.All);                        
                        }
                        LastDash = Time.time;
                    }
                    else
                    {
                        // anim.SetBool("Dash", false);
                        view.RPC("useObject", RpcTarget.All);
                    }
                    if (!infiniteDash)
                    {
                        canDash = false;
                    }
                }
                if (canHeal)
                {
                    if (vidas < 4)
                    {
                        view.RPC("useObject", RpcTarget.All);
                        pc.addPoints(10);

                    }
                }
                if (canAttack)
                {
                    view.RPC("useObject", RpcTarget.All);
                    pc.addPoints(10);
                }
                if (canBomb)
                {
                    view.RPC("useObject", RpcTarget.All);
                    Bomb();      
                }
            }
        }
    }

    [PunRPC]
    private void useObject(){ 
        if (canDash)
        {
            canDash = false;
        }
        
        if (canHeal)
        {
            vidas = vidas + 1;
            canHeal = false;
        }
        if (canAttack)
        {
            fuerzaAtaque = 2;
            canAttack = false;
        }
        if (canBomb)
        {
            canBomb = false;     
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    private void Dash(Vector2 direction)
    {
        Rigidbody2D.AddForce(direction * 1000);
        pc.addPoints(10);

    }
    private void Bomb()
    {      
        GameObject bomb = PhotonNetwork.Instantiate(BombPrefab.name,transform.position + new Vector3(0,2,0),Quaternion.identity);
        bomb.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * 2000f);
        pc.addPoints(10);

        // bomb.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shot()
    {
        Vector3 mousePos = mira.position;

        Vector3 position = gameObject.transform.position;
        Vector3 direction = (mousePos - position);
        mousePos.x = mousePos.x - position.x;
        mousePos.y = mousePos.y - position.y;

        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;

        if (direction.y > 3)
        {
            if (direction.x > 2)
            {
                direction = new Vector3(1, 1, 0);
                angle = 45;
            }
            else if (direction.x < -2)
            {
                direction = new Vector3(-1, 1, 0);
                angle = 135;
            }
            else
            {
                direction = Vector3.up * 2;
                angle = 90;
            }
        }
        else if (direction.x > 0)
        {
            direction = Vector3.right;
            angle = 0;
        }
        else
        {
            direction = Vector3.left;
            angle = 180;
        }
        object[] myCustomInitData = new object[3];
        myCustomInitData[0] = direction;
        myCustomInitData[1] = angle;
        myCustomInitData[2] = view.ViewID;
        GameObject bullet = PhotonNetwork.Instantiate(
            BulletPrefab.name,
            transform.position + direction * 1f,
            Quaternion.identity,0,myCustomInitData
        );
    }

    public void Attack(int fuerza)
    {   
        anim.SetBool("Attack", true);
        Collider2D[] colisionesAtaque = Physics2D.OverlapCircleAll(
        gameObject.GetComponentInChildren<Transform>().position + new Vector3(0f, 0.80f, 0f),2f);
        positionPlayer = gameObject.GetComponentInChildren<Transform>().position;

        foreach (Collider2D stickman in colisionesAtaque)
        {   
            Vector2 direction = stickman.transform.position - positionPlayer;
            if (stickman.tag=="Player" && stickman.GetComponent<Movement>().isme==false)
            {
                var photonPlayer=stickman.transform.GetComponent<PhotonView>().Owner;
                Debug.Log(photonPlayer);

                view.RPC("Hit", RpcTarget.All, fuerza, direction,stickman.transform.GetComponent<PhotonView>().ViewID);
                pc.addPoints(10);
            }
        }
    }

    [PunRPC]
    public void Hit(int fuerza, Vector2 direction,int stickmanID)
    {   

        var stickman=PhotonView.Find(stickmanID).GetComponent<Movement>();
        // Debug.Log($"Ay,{view.Owner.NickName} fue golpeado");
        if(stickman.canReceiveDamage){
            stickman.vidas=stickman.vidas-fuerza;
        }
        // vidas = vidas - fuerza;
        if (direction.x > 0)
            stickman.direcionNormalizada = new Vector2(1, 0);
        else if (direction.x == 0)
            stickman.direcionNormalizada = new Vector2(0, 1);
        else
            stickman.direcionNormalizada  = new Vector2(-1, 0);

        Rigidbody2D.AddForce(
            stickman.direcionNormalizada * new Vector2(5000 * fuerzaAtaque, 1000 * fuerzaAtaque)
        );
        if (stickman.vidas <= 0 && stickman.canReceiveDamage)
        {
            stickman.GetComponent<Animator>().SetBool("Hit", true);
            stickman.GetComponent<Animator>().SetBool("Dead", true);
            stickman.canMove = false;     
            

        }
        else
        {
            stickman.GetComponent<Animator>().SetBool("Hit", true);
            stickman.GetComponent<Animator>().SetBool("Dead", false);
        }
    }
   public void ReceiveDamage(int fuerza, Vector2 direction)
    {   
        if(canReceiveDamage){
            vidas=vidas-fuerza;
        }
        if (direction.x > 0)
            direcionNormalizada = new Vector2(1, 0);
        else if (direction.x == 0)
            direcionNormalizada = new Vector2(0, 1);
        else
            direcionNormalizada  = new Vector2(-1, 0);

        Rigidbody2D.AddForce(
            direcionNormalizada * new Vector2(5000 * fuerzaAtaque, 1000 * fuerzaAtaque)
        );

        if (vidas <= 0)
        {
            GetComponent<Animator>().SetBool("Hit", true);
            GetComponent<Animator>().SetBool("Dead", true);
            canMove = false;          
        }
        else
        {
            GetComponent<Animator>().SetBool("Hit", true);
            GetComponent<Animator>().SetBool("Dead", false);
        }
    }

    public void SelectCanMove()
    {
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<Movement>().canMove = true;
        }
    }

    public void UnselectCanMove()
    {
        gameObject.GetComponent<Movement>().canMove = false;
    }

    public void DestroyStickman()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void StopHitAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
    }

    public void StopDashAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("Dash", false);
    }
}
