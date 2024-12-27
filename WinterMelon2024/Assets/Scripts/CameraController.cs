using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed;

    private float panAmt;

    private void Update()
    {
        transform.Translate(new Vector3(panAmt * Time.deltaTime, 0f, 0f));
    }

    private void OnPan(InputValue _pan)
    {
        panAmt = _pan.Get<float>();
    }
}
