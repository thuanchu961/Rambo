using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController player_instance;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private Transform LaunchPoint;
    [SerializeField] public int totalGrenade;
    [SerializeField] public int totalLife;
    [SerializeField] public GameObject grenadePrefabs;
    [SerializeField] public GameObject effectBoostAttack;
 


    public int score;
    private float timeShoot = 1f;


    private bool isShoot = false;
    private bool isThrow = false;
    private bool isGround;
    private bool isAlive;
    private string currentState;
    Vector2 movement = Vector2.zero;
    float timer;



    AnimController animController;
    Animator anim;
    BoxCollider2D box;
    Rigidbody2D rigi;

    private void Awake()
    {
        player_instance = this;
        box = this.GetComponent<BoxCollider2D>();
        rigi = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        isAlive = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("Score", score);
        timer = timeShoot;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;
        Move();
        //Shoot();
        //if(totalGrenade > 0)
        //{
        //    Throw();
        //}
        ChangeAnimation();
        Debug.Log("totalLife: " + totalLife);
        StopCoroutine(delayAnim());
    }
    
    public void Move()
    {
        isGround = GroundCheck();
        
        //movement.x = Input.GetAxisRaw("Horizontal");
        movement.x = JoyStick.Instant.GetJoyVectorRaw().x;
        Debug.Log(VariableJoystick.instance.Horizontal);
        movement.y = rigi.velocity.y;
        Vector2 scale = this.transform.localScale;
    
       
        if (JoyStick.Instant.GetJoyVectorRaw().x > 0)
        {
            movement.x += speed * Time.deltaTime;
            //rigi.velocity = new Vector2(speed * Time.deltaTime, movement.y);
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (JoyStick.Instant.GetJoyVectorRaw().x < 0)
        {
            movement.x -= speed * Time.deltaTime;
            //rigi.velocity = new Vector2(-speed * Time.deltaTime, movement.y);
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            rigi.velocity = Vector2.zero;
        }
        this.transform.localScale = scale;

        rigi.velocity = movement;
    }
  
    //private void Shoot1()
    //{
    //    if (timer > 0)
    //    {
    //        timer -= Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.J))
    //    {
    //        if(timer > 0)
    //        {
    //            return;
    //        }
    //        PlayerBullet b1 = PlayerObjectPooling.instance.GetBullet().GetComponent<PlayerBullet>();
    //        b1.way = this.transform.localScale.x;
    //        b1.transform.position = FirePoint.position;
    //        b1.gameObject.SetActive(true);
    //        AudioManager.instance.PlaySound(0);
    //        timer = timeShoot;
    //        Debug.Log("shooting");
    //    }
    //}
    public void Shoot()
    {
        isShoot = true;
        //anim.SetTrigger(AnimController.STATE.shoot.ToString());
        //anim.SetFloat("shoot_state", JoyStick.Instant.GetJoyVectorRaw().x != 0 ? 1 : 0);  // = 0 idle shoot ||  = 1 walk shoot
        //Debug.Log("shoot state " + anim.GetFloat("shoot_state"));
        PlayerBullet b1 = PlayerObjectPooling.instance.GetBullet().GetComponent<PlayerBullet>();
        b1.way = this.transform.localScale.x;
        b1.transform.position = FirePoint.position;
        b1.gameObject.SetActive(true);
        AudioManager.instance.PlaySound(0);
        Debug.Log("shooting");
    }

    public void Throw()
    {
        if (totalGrenade > 0)
        {
            isThrow = true;
            anim.SetTrigger(AnimController.STATE._throw.ToString());
            anim.SetFloat("throw_state", JoyStick.Instant.GetJoyVectorRaw().x != 0 ? 1 : 0);
            Grenade g = Instantiate(grenadePrefabs, LaunchPoint.position, Quaternion.identity).GetComponent<Grenade>();
            g.way = this.transform.localScale.x;
            totalGrenade--;
            Debug.Log("Throw grenade");
        }

    }
    public void Jump()
    {
        if (isGround)
        {
            rigi.velocity = Vector2.up * jumpForce;
            anim.SetTrigger(AnimController.STATE.jump.ToString());
            isGround = false;
        }
    }
    public void ChangeAnimation()
    {
        if (isGround)
        {
            if(rigi.velocity.x == 0 && JoyStick.Instant.GetJoyVectorRaw().x == 0 && JoyStick.Instant.GetJoyVectorRaw().y == 0)
            {
                if (isShoot)
                {
                    anim.SetTrigger(AnimController.STATE.shoot.ToString());
                    //anim.SetFloat("shoot_state", JoyStick.Instant.GetJoyVectorRaw().x != 0 ? 1 : 0);  // = 0 idle shoot ||  = 1 walk shoot
                    anim.SetFloat("shoot_state", 0);
                    StartCoroutine(delayAnim());
                }
                else if (isThrow)
                {
                    anim.SetTrigger(AnimController.STATE._throw.ToString());
                    //anim.SetFloat("shoot_state", JoyStick.Instant.GetJoyVectorRaw().x != 0 ? 1 : 0);  // = 0 idle shoot ||  = 1 walk shoot
                    anim.SetFloat("throw_state", 0);
                    StartCoroutine(delayAnim());
                }
                else
                {
                    anim.SetTrigger(AnimController.STATE.idle.ToString());
                }
                
            }
            else if (Input.GetAxisRaw("Vertical") > 0 || JoyStick.Instant.GetJoyVectorRaw().y > 0 && rigi.velocity.x == 0 || VariableJoystick.instance.Vertical > 0 && rigi.velocity.x ==0)
            {
                anim.SetTrigger(AnimController.STATE.up.ToString());
            }
            else if (Input.GetAxisRaw("Vertical") < 0 || JoyStick.Instant.GetJoyVectorRaw().y < 0 && rigi.velocity.x == 0 || VariableJoystick.instance.Vertical < 0 && rigi.velocity.x == 0)
            {
                anim.SetTrigger(AnimController.STATE.down.ToString());
            }
            else if(rigi.velocity.x != 0 && JoyStick.Instant.GetJoyVectorRaw().x != 0)
            {
                if (isShoot)
                {
                    anim.SetTrigger(AnimController.STATE.shoot.ToString());
                    //anim.SetFloat("shoot_state", JoyStick.Instant.GetJoyVectorRaw().x != 0 ? 1 : 0);  // = 0 idle shoot ||  = 1 walk shoot
                    anim.SetFloat("shoot_state", 1);
                    StartCoroutine(delayAnim());
                }
                else if(isThrow) 
                {
                    anim.SetTrigger(AnimController.STATE._throw.ToString());
                    //anim.SetFloat("shoot_state", JoyStick.Instant.GetJoyVectorRaw().x != 0 ? 1 : 0);  // = 0 idle shoot ||  = 1 walk shoot
                    anim.SetFloat("throw_state", 1);
                    StartCoroutine(delayAnim());
                }
                else
                {
                    anim.SetTrigger(AnimController.STATE.walk.ToString());
                }
                
            }
        }
        else
        {
            if(rigi.velocity.y > 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.J)|| isShoot)
                {
                    anim.SetTrigger(AnimController.STATE.jump_shoot.ToString());
                    StartCoroutine(delayAnim());
                }
                else if (Input.GetKeyDown(KeyCode.K)| isThrow)
                {
                    anim.SetTrigger(AnimController.STATE.jump_throw.ToString());
                    StartCoroutine(delayAnim());
                }
                else
                {
                    anim.SetTrigger(AnimController.STATE.jump.ToString());
                    //play sound jump
                }
            }
            else if(rigi.velocity.y < -0.1f)
            {
                // player fall
            }
            
            if(rigi.velocity.x == 0 && rigi.velocity.y == 0)
            {
                anim.SetTrigger(AnimController.STATE.idle.ToString());
            }
        }
    }
    IEnumerator delayAnim()
    {
        yield return new WaitForSeconds(0.5f);
        isShoot = false;
        isThrow = false;
    }

    //public void ChangeAnimationState(string newState)
    //{
    //    if(currentState == newState)
    //    {
    //        return;
    //    }

    //    anim.Play(newState);

    //    currentState = newState;
    //}
    bool GroundCheck()
    {
        float lenRay = 0.15f;
        RaycastHit2D[] hits = new RaycastHit2D[10];
        box.Cast(Vector2.down, hits, lenRay, true);
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider != null)
            {
                if (h.collider.gameObject.tag == "Ground")
                    return true;
            }
        }

        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            anim.SetTrigger(AnimController.STATE.idle_melee_attack.ToString());  
        }
        if( collision.gameObject.tag == "Enemy_bullet")
        {
            totalLife--;
            if (totalLife <= 0)
            {
                isAlive = false;
                if (!isAlive)
                {
                    anim.SetTrigger(AnimController.STATE.die.ToString());
                }
                GameManager.instance.MissionFailed();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bomb" || collision.gameObject.tag == "Enemy_grenade")
        {
            totalLife--;
            if (totalLife <= 0)
            {
                isAlive = false;
                if (!isAlive)
                {
                    anim.SetTrigger(AnimController.STATE.die.ToString());
                }
                GameManager.instance.MissionFailed();
            }
        }
    }
    
}
