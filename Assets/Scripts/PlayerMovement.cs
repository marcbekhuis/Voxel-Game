using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 100;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        inputVector.Normalize();
        Vector3 velocity = transform.rotation * new Vector3(inputVector.x * speed, rigidbody.velocity.y, inputVector.y * speed);

        rigidbody.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space)) rigidbody.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"),0));
        transform.GetChild(0).Rotate(new Vector3(0, 0, Input.GetAxis("Mouse Y")));
    }
}
