using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spin : MonoBehaviour
{

    [UnityEngine.SerializeField] private InputAction pressed,axis;
    [UnityEngine.SerializeField] private float speed = 0.35f;

    private bool rotateAllowed;

    private Vector2 rotation;

    private void Awake() {
        pressed.Enable();
        axis.Enable();

        pressed.performed += _ => {StartCoroutine(Rotate());};
        pressed.canceled += _ =>{rotateAllowed = false;};

        axis.performed += context => {
            rotation = context.ReadValue<Vector2>();
        };
    }
    private IEnumerator Rotate ()
    {
        rotateAllowed = true;
        while ((rotateAllowed))
        {
            rotation *= speed;
            transform.Rotate(Vector3.up, rotation.x);
            transform.Rotate(-Vector3.right, rotation.y);
            yield return null;
        }
    }

}
