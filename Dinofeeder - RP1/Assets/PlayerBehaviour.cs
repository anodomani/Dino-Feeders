using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Vector3 mousePos;
    GameManager gM;
    public float moveSpeed;
    public float reach;
    public SpriteRenderer avatarSR;
    public Animator avatarAnimator;
    public LayerMask interactMask;
    Rigidbody2D rb;
    public event System.Action interactEvent;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Interact();
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            if(Input.GetAxisRaw("Horizontal") != 0){
                avatarSR.flipX = Input.GetAxisRaw("Horizontal") < 0 ? true : false;
            }
            if(avatarSR.flipX){
                avatarAnimator.Play("Lean Right",1);
            }else{
                avatarAnimator.Play("Lean Left",1);
            }
        }else{
            avatarAnimator.Play("IdleRotation",1);
        }
    }

    void Movement(){
        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed, 0.5f);
    }

    void Interact(){
        if(Input.GetButtonDown("Interact")){
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 200, interactMask)){
                    print("name: " + hit.collider.name + ", distance: " + ", " + transform.position + ", " + hit.transform.position + ", " + Vector3.Distance(transform.position, hit.transform.position) + ", reach: " + reach);
                    if(hit.transform.GetComponent<TileBehaviour>() != null && Vector3.Distance(transform.position, hit.transform.position) < reach){
                        hit.transform.gameObject.GetComponent<TileBehaviour>().currentState = GameManager.State.live;
                    }
                }
            }
            /*
            RaycastHit hit;
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(mousePos, out hit, 100)){
                print("interacting " + Vector3.Distance(transform.position, Input.mousePosition) + ", " + mousePos + ", " + hit.transform.gameObject.name);
                if(Vector3.Distance(transform.position, Input.mousePosition) < reach){
                    if(hit.transform != null){
                        print("grass here!");
                        hit.transform.gameObject.GetComponent<TileBehaviour>().currentState = GameManager.State.live;
                    }
                }   
            }
            */
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawLine(mousePos, new Vector3(mousePos.x, mousePos.y, 20));
        Gizmos.DrawLine(transform.position, mousePos);
    }
}
