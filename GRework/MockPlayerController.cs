using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FootStepInputSender))]
public class MockPlayerController : MonoBehaviour
{
    [SerializeField] float movementAmount = 2f;
    public event Action<Vector2> inputEvent;
    private FootStepInputSender InputSenderFoot;

    //GB savers
    Vector2 inputVector = new Vector2();

    void GetRandomInput()
    {
        inputVector.x += UnityEngine.Random.Range(-0.3f, 0.3f);
        inputVector.y += UnityEngine.Random.Range(-0.3f, 0.3f);
        inputVector.x = Mathf.Clamp(inputVector.x, -1, 1);
        inputVector.y = Mathf.Clamp(inputVector.y, -1, 1);

        inputEvent.Invoke(inputVector);
    }


    void Start()
    {
        inputVector = new Vector2();
        InputSenderFoot = GetComponent<FootStepInputSender>();
        InputSenderFoot.ListenFor(ref inputEvent);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        GetRandomInput();
        transform.position += (Vector3)inputVector * (Time.fixedDeltaTime * movementAmount);
    }
}
