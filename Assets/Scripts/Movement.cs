using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour, IPunObservable
{
    public Memory memory;
    public GameObject BulletPrefab;
    public GameObject BombPrefab;
    public float Speed;
    public float JumpForce;
    public bool canMove;
    public int vidas = 3;

    public List<Color> colores = new List<Color>();
    public string nickname;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private float Vertical;
    private bool Grounded;
    private Animator anim;

    private float LastShot;
    private float LastDash;

    private Vector3 positionEnemigo;
    private Vector3 positionPlayer;
    private float distanciaEnemigo;

    public bool canHeal;
    public bool canDash;
    public bool canBomb;
    public bool canAttack;
    private int fuerzaAtaque = 1;
    public bool infiniteDash = false;

    public bool canShoot = true;
    public bool canAttackPlayer = true;

    public Transform mira;
    private Vector2 direcionNormalizada;

    private PhotonView view;
    private SpriteRenderer spriteRenderer;
    public bool isme=false;
    // void Awake()
    // {
    //     colores.Add(new Color32(255, 0, 215, 255));
    //     colores.Add(new Color32(36, 0, 255, 255));
    //     colores.Add(new Color32(50, 210, 0, 255));
    //     colores.Add(new Color32(0, 0, 0, 255));
    //     colores.Add(new Color32(255, 28, 0, 255));
    //     colores.Add(new Color32(0, 221, 255, 255));
    //     colores.Add(new Color32(255, 214, 0, 255));
    //     colores.Add(new Color32(255, 118, 0, 255));
    //     colores.Add(new Color32(255, 255, 255, 255));
    //     colores.Add(new Color32(164, 0, 255, 255));
    //     colores.Add(new Color32(108, 0, 255, 255));
    //     colores.Add(new Color32(0, 136, 255, 255));
    //     colores.Add(new Color32(0, 255, 166, 255));
    //     memory = GameObject.FindWithTag("Memory").GetComponent<Memory>();
    // }

    void Start()
    {
        anim = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        //0.ROSA 1.AZUL 2.VERDE 3.NEGRO 4.ROJO 5.CIAN 6.AMARILLO 7.NARANJA 8.BLANCO 0.VIOLETA 10.MORADO 11.AZUL2 12.VERDE2

        //Ponemos el Photon View como el que instancia
        view = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // view.RPC("RPC_SetColor", RpcTarget.AllBuffered, spriteRenderer.color);
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
                // stream.SendNext(spriteRenderer.color);
            }
            else
            {
                spriteRenderer.flipX = (bool) stream.ReceiveNext();
                // spriteRenderer.color = (Color32) stream.ReceiveNext();
            }
        }
            
        catch{}
    }

    // [PunRPC]
    // void RPC_SetColor(Color transferredColor)
    // {
    //     gameObject.GetComponentInChildren<SpriteRenderer>().color = transferredColor;
    // }

    void Update()
    {
        if (view.IsMine)
        {
            isme=true;
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeColor(12);
            }

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
            if (Input.GetKeyDown(KeyCode.W) && Grounded && canMove)
            {
                Jump();
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
                && canMove
                && canAttackPlayer
            )
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Attack", true);
                Attack(fuerzaAtaque);
                fuerzaAtaque = 1;
            }
            else
            {
                anim.SetBool("Attack", false);
            }

            // ANIMACION DE DISPARAR
            if (
                (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L))
                && Time.time > LastShot + 0.4f
                && canMove
                && canShoot
            )
            {
                
                LastShot = Time.time;
                anim.SetBool("Shot", true);
                Shot();
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
                    if (!infiniteDash)
                    {
                        canDash = false;
                    }
                }
                if (canHeal)
                {
                    if (vidas < 4)
                    {
                        vidas = vidas + 1;
                        canHeal = false;
                    }
                }
                if (canAttack)
                {
                    fuerzaAtaque = 2;
                    canAttack = false;
                }
                if (canBomb)
                {
                    Bomb();
                    canBomb = false;
                }
            }
        }
    }

    public void ChangeColor(int nuevoColor)
    {
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().color = colores[nuevoColor];
        }
    }

    private void Dash(Vector2 direction)
    {
        Rigidbody2D.AddForce(direction * 1000);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Bomb()
    {
        Vector3 mousePos = mira.position;

        Vector3 position = gameObject.transform.position;
        Vector3 direction = (mousePos - position);
        mousePos.x = mousePos.x - position.x;
        mousePos.y = mousePos.y - position.y;

        if (direction.y > 3)
        {
            if (direction.x > 2)
            {
                direction = new Vector3(1, 1, 0);
            }
            else if (direction.x < -2)
            {
                direction = new Vector3(-1, 1, 0);
            }
            else
            {
                direction = Vector3.up * 2;
            }
        }
        else if (direction.x > 0)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
        }

        GameObject bomb = PhotonNetwork.Instantiate(
            BombPrefab.name,
            transform.position + direction * 1f,
            Quaternion.identity
        );
        bomb.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * 2000f);
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

        GameObject bullet = PhotonNetwork.Instantiate(
            BulletPrefab.name,
            transform.position + direction * 1f,
            Quaternion.identity
        );
        bullet.GetComponent<Bullet>().SetDirection(direction);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Attack(int fuerza)
    {
        Collider2D[] colisionesAtaque = Physics2D.OverlapCircleAll(
            gameObject.GetComponentInChildren<Transform>().position + new Vector3(0f, 0.69f, 0f),
            1.5f
        );
        positionPlayer = gameObject.GetComponentInChildren<Transform>().position;

        foreach (Collider2D stickman in colisionesAtaque)
        {
            Vector2 direction = stickman.transform.position - positionPlayer;
            if (stickman.tag=="Player" && stickman.GetComponent<Movement>().isme==false)
            {
                // stickman.tag="Player";
                stickman.GetComponent<Movement>().Hit(fuerza, direction);
            }
        }
    }

    public void Hit(int fuerza, Vector2 direction)
    {
        vidas = vidas - fuerza;
        if (direction.x > 0)
            direcionNormalizada = new Vector2(1, 0);
        else if (direction.x == 0)
            direcionNormalizada = new Vector2(0, 1);
        else
            direcionNormalizada = new Vector2(-1, 0);

        Rigidbody2D.AddForce(
            direcionNormalizada * new Vector2(5000 * fuerzaAtaque, 1000 * fuerzaAtaque)
        );
        if (vidas <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("Hit", true);
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            gameObject.GetComponent<Movement>().canMove = false;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Hit", true);
            gameObject.GetComponent<Animator>().SetBool("Dead", false);
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
