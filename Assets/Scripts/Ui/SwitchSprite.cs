using UnityEngine;
using UnityEngine.UI;

public class SwitchSprite : MonoBehaviour {

    [SerializeField]
    Sprite spriteStart;

    [SerializeField]
    Sprite spriteAlt;

    bool altActive;

    public void Switch()
    {
        if (GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().sprite = altActive ? spriteStart : spriteAlt;
        else if (GetComponent<Image>())
            GetComponent<Image>().sprite = altActive ? spriteStart : spriteAlt;
        altActive = !altActive;
    }
}
