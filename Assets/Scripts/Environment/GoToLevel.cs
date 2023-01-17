using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

class GoToLevel : Interactable
{
    [SerializeField] private string sceneName;

    public void SetNextLevel(string levelName) { sceneName= levelName; }

    public override void DoInteract(InputAction.CallbackContext cb)
    {
        GameManager.Instance.TransitionToLevel(sceneName);
    }
}
