using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	[SerializeField] private float attackRange1 = 0.5f;

	public int currnentHealth;
	public int damage;
	public int doubleDamage;
	public LayerMask enemyLayers;
	public Transform attackPoint;
	public ParticleSystem[] particleEffects;
	public Animator animator;
	public HealthBar healthBar;

	// Characters stats initialization
	protected void InstantiateCharacter(int maxHealth, int damage, int heavyDamage) 
	{
		this.maxHealth = maxHealth;
		currnentHealth = this.maxHealth;
		this.damage = damage;
		this.doubleDamage = heavyDamage;
		healthBar.SetMaxHealth(this.maxHealth);
	}

	//Play particle effects for characters
	protected IEnumerator ParticleEffects(int effectsNum, float activationTime)
	{
		yield return new WaitForSeconds(activationTime);
		particleEffects[effectsNum].Play();

	}

	protected void Attack(string animationName, int particleEffectNumber, float particleActivationTime, Transform attackPoint, LayerMask enemyLayers, int damage, int bloodParticleEffect, string attackSoundName, string hitSoundName, string screamSoundName)

	{
		//Play particle effect
		StartCoroutine(ParticleEffects(particleEffectNumber, particleActivationTime));

		// Play an attack animation
		animator.SetTrigger(animationName);

		//Play audio
		AudioManager.instance.Play(attackSoundName);

		// attack functionality goes here
		Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange1, enemyLayers);
		
		foreach (var enemy in hitEnemies) 
		{
			enemy.GetComponent<Character>().GetDamage(damage, bloodParticleEffect, enemy.transform.position + new Vector3(0, 1.5f, 0), hitSoundName, screamSoundName);
		}
		
	}
	protected void GetDamage(int damage, int bloodParticleEfffect, Vector3 transform, string hitSoundName, string screamSoundName)
	{
	
		//Audio
		AudioManager.instance.Play(hitSoundName);
		AudioManager.instance.Play(screamSoundName);

		//Particle effect
		Instantiate(particleEffects[bloodParticleEfffect], transform, Quaternion.identity).Play();

		currnentHealth -= damage;
		healthBar.SetHealth(currnentHealth);
		if (currnentHealth <= 0) 
		{
			Die();
		}
	}

	protected void Die() 
	{
		//Death functionality goes here
		animator.SetBool("is_dead", true);
		GetComponent<Collider>().enabled = false;
		this.enabled = false;
		Destroy(this.gameObject, 2);
	}

	protected void OnDrawGizmos()
	{
		// gizmos definition goes here

		if (attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(attackPoint.position, attackRange1);
	}
}
