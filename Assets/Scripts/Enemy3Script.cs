using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy3Script : MonoBehaviour
{
    //public ParticleSystem parsys;
    
    public int Health;
    public int HealthMax;

    AudioSource scream;

    bool audioonce;
    
    [SerializeField]
    float agroRangeMax;
    [SerializeField]
    float agroRangeMin;
    [SerializeField]
    float agroRangeOk;
    [SerializeField]
    float agroRangeAttack;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float wanderSpeed;
    [SerializeField]
    float maxDistance;    
    [SerializeField]
    float range;              
    [SerializeField]
    Transform CastPos;
    [SerializeField]
    Transform BackPos;
    [SerializeField]
    GameObject ParHitPoison;
    public GameObject HitGun;


    private Vector2 endPos;


    public AudioClip gunShot;
    public AudioClip gunHit;


    public float countCast = 0.0f;
    public float timeCast;
    public float timeCastC;
    public float RespTime;

    Rigidbody2D rb2d;
    Animator anime;
    BoxCollider2D boxC;

    public GameObject Player;

    float distPlayer;

    bool isCharging = false;
    bool isCasting = false;
    public bool isFacingLeft = false;
    bool isPoison = false;
    bool isAboutCast = false;
    bool isFlee = false;
    bool isHiding = false;
    public bool detected;
    bool isWaiting;
    bool CanShoot = true;
    bool isShooting;


    string magiaC;

    Vector2 wayPoint;
    Vector2 respawn;
    bool newWay;

    UnityEngine.Object magiaRef;
    UnityEngine.Object EnemyRef;
    IEnumerator MagiaChargeR;
    IEnumerator poisonR;
    IEnumerator detectedTimeR;

    bool detectedTimer;

    private SpriteRenderer rend;

    Transform player;

    public Slider castSlider;

    private float tiroCount;
    private float tiroMax = 1f;


    void Start()
    {
        //parsys = GetComponent<ParticleSystem>();
        rb2d = GetComponent<Rigidbody2D>();
        magiaRef = Resources.Load("MagiaEnemy");
        EnemyRef = Resources.Load("Enemy1");
        anime = GetComponent<Animator>();
        boxC = GetComponent<BoxCollider2D>();
        respawn = transform.position;
        Health = HealthMax;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 7, true);
        scream = GetComponent<AudioSource>();
        SetNewDestination();
        rend = GetComponent<SpriteRenderer>();    


        
        if(isFacingLeft)
            transform.localScale = new Vector3(-1, 1, 1);
        

    }

    void Update()
    {
        GetPlayerTransform();

        tiroCount += .2f * Time.deltaTime;
        if(tiroCount >= tiroMax) tiroCount = tiroMax;

        if (Health <= 0)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
            Invoke ("Death", RespTime);

        }

        ////////////////////////////////////////////AGRO PLAYER 
        if(CanSeePlayer(agroRangeMax))
           isAboutCast = false;

        if(CanSeePlayer(agroRangeOk)) 
           { detected = true;
            if(!audioonce)
                {
                scream.Play();
                 audioonce = true;
                 }
           }

        else if(!CanSeePlayer(agroRangeMin) && !detectedTimer)
            {      
            if (detectedTimeR != null )
            StopCoroutine(detectedTimeR);
            
            detectedTimeR = detectedTime();
            StartCoroutine(detectedTimeR);    
            }
        if (CanSeePlayer(agroRangeMin) && !CanSeePlayer(agroRangeAttack) && !isCharging && !isCasting && !isHiding && detected  && !isShooting)
        {
            ChasePlayer();

        }
        else if(CanSeePlayer(agroRangeAttack) && detected && !isHiding  && !isShooting)
        { 
            Attack();
        }
        
        else if(isFlee && !isAboutCast && !isCasting && !isHiding  && !isShooting)
        {
            FleePlayer();
        }
        else if(isAboutCast && !isHiding  && !isShooting)    
        {
            ChasePlayer();
        }

              
        else
        {
            StopChase();

        }
       ////////////////////////////////////////////BARRA DE CARREGAMENTO DE MAGIA

       if (isCharging)
            
            {
            castSlider.gameObject.SetActive(true);   
            countCast += Time.deltaTime;
            countCast = Mathf.Clamp (countCast, 0.0f, timeCastC);
            castSlider.value = countCast / timeCastC;
            }
        else
        {
            castSlider.gameObject.SetActive(false);   
        }
    }

  //////////////////////////////////////////////////////ACTIONS  
    void AttackPlayer()
    {
            Debug.Log("shoot");
            Debug.Log(tiroCount);
            Debug.Log(CanShoot);
            Debug.Log(isShooting);

            if(tiroCount > 0 && CanShoot && !isShooting)
            {
            Debug.Log("shoot a");
            isShooting = true;
            StartCoroutine(ShootTimer());
            Debug.Log("shoot b");
            tiroCount --;

            }

    }

    void Hiding()
    {
        if (!isCasting && !isHiding && !isShooting)
        {
            StartCoroutine(HidingR());
        }
    }

    void ChasePlayer()
    {
        

        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(0.0015f, 0.005f);
            isFacingLeft = false;

        }

        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(-1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(-0.0015f, 0.005f);
            isFacingLeft = true;

        }
    }

    void turnToPlayer()
    {

        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            castSlider.gameObject.transform.localScale = new Vector2(0.0015f, 0.005f);
            isFacingLeft = false;
            

        }

        else if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            castSlider.gameObject.transform.localScale = new Vector2(-0.0015f, 0.005f);
            isFacingLeft = true;

        }
    }

    void FleePlayer()
    {
        

        if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(0.0015f, 0.005f);
            isFacingLeft = false;
        }

        else if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(-1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(-0.0015f, 0.005f);
            isFacingLeft = true;
        }
    }
    void StopChase()
    {
        if(!isCasting && !isHiding && !isShooting)
           { 
        if(Vector2.Distance(transform.position, wayPoint) < range || detected)
        {
            anime.Play("PlayerIdle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            StartCoroutine(wanderTime(Random.Range(5, 7)));
            SetNewDestination();
        }

        if (transform.position.x < wayPoint.x && !isWaiting)
        {
            rb2d.velocity = new Vector2(wanderSpeed, rb2d.velocity.y);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(-1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(0.0015f, 0.005f);
            isFacingLeft = false;

        }

        else if (transform.position.x > wayPoint.x && !isWaiting)
        {
            rb2d.velocity = new Vector2(-wanderSpeed, rb2d.velocity.y);
            anime.Play("PlayerRun");
            transform.localScale = new Vector3(-1, 1, 1);
            castSlider.gameObject.transform.localScale = new Vector2(-0.0015f, 0.005f);
            isFacingLeft = true;

        }
           } 

     

    }

/////////////////////////////////////////////////////ENEMY WANDER

    void SetNewDestination()
    {
        if(!newWay)
        {
            wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y);
            newWay = true;
        }

        else if(newWay)
        {
            wayPoint = respawn;
            newWay = false;
        }
    }


/////////////////////////////////////////////////////PLAYER TRACER
    void GetPlayerTransform()
    {
        GameObject playerPos = GameObject.Find("Player");
        if(playerPos != null)
          player = playerPos.transform;
            
        
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
       

        if(isFacingLeft)
            {
                castDist = -distance;
            }

        Vector2 endPos = CastPos.position + Vector3.right * castDist;
        Vector2 endPosBack = BackPos.position - Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(CastPos.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D hitBack = Physics2D.Linecast(BackPos.position, endPosBack, 1 << LayerMask.NameToLayer("Player"));

        if(hit.collider !=null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(CastPos.position, hit.point, Color.red);
        }

        else if(hitBack.collider !=null)
        {
            if (hitBack.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(BackPos.position, hitBack.point, Color.red);
        }

        
        else
        {
            Debug.DrawLine(CastPos.position, endPos, Color.blue);
            Debug.DrawLine(BackPos.position, endPosBack, Color.blue);

        }
        return val;
           
    }
//////////////////////////////////////////////////////ROTINAS MAGIA
        
        IEnumerator MagiaCharge(int attack)
    {
        isCharging = true;
        countCast = 0.0f;
        timeCastC = 0.9f;

        if(attack == 0)
        {
        isFlee = true;
        yield return new WaitForSeconds(timeCastC / 3);
        isAboutCast = true;
        yield return new WaitForSeconds(2 * timeCastC / 3);
        }

        else if(attack == 1)
        {
        yield return new WaitForSeconds(timeCastC);

        
        }

        else if(attack == 2)
        {
        yield return new WaitForSeconds(timeCastC / 3);
        isAboutCast = true;
        yield return new WaitForSeconds(2 * timeCastC / 3);
        }
        
        else if(attack == 3)
        {
        isAboutCast = true;
        yield return new WaitForSeconds(timeCastC / 3);
        Hiding();
        yield return new WaitForSeconds(2 * timeCastC / 3);
        }

        else if(attack == 4)
        {
        isFlee = true;
        yield return new WaitForSeconds(2 * timeCastC / 3);
        isAboutCast = true;
        yield return new WaitForSeconds(timeCastC / 3);
        }


        if (!isCasting)
        {

            StartCoroutine(MagiaLaunch());

        }
    }

   
    IEnumerator MagiaLaunch()
    {
        isCasting = true;
        isCharging = false;
        isFlee = false;
        turnToPlayer();
        yield return new WaitForSeconds(.2f);
        isAboutCast = false;

        anime.Play("PlayerCast");
        rb2d.velocity = new Vector2(0, 0);
        GameObject magia = (GameObject)Instantiate(magiaRef);
        magia.transform.position = CastPos.transform.position;

        if(isFacingLeft)
        magia.GetComponent<Magia>().StartMagic(true);

        else
        magia.GetComponent<Magia>().StartMagic(false);
        
        yield return new WaitForSeconds(.4f);
        isCasting = false;

        anime.Play("PlayerIdle");

    }

     IEnumerator Poison(int damage)
     {
        
        if (poisonR != null)
        {
        StopCoroutine(poisonR);
        }

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
            isFlee = false;

            Health -= damage;
            Instantiate(ParHitPoison, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            yield return new WaitForSeconds(2);
        } 

        isPoison = false;
    }

        void Hit(int damage)
    {
        //parsys.Play();
        Health -= damage;
        isCharging = false;
        isAboutCast = false;
        isFlee = false;
        detected = true;

    
        if (MagiaChargeR != null)
            StopCoroutine(MagiaChargeR);

    }

    void Attack()
    {
    System.Action[] allAttacks = new System.Action[] 
    {
        AttackPlayer,
        Hiding,
    };

    int whichAttack = Random.Range( 0,  allAttacks.Length);

    System.Action whatToCall = allAttacks[whichAttack];

    whatToCall();
    }

    IEnumerator HidingR()
    {
        isHiding = true;
        anime.Play("PlayerHiding");
        rb2d.velocity = new Vector2(0, 0);
        boxC.enabled = false;
        Player.transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y, 3);
        yield return new WaitForSeconds(1);
        Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 1);

        isHiding = false;
        boxC.enabled = true;
    }
    
        IEnumerator wanderTime(float timeWander)
    {
        isWaiting = true;
        yield return new WaitForSeconds(timeWander);
        isWaiting = false;
      
    }

    IEnumerator detectedTime()
    {
        detectedTimer = true;
        yield return new WaitForSeconds(10);
        if(!CanSeePlayer(agroRangeMin))
        detected = false;
        detectedTimer = false;



    }

    void Death()
    {
        GameObject EnemyResp = (GameObject)Instantiate(EnemyRef);
        EnemyResp.transform.position = respawn;
        Destroy(gameObject);
    }

private void Shoot()
{
    turnToPlayer();
    isShooting = true;
    anime.Play("PlayerGun");
    AudioSource.PlayClipAtPoint(gunShot, transform.position);
    if(!isFacingLeft)
    endPos = CastPos.position + Vector3.right * 5;

    else if(isFacingLeft)
    endPos = CastPos.position + Vector3.left * 5;
                
    Debug.DrawLine(CastPos.position, endPos, Color.blue);
    StartCoroutine(ShootR());
    StartCoroutine(ShootRC());
    RaycastHit2D raycastGun = Physics2D.Linecast(CastPos.position, endPos, 1 << LayerMask.NameToLayer("Player"));
    if(raycastGun.collider != null)
    {
        if(raycastGun.collider.gameObject.tag == "Player")
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
    rb2d.velocity = new Vector2(0, 0);
    yield return new WaitForSeconds(0.4f);
    isShooting = false;

}

IEnumerator ShootRC()
{
    CanShoot = false;
    yield return new WaitForSeconds(.5f);
    CanShoot = true;

}

IEnumerator ShootTimer()
{
    anime.Play("PlayerGun");
    yield return new WaitForSeconds(Random.Range(1f, 1.5f));
    Shoot();
}

}