using System;
using UnityEngine;
using UnityEngine.UIElements;

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
    private float _originalSpeed;
    private bool _ralentizado;
    private Vector2 _direction;

    [SerializeField] Healthbar _healthBar;
    [SerializeField] StaminaBar _StaminaBar;
    [SerializeField] AudioSource _walkSound;
    [SerializeField] GameObject _victoryText;

    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Button _sprintButton;

    public bool controlHuman;

    private bool clickingInInputSprint = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalSpeed = speed;
        controlHuman = true;
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
            speed = _originalSpeed / 2;
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

    public void ControlHuman()
    {
        controlHuman = !controlHuman;
    }
    
    protected new void Update()
    {
        _StaminaBar.SetStamina(sprintLimit);

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (!controlHuman) return;

        // Limitar velocidad diagonal
        if (_rigidbody.velocity.magnitude > speed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * speed;
        }
        
        // Controles de movimiento
        print("Y: " + variableJoystick.Horizontal + " X: " + variableJoystick.Vertical);
        _direction = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);
        _rigidbody.velocity = _direction * speed;

        if (Input.GetKey(KeyCode.A) || _direction.x < 0)
        {
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.D) || _direction.x > 0)
        {
            _spriteRenderer.flipX = true;
        }

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

        doSprint();
    }

    public void sprintInput(bool canSprint)
    {
        clickingInInputSprint = canSprint;
    }

    private void doSprint()
    {
        bool inputUsed = Input.GetKey(KeyCode.LeftShift) || clickingInInputSprint;
        print("CAN SPRINT?? " + inputUsed);
        if (sprintLimit > 0 && !_ralentizado && inputUsed)
        {
            if (_rigidbody.velocity != Vector2.zero)
            {
                speed = _originalSpeed * 2;
                sprintLimit -= Time.deltaTime;
            }
            else
            {
                speed = _originalSpeed;
            }
        }

        if (sprintLimit < 3f && speed == _originalSpeed)
        {
            speed = _originalSpeed;
            sprintLimit += Time.deltaTime;
        }
    }
}