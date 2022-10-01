using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.NPC;

namespace StalkerRP
{
	// Token: 0x02000024 RID: 36
	public static class DamageEffectsManager
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00009760 File Offset: 0x00007960
		public static void ApplyDamageEffect(this Entity entity, DamageEffectBase effectBase)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DamageEffectsManager.EffectsList.Add(effectBase);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DamageEffectsManager.EntityToEffect[entity] = effectBase;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			effectBase.Host = entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			effectBase.OnEffectAdded();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000979A File Offset: 0x0000799A
		public static bool HasDamageEffect(this Entity entity)
		{
			return DamageEffectsManager.EntityToEffect.ContainsKey(entity);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000097A8 File Offset: 0x000079A8
		public static void TransferDamageEffect(this Entity entity, Entity newHost)
		{
			DamageEffectBase effectBase;
			if (DamageEffectsManager.EntityToEffect.TryGetValue(entity, out effectBase))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				effectBase.DoTransfer(newHost);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DamageEffectsManager.EntityToEffect.Remove(entity);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000097E1 File Offset: 0x000079E1
		public static bool IsOrganicTarget(Entity entity)
		{
			return entity is Player || entity is NPCBase || entity is DeathRagdoll;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009800 File Offset: 0x00007A00
		[Event.Tick.ServerAttribute]
		public static void EffectManagerTick()
		{
			for (int i = DamageEffectsManager.EffectsList.Count - 1; i >= 0; i--)
			{
				DamageEffectBase effectBase = DamageEffectsManager.EffectsList[i];
				if (!effectBase.Host.IsValid() || effectBase.TimeSinceCreated >= effectBase.LifeTime)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					DamageEffectsManager.EffectsList.RemoveAt(i);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					DamageEffectsManager.EntityToEffect.Remove(effectBase.Host);
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					effectBase.EffectTick();
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00009884 File Offset: 0x00007A84
		[ClientRpc]
		public static void ApplyEffect(Entity entity, string particle)
		{
			if (!DamageEffectsManager.ApplyEffect__RpcProxy(entity, particle, null))
			{
				return;
			}
			if (entity.IsValid())
			{
				Particles.Create(particle, entity, null, true);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000098B8 File Offset: 0x00007AB8
		private static bool ApplyEffect__RpcProxy(Entity entity, string particle, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ApplyEffect", new object[]
				{
					entity,
					particle
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(80785712, null))
			{
				if (!NetRead.IsSupported(entity))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ApplyEffect is not allowed to use Entity for the parameter 'entity'!");
					return false;
				}
				writer.Write<Entity>(entity);
				if (!NetRead.IsSupported(particle))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ApplyEffect is not allowed to use String for the parameter 'particle'!");
					return false;
				}
				writer.Write<string>(particle);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009968 File Offset: 0x00007B68
		public static void ApplyEffect(To toTarget, Entity entity, string particle)
		{
			DamageEffectsManager.ApplyEffect__RpcProxy(entity, particle, new To?(toTarget));
		}

		// Token: 0x04000071 RID: 113
		public static readonly List<DamageEffectBase> EffectsList = new List<DamageEffectBase>();

		// Token: 0x04000072 RID: 114
		public static readonly Dictionary<Entity, DamageEffectBase> EntityToEffect = new Dictionary<Entity, DamageEffectBase>();
	}
}
