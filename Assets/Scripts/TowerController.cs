using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

public class TowerController: MonoBehaviour {
	public LayerMask enemyLayer;
	protected float lastAttackTime = 0f;
	protected int damage = 1;
	protected int towerHealth = 100;
	protected float attackCooldown = 1f;
	
	protected bool placed = false;
	
	protected virtual void Start() {
		
	}

	protected virtual void Update() {
		// no target detection or attack without being placed
		if (placed) {
			Debug.Log("works");
			// attack if not on cooldown and if target near by
			if (TargetDetected() && AttackReady()) {
				Attack();
				lastAttackTime = Time.time;
			}
		}
	}
	
	// initialize tower
	protected virtual void Initialize() {
		return;
	}
	
	// target detected = true ; no target near by = false;
	protected virtual bool TargetDetected() {
		return false;
	}
	
	// attack cooldown
	// returns true if attack ready
	protected virtual bool AttackReady() {
		return (Time.time - lastAttackTime >= attackCooldown);
	}

	// attack target (depends on tower type)
	protected virtual void Attack() {
		return;
	}
	
	// activate tower
	public void PlaceTower() {
		Initialize();
		placed = true;
	}

    private void OnTriggerEnter(Collider other) { //TODO: Add LifeBar
		//Collision EnemyBullet -> Tower
        if (other.gameObject.tag == "EnemyBullet")
        {
            BulletController bulletController = other.gameObject.GetComponent<BulletController>();
            if (bulletController != null)
            {
                int damage = bulletController.GetDamage();
                bulletController.TargetHit();
                TakeDamage(damage);
				Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        towerHealth -= damage;
        if (towerHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Tower destroyed.");
            //GameManager.instance.UpdatePlayerScore(enemyValue); Minuspunkte hinzuf�gen?
        }
    }
	
	public bool hasBeenPlaced() {
		return placed;
	}


}
