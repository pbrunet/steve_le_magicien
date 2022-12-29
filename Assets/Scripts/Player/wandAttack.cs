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
        InputAction weaponAction = actionMap.FindAction("ChangeWeapon");
        weaponAction.started += NextWeapon;
    }

    void DoAttack(InputAction.CallbackContext cb)
    {
        WeaponUpgradeData weapon = PlayerData.Instance.GetCurrentWeapon();
        if (weapon.kind == AttackKind.SHOT_BULLET)
        {
            MagicBall bullet = Instantiate<MagicBall>(weapon.magicBall, new Vector3(gameObject.transform.position.x + 5 * GetComponent<PlayerController>().GetDirection(), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed * GetComponent<PlayerController>().GetDirection(), 0, 0);
        } else
        {
            List<int> seen = new List<int>();
            int rnd;
            for (int i = 0; i < 4; i++)
            {
                do
                {
                    rnd = Random.Range(10, 20);
                    if (!seen.Contains(rnd))
                    {
                        seen.Add(rnd);
                        break;
                    }
                } while (true);

                MagicBall bullet = Instantiate<MagicBall>(weapon.magicBall, new Vector3(gameObject.transform.position.x + rnd * 1.1f * GetComponent<PlayerController>().GetDirection(), gameObject.transform.position.y + 50, gameObject.transform.position.z), Quaternion.identity);
                bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(10, -projectileSpeed, 0);
            }
        }
        wandParticules.Play();
    } 
    
    void NextWeapon(InputAction.CallbackContext cb)
    {
        PlayerData.Instance.GetNextWeapon();
        UIManager.Instance.inGameHUD.UpdateWeaponGUI();
    }
}
