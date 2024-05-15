using Godot;
using System;

public interface IEnemy
{
	void Attack();
	Vector2 GetPosition();
	void Damage(float damageAmount);
    void OnDeath();
	bool IsAlive();
}
