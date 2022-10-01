using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.NPC;

namespace StalkerRP.Anomaly
{
	// Token: 0x0200013C RID: 316
	[DebugSpawnable(Name = "Cannon", Category = "Anomalies")]
	[Title("Cannon")]
	[HammerEntity]
	[Category("Anomalies")]
	[EditorModel("models/stalker/anomalies/fruit_punch.vmdl", "white", "white")]
	[Spawnable]
	public class AnomalyCannon : AnomalyBase
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x000385CB File Offset: 0x000367CB
		public float PullStrength
		{
			get
			{
				return 3500f;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x000385D2 File Offset: 0x000367D2
		public Vector3 HoverPos
		{
			get
			{
				return this.Position + Vector3.Up * 45f;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x000385EE File Offset: 0x000367EE
		[Description("How long in seconds after the active state ends the anomaly waits before exploding.")]
		private float BurstDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000385F5 File Offset: 0x000367F5
		public override float ActiveTime
		{
			get
			{
				return 1.9f;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x000385FC File Offset: 0x000367FC
		public override float ActivationDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00038603 File Offset: 0x00036803
		public override float TriggerSize
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003860A File Offset: 0x0003680A
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00038617 File Offset: 0x00036817
		protected override bool AnomalyActiveTick(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PullEntities(deltaTime);
			return this.TimeSinceActivated <= this.ActiveTime;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003863B File Offset: 0x0003683B
		protected override void OnAnomalyInitialTrigger()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InitialTriggerEffects();
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00038648 File Offset: 0x00036848
		protected override void OnAnomalyActivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnomalyActivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RagdollNPCs();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActivatedEffect();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003866B File Offset: 0x0003686B
		protected override void OnAnomalyActiveFinished()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EndTriggeredEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ThrowAllEnts();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurst(this.BurstDelay);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000386A0 File Offset: 0x000368A0
		private void PullEntities(float deltaTime)
		{
			Vector3 targPos = this.HoverPos;
			foreach (Entity ent in base.TouchingEntities)
			{
				if (ent.IsValid())
				{
					Vector3 entPos = ent.Position;
					Vector3 dir = (targPos - entPos).Normal;
					float frac = entPos.Distance(targPos) / this.TriggerSize;
					DeathRagdoll rag = ent as DeathRagdoll;
					if (rag != null)
					{
						if (rag.PhysicsGroup != null)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							ent.Velocity = dir * this.PullStrength * frac * 0.2f;
							Vector3 angleForce = Vector3.Up * 5f + Vector3.Random * 2f;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							angleForce *= 1f + this.TimeSinceActivated / this.ActiveTime * 2f;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							angleForce *= rag.PhysicsGroup.Mass;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							rag.PhysicsGroup.ApplyAngularImpulse(angleForce, false);
						}
					}
					else if (ent is Player)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						ent.Velocity += dir * this.PullStrength * deltaTime;
					}
					else
					{
						Vector3 angleForce2 = Vector3.Right * 5f + Vector3.Random * 2f;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						angleForce2 *= 1f + this.TimeSinceActivated / this.ActiveTime * 2f;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						angleForce2 *= ent.PhysicsGroup.Mass;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						angleForce2 *= 0.001f;
						Vector3 force = dir * this.PullStrength * frac * 0.2f;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						ent.ApplyAbsoluteImpulse(force);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						ent.ApplyLocalAngularImpulse(angleForce2);
					}
				}
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000388F0 File Offset: 0x00036AF0
		protected override void OnEntityEndTouch(Entity entity)
		{
			DeathRagdoll rag = entity as DeathRagdoll;
			if (rag == null || rag.PhysicsGroup == null)
			{
				return;
			}
			for (int i = 0; i < rag.PhysicsGroup.BodyCount; i++)
			{
				rag.PhysicsGroup.GetBody(i).GravityEnabled = true;
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00038938 File Offset: 0x00036B38
		protected override void OnEntityBeginTouch(Entity entity)
		{
			if (base.IsActive)
			{
				NPCBase npc = entity as NPCBase;
				if (npc != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.RagdollNPC(npc);
				}
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00038964 File Offset: 0x00036B64
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles idleDistortion = this.IdleDistortion;
			if (idleDistortion != null)
			{
				idleDistortion.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles dustParticles = this.DustParticles;
			if (dustParticles != null)
			{
				dustParticles.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles activatedParticle = this.ActivatedParticle;
			if (activatedParticle == null)
			{
				return;
			}
			activatedParticle.Destroy(false);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000389C0 File Offset: 0x00036BC0
		private void ThrowAllEnts()
		{
			foreach (Entity entity in base.TouchingEntities.ToArray<Entity>())
			{
				Vector3 vel = Vector3.Random.Normal * 5000f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vel.z = Math.Max(-100f, vel.z);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.ApplyAbsoluteImpulse(vel);
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00038A2C File Offset: 0x00036C2C
		private void RagdollNPCs()
		{
			Entity[] array = base.TouchingEntities.ToArray<Entity>();
			for (int i = 0; i < array.Length; i++)
			{
				NPCBase npc = array[i] as NPCBase;
				if (npc != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.RagdollNPC(npc);
				}
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00038A6C File Offset: 0x00036C6C
		private void RagdollNPC(NPCBase npc)
		{
			DeathRagdoll rag = npc.CreateDeathRagdollAndRemove();
			for (int i = 0; i < rag.PhysicsGroup.BodyCount; i++)
			{
				rag.PhysicsGroup.GetBody(i).GravityEnabled = false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TouchingEntities.Add(rag);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00038ABC File Offset: 0x00036CBC
		private void DoBurst(float delay)
		{
			AnomalyCannon.<DoBurst>d__27 <DoBurst>d__;
			<DoBurst>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DoBurst>d__.<>4__this = this;
			<DoBurst>d__.delay = delay;
			<DoBurst>d__.<>1__state = -1;
			<DoBurst>d__.<>t__builder.Start<AnomalyCannon.<DoBurst>d__27>(ref <DoBurst>d__);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00038AFB File Offset: 0x00036CFB
		public override bool IsValidTriggerEnt(Entity ent)
		{
			return !ent.IsWorld;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00038B08 File Offset: 0x00036D08
		[ClientRpc]
		private void CreateIdleEffect()
		{
			if (!this.CreateIdleEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleDistortion = Particles.Create("particles/stalker/anomalies/vortex_idle.vpcf", this, null, true);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00038B40 File Offset: 0x00036D40
		[ClientRpc]
		private void ActivatedEffect()
		{
			if (!this.ActivatedEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DustParticles = Particles.Create("particles/stalker/anomalies/vortex_dust.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("vortex.blowout");
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00038B88 File Offset: 0x00036D88
		[ClientRpc]
		private void InitialTriggerEffects()
		{
			if (!this.InitialTriggerEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("grav.trigger");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles idleDistortion = this.IdleDistortion;
			if (idleDistortion != null)
			{
				idleDistortion.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActivatedParticle = Particles.Create("particles/stalker/anomalies/grav_burst.vpcf", this, null, true);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00038BE8 File Offset: 0x00036DE8
		[ClientRpc]
		private void EndTriggeredEffects()
		{
			if (!this.EndTriggeredEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles dustParticles = this.DustParticles;
			if (dustParticles != null)
			{
				dustParticles.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles activatedParticle = this.ActivatedParticle;
			if (activatedParticle == null)
			{
				return;
			}
			activatedParticle.Destroy(false);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00038C34 File Offset: 0x00036E34
		[ClientRpc]
		private void DoBurstEffect()
		{
			if (!this.DoBurstEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/grav_implode.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/vortex_explode.vpcf", this, null, true);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00038C7C File Offset: 0x00036E7C
		protected bool CreateIdleEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("CreateIdleEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(118207893, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00038CDC File Offset: 0x00036EDC
		protected bool ActivatedEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ActivatedEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1073951328, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00038D3C File Offset: 0x00036F3C
		protected bool InitialTriggerEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("InitialTriggerEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(175600792, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00038D9C File Offset: 0x00036F9C
		protected bool EndTriggeredEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("EndTriggeredEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(173842672, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00038DFC File Offset: 0x00036FFC
		protected bool DoBurstEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoBurstEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-842590676, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00038E5C File Offset: 0x0003705C
		private void CreateIdleEffect(To toTarget)
		{
			this.CreateIdleEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00038E6B File Offset: 0x0003706B
		private void ActivatedEffect(To toTarget)
		{
			this.ActivatedEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00038E7A File Offset: 0x0003707A
		private void InitialTriggerEffects(To toTarget)
		{
			this.InitialTriggerEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00038E89 File Offset: 0x00037089
		private void EndTriggeredEffects(To toTarget)
		{
			this.EndTriggeredEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00038E98 File Offset: 0x00037098
		private void DoBurstEffect(To toTarget)
		{
			this.DoBurstEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00038EA8 File Offset: 0x000370A8
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= 118207893)
			{
				if (id == -842590676)
				{
					if (!Prediction.WasPredicted("DoBurstEffect", Array.Empty<object>()))
					{
						this.DoBurstEffect();
					}
					return;
				}
				if (id == 118207893)
				{
					if (!Prediction.WasPredicted("CreateIdleEffect", Array.Empty<object>()))
					{
						this.CreateIdleEffect();
					}
					return;
				}
			}
			else
			{
				if (id == 173842672)
				{
					if (!Prediction.WasPredicted("EndTriggeredEffects", Array.Empty<object>()))
					{
						this.EndTriggeredEffects();
					}
					return;
				}
				if (id == 175600792)
				{
					if (!Prediction.WasPredicted("InitialTriggerEffects", Array.Empty<object>()))
					{
						this.InitialTriggerEffects();
					}
					return;
				}
				if (id == 1073951328)
				{
					if (!Prediction.WasPredicted("ActivatedEffect", Array.Empty<object>()))
					{
						this.ActivatedEffect();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x0400047E RID: 1150
		private Particles IdleDistortion;

		// Token: 0x0400047F RID: 1151
		private Particles ActivatedParticle;

		// Token: 0x04000480 RID: 1152
		private Particles DustParticles;
	}
}
