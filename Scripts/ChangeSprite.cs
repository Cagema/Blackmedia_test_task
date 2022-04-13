using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.sprite = sprites[PlayerPrefs.GetInt("SelectedSkin", 0)];
    }
}
