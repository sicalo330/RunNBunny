using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isShoot = false;
    public bool isKnife = false;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.obj != null){
            isMoving = Player.obj.isMoving;
            isGrounded = Player.obj.isGrounded;
            isShoot = Player.obj.isShoot;
            isKnife = Player.obj.knife;

            anim.SetBool("isMoving", isMoving);
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isShoot", isShoot);
            // anim.SetBool("isKnife", isKnife);
        }
        else{
            Debug.LogWarning("Player.obj no está asignado.");
        }
    }
}
