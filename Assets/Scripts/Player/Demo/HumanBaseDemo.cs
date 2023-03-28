
using System;
using UnityEngine;

public class HumanBaseDemo : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float strength;
    [SerializeField] protected float size;
    [SerializeField] protected bool hasKey = false;
    [SerializeField] protected float sprintLimit = 3f;
    [SerializeField] protected float speed;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private readonly float _originalSpeed = 5f;
    private bool _ralentizado;
    private Vector2 _direction;
    
    [SerializeField] Healthbar _healthBar;
    [SerializeField] StaminaBar _StaminaBar;
    [SerializeField] AudioSource _walkSound;
    [SerializeField] GameObject _victoryText;

    [SerializeField] private VariableJoystick variableJoystick;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float Health
    {
        get => health;
        set => health = value;
    }
    
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door") && hasKey)
        {
            Debug.Log("Puerta abierta");
            collision.gameObject.GetComponent<Door>().OpenDoor();
            hasKey = false;
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Llave recogida");
            hasKey = true;
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Golpe de remo");
            health -= collision.gameObject.GetComponent<Projectile>().Damage;
            _healthBar.SetHealth(health);
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("Computer"))
        {
            Debug.Log("Victoria");
            _victoryText.SetActive(true);
            _spriteRenderer.enabled = false;
            Time.timeScale = 0;
        }
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StickyFloor"))
        {
            Debug.Log("Ralentizado");
            _ralentizado = true;
            speed = _originalSpeed / 4;
        }

        if (other.gameObject.CompareTag("StickyFloor") && _rigidbody.velocity == Vector2.zero)
        {
            _walkSound.pitch = 0.5f;
            _walkSound.Play();
        }
        else
        {
            _walkSound.Stop();
        }
    }

   protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StickyFloor"))
        {
            Debug.Log("Velocidad normal");
            _ralentizado = false;
            speed = _originalSpeed;
        }
    }

   public void FixedUpdate()
   {
       Vector3 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
       _rigidbody.AddForce((Vector2)direction * ((speed * 20) * Time.fixedDeltaTime), (ForceMode2D)ForceMode.VelocityChange);
   }
   
   
    protected new void Update()
    {
        _StaminaBar.SetStamina(sprintLimit);
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        // Limitar velocidad diagonal
        if (_rigidbody.velocity.magnitude > speed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * speed;
        }
        
        // Controles de movimiento
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = _direction * speed;
        
        if (Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = true;
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.W))
        {
            _rigidbody.velocity = Vector3.zero;
        } 
        
        if (sprintLimit > 0 && !_ralentizado && Input.GetKey(KeyCode.LeftShift))
        {
            if (_rigidbody.velocity != Vector2.zero)
            {
                speed = _originalSpeed * 2;
                sprintLimit -= Time.deltaTime;
            }

        } 
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = _originalSpeed;
        }
        else if (sprintLimit < 3f)
        {
            speed = _originalSpeed;
            sprintLimit += Time.deltaTime;

        }
        
        
        /* if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (touchPosition.x < 0)
                    {
                        _spriteRenderer.flipX = false;
                    }
                    else
                    {
                        _spriteRenderer.flipX = true;
                    }
                    break;
                case TouchPhase.Moved:
                    _rigidbody2D.velocity = touchPosition;
                    break;
                case TouchPhase.Ended:
                    _rigidbody2D.velocity = Vector2.zero;
                    break;
            }
        } */
       
        
        
        //sound
        if (_rigidbody.velocity != Vector2.zero)
        {
            if (!_walkSound.isPlaying)
            {
                _walkSound.pitch = 1.2f;
                _walkSound.Play();
                
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && _rigidbody.velocity != Vector2.zero)
        {
            if (!_walkSound.isPlaying)
            {
                _walkSound.pitch = 1.8f;
                _walkSound.Play();
                
            }
        }
        else
        {
            _walkSound.Stop();
        }
    }
}

