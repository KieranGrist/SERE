 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
/// <summary>
/// This node simply returns success after the allotted delay time has passed
/// </summary>
[System.Serializable]
public class DelayNode : Node
{
    public float Delay = 0.0f;
    public bool Started = false;
    public Timer regulator;
    public bool DelayFinished = false;
    public DelayNode(Agent bb, string name,float DelayTime) : base(bb, name)
    {

      NodeName = name;
    
        this.Delay = DelayTime;
        regulator = new Timer(Delay * 1000.0f); // in milliseconds, so multiply by 1000
        regulator.Elapsed += OnTimedEvent;
        regulator.Enabled = true;
        regulator.Stop();
    }

    public override NodeStatus Execute()
    {
        NodeStatus rv = NodeStatus.RUNNING;
        if (!Started
            && !DelayFinished)
        {
            Started = true;
            regulator.Start();
        }
        else if (DelayFinished)
        {
            DelayFinished = false;
            Started = false;
            rv = NodeStatus.SUCCESS;
        }

        return rv;
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        Started = false;
        DelayFinished = true;
        regulator.Stop();
    }

    //Timers count down independently of the Behaviour Tree, so we need to stop them when the behaviour is aborted/reset
    public override void Reset()
    {
        regulator.Stop();
        DelayFinished = false;
        Started = false;
    }
}
