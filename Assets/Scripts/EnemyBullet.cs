using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;

    void Start()
    {
        Destroy(gameObject, 3f); // Пуля босса исчезнет через 3 секунды
    }

    void Update()
    {
        // Пуля босса летит влево (навстречу игроку)
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Если пуля попала в Игрока
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null) 
            {
                player.TakeDamage(1); // Отнимает 1 ХП у тебя
            }
            Destroy(gameObject); // Пуля исчезает
        }
    }
}