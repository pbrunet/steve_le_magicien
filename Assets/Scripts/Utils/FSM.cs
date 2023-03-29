using UnityEngine;
using System;
using JetBrains.Annotations;

public class FSM
{
    public enum STEP { START, UPDATE, EXIT }

    private STEP currentStep = STEP.START;

    protected FSM nextState;

    protected virtual void Init() { }
    protected virtual void Update() { }
    protected virtual void Exit() { }

    public FSM()
    {
        this.nextState = this;
    }

    public FSM Progress()
    {
        if (currentStep == STEP.START)
        {
            Debug.Log("Init state" + Type.GetTypeFromHandle(Type.GetTypeHandle(this)).FullName);
            Init();
            currentStep = STEP.UPDATE;
        }
        if (currentStep == STEP.UPDATE)
        {
            Update();
        }
        if (this != nextState)
        {
            Exit();
        }

        return nextState;
    }

    public void Terminate()
    {
        Exit();
    }
}