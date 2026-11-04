using UnityEngine;

public class Hazard : MonoBehaviour
{
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        CheckImpact(other.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        CheckImpact(other.gameObject);
    }

    private void CheckImpact(GameObject hitObject)
    {
        // 1. Если попало в игрока - наносим урон
        if (hitObject.CompareTag("Player") || hitObject.name.ToLower().Contains("player"))
        {
            Health playerHealth = hitObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                OnHitPlayer(playerHealth);
            }
            Destroy(gameObject);
        }
        // 2. Если упало на землю
        else if (hitObject.CompareTag("Ground") || hitObject.name.ToLower().Contains("floor") || hitObject.name.ToLower().Contains("platform"))
        {
            // Умная логика: 
            // Если это БОМБА, мы её вообще не трогаем! Она взорвется сама через 2 секунды (в скрипте BombHazard).
            // А Камни и Сердечки пусть лежат на земле 4 секунды (чтобы ты успел их расстрелять или подобрать), а потом исчезают.
            if (this.GetComponent<BombHazard>() == null)
            {
                Destroy(gameObject, 4f); 
            }
        }
    }

    protected virtual void OnHitPlayer(Health player)
    {
        player.TakeDamage(1); 
    }
}