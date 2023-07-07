using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;


public class Control : MonoBehaviour
{
    
    Animator anime;
    Rigidbody2D rg2d;
    SpriteRenderer spriter;
    UnityEngine.Object magiaRef;
    UnityEngine.Object magiaRefPoison;
    UnityEngine.Object poderRef;
    public AudioClip gunShot;
    public AudioClip gunHit;
    public AudioClip portaAbrindo;
    public AudioClip portaDestrancando;
    public AudioClip portaTrancada;


    private GameObject portaAtual;
    BoxCollider2D boxC;
 
    public HealthBar healthBar;
    public PoderBar poderBar;
    public GameObject map;

    AudioSource passos;

    [SerializeField] private LayerMask cenario;

    public GameObject HitGun;
    public GameObject resetGame;
    public GameObject camera;
    public GameObject cameraPos;
    public GameObject fizzle;
    ParticleSystem fizzlePar;    


    public GameObject Player;
    public ParticleSystem parsys;
    bool parsysOnce;

    public int Health = 100;

    public int maxHealth = 100;
    public int currentHealth;

    public int bulletMax = 2;

    public int poderMax = 3;
    public int poderAtual = 0;


    public float countCast = 0.0f;
    public float timeCast;
    public float timeCastC;
    public float jumpForce;
    public float jumpMin;
    public float jumpMax;

    bool isHiding = false;
    bool isCasting = false;
    bool isCharging = false;
    bool isChargingJump = false;
    bool isFacingLeft = false;
    bool isPoison = true;
    bool magiaC = false;
    bool gotHit = false;
    bool poderC = false;
    public bool canJump = true;
    bool CanShoot = true;
    bool _openMap = false;

    bool isShooting = false;
    ///public bool isGrounded;
    public float jumpValue = 0.0f;

    CircleCollider2D cc2D;

    public GameObject ParHitPoison;

    public Slider castSlider;

    public MapScript maps;
    
    IEnumerator MagiaChargeR;
    IEnumerator poisonR;

    private Vector2 endPos;
    
   

    [SerializeField]
    Transform CastPos;


    void Start()
    {
        anime = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        magiaRef = Resources.Load("MagiaProjetil");
        magiaRefPoison = Resources.Load("MagiaProjetilPoison");
        poderRef = Resources.Load("Poder");
        parsys = GetComponent<ParticleSystem>();
        boxC = GetComponent<BoxCollider2D>();
        cc2D = GetComponent<CircleCollider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        resetGame.SetActive(false);
        poderBar.SetMaxPoder(poderMax);
        passos = GetComponent<AudioSource>();
        fizzlePar = fizzle.GetComponentInChildren<ParticleSystem>();


  
    }
    

    private void Update()
    {
        if (Input.GetKeyDown("m"))
        {
           if(!_openMap)
            {
            _openMap = true;
            map.SetActive(true);
            }


            else if(_openMap)
            {
            _openMap = false;
            map.SetActive(false);
            }
        }
         

        if(poderAtual > poderMax)
            poderAtual = poderMax;

        if(BulletCounterScript.bulletCount > bulletMax)  
            BulletCounterScript.bulletCount = bulletMax;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            resetGame.SetActive(true);
        }

        if (Input.GetKey("space") && isGrounded() && canJump && !isCasting && !gotHit && !isHiding && !isShooting)
        {
            jumpValue += 1 * Time.deltaTime;
            timeCastC = jumpMax;
            isCharging = true;
            isChargingJump = true;
            rg2d.velocity = new Vector2(0f, 0f);
            anime.Play("PlayerIdle");

        }

        if (jumpValue >= jumpMax && isGrounded() && canJump && !isHiding && !isCasting && !gotHit && !isShooting)
        {
            if(!isFacingLeft)
            {
            float tempx = 1f;
            float tempy = jumpValue * 3;
            rg2d.velocity = new Vector2(tempx, tempy);
            canJump = false;
            countCast = 0.0f;
            isCharging = false;
            StartCoroutine(jumpR());
            }
            else if(isFacingLeft)
            {
            float tempx = -1f;
            float tempy = jumpValue * 3;
            rg2d.velocity = new Vector2(tempx, tempy);
            canJump = false;
            countCast = 0.0f;
            isCharging = false;
            StartCoroutine(jumpR());
            }  


                        
        }

