using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    public Mole currentMole;
    private Animator anim;

    // Called by animation event
    public void HammerAnimComplete()
    {
        currentMole.hammerAnimComplete = true;
    }

    public void SetHammerAnim(bool enable)
    {
        if(anim == null) { anim = gameObject.GetComponent<Animator>(); }

        if (enable)
        {
            anim.SetBool("HammerHit", true);
        }
        else
        {
            currentMole.hammerAnimComplete = false;
            anim.SetBool("HammerHit", false);
            gameObject.SetActive(false);
        }
    }
}
