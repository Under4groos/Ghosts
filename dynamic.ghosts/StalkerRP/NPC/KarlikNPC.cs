using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.PostProcessing;

namespace StalkerRP.NPC
{
	// Token: 0x020000B6 RID: 182
	[DebugSpawnable(Name = "Karlik", Category = "Mutants")]
	[Title("Karlik")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/karlik/karlik.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class KarlikNPC : NPCBase
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000235C2 File Offset: 0x000217C2
		public virtual string GrowlSound
		{
			get
			{
				return "karlik.growl";
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x000235C9 File Offset: 0x000217C9
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000235D0 File Offset: 0x000217D0
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x000235D7 File Offset: 0x000217D7
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 14f;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x000235DE File Offset: 0x000217DE
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000235E1 File Offset: 0x000217E1
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000235E8 File Offset: 0x000217E8
		private float psyDamageRange
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000235EF File Offset: 0x000217EF
		private float psyDamagePerSecond
		{
			get
			{
				return 13.5f;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000235F6 File Offset: 0x000217F6
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(32f, 20f);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00023607 File Offset: 0x00021807
		protected override string NPCAssetID
		{
			get
			{
				return "karlik";
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00023610 File Offset: 0x00021810
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementDragTurn(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new KarlikIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CircleState = new KarlikCircleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new KarlikChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new KarlikMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
			PsyDamageEmitterComponent psyDamageEmitterComponent = this.Components.Create<PsyDamageEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			psyDamageEmitterComponent.Initialise(this.psyDamagePerSecond, this.psyDamageRange);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000236AA File Offset: 0x000218AA
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000236C2 File Offset: 0x000218C2
		public override void ClientSpawn()
		{
			PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			postProcessEmitterComponent.SetUpEmitter(EffectsPostProcessManager.STALKER_PSY_KARLIK_COLOR, 4000f, 1.5f, "psy_aura_loop", 0.05f, 0f);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000236F8 File Offset: 0x000218F8
		protected override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter("fSpeedFraction", this.Velocity.Length / this.GetWishSpeed());
		}

		// Token: 0x0400029A RID: 666
		public KarlikIdleState IdleState;

		// Token: 0x0400029B RID: 667
		public KarlikCircleState CircleState;

		// Token: 0x0400029C RID: 668
		public KarlikChaseState ChaseState;

		// Token: 0x0400029D RID: 669
		public KarlikMeleeState MeleeState;
	}
}
