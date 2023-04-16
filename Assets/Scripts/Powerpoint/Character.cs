using System;
using UnityEngine;

namespace PowerPoint {
public class Character : MonoBehaviour {
    public CharacterType type;
    private Animator anim;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start() {
        switch (type) {
            case CharacterType.Soldier:
                ChangeAnimState(Data.IDLE_ANIM);
                break;
            case CharacterType.Civilian:
                break;
        }
    }

    public void ChangeAnimState(int state) {
        anim.CrossFade(state,0,0);
    }
}

public enum CharacterType {
    None,
    Soldier,
    Civilian,
}
}

