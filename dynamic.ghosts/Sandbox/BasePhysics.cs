using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000174 RID: 372
	[PhysicsSimulated]
	[Description("Base entity with physical properties, enables impact damage and the like")]
	public class BasePhysics : ModelEntity
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x00043A7D File Offset: 0x00041C7D
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.UsePhysicsCollision = true;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00043AA2 File Offset: 0x00041CA2
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ApplyDamageForces(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00043ABC File Offset: 0x00041CBC
		protected void ApplyDamageForces(DamageInfo info)
		{
			PhysicsBody body = info.Body;
			if (!body.IsValid())
			{
				body = base.PhysicsBody;
			}
			if (body.IsValid() && !info.Flags.HasFlag(DamageFlags.PhysicsImpact))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body.ApplyImpulseAt(info.Position, info.Force * 100f);
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00043B28 File Offset: 0x00041D28
		protected virtual ModelPropData GetModelPropData()
		{
			ModelPropData propData;
			if (base.Model != null && !base.Model.IsError && base.Model.TryGetData<ModelPropData>(out propData))
			{
				return propData;
			}
			ModelPropData defaultData = new ModelPropData();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			defaultData.Health = -1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			defaultData.ImpactDamage = 10f;
			if (base.PhysicsGroup != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				defaultData.ImpactDamage = base.PhysicsGroup.Mass / 10f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			defaultData.MinImpactDamageSpeed = 500f;
			return defaultData;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00043BB8 File Offset: 0x00041DB8
		protected override void OnPhysicsCollision(CollisionEventData eventData)
		{
			ModelPropData propData = this.GetModelPropData();
			if (propData == null)
			{
				return;
			}
			float minImpactSpeed = propData.MinImpactDamageSpeed;
			if (minImpactSpeed <= 0f)
			{
				minImpactSpeed = 500f;
			}
			float impactDmg = propData.ImpactDamage;
			if (impactDmg <= 0f)
			{
				impactDmg = 10f;
			}
			float speed = eventData.Speed;
			if (speed > minImpactSpeed)
			{
				if (base.Health > 0f)
				{
					float damage = speed / minImpactSpeed * impactDmg;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TakeDamage(DamageInfo.Generic(damage).WithFlag(DamageFlags.PhysicsImpact));
				}
				CollisionEntityData other = eventData.Other;
				if (other.Entity.IsValid() && other.Entity != this)
				{
					float damage2 = speed / minImpactSpeed * impactDmg * 1.2f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					other.Entity.TakeDamage(DamageInfo.Generic(damage2).WithFlag(DamageFlags.PhysicsImpact).WithAttacker(this, null).WithPosition(eventData.Position).WithForce(eventData.This.PreVelocity));
				}
			}
		}
	}
}
