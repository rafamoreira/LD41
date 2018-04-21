using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("OnHover", false);

    }

    public void OnHover() {
        animator.SetBool("OnHover", true);
    }

    public void OnHoverExit() {
        animator.SetBool("OnHover", false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        print("Pointer over");
        animator.SetBool("OnHover", true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        print("Pointer left");
        animator.SetBool("OnHover", false);
    }
}
