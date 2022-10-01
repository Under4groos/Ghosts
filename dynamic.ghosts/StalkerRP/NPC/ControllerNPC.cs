using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.PostProcessing;

namespace StalkerRP.NPC
{
	// Token: 0x02000097 RID: 151
	[DebugSpawnable(Name = "Controller", Category = "Mutants")]
	[Title("Controller")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/controller/controller.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class ControllerNPC : NPCBase, IPsyCreature
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001F0C7 File Offset: 0x0001D2C7
		public float PsyBoltDamage
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001F0CE File Offset: 0x0001D2CE
		public float PsyBoltMaxRange
		{
			get
			{
				return 3000f;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001F0D5 File Offset: 0x0001D2D5
		public float PsyBoltMinRange
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		public string ControllerDominateTag
		{
			get
			{
				return "ControllerDominated";
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001F0E3 File Offset: 0x0001D2E3
		public int DominatedCreaturesCount
		{
			get
			{
				return this.DominatedCreatures.Count;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(72f, 15f);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001F101 File Offset: 0x0001D301
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001F108 File Offset: 0x0001D308
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001F10B File Offset: 0x0001D30B
		private float psyDamageRange
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001F112 File Offset: 0x0001D312
		private float psyDamagePerSecond
		{
			get
			{
				return 16f;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001F119 File Offset: 0x0001D319
		protected override string NPCAssetID
		{
			get
			{
				return "controller";
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001F120 File Offset: 0x0001D320
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 62f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new ControllerIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new ControllerChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyBoltState = new ControllerPsyBoltState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DominateState = new ControllerDominateState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaggerState = new ControllerStaggerState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new ControllerMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
			PsyDamageEmitterComponent psyDamageEmitterComponent = this.Components.Create<PsyDamageEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			psyDamageEmitterComponent.Initialise(this.psyDamagePerSecond, this.psyDamageRange);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001F1E5 File Offset: 0x0001D3E5
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001F200 File Offset: 0x0001D400
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			postProcessEmitterComponent.SetUpEmitter(EffectsPostProcessManager.STALKER_PSY_CONTROLLER_COLOR, 5500f, 1.55f, "controller.presence", 0.55f, 0f);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001F24C File Offset: 0x0001D44C
		public override float GetWishSpeed()
		{
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001F259 File Offset: 0x0001D459
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DisposeDominatedCreatures();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001F271 File Offset: 0x0001D471
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CleanUpFX();
			if (base.IsServer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DisposeDominatedCreatures();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001F29C File Offset: 0x0001D49C
		public void DominateTarget(NPCBase target)
		{
			TargetComponent targetComponent = target.TargetComponent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TargetingComponent.AddForcedFriendly(targetComponent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TargetingComponent.SetThreat(targetComponent, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.TargetingComponent.AddForcedFriendly(base.TargetComponent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.TargetingComponent.SetThreat(base.TargetComponent, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.Tags.Add(this.ControllerDominateTag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/controller_dominate.vpcf", target, "head", true);
			foreach (NPCBase npc in this.DominatedCreatures)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npc.TargetingComponent.AddForcedFriendly(targetComponent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npc.TargetingComponent.SetThreat(targetComponent, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				target.TargetingComponent.AddForcedFriendly(npc.TargetComponent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				target.TargetingComponent.SetThreat(npc.TargetComponent, 0f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DominatedCreatures.Add(target);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.TargetingComponent.TrySetTarget(base.Target);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		public override void OnTargetAcquired(Entity target)
		{
			foreach (NPCBase npc in this.DominatedCreatures)
			{
				if (npc.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					npc.TargetingComponent.TrySetTarget(target);
				}
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001F45C File Offset: 0x0001D65C
		private void DisposeDominatedCreatures()
		{
			foreach (NPCBase npc in this.DominatedCreatures)
			{
				if (npc.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					npc.TakeDamage(DamageInfo.Generic(10000f));
				}
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		public void DoZoomInEffect(StalkerPlayer player, float zoomTime, float targetFOV)
		{
			ControllerNPC.<DoZoomInEffect>d__39 <DoZoomInEffect>d__;
			<DoZoomInEffect>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DoZoomInEffect>d__.<>4__this = this;
			<DoZoomInEffect>d__.player = player;
			<DoZoomInEffect>d__.zoomTime = zoomTime;
			<DoZoomInEffect>d__.<>1__state = -1;
			<DoZoomInEffect>d__.<>t__builder.Start<ControllerNPC.<DoZoomInEffect>d__39>(ref <DoZoomInEffect>d__);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001F510 File Offset: 0x0001D710
		[ClientRpc]
		public void DoPsyBoltFX()
		{
			if (!this.DoPsyBoltFX__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyBoltStart = base.PlaySound("controller.psyboltstart");
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001F548 File Offset: 0x0001D748
		[ClientRpc]
		public void PlayWooshSound()
		{
			ControllerNPC.<PlayWooshSound>d__42 <PlayWooshSound>d__;
			<PlayWooshSound>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<PlayWooshSound>d__.<>4__this = this;
			<PlayWooshSound>d__.<>1__state = -1;
			<PlayWooshSound>d__.<>t__builder.Start<ControllerNPC.<PlayWooshSound>d__42>(ref <PlayWooshSound>d__);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001F580 File Offset: 0x0001D780
		[ClientRpc]
		public void CancelPsyBolt()
		{
			if (!this.CancelPsyBolt__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyBoltStart.Stop();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		private void CleanUpFX()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyBoltStart.Stop();
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001F5C4 File Offset: 0x0001D7C4
		protected bool DoPsyBoltFX__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoPsyBoltFX", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(855296448, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001F624 File Offset: 0x0001D824
		protected bool PlayWooshSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("PlayWooshSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-865919983, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001F684 File Offset: 0x0001D884
		protected bool CancelPsyBolt__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("CancelPsyBolt", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-130513421, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		public void DoPsyBoltFX(To toTarget)
		{
			this.DoPsyBoltFX__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001F6F3 File Offset: 0x0001D8F3
		public void PlayWooshSound(To toTarget)
		{
			this.PlayWooshSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001F702 File Offset: 0x0001D902
		public void CancelPsyBolt(To toTarget)
		{
			this.CancelPsyBolt__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001F714 File Offset: 0x0001D914
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -865919983)
			{
				if (!Prediction.WasPredicted("PlayWooshSound", Array.Empty<object>()))
				{
					this.PlayWooshSound();
				}
				return;
			}
			if (id == -130513421)
			{
				if (!Prediction.WasPredicted("CancelPsyBolt", Array.Empty<object>()))
				{
					this.CancelPsyBolt();
				}
				return;
			}
			if (id == 855296448)
			{
				if (!Prediction.WasPredicted("DoPsyBoltFX", Array.Empty<object>()))
				{
					this.DoPsyBoltFX();
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x0400024E RID: 590
		public ControllerIdleState IdleState;

		// Token: 0x0400024F RID: 591
		public ControllerChaseState ChaseState;

		// Token: 0x04000250 RID: 592
		public ControllerPsyBoltState PsyBoltState;

		// Token: 0x04000251 RID: 593
		public ControllerDominateState DominateState;

		// Token: 0x04000252 RID: 594
		public ControllerStaggerState StaggerState;

		// Token: 0x04000253 RID: 595
		public ControllerMeleeState MeleeState;

		// Token: 0x04000254 RID: 596
		public int MaxDominatedTargets = 5;

		// Token: 0x04000255 RID: 597
		private readonly List<NPCBase> DominatedCreatures = new List<NPCBase>();

		// Token: 0x04000256 RID: 598
		private Sound psyBoltStart;
	}
}
