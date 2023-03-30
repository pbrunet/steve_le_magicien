using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

class GoToLevel : Interactable
{
    [SerializeField] private LevelInfo nextScene;

    public void SetNextLevel(LevelInfo levelName) { nextScene = levelName; }

    public override void DoInteract(InputAction.CallbackContext cb)
    {
        base.DoInteract(cb);
        GameManager.Instance.TransitionToLevel(nextScene);
    }
}
