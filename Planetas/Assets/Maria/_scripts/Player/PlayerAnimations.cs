using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator; // Guarda el Animator para disparar las animaciones
    private NewInput _input; // Guarda el script de controles para leer lo que presiona el jugador
    private SpriteRenderer _spriteRenderer; // Guarda el SpriteRenderer para voltear el personaje
    private Rigidbody2D _rigidbody; // Guarda el Rigidbody2D para manejar la física y el movimiento 2D del personaje
    private bool _isDead; // Variable interna de C# para bloquear las demás animaciones cuando el jugador muere

    // Se ejecuta al arrancar el juego antes del primer frame
    void Start()
    {
        _animator = GetComponent<Animator>(); // Busca el componente Animator en este mismo objeto
        _input = GetComponent<NewInput>(); // Busca el script NewInput en este mismo objeto
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Busca el SpriteRenderer para poder girar el sprite
        _rigidbody = GetComponent<Rigidbody2D>(); // Busca y guarda el componente Rigidbody2D del objeto para usarlo después
    }

    // Se ejecuta en cada frame del juego
    void Update()
    {
        if (_isDead) return; // Si ya murió, corta aquí y no actualiza movimiento, giro ni salto

        PlayerMoving(); // Revisa si el jugador se mueve para actualizar la animación de caminar/correr
        Flip(); // Gira el sprite según la dirección en X
        PlayerJumping(); // Actualiza la animación de salto según la velocidad vertical
    }

    public void PlayerMoving() // Controla el estado de movimiento horizontal
    {
        _animator.SetBool("isMoving", _input.inputX != 0); // Activa "isMoving" si hay input horizontal, y lo apaga si es 0
    }

    public void PlayerJumping() // Controla el estado de salto/caída
    {
        // Manda la velocidad vertical en positivo (valor absoluto) al float "isJumping" del Animator
        _animator.SetFloat("isJumping", Mathf.Abs(_rigidbody.velocity.y));
    }

    public void Flip()
    {
        if (_input.inputX < 0) // Si va a la izquierda (valor negativo)...
        {
            _spriteRenderer.flipX = true; // ...voltea el sprite horizontalmente
        }
        else if (_input.inputX > 0) // Si va a la derecha (valor positivo)...
        {
            _spriteRenderer.flipX = false; // ...deja el sprite en su posición normal
        }
        // Si inputX es 0 no cambia nada para que mantenga la última dirección a la que miraba
    }

    public void PlayDamage() // Dispara la animación cuando el jugador recibe un golpe
    {
        if (_isDead) return; // Si ya murió, no reproduce la animación de daño

        _animator.SetTrigger("damage"); // Activa el Trigger "damage" en el Animator
    }

    public void PlayDead() // Dispara la animación cuando el jugador muere
    {
        if (_isDead) return; // Evita ejecutar la muerte más de una vez

        _isDead = true; // Marca que el personaje está muerto para frenar el Update
        _animator.SetBool("isMoving", false); // Apaga la animación de moverse por si venía caminando
        _animator.SetFloat("isJumping", 0f); // Reinicia el parámetro de salto
        _animator.SetTrigger("Dead"); // Activa el Trigger "Dead" en el Animator
    }
}