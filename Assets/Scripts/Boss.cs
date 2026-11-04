using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Обязательно для работы со Слайдером!
using System.Collections;

public class Boss : MonoBehaviour
{
    public int maxHealth = 8; // Теперь у босса 8 жизней!
    private int currentHealth;

    public GameObject enemyBulletPrefab; 
    public Transform firePoint;          
    
    // Сюда мы перетащим нашу новую полоску ХП
    public Slider bossHpSlider; 

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        // Настраиваем Слайдер при старте
        if (bossHpSlider != null)
        {
            bossHpSlider.maxValue = maxHealth;
            bossHpSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("ОШИБКА БОССА: Ты забыл перетащить Slider в Инспектор!");
        }

        if (enemyBulletPrefab == null) Debug.LogError("ОШИБКА БОССА: Нет пули!");
        if (firePoint == null) Debug.LogError("ОШИБКА БОССА: Нет FirePoint!");
        if (GetComponent<BoxCollider2D>() == null) Debug.LogError("ОШИБКА БОССА: Нет коллайдера!");

        StartCoroutine(ShootRoutine());
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        
        // Отнимаем кусок от полоски ХП
        if (bossHpSlider != null) 
        {
            bossHpSlider.value = currentHealth;
        }
        
        Debug.Log("✅ БОСС РАНЕН! Осталось ХП: " + currentHealth);

        GetComponent<SpriteRenderer>().color = Color.red; 
        Invoke("ResetColor", 0.1f);

        if (currentHealth <= 0)
        {
            Debug.Log("💀 БОСС УМЕР! Пытаюсь загрузить WinScene...");
            isDead = true;
            Destroy(gameObject); 
            SceneManager.LoadScene("WinScene"); 
        }
    }

    void ResetColor() { GetComponent<SpriteRenderer>().color = Color.white; }

    IEnumerator ShootRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(2f); 
            
            if (enemyBulletPrefab != null && firePoint != null)
            {
                Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }
}