using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPos; // hitta spelare
    private GameObject playerObject;
    private PlayerController playerController;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothing = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate() // late update kommer i slutet av en frame
    {
        

        if (playerController.GetDirection() < 0)
            offset.x = playerPos.position.x; 

        Vector3 newPosition = Vector3.Slerp(transform.position, playerPos.position + offset, smoothing * Time.deltaTime);

        transform.position = newPosition;
    }
}
