using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; 

public class Player2D : MonoBehaviour
{
    public float speed = 7f;
    public float jumpForce = 12f;
    
    public SimpleJoystick joystick; // Джойстик

    public Animator _animator;
    public TextMeshProUGUI statusText;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool onGround;
    private int coins;
    private int coinsTotal;
    private bool isWon = false; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Аниматорды автоматты түрде табу
        if (_animator == null) _animator = GetComponent<Animator>();
    }

    void Start()
    {
        coinsTotal = GameObject.FindGameObjectsWithTag("Coin").Length;
        UpdateUI("Монеты: 0 / " + coinsTotal);
    }

    void Update()
    {
        if (isWon) return; 

        // Персонажды клавиатура арқылы секірту (Пробел)
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isWon) return; 

        // Клавиатура мен Джойстикті қатар қосу
        float x = Input.GetAxisRaw("Horizontal");
        if (joystick != null && joystick.Horizontal() != 0) 
        {
            x = joystick.Horizontal();
        }

        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
        
        if (x != 0) 
        {
            _animator.SetBool("isRunning", true);   
            
            // Бағытқа қарай бетін бұру (flipX)
            if (x > 0) spriteRenderer.flipX = false;
            else if (x < 0) spriteRenderer.flipX = true;
        }
        else 
        {
            _animator.SetBool("isRunning", false); 
        }
    }

    // Секіру функциясы (Экрандағы түйме және Пробел үшін)
    public void Jump()
    {
        if (onGround && !isWon)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            onGround = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isWon) return;

        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            UpdateUI("Монеты: " + coins + " / " + coinsTotal);
        }

        if (other.CompareTag("Exit"))
        {
            if (coins >= coinsTotal)
            {
                StartCoroutine(WinRoutine());
            }
            else
            {
                UpdateUI("Соберите все монеты! (" + coins + " / " + coinsTotal + ")");
            }
        }
    }
    
    IEnumerator WinRoutine()
    {
        isWon = true;
        rb.linearVelocity = Vector2.zero; 
        UpdateUI("ВЫ ВЫИГРАЛИ! ");

        yield return new WaitForSeconds(3f); 
       
        // Босс аренасына өту
        SceneManager.LoadScene("Level3"); 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isWon) return;

        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void UpdateUI(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }
}