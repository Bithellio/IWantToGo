using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float MoveSpeed = 5f;

    [SerializeField]
    private float DashSpeed = 50f;

    [SerializeField]
    private float StartDashtime = 0.1f;

    [SerializeField]
    private float SlideSpeed = 50f;

    [SerializeField]
    private float StartSlidetime = 0.1f;


    private float _dashTime;

    private int _direction;

    private int _slideDir;


    private float _slideTime;

    [SerializeField]
    [Range(0,10f)]
    private float JumpSpeed = 5f;

    public bool CanDoubleJump;
    
    public bool CanWallJump;
    public bool CanSlide;
    private bool _crouch;

    [SerializeField]
    private Transform GroundCheckPoint;

    [SerializeField]
    private Transform CeilingCheck;

    [SerializeField]
    private Collider2D CrouchDisableCollider;

    [SerializeField]
    private GameObject Spawner;

    [SerializeField]
    private GameObject Crosshair;


    public bool CanDash; 
    
    public bool CanMoveSpawn;

    [SerializeField]
    private float GroundCheckRadius;

    [SerializeField]
    private float CeilingCheckRadius; 

    [SerializeField]
    private LayerMask GroundLayer;

    [SerializeField]
    public Vector3 RespawnPoint;

    [SerializeField]
    public LevelManager GameLevelManager;

    private bool _isTouchingGround;
    private float _movement = 0f;
    private float _aim = 0f;
    private bool _hasJumped;
    private float _rotateSpeed = 2f;


    private Rigidbody2D _rigidBody;
    private Animator _playerAnimation;
    private bool _isRight = true;
    private bool m_wasCrouching = false;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<Animator>();
        GameLevelManager = FindObjectOfType<LevelManager>();
        RespawnPoint = transform.position;
        _dashTime = StartDashtime;
        _slideTime = StartSlidetime;
    }



    // Update is called once per frame
    void Update()
    {

        _isTouchingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, GroundCheckRadius, GroundLayer);
        
        if(_isTouchingGround)
        {
            _hasJumped = false; 
        }

        _movement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        _aim = Input.GetAxisRaw("Aim") ;
        if (_movement != 0f )
        { 
            if(_movement > 0 && _isRight == false )
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y) ;
                _isRight = true;
                
            }
            else if(_movement < 0 && _isRight == true)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                _isRight = false;

            }
            
            _rigidBody.velocity = new Vector2(_movement, _rigidBody.velocity.y);
        }

        if(_aim != 0 )
        {
            Crosshair.transform.Rotate(0,0,_aim,Space.Self);
        }
        if(Input.GetButtonDown("Jump") && (_isTouchingGround || (CanDoubleJump && !_hasJumped)))
        {
            SoundManagerScript.PlaySound("jump");
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpSpeed);
            _hasJumped = true;
        }
       if(Input.GetButtonDown("ResetSpawn"))
        {
            Spawner.transform.position = new Vector3(-9.4f ,0, 0);
            
        }
       
       if(CanSlide)
        {
            
                SlidePlayer(true);
                
            
            
        }

       if(CanDash)
       {
            if (_direction == 0)
            {
                if (Input.GetButtonDown("Dash"))
                {
                    if (_rigidBody.velocity.x < 0)
                    {
                        _direction = 1;
                    }
                    else if (_rigidBody.velocity.x > 0)
                    {
                        _direction = 2;
                    }
                }
            }
            else
            {
                if (_dashTime <= 0)
                {
                    _direction = 0;
                    _dashTime = StartDashtime;
                    _rigidBody.velocity = new Vector2(_movement, _rigidBody.velocity.y);
                    _playerAnimation.SetBool("Dashing", false);
                }
                else
                {
                    _playerAnimation.SetBool("Dashing", true);
                    _dashTime -= Time.deltaTime;
                    if (_direction == 1)
                    {
                        _rigidBody.velocity = Vector2.left * DashSpeed;
                    }
                    else if (_direction == 2)
                    {
                        _rigidBody.velocity = Vector2.right * DashSpeed;
                    }
                    
                }


            }
            
        }
        if(Input.GetButtonDown("Respawn"))
        {
          //  _playerAnimation.SetBool("Dead", true);
            GameLevelManager.Respawn();
        }

        _playerAnimation.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x));
        _playerAnimation.SetBool("OnGround", _isTouchingGround);
        
    }



    public void SlidePlayer(bool crouch)

    {     
       
        if (crouch)
        {
            if (_slideDir == 0)
            {
                if (Input.GetButtonDown("Crouch"))
                {
                    if (_rigidBody.velocity.x < 0)
                    {
                        _slideDir = 1;
                    }
                    else if (_rigidBody.velocity.x > 0)
                    {
                        _slideDir = 2;
                    }
                }
            }
            else
            {
              
                if (_slideTime <= 0)
                {
                    _slideDir = 0;
                    _slideTime = StartSlidetime;
                    _rigidBody.velocity = new Vector2(_movement, _rigidBody.velocity.y);
                    _playerAnimation.SetBool("Sliding", false);
                    if (CrouchDisableCollider != null)
                        CrouchDisableCollider.enabled = true;
                }
                else
                {
                    if (CrouchDisableCollider != null)
                        CrouchDisableCollider.enabled = false;
                    _playerAnimation.SetBool("Sliding", true);
                    _slideTime -= Time.deltaTime;
                    if (_slideDir == 1)
                    {
                        _rigidBody.velocity = Vector2.left * SlideSpeed;
                    }
                    else if (_slideDir == 2)
                    {
                        _rigidBody.velocity = Vector2.right * SlideSpeed;
                    }

                }
               
            }
        }     
    }

    public IEnumerator PlayerDashCoRoutine()
    {
         var dash = Input.GetAxisRaw("Horizontal") * DashSpeed;
        _rigidBody.velocity = new Vector2(dash, _rigidBody.velocity.y);
        yield return new WaitForSeconds(1);
        _rigidBody.velocity = new Vector2(_movement, _rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Spawner")
        //{
        //    Physics2D.IgnoreCollision(collision,GetComponent<BoxCollider2D>());
        //    Physics2D.IgnoreCollision(collision,GetComponent<CircleCollider2D>());
        //}
        if (collision.tag =="FallDetector" || collision.tag == "Spikes")
        {
            GameLevelManager.Respawn(); 
        }
        if (collision.tag =="DoubleJump")
        {
            CanDoubleJump = true;
        }
        {
        if(collision.tag =="WallJump")
            CanWallJump = true;
            GroundLayer |= (1 << LayerMask.NameToLayer("Walls"));
        }

       if(collision.tag == "Dash")
        {
            CanDash = true; 
        }

        if(collision.tag =="SpawnShift")
        {
            CanMoveSpawn = true;
            Crosshair.SetActive(true);
            Spawner.SetActive(true);
        }
        if(collision.tag == "Slide")
        {
            CanSlide = true;
        }

    }


    public void SetPlayerDeath(bool set)
    {
        _playerAnimation.SetBool("Dead", set);
    }
}
