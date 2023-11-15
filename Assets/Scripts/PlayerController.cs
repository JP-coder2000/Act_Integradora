using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject bulletPrefab; // Prefab de la bala
    public float bulletSpeed = 20.0f; // Velocidad de la bala
    public float slowMotionFactor = 0.1f; // Factor de ralentización para el modo lento (ajustado)
    private bool isSlowMotion = false; // Indica si el modo lento está activo
    private bool isShooting = false; // Controla si el jugador está disparando

    void Update()
    {
        // Control de movimiento del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        // Activar o desactivar el modo lento al presionar la tecla espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSlowMotion();
        }

        // Disparo de balas al hacer clic con el ratón
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            StartCoroutine(FireBulletRoutine());
        }
    }

    void ToggleSlowMotion()
    {
        isSlowMotion = !isSlowMotion;
        Time.timeScale = isSlowMotion ? slowMotionFactor : 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajusta fixedDeltaTime para mantener la física consistente
    }

    IEnumerator FireBulletRoutine()
    {
        isShooting = true;
        FireBullet();
        yield return new WaitForSeconds(0.1f); // Ajusta este tiempo según sea necesario para controlar la cadencia de disparo
        isShooting = false;
    }

    void FireBullet()
    {
        Vector3 spawnPosition = transform.position + transform.up * -1 * 0.5f; // Posición ajustada para evitar que las balas se acumulen
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = transform.up * -1 * bulletSpeed;
        }
    }
}
