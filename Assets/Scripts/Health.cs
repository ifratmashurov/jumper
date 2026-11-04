using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Обязательно для работы с текстом

public class Health : MonoBehaviour
{
    public int hp = 3;
    public int maxHp = 5;
    
    [Header("UI")]
    public TextMeshProUGUI hpText; // Сюда мы перетащим наш новый текст

    [Header("Invincibility")]
    public int blinkCount = 10;
    public float blinkInterval = 0.2f;

    private bool isInvincible;
    private SpriteRenderer[] allRenderers;

    void Awake()
    {
        allRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Start()
    {
        UpdateHealthUI(); // Обновляем текст при старте игры
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        hp -= damage;
        UpdateHealthUI(); // Обновляем текст при уроне

        if (hp <= 0)
        {
            if (hpText != null) hpText.text = "ИГРА ОКОНЧЕНА!";
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    public void Heal(int amount)
    {
        hp = Mathf.Min(hp + amount, maxHp);
        UpdateHealthUI(); // Обновляем текст при лечении
    }

    private void UpdateHealthUI()
    {
        // Обновляем только наш новый текст здоровья
        if (hpText != null)
        {
            hpText.text = $"ЖИЗНИ: {hp}";
        }
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        for (int i = 0; i < blinkCount; i++)
        {
            foreach (var sr in allRenderers)
                if (sr != null) sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
        foreach (var sr in allRenderers)
            if (sr != null) sr.enabled = true;
        
        isInvincible = false;
    }
}