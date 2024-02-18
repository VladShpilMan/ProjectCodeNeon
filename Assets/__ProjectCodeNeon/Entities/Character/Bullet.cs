using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Скорость полета пули
    public float lifetime = 3f;  // Время жизни пули

    void Start()
    {
        // Запускаем таймер уничтожения пули после заданного времени
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Перемещаем пулю вперед
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Если пуля сталкивается с объектом, отличным от врага, уничтожаем пулю
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
