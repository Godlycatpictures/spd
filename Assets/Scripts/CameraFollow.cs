using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPos; // hitta spelare
    
    private PlayerController playerController;
    [SerializeField] private Vector3 offset = new Vector3(2, 0, -10);
    [SerializeField] private float smoothing = 1f;
    private float lastDirection = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate() // late update kommer i slutet av en frame
    {
        float direction = playerController.GetDirection();
                
        if (direction == 0)
            direction = lastDirection;
        else
            lastDirection = direction;

        Vector3 directionOffset = offset;

        if (direction < 0)
            directionOffset.x = -Mathf.Abs(offset.x);
        else if (direction > 0)
            directionOffset.x = Mathf.Abs(offset.x);


        Vector3 newPosition = playerPos.position + directionOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothing * Time.deltaTime);
    }
}
