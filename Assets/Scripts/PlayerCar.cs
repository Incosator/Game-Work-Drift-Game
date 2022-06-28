using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    Quaternion targetRotation;
    Vector3 lastPosition;
    Rigidbody rb;

    [SerializeField] float motorForce;
    [SerializeField] float turnSpeed;

    public GameObject[] wheels;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        SetRotationPoint();
        SetSideSlip();
    }

    public float SidesSlipAmount { get; set; } = 0;

    private void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPosition;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;
        SidesSlipAmount = movement.x;
    }
    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    private void FixedUpdate()
    {
        float speed = rb.velocity.magnitude / 1000f;

        float accelerationInput = motorForce * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        rb.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(speed, -1, 1) * Time.fixedDeltaTime);

        foreach (var wheel in wheels)
            wheel.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed / 4 * Time.fixedDeltaTime);
    }
}
