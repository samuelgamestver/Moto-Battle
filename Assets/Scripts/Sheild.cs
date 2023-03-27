using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{

    [SerializeField] Player player;

    private void OnCollisionEnter(Collision collision)
    {
          if (player && collision.gameObject.tag == "Spear")
            player.Damage(1);
    }
}
