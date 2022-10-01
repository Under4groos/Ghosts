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
	// Token: 0x0200013D RID: 317
	[DebugSpawnable(Name = "Vortex", Category = "Anomalies")]
	[Title("Vortex")]
	[HammerEntity]
	[Category("Anomalies")]
	[EditorModel("models/stalker/anomalies/fruit_punch.vmdl", "white", "white")]
	[Spawnable]
	public class AnomalyVortex : AnomalyBase
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00038F77 File Offset: 0x00037177
		public float PullStrength
		{
			get
			{
				return 3000f;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00038F7E File Offset: 0x0003717E
		public Vector3 HoverPosDefault
		{
			get
			{
				return this.Position + Vector3.Up * 45f;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00038F9A File Offset: 0x0003719A
		public Vector3 HoverPosPlayer
		{
			get
			{
				return this.Position + Vector3.Up * 25f;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00038FB6 File Offset: 0x000371B6
		public float BurstDamage
		{
			get
			{
				return 70f;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00038FBD File Offset: 0x000371BD
		[Description("How long in seconds after the active state ends the anomaly waits before exploding.")]
		private float BurstDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00038FC4 File Offset: 0x000371C4
		public override float ActiveTime
		{
			get
			{
				return 1.9f;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00038FCB File Offset: 0x000371CB
		public override float ActivationDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00038FD2 File Offset: 0x000371D2
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartIdleSound();
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00038FEA File Offset: 0x000371EA
		private void StartIdleSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound = Sound.FromEntity("grav.idle", this);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00039002 File Offset: 0x00037202
		protected override bool AnomalyActiveTick(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PullEntities(deltaTime);
			return this.TimeSinceActivated <= this.ActiveTime;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00039026 File Offset: 0x00037226
		protected override void OnAnomalyInitialTrigger()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InitialTriggerEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound.SetVolume(0.1f);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00039049 File Offset: 0x00037249
		protected override void OnAnomalyActivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnomalyActivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RagdollNPCs();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActivatedEffect();
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003906C File Offset: 0x0003726C
		protected override void OnAnomalyActiveFinished()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EndTriggeredEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurst(this.BurstDelay);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00039098 File Offset: 0x00037298
		private void PullEntities(float deltaTime)
		{
			foreach (Entity ent in base.TouchingEntities)
			{
				if (ent.IsValid())
				{
					Vector3 targPos = (ent is Player) ? this.HoverPosPlayer : this.HoverPosDefault;
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
					else
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						ent.ApplyAbsoluteImpulse(dir * this.PullStrength * deltaTime);
					}
				}
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00039228 File Offset: 0x00037428
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

		// Token: 0x06000E3B RID: 3643 RVA: 0x00039270 File Offset: 0x00037470
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

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003929C File Offset: 0x0003749C
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
			Particles activatedParticle = this.ActivatedParticle;
			if (activatedParticle == null)
			{
				return;
			}
			activatedParticle.Destroy(false);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003930C File Offset: 0x0003750C
		private void GibAllRagdolls()
		{
			Entity[] array = base.TouchingEntities.ToArray<Entity>();
			for (int i = 0; i < array.Length; i++)
			{
				DeathRagdoll rag = array[i] as DeathRagdoll;
				if (rag != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					rag.Gib();
				}
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003934C File Offset: 0x0003754C
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

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003938C File Offset: 0x0003758C
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

		// Token: 0x06000E40 RID: 3648 RVA: 0x000393DC File Offset: 0x000375DC
		private void DoBurst(float delay)
		{
			AnomalyVortex.<DoBurst>d__31 <DoBurst>d__;
			<DoBurst>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DoBurst>d__.<>4__this = this;
			<DoBurst>d__.delay = delay;
			<DoBurst>d__.<>1__state = -1;
			<DoBurst>d__.<>t__builder.Start<AnomalyVortex.<DoBurst>d__31>(ref <DoBurst>d__);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003941C File Offset: 0x0003761C
		public override void OnHitByBolt(BoltProjectile bolt)
		{
			if (base.IsTriggered || base.IsActive)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBoltHitEffect(bolt.Position);
			Vector3 normal = (bolt.Position - this.Position).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bolt.Velocity = 0f;
			Vector3 force = normal * Rand.Float(200f, 350f) + Vector3.Random * 45f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bolt.ApplyAbsoluteImpulse(force);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bolt.PhysicsBody.ApplyAngularImpulse(force * Vector3.Random);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryBoltHitSound();
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000394D4 File Offset: 0x000376D4
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

		// Token: 0x06000E43 RID: 3651 RVA: 0x00039510 File Offset: 0x00037710
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

		// Token: 0x06000E44 RID: 3652 RVA: 0x00039548 File Offset: 0x00037748
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

		// Token: 0x06000E45 RID: 3653 RVA: 0x00039590 File Offset: 0x00037790
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

		// Token: 0x06000E46 RID: 3654 RVA: 0x000395F0 File Offset: 0x000377F0
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

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003963C File Offset: 0x0003783C
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

		// Token: 0x06000E48 RID: 3656 RVA: 0x00039684 File Offset: 0x00037884
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

		// Token: 0x06000E49 RID: 3657 RVA: 0x000396B8 File Offset: 0x000378B8
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
			using (NetWrite writer = NetWrite.StartRpc(-1905560059, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00039718 File Offset: 0x00037918
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
			using (NetWrite writer = NetWrite.StartRpc(-231176336, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00039778 File Offset: 0x00037978
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
			using (NetWrite writer = NetWrite.StartRpc(-289120664, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000397D8 File Offset: 0x000379D8
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
			using (NetWrite writer = NetWrite.StartRpc(1200054880, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00039838 File Offset: 0x00037A38
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
			using (NetWrite writer = NetWrite.StartRpc(-2072155556, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00039898 File Offset: 0x00037A98
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
			using (NetWrite writer = NetWrite.StartRpc(-1856863804, this))
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

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003992C File Offset: 0x00037B2C
		private void CreateIdleEffect(To toTarget)
		{
			this.CreateIdleEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003993B File Offset: 0x00037B3B
		private void ActivatedEffect(To toTarget)
		{
			this.ActivatedEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003994A File Offset: 0x00037B4A
		private void InitialTriggerEffects(To toTarget)
		{
			this.InitialTriggerEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00039959 File Offset: 0x00037B59
		private void EndTriggeredEffects(To toTarget)
		{
			this.EndTriggeredEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00039968 File Offset: 0x00037B68
		private void DoBurstEffect(To toTarget)
		{
			this.DoBurstEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00039977 File Offset: 0x00037B77
		private void DoBoltHitEffect(To toTarget, Vector3 pos)
		{
			this.DoBoltHitEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00039988 File Offset: 0x00037B88
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= -1856863804)
			{
				if (id == -2072155556)
				{
					if (!Prediction.WasPredicted("DoBurstEffect", Array.Empty<object>()))
					{
						this.DoBurstEffect();
					}
					return;
				}
				if (id == -1905560059)
				{
					if (!Prediction.WasPredicted("CreateIdleEffect", Array.Empty<object>()))
					{
						this.CreateIdleEffect();
					}
					return;
				}
				if (id == -1856863804)
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
			}
			else
			{
				if (id == -289120664)
				{
					if (!Prediction.WasPredicted("InitialTriggerEffects", Array.Empty<object>()))
					{
						this.InitialTriggerEffects();
					}
					return;
				}
				if (id == -231176336)
				{
					if (!Prediction.WasPredicted("ActivatedEffect", Array.Empty<object>()))
					{
						this.ActivatedEffect();
					}
					return;
				}
				if (id == 1200054880)
				{
					if (!Prediction.WasPredicted("EndTriggeredEffects", Array.Empty<object>()))
					{
						this.EndTriggeredEffects();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x04000481 RID: 1153
		private Particles IdleDistortion;

		// Token: 0x04000482 RID: 1154
		private Particles ActivatedParticle;

		// Token: 0x04000483 RID: 1155
		private Particles DustParticles;

		// Token: 0x04000484 RID: 1156
		private Sound IdleSound;

		// Token: 0x04000485 RID: 1157
		private TimeSince timeSinceLastHit = 0f;
	}
}
