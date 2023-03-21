using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10f;     // velocidad de movimiento del jugador
    public float slowdown = 5f;         // velocidad a la que el jugador se detiene cuando no se está moviendo

    private CharacterController controlador;    // componente CharacterController para controlar al jugador
    public Animator camAnimator;                // animador del personaje
    private bool isWalking;                     // indica si el jugador está moviendose o no

    private Vector3 inputVector;        // vector de entrada del usuario
    private Vector3 movementVector;     // vector de movimiento del jugador
    private float gravity = -10f;       // fuerza de la gravedad

    // Start se llama antes del primer cuadro de actualización del juego
    void Start()
    {
        controlador = GetComponent<CharacterController>();   // obtiene el componente CharacterController del jugador
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();     // obtiene la entrada del usuario
        MovePlayer();   // mueve al jugador según la entrada del usuario

        camAnimator.SetBool("isWalking", isWalking);   // actualizamos la variable booleana isWalking del animador
    }

    // Obtiene la entrada del usuario
    void GetInput()
    {
        // Si el usuario está presionando alguna tecla de movimiento
        if(Input.GetKey(KeyCode.W) ||
           Input.GetKey(KeyCode.A) ||
           Input.GetKey(KeyCode.S) ||
           Input.GetKey(KeyCode.D))
        {
            // Obtiene el vector de entrada del usuario y lo normaliza
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();

            // Transforma el vector de entrada del usuario en la dirección del jugador y lo marca como que está caminando
            inputVector = transform.TransformDirection(inputVector);
            isWalking = true;
        }
        else
        {
            // Si el usuario no está presionando ninguna tecla de movimiento, suavemente reduce la velocidad del jugador hasta detenerlo y marca que no está caminando
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, slowdown * Time.deltaTime);
            isWalking = false;
        }

        // Calcula el vector de movimiento del jugador en función de su velocidad y la fuerza de la gravedad
        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    }

    void MovePlayer()
    {
        controlador.Move(movementVector * Time.deltaTime);
    }
}