        if (Input.GetKeyUp("space") && isGrounded() && canJump && !isHiding && !isCasting && !gotHit && !isShooting)
        {
            if(jumpValue >= jumpMin && !isFacingLeft)
            {
            float tempx = 2f;
            float tempy = jumpValue * 3;
            rg2d.velocity = new Vector2(tempx, tempy);
            canJump = false;
            jumpValue = 0.0f;  
            countCast = 0.0f;
            isCharging = false;
            StartCoroutine(jumpR());

            }
            else if(jumpValue >= jumpMin && isFacingLeft)
            {
            float tempx = -2f;
            float tempy = jumpValue * 3;
            rg2d.velocity = new Vector2(tempx, tempy);
            canJump = false;
            jumpValue = 0.0f;  
            countCast = 0.0f;
            isCharging = false;
            StartCoroutine(jumpR());

            }            
            
            else if (jumpValue < jumpMin)
            {
            isChargingJump = false;            
            jumpValue = 0.0f;  
            countCast = 0.0f;
            isCharging = false;
            }

        }          
                
        if (Input.GetKeyDown("h") && !isHiding && !isCasting && !isCharging && !gotHit && isGrounded() && !isShooting)
        {
            if (MagiaChargeR != null)
            StopCoroutine(MagiaChargeR);
        
            MagiaChargeR = MagiaCharge(0.8f);
            StartCoroutine(MagiaChargeR);
            
        }

        if (Input.GetKeyDown("j") && !isHiding && !isCasting && !isCharging && !gotHit && isGrounded() && !isShooting)
        {
            if (MagiaChargeR != null)
            StopCoroutine(MagiaChargeR);

            magiaC = true;
            MagiaChargeR = MagiaCharge(1.5f);
            StartCoroutine(MagiaChargeR);

        }

        if (Input.GetKeyDown("w") && !isHiding && !isCasting && !gotHit && isGrounded() && !isShooting)
        {
           StartCoroutine(HidingDelay());

        }

        if (Input.GetKeyDown("g") && !isHiding && !isCasting && !gotHit && isGrounded() && !isShooting && CanShoot)
        {
            if(BulletCounterScript.bulletCount >= 1)
            {
            isShooting = true;
            anime.Play("PlayerGun");
            AudioSource.PlayClipAtPoint(gunShot, transform.position);
            Shoot();
            BulletCounterScript.bulletCount --;
            }
        }        

        if (Input.GetKeyDown("e") && !isHiding && !isCasting && !gotHit && isGrounded() && !isShooting)
        {
           if(portaAtual != null)
            {
                
                if(portaAtual.GetComponent<Porta>().openfromOther == true)
                     AudioSource.PlayClipAtPoint(portaTrancada, transform.position, 1f);

                
                else if (portaAtual.GetComponent<Porta>().closeKey == true)
                    {
                        if (KeyScript.keyValor >= 1)
                        {
                            portaAtual.GetComponent<Porta>().closeKey = false;
                            KeyScript.keyValor --;
                            AudioSource.PlayClipAtPoint(portaDestrancando, transform.position, 1f);
                            StartCoroutine(KeyOpen());

                        }

                        else
                        AudioSource.PlayClipAtPoint(portaTrancada, transform.position, 1f);

                    }                        


                else if(portaAtual.GetComponent<Porta>().openfromOther == false || portaAtual.GetComponent<Porta>().closeKey == false)
                {                
                if(portaAtual.GetComponent<Porta>().isFacingLeft == true)
                {
                transform.localScale = new Vector3(-1, 1, 1);
                castSlider.gameObject.transform.localScale = new Vector2(-0.0015f, 0.005f);
                }
                else if(portaAtual.GetComponent<Porta>().isFacingRight == true)
                {
                transform.localScale = new Vector3(1, 1, 1);
                castSlider.gameObject.transform.localScale = new Vector2(0.0015f, 0.005f);
                }
                AudioSource.PlayClipAtPoint(portaAbrindo, transform.position);
                MapScript._map = portaAtual.GetComponent<Porta>()._mapV;
                MapScript._area = portaAtual.GetComponent<Porta>()._areaV;
                maps.MapUpdate();
                transform.position = portaAtual.GetComponent<Porta>().GetDestino().position;
                transform.position = new Vector3(transform.position.x, transform.position.y, 1);
                camera.transform.position = new Vector3(cameraPos.transform.position.x, cameraPos.transform.position.y + 0.8f, -10);              
                }
            }

        }

        if (Input.GetKeyDown("k") && !isHiding && !isCasting && !gotHit && isGrounded() && !isShooting)
        {
            if(poderAtual == poderMax)
            {

            if (MagiaChargeR != null)
            StopCoroutine(MagiaChargeR);
            
            poderC = true;
            magiaC = false;

            MagiaChargeR = MagiaCharge(.1f);
            StartCoroutine(MagiaChargeR);
            poderAtual = 0;
            poderBar.SetPoder(poderAtual);
            }
            

        }

        if (isCharging)
            
            {
                if(!parsysOnce)
            {
            parsys.Play();
            parsysOnce = true;
            }
            castSlider.gameObject.SetActive(true);   
            countCast += Time.deltaTime;
            countCast = Mathf.Clamp (countCast, 0.0f, timeCastC);
            castSlider.value = countCast / timeCastC;
            
            }
        else
        {
            castSlider.gameObject.SetActive(false);  
            parsys.Stop();
            parsysOnce = false;

        }

    BulletCounterScript.bulletCount  += .2f * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (Input.GetKey("d") && !isHiding && !isCasting && !gotHit && isGrounded() && !isChargingJump && !isShooting)
        {
            rg2d.velocity = new Vector2(2, 0);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(0.0015f, 0.005f);
            isFacingLeft = false;
            passos.enabled = true;
        }


        else if (Input.GetKey("a") && !isHiding && !isCasting && !gotHit && isGrounded() && !isChargingJump && !isShooting)
        {
            rg2d.velocity = new Vector2(-2, 0);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(-1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(-0.0015f, 0.005f);
            isFacingLeft = true;
            passos.enabled = true;

        }
           
        else if  (!isHiding && !isCasting && isGrounded() && !isChargingJump && !isShooting)
        {
            anime.Play("PlayerIdle");
            rg2d.velocity = new Vector2(0, 0);
            passos.enabled = false;

        }

        

    }



