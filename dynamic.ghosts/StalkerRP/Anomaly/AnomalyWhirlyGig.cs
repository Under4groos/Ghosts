using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.NPC;

namespace StalkerRP.Anomaly
{
	// Token: 0x0200013E RID: 318
	[DebugSpawnable(Name = "Whirlygig", Category = "Anomalies")]
	[Title("Whirly Gig")]
	[HammerEntity]
	[Category("Anomalies")]
	[EditorModel("models/stalker/anomalies/fruit_punch.vmdl", "white", "white")]
	[Spawnable]
	public class AnomalyWhirlyGig : AnomalyBase
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00039AA9 File Offset: 0x00037CA9
		public float PullStrength
		{
			get
			{
				return 1500f;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00039AB0 File Offset: 0x00037CB0
		public Vector3 HoverPos
		{
			get
			{
				Vector3 c = this.Position + Vector3.Up * this.TriggerSize;
				Vector3 sway = Vector3.Right * 45f * MathF.Sin(Time.Now * 4f);
				Vector3 bounce = Vector3.Up * 15f * MathF.Sin(Time.Now);
				Vector3 bingus = Vector3.Forward * 45f * MathF.Cos(Time.Now * 4f);
				return c + sway + bounce + bingus;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00039B53 File Offset: 0x00037D53
		public float BurstDamage
		{
			get
			{
				return 70f;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00039B5A File Offset: 0x00037D5A
		[Description("How long in seconds after the active state ends the anomaly waits before exploding.")]
		private float BurstDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00039B61 File Offset: 0x00037D61
		public override float TriggerSize
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00039B68 File Offset: 0x00037D68
		public override float ActiveTime
		{
			get
			{
				return 1.9f;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00039B6F File Offset: 0x00037D6F
		public override float ActivationDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00039B76 File Offset: 0x00037D76
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartIdleSound();
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00039B8E File Offset: 0x00037D8E
		private void StartIdleSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound = Sound.FromEntity("grav.idle", this);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00039BA6 File Offset: 0x00037DA6
		protected override bool AnomalyActiveTick(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PullEntities(deltaTime);
			return this.TimeSinceActivated <= this.ActiveTime;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00039BCA File Offset: 0x00037DCA
		public override bool IsValidTriggerEnt(Entity ent)
		{
			return ent is NPCBase || ent is StalkerPlayer || ent is DeathRagdoll || ent is Prop || ent is StalkerProjectileBase;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00039BF7 File Offset: 0x00037DF7
		protected override void OnAnomalyInitialTrigger()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InitialTriggerEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound.SetVolume(0.1f);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00039C1A File Offset: 0x00037E1A
		protected override void OnAnomalyActivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnomalyActivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActivatedEffect();
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00039C32 File Offset: 0x00037E32
		protected override void OnAnomalyActiveFinished()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EndTriggeredEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurst(this.BurstDelay);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00039C5C File Offset: 0x00037E5C
		private void PullEntities(float deltaTime)
		{
			Vector3 targPos = this.HoverPos;
			foreach (Entity ent in Entity.FindInSphere(this.HoverPos, this.TriggerSize).Where(new Func<Entity, bool>(this.IsValidTriggerEnt)))
			{
				if (ent.IsValid())
				{
					NPCBase npc = ent as NPCBase;
					if (npc != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.RagdollNPC(npc);
					}
					else
					{
						Vector3 entPos = ent.Position;
						Vector3 dir = (targPos - entPos).Normal;
						float frac = entPos.Distance(targPos) / this.TriggerSize * 3f;
						Vector3 angleForce = Vector3.Up * 5f + Vector3.Random * 20f;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						angleForce *= 1f + this.TimeSinceActivated / this.ActiveTime * 2f;
						DeathRagdoll rag = ent as DeathRagdoll;
						if (rag != null)
						{
							if (rag.PhysicsGroup != null)
							{
								RuntimeHelpers.EnsureSufficientExecutionStack();
								ent.Velocity = dir * this.PullStrength * frac * 0.2f;
								RuntimeHelpers.EnsureSufficientExecutionStack();
								angleForce *= rag.PhysicsGroup.Mass;
								RuntimeHelpers.EnsureSufficientExecutionStack();
								rag.PhysicsGroup.ApplyAngularImpulse(angleForce, false);
								RuntimeHelpers.EnsureSufficientExecutionStack();
								rag.ApplyAbsoluteImpulse(Vector3.Random * this.PullStrength * 0.05f);
							}
						}
						else
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							angleForce *= ent.PhysicsGroup.Mass;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							ent.Velocity += dir * this.PullStrength * frac * 1f * deltaTime;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							ent.ApplyAbsoluteImpulse(dir * this.PullStrength * deltaTime * 0.25f);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							ent.ApplyLocalAngularImpulse(angleForce * deltaTime * 2f);
							float projScalar = ent.Velocity.Dot(-dir);
							if (projScalar > 0f)
							{
								Vector3 vecProj = projScalar * -dir;
								RuntimeHelpers.EnsureSufficientExecutionStack();
								ent.Velocity -= vecProj;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00039F00 File Offset: 0x00038100
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

		// Token: 0x06000E67 RID: 3687 RVA: 0x00039F48 File Offset: 0x00038148
		protected override void OnEntityBeginTouch(Entity entity)
		{
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00039F4C File Offset: 0x0003814C
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound.Stop();
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
			Particles dustParticlesPeak = this.DustParticlesPeak;
			if (dustParticlesPeak != null)
			{
				dustParticlesPeak.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles activatedParticle = this.ActivatedParticle;
			if (activatedParticle == null)
			{
				return;
			}
			activatedParticle.Destroy(false);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00039FD0 File Offset: 0x000381D0
		public override void DebugDraw()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DebugDraw();
			if (AnomalyBase.stalker_debug_anomaly_draw > 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Sphere(this.HoverPos, this.TriggerSize, Color.Green, 0f, true);
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003A00C File Offset: 0x0003820C
		private void GibAllRagdolls()
		{
			foreach (Entity entity in Entity.FindInSphere(this.HoverPos, this.TriggerSize))
			{
				DeathRagdoll rag = entity as DeathRagdoll;
				if (rag != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					rag.Gib();
				}
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003A070 File Offset: 0x00038270
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

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003A0B0 File Offset: 0x000382B0
		private DeathRagdoll RagdollNPC(NPCBase npc)
		{
			DeathRagdoll rag = npc.CreateDeathRagdollAndRemove();
			for (int i = 0; i < rag.PhysicsGroup.BodyCount; i++)
			{
				rag.PhysicsGroup.GetBody(i).GravityEnabled = false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TouchingEntities.Add(rag);
			return rag;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003A100 File Offset: 0x00038300
		private void DoBurst(float delay)
		{
			AnomalyWhirlyGig.<DoBurst>d__34 <DoBurst>d__;
			<DoBurst>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DoBurst>d__.<>4__this = this;
			<DoBurst>d__.delay = delay;
			<DoBurst>d__.<>1__state = -1;
			<DoBurst>d__.<>t__builder.Start<AnomalyWhirlyGig.<DoBurst>d__34>(ref <DoBurst>d__);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003A13F File Offset: 0x0003833F
		private void TryBoltHitSound()
		{
			if (this.timeSinceLastHit > 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound("grav.bolt_hit");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastHit = 0f;
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003A17C File Offset: 0x0003837C
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

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003A1B4 File Offset: 0x000383B4
		[ClientRpc]
		private void ActivatedEffect()
		{
			if (!this.ActivatedEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DustParticles = Particles.Create("particles/stalker/anomalies/whirl_dust.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DustParticlesPeak = Particles.Create("particles/stalker/anomalies/whirl_dust_peak.vpcf", this.HoverPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("vortex.blowout");
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003A218 File Offset: 0x00038418
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
			this.ActivatedParticle = Particles.Create("particles/stalker/anomalies/grav_burst.vpcf", this.HoverPos);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003A27C File Offset: 0x0003847C
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
			Particles dustParticlesPeak = this.DustParticlesPeak;
			if (dustParticlesPeak != null)
			{
				dustParticlesPeak.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles activatedParticle = this.ActivatedParticle;
			if (activatedParticle == null)
			{
				return;
			}
			activatedParticle.Destroy(false);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003A2E0 File Offset: 0x000384E0
		[ClientRpc]
		private void DoBurstEffect()
		{
			if (!this.DoBurstEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/grav_implode.vpcf", this.HoverPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/vortex_explode.vpcf", this.HoverPos);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003A32C File Offset: 0x0003852C
		[ClientRpc]
		private void DoBoltHitEffect(Vector3 pos)
		{
			if (!this.DoBoltHitEffect__RpcProxy(pos, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/grav_bolt_impact.vpcf", pos);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003A360 File Offset: 0x00038560
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
			using (NetWrite writer = NetWrite.StartRpc(2007055253, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003A3C0 File Offset: 0x000385C0
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
			using (NetWrite writer = NetWrite.StartRpc(578799712, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003A420 File Offset: 0x00038620
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
			using (NetWrite writer = NetWrite.StartRpc(-303027560, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003A480 File Offset: 0x00038680
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
			using (NetWrite writer = NetWrite.StartRpc(1310062320, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003A4E0 File Offset: 0x000386E0
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
			using (NetWrite writer = NetWrite.StartRpc(1443253292, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003A540 File Offset: 0x00038740
		protected bool DoBoltHitEffect__RpcProxy(Vector3 pos, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoBoltHitEffect", new object[]
				{
					pos
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(282200340, this))
			{
				if (!NetRead.IsSupported(pos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoBoltHitEffect is not allowed to use Vector3 for the parameter 'pos'!");
					return false;
				}
				writer.Write<Vector3>(pos);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003A5D4 File Offset: 0x000387D4
		private void CreateIdleEffect(To toTarget)
		{
			this.CreateIdleEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003A5E3 File Offset: 0x000387E3
		private void ActivatedEffect(To toTarget)
		{
			this.ActivatedEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003A5F2 File Offset: 0x000387F2
		private void InitialTriggerEffects(To toTarget)
		{
			this.InitialTriggerEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003A601 File Offset: 0x00038801
		private void EndTriggeredEffects(To toTarget)
		{
			this.EndTriggeredEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003A610 File Offset: 0x00038810
		private void DoBurstEffect(To toTarget)
		{
			this.DoBurstEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003A61F File Offset: 0x0003881F
		private void DoBoltHitEffect(To toTarget, Vector3 pos)
		{
			this.DoBoltHitEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003A630 File Offset: 0x00038830
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= 578799712)
			{
				if (id == -303027560)
				{
					if (!Prediction.WasPredicted("InitialTriggerEffects", Array.Empty<object>()))
					{
						this.InitialTriggerEffects();
					}
					return;
				}
				if (id == 282200340)
				{
					Vector3 __pos = read.ReadData<Vector3>(default(Vector3));
					if (!Prediction.WasPredicted("DoBoltHitEffect", new object[]
					{
						__pos
					}))
					{
						this.DoBoltHitEffect(__pos);
					}
					return;
				}
				if (id == 578799712)
				{
					if (!Prediction.WasPredicted("ActivatedEffect", Array.Empty<object>()))
					{
						this.ActivatedEffect();
					}
					return;
				}
			}
			else
			{
				if (id == 1310062320)
				{
					if (!Prediction.WasPredicted("EndTriggeredEffects", Array.Empty<object>()))
					{
						this.EndTriggeredEffects();
					}
					return;
				}
				if (id == 1443253292)
				{
					if (!Prediction.WasPredicted("DoBurstEffect", Array.Empty<object>()))
					{
						this.DoBurstEffect();
					}
					return;
				}
				if (id == 2007055253)
				{
					if (!Prediction.WasPredicted("CreateIdleEffect", Array.Empty<object>()))
					{
						this.CreateIdleEffect();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x04000486 RID: 1158
		private Particles IdleDistortion;

		// Token: 0x04000487 RID: 1159
		private Particles ActivatedParticle;

		// Token: 0x04000488 RID: 1160
		private Particles DustParticles;

		// Token: 0x04000489 RID: 1161
		private Particles DustParticlesPeak;

		// Token: 0x0400048A RID: 1162
		private Sound IdleSound;

		// Token: 0x0400048B RID: 1163
		private TimeSince timeSinceLastHit = 0f;
	}
}
