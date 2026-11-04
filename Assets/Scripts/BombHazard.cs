using UnityEngine;

public class BombHazard : Hazard
{
    void Start()
    {
        // Запускаем таймер взрыва через 2 секунды после появления
        Invoke(nameof(Explode), 2f);
    }

    protected override void OnHitPlayer(Health player)
    {
        CancelInvoke(nameof(Explode)); // Если попала в игрока, таймер отменяем
        player.TakeDamage(2);          // Наносит двойной урон
    }

    void Explode()
    {
        // Бомба взрывается и исчезает
        Destroy(gameObject);
    }
}