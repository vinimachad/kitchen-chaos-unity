using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{

    [SerializeField] float magnitudeForce = 1f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb != null)
        {
            Vector3 force = hit.transform.position - transform.position;
            force.y = 0;
            force.Normalize();

            rb.AddForce(force * magnitudeForce, ForceMode.Impulse);
        }
    }
}
