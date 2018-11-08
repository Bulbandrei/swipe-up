using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickSfx : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    AudioType audioType = AudioType.RingCollision;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        AudioController.Instance.PlayAudio(audioType);
    }
}
