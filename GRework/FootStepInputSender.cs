using System;
using System.Collections.Generic;
using UnityEngine;

public class FootStepInputSender : MonoBehaviour
{

    //GB savers
    float inputAngle;
    int octant;
    DirectionStep tempDir;


    public event Action<DirectionStep, float> footStep;

    private List<Action<Vector2>> registeredAction = new List<Action<Vector2>>();

    public void ListenFor(ref Action<Vector2> inputEvent)
    {
        inputEvent += invokeFootStep;
        registeredAction.Add(inputEvent);
    }
    public void StopListenFor(ref Action<Vector2> inputEvent)
    {
        inputEvent -= invokeFootStep;
        registeredAction.Remove(inputEvent);
    }

    private void invokeFootStep(Vector2 input)
    {
        footStep?.Invoke(inputToDirection(input), input.magnitude);
    }

    private DirectionStep inputToDirection(Vector2 input)
    {
        inputAngle = (float)Math.Atan2(input.x, input.y);
        octant = (int)Math.Round(8 * inputAngle/(2*Math.PI) + 8) % 8;
        tempDir = (DirectionStep)(Mathf.Pow(2f, octant));
        return tempDir;
    }

    private void OnDisable()
    {
        for (int i = 0; i < registeredAction.Count; i++)
        {
            registeredAction[i] -= invokeFootStep;
        }
    }
}

[System.Flags]
public enum DirectionStep //North == forward
{
    E = 1, NE = 2,
    N = 4, NW = 8,
    W = 16, SW = 32,
    S = 64, SE = 128
};