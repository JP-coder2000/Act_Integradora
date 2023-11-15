using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 5.0f; // Vida útil de la bala en segundos
    private Renderer m_Renderer;

    void Start()
    {
        BulletCounter.Increment();
        m_Renderer = GetComponent<Renderer>();
        Destroy(gameObject, lifeTime); // Destruye la bala después de su vida útil
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Si el objeto no es visible para ninguna cámara, destrúyelo.
        if (!m_Renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        BulletCounter.Decrement();
    }
}
