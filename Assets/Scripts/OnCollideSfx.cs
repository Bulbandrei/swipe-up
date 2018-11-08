using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideSfx : MonoBehaviour
{
    [SerializeField]
    AudioType audioType = AudioType.RingCollision;

    private void OnCollisionEnter(Collision col)
    {
        if (col.relativeVelocity.sqrMagnitude < 0.2f)
        {
            return;
        }

        AudioController.Instance.PlayAudio(audioType);
    }
}
