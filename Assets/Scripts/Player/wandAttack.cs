using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class wandAttack : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private ParticleSystem wandParticules;
    [SerializeField] private float projectileSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        InputActionMap actionMap = actionAsset.FindActionMap("Player");
        InputAction action = actionMap.FindAction("Attack");
        action.started += DoAttack;
    }

    // Update is called once per frame
    void DoAttack(InputAction.CallbackContext cb)
    {
        WeaponUpgradeData weapon = PlayerData.Instance.GetCurrentWeapon();
        MagicBall bullet = Instantiate<MagicBall>(weapon.magicBall, new Vector3(gameObject.transform.position.x + 5 * GetComponent<PlayerController>().GetDirection(), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed * GetComponent<PlayerController>().GetDirection(), 0, 0);
        wandParticules.Play();
    }
}
