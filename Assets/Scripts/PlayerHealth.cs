using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    
    public TextMeshProUGUI hpText;
    public GameObject[] hearts;   // иконки сердец в HUD 
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; 

        currentHealth -= damage;
        UpdateUI();

        if (currentHealth <= 0)
        {
            // Смерть -> рестарт текущего уровня
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // Запускаем корутину неуязвимости с миганием
            StartCoroutine(InvincibilityRoutine());
        }
    }

    IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f); 
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);   
            yield return new WaitForSeconds(0.2f);
        }
        isInvincible = false;
    }

    void UpdateUI()
{
    if (hpText != null)
        hpText.text = "HP: " + currentHealth;

    if (hearts != null)
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].SetActive(i < currentHealth);
}
    // Если игрок касается врага
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(1);
        }
    }
}