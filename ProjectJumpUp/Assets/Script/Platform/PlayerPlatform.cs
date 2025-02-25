using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : BasePlatform
{
    public override void Disappear()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (boxCollider2d != null)
        {
            boxCollider2d.enabled = false;
        }
    }

}
