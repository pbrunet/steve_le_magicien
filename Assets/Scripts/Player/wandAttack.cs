using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class wandAttack : MonoBehaviour
{
    [SerializeField] private ParticleSystem wandParticules;
    [SerializeField] private float projectileSpeed = 50;

    private Shuriken shuriken;

    public AttackKind GetAttackKind()
    {
        return PlayerData.Instance.GetCurrentWeapon().kind;
    }

    public GameObject Invoke()
    {
        WeaponUpgradeData weapon = PlayerData.Instance.GetCurrentWeapon();
        return Instantiate(weapon.magicBall, new Vector3(gameObject.transform.position.x + 5 * PlayerController.Instance.GetDirection(), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
    }

    public void ChangeWeapon()
    {
        WeaponUpgradeData weapon = PlayerData.Instance.GetCurrentWeapon();
        if (weapon.kind == AttackKind.SHURIKEN)
        {
            shuriken = Instantiate(weapon.magicBall, gameObject.transform).GetComponent<Shuriken>();
        } else if(shuriken != null)
        {
            Destroy(shuriken);
        }

    }

    public void DoAttack()
    {
        WeaponUpgradeData weapon = PlayerData.Instance.GetCurrentWeapon();
        if (weapon.kind == AttackKind.SHOT_BULLET)
        {
            GameObject bullet = Instantiate(weapon.magicBall, new Vector3(gameObject.transform.position.x + 5 * PlayerController.Instance.GetDirection(), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed * PlayerController.Instance.GetDirection(), 0, 0);
        }
        else
        if (weapon.kind == AttackKind.SHURIKEN)
        {
            shuriken.GetComponent<Shuriken>().Launch(new Vector3(projectileSpeed * PlayerController.Instance.GetDirection(), 0, 0));
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

                GameObject bullet = Instantiate(weapon.magicBall, new Vector3(gameObject.transform.position.x + rnd * 1.1f * PlayerController.Instance.GetDirection(), gameObject.transform.position.y + 50, gameObject.transform.position.z), Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(10, -projectileSpeed, 0);
            }
        }
        wandParticules.Play();
    }
}
