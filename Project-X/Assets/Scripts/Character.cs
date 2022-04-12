using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	[SerializeField] private int currnentHealth;
	[SerializeField] private float attackRange1 = 0.5f;

	public int damage;
	public int doubleDamage;
	public LayerMask enemyLayers;
	public Transform attackPoint;
	public ParticleSystem[] particleEffects;
	public Animator animator;

	// Characters stats initialization
	protected void InstantiateCharacter(int maxHealth, int damage, int heavyDamage) 
	{
		this.maxHealth = maxHealth;
		currnentHealth = this.maxHealth;
		this.damage = damage;
		this.doubleDamage = heavyDamage;
	}

	//Play particle effects for characters
	public IEnumerator ParticleEffects(int effectsNum, float activationTime)
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

		Debug.Log(gameObject.layer);
		// attack functionality goes here
		Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange1, enemyLayers);
		//Debug.Log(hitEnemies);
		foreach (var enemy in hitEnemies) 
		{
			Debug.Log(enemy.name);
			enemy.GetComponent<Character>().GetDamage(damage, bloodParticleEffect, enemy.transform.position + new Vector3(0, 1.5f, 0), hitSoundName, screamSoundName);
		}
		
	}
	public void GetDamage(int damage, int bloodParticleEfffect, Vector3 transform, string hitSoundName, string screamSoundName)
	{
		//Animation
		//animator.SetTrigger("Hurt");

		//Audio
		AudioManager.instance.Play(hitSoundName);
		AudioManager.instance.Play(screamSoundName);

		//Particle effect
		Instantiate(particleEffects[bloodParticleEfffect], transform, Quaternion.identity).Play();

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
