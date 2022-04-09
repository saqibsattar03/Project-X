using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	[SerializeField] private int currnentHealth;
	public int damage;
	[SerializeField] private float attackRange = 0.5f;
	public int doubleDamage;

	public LayerMask enemyLayers;

	// Characters stats initialization
	protected void InstantiateCharacter(int maxHealth, int damage, int heavyDamage) 
	{
		this.maxHealth = maxHealth;
		currnentHealth = this.maxHealth;
		this.damage = damage;
		this.doubleDamage = heavyDamage;
	}

	protected void Attack(int damage,LayerMask enemyLayer) 
	{
		// attack functionality goes here
		Collider[] hitEnemies = Physics.OverlapSphere(transform.position,attackRange,enemyLayer);
		foreach (var enemy in hitEnemies) 
		{
			enemy.SendMessage("Add Damage");
			enemy.GetComponent<Enemy>().GetDamage(damage);
		}
		
	}
	protected void GetDamage(int damage)
	{
		
		currnentHealth -= damage;
		Debug.Log(gameObject.name + "health" + currnentHealth);
		if (currnentHealth < 0) 
		{
			Die();
		}
	}

	protected void Die() 
	{
		//Death functionality goes here
		Destroy(gameObject);
	}

	protected void OnDrawGizmos()
	{
		// gizmos definition goes here
	}
}
