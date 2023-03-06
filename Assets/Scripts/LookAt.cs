using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    [SerializeField] Mode mode;

    enum Mode
    {
        LookAt,
        LookAtInverted,
        LookAtFoward,
        LookAtFowardInverted
    }

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 invertedDir = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + invertedDir);
                break;
            case Mode.LookAtFoward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.LookAtFowardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
