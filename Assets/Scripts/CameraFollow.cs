using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // hitta spelare
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothing = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate() // late update kommer i slutet av en frame
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, player.position + offset, smoothing * Time.deltaTime);

        transform.position = newPosition;
    }
}
