using Godot;
using System;

public interface IEnemy
{
	void Attack();
	Vector2 GetPosition();
	void Damage(float damageAmount);
	void MakeInvincible();
    bool CanBeAttacked();
    void OnDeath();
}
