using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Сюда закинь префаб пули
    public Transform firePoint;     // Создай пустой объект внутри героя и перетащи сюда
    public float cooldown = 0.5f;   // Задержка между выстрелами
    public AudioClip shootClip;     // Звук выстрела (по желанию)

    private float nextShotTime = 0f;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Выстрел с клавиатуры (кнопка X) для тестов на ПК
        if (Input.GetKeyDown(KeyCode.X) && Time.time >= nextShotTime)
        {
            Shoot();
        }
    }

    // Эта функция PUBLIC, чтобы мы могли нажать ее с экрана телефона!
    public void Shoot()
    {
        if (Time.time >= nextShotTime && bulletPrefab != null && firePoint != null)
        {
            nextShotTime = Time.time + cooldown; // Запускаем кулдаун

            // Создаем пулю
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            // Определяем, куда летит пуля (вправо или влево), основываясь на flipX героя
            float direction = spriteRenderer != null && spriteRenderer.flipX ? -1f : 1f;
            
            // Задаем скорость пуле
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                // Скорость 10f. Можешь поменять на нужную.
                bulletRb.linearVelocity = new Vector2(direction * 10f, 0f); 
            }

            // Проигрываем звук
            if (audioSource != null && shootClip != null)
            {
                audioSource.PlayOneShot(shootClip);
            }
        }
    }
}