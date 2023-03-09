using System.Collections;
using System.Collections.Generic;
using MiniTools.BetterGizmos;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private Vector2 input;
    [SerializeField] private float speed = 7;
    [SerializeField] private float gravity = -2;
    [SerializeField] private float rotateSpeed = 15f;
    [SerializeField] private GameInput gameInput;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        this.input = gameInput.GetPlayerInputNormalized();
        Vector3 moveDir = new Vector3(input.x, gravity, input.y);
        characterController.Move(moveDir * speed * Time.deltaTime);

        transform.forward = Vector3.Slerp(transform.forward, new(input.x, 0f, input.y), Time.deltaTime * rotateSpeed);
    }
}
