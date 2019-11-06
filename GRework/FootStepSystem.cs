using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FootStepInputSender))]
public class FootStepSystem : MonoBehaviour
{


     
    public List<AudioInfo> audioInfos;

    Dictionary<DirectionStep, List<AudioInfo>> dirAudio = new Dictionary<DirectionStep, List<AudioInfo>>();
    
    private FootStepInputSender fsis;

    private void EvaluateEvent(DirectionStep direction, float strengh)
    {
        throw new NotImplementedException();
    }
    //Changing audioinfos list to a spread out one

    private void Awake()
    {
        fsis.footStep += EvaluateEvent;
        foreach (AudioInfo n in audioInfos)
        {
            DirectionStep tempTag = n.tag;
            if (tempTag == 0)
            {
                tempTag = (DirectionStep)~0;
            } //If no tag has been given it will be used in all

            if (tempTag.HasFlag(DirectionStep.E))
            {
                dirAudio[DirectionStep.E].Add(n);
            }

            if (tempTag.HasFlag(DirectionStep.N))
            {
                dirAudio[DirectionStep.E].Add(n);
            }

            if (tempTag.HasFlag(DirectionStep.NE))
            {
                dirAudio[DirectionStep.E].Add(n);
            }

            if (tempTag.HasFlag(DirectionStep.NW))
            {
                dirAudio[DirectionStep.E].Add(n);
            }

            if (tempTag.HasFlag(DirectionStep.SE))
            {
                dirAudio[DirectionStep.E].Add(n);
            }

            if (tempTag.HasFlag(DirectionStep.S))
            {
                dirAudio[DirectionStep.E].Add(n);
            }

            if (tempTag.HasFlag(DirectionStep.SW))
            {
                dirAudio[DirectionStep.E].Add(n);
            }

  
        }
    }

    private void OnDisable()
    {
        fsis.footStep -= EvaluateEvent;
    }


}

[System.Serializable]
public struct AudioInfo
{
    public AudioClip clip;
    [EnumFlag]
    public DirectionStep tag;
}