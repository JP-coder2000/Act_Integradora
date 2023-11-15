using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1.0f;
    public int bulletsPerShot = 60; // Cantidad original de balas por disparo
    public float firstPatternChangeTime = 20.0f;
    public float secondPatternChangeTime = 40.0f;
    public float speed = 5.0f;
    private bool isSecondPattern = false;
    private bool isThirdPattern = false;
    private float time;
    private float zMovement = 11.0f; // Iniciar en z = 11

    void Start()
    {
        StartCoroutine(FireBullets());
        StartCoroutine(ChangePatternAfterSeconds(firstPatternChangeTime, secondPatternChangeTime));
    }

    IEnumerator FireBullets()
    {
        while (true)
        {
            int bulletCount = bulletsPerShot; // Cantidad original de balas por disparo

            if (isSecondPattern)
            {
                bulletCount = 3; // Disparo en tres columnas
            }
            else if (isThirdPattern)
            {
                bulletCount = 4; // Disparo en patrón de cruz
            }

            for (int i = 0; i < bulletCount; i++)
            {
                Vector3 bulletDirection = Vector3.forward;
                Vector3 bulletPosition = transform.position;

                if (isThirdPattern)
                {
                    // Disparo en patrón de cruz
                    switch (i % 4)
                    {
                        case 0:
                            bulletDirection = Vector3.forward;
                            break;
                        case 1:
                            bulletDirection = Vector3.back;
                            break;
                        case 2:
                            bulletDirection = Vector3.left;
                            break;
                        case 3:
                            bulletDirection = Vector3.right;
                            break;
                    }
                }
                else if (isSecondPattern)
                {
                    // Disparo en tres columnas
                    int column = i % 3;
                    float columnOffset = (column - 1) * 1.0f;
                    bulletDirection = Vector3.back;
                    bulletPosition += new Vector3(columnOffset, 0, 0);
                }
                else
                {
                    // Patrón de disparo circular al inicio
                    float angle = i * (360f / bulletsPerShot);
                    Quaternion rotation = Quaternion.Euler(0, angle, 0);
                    bulletDirection = rotation * Vector3.forward;
                    bulletPosition += bulletDirection;
                }

                Instantiate(bulletPrefab, bulletPosition, Quaternion.LookRotation(bulletDirection));
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator ChangePatternAfterSeconds(float firstSeconds, float secondSeconds)
    {
        yield return new WaitForSeconds(firstSeconds);
        isSecondPattern = true;
        yield return new WaitForSeconds(secondSeconds - firstSeconds);
        isSecondPattern = false;
        isThirdPattern = true;
    }

    void Update()
    {
        // Movimiento constante de izquierda a derecha
        float xPosition = Mathf.PingPong(time * speed, 20) - 10;
        transform.position = new Vector3(xPosition, transform.position.y, zMovement);

        if (isThirdPattern)
        {
            // Ajusta esta variable para cambiar la velocidad de oscilación en z
            float zOscillationSpeed = 0.5f; // Valor más alto = movimiento más rápido

            // Movimiento oscilante entre 10z y -10z en el tercer patrón
            zMovement = 10 * Mathf.Sin(time * zOscillationSpeed);
        }
        time += Time.deltaTime;
    }
}