    // Tempo de Hiding
    IEnumerator HidingDelay()
    {
        isHiding = true;
        rg2d.velocity = new Vector2(0, 0);
        anime.Play("PlayerHiding");
        boxC.enabled = false;
        Player.transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y, 3);
        yield return new WaitForSeconds(.5f);
        Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 1);

        isHiding = false;
        boxC.enabled = true;

    }

    /// Poison

    IEnumerator Poison(int damage)
    {
        if (poisonR != null)
        StopCoroutine(poisonR);

        poisonR = PoisonR(damage);
        StartCoroutine(poisonR);

        yield return new WaitForSeconds(1);
    }


    IEnumerator PoisonR(int damage)
    {

        for(int i = 10; i > 0; i--)  
        {
            if (MagiaChargeR != null)
            StopCoroutine(MagiaChargeR);
        
            isPoison = true;
            isCharging = false;
            Player.transform.GetChild(0).gameObject.SetActive(false);
            Health -= damage;
            Instantiate(ParHitPoison, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

            yield return new WaitForSeconds(3);
        } 

      isPoison = false;
    }

    /// Tempo de Charge Magia
    IEnumerator MagiaCharge(float timeCast)
    {   
        countCast = 0.0f;
        timeCastC = timeCast;
        isCharging = true;
        Player.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(timeCast);
        if (!isHiding && !isCasting)
        {

            StartCoroutine(MagiaLaunch());

        }

       if (isHiding == true)
       {
            Player.transform.GetChild(0).gameObject.SetActive(false);
            isCharging = false;
            ///fizzlePar.Play();

        }
    }


    /////////////////// TEMPO ENTRE PULOS
    IEnumerator jumpR()
    {
        isCharging = false;  
        yield return new WaitForSeconds(.2f);
        isChargingJump = false;            
        canJump = true;
    }

    // Tempo de Cast Magia
    IEnumerator MagiaLaunch()
    {
            countCast = 0.0f;
            isCasting = true;
            isCharging = false;
            anime.Play("PlayerCast");
            if(!magiaC && !poderC)
            {
            GameObject magia = (GameObject)Instantiate(magiaRef);
            magia.transform.position = CastPos.transform.position;
            magia.GetComponent<Magia>().StartMagic(isFacingLeft);
            rg2d.velocity = new Vector2(0, 0);
            }
            else if(magiaC)
            {
            GameObject magia = (GameObject)Instantiate(magiaRefPoison);
            magia.transform.position = CastPos.transform.position;
            magia.GetComponent<MagiaPoison>().StartMagic(isFacingLeft);
            rg2d.velocity = new Vector2(0, 0);
            magiaC = false;
            }

            else if(poderC)
            {
            GameObject poder = (GameObject)Instantiate(poderRef);
            poder.transform.position = CastPos.transform.position;
            poder.GetComponent<Poder>().StartMagic(isFacingLeft);
            rg2d.velocity = new Vector2(0, 0);
            poderC = false;
            }
            yield return new WaitForSeconds(.2f);
            Player.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(.2f);
            isCasting = false;
                              
    }

/////////////////////////////////RECIEVERS / HIT E PODER
    IEnumerator Hit(int damage)
    {
        gotHit = true;
        isCharging = false;

        if(MagiaChargeR != null)
        StopCoroutine(MagiaChargeR);

        Player.transform.GetChild(0).gameObject.SetActive(false);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        magiaC = false;
        rg2d.velocity = new Vector2(0, 0);
        anime.Play("PlayerIdle");

        yield return new WaitForSeconds(.2f);
        gotHit = false;
        ///fizzlePar.Play();


     
    }

   public void setPoder()
    {
    if(poderAtual != poderMax)
    {
    poderAtual += 1;
    }
    poderBar.SetPoder(poderAtual);

//////////////////////////////////// PORTA

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Porta"))
        {
            portaAtual = collision.gameObject;

        }
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Porta"))
        {
            if(collision.gameObject == portaAtual)
            {
                portaAtual = null;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
            if(col.gameObject.tag == "Ground")
        {
            if(isGrounded())
        {jumpValue = 0.0f;
            rg2d.velocity = new Vector2(0, 0);}


        }   
    
    }

public bool isGrounded()
{   
    bool check = false;;
    float extraH = .03f;
    RaycastHit2D raycastHit = Physics2D.Raycast(cc2D.bounds.center, Vector2.down, cc2D.bounds.extents.y + extraH, cenario);
    Color rayColor;
    if(raycastHit.collider != null) 
    {    
        check = true;
        Debug.DrawRay(cc2D.bounds.center, Vector2.down * (cc2D.bounds.extents.y + extraH), Color.red);
    }

       else 
       { 
        check = false;
        Debug.DrawRay(cc2D.bounds.center, Vector2.down * (cc2D.bounds.extents.y + extraH));
       }

    return check;
}

private void Shoot()
{
    if(!isFacingLeft)
    endPos = CastPos.position + Vector3.right * 5;

    else if(isFacingLeft)
    endPos = CastPos.position + Vector3.left * 5;
                
    Debug.DrawLine(CastPos.position, endPos, Color.blue);
    StartCoroutine(ShootR());
    StartCoroutine(ShootRC());
    RaycastHit2D raycastGun = Physics2D.Linecast(CastPos.position, endPos, 1 << LayerMask.NameToLayer("Enemy"));
    if(raycastGun.collider != null)
    {
        if(raycastGun.collider.gameObject.tag == "Enemy")
        {
                CinemachineShake.Instance.ShakeCamera(.5f, .1f);
        raycastGun.collider.gameObject.SendMessage("Hit", 5);
        AudioSource.PlayClipAtPoint(gunShot, raycastGun.collider.gameObject.transform.position);
        Instantiate(HitGun, raycastGun.collider.gameObject.transform.position, Quaternion.identity);
        }
    }
}

IEnumerator ShootR()
{
    rg2d.velocity = new Vector2(0, 0);
    yield return new WaitForSeconds(.4f);
    isShooting = false;

}

IEnumerator ShootRC()
{
    CanShoot = false;
    yield return new WaitForSeconds(.5f);
    CanShoot = true;

}

IEnumerator KeyOpen()
{
    isCasting = true;
    rg2d.velocity = new Vector2(0, 0);
    anime.Play("PlayerIdle");
    yield return new WaitForSeconds(1f);
    isCasting = false;

}

}

