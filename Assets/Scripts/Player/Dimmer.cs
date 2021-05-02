using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class Dimmer : MonoBehaviour
{
    public Light2DE OnHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            foreach (Light2DE light in Resources.FindObjectsOfTypeAll(typeof(Light2DE))) {
                if (light == OnHit)
                {
                    continue;
                }

                light.enabled = false;
            }

            foreach (SpriteRenderer sr in Resources.FindObjectsOfTypeAll(typeof(SpriteRenderer)))
            {
                if (sr.gameObject == collision.gameObject || this.gameObject == sr.gameObject)
                {
                    continue;
                }

                sr.enabled = false;
            }

            foreach (TilemapRenderer tr in Resources.FindObjectsOfTypeAll(typeof(TilemapRenderer)))
            {
                tr.enabled = false;
            }

            OnHit.enabled = true;
        }
    }
}
