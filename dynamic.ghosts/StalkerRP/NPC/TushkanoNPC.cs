using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000E6 RID: 230
	[DebugSpawnable(Name = "Tushkano", Category = "Mutants")]
	[Title("Tushkano")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/tushkano/tushkano.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class TushkanoNPC : NPCBase
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002AC73 File Offset: 0x00028E73
		public float AttackDamage
		{
			get
			{
				return 15f;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002AC7A File Offset: 0x00028E7A
		public float MaxGroupFormDistance
		{
			get
			{
				return 3000f;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002AC81 File Offset: 0x00028E81
		public virtual string GrowlSound
		{
			get
			{
				return "tushkano.growl";
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002AC88 File Offset: 0x00028E88
		public virtual string PanicSound
		{
			get
			{
				return "tushkano.panic";
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002AC8F File Offset: 0x00028E8F
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002AC96 File Offset: 0x00028E96
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002AC9D File Offset: 0x00028E9D
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 2.5f;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0002ACA4 File Offset: 0x00028EA4
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002ACA7 File Offset: 0x00028EA7
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0002ACAE File Offset: 0x00028EAE
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(26f, 12f);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002ACBF File Offset: 0x00028EBF
		protected override string NPCAssetID
		{
			get
			{
				return "tushkano";
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 15f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new BlindDogMovement(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new TushkanoIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new TushkanoChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FleeState = new TushkanoFleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new TushkanoMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindGroup();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002AD68 File Offset: 0x00028F68
		protected virtual void FindGroup()
		{
			foreach (TushkanoNPC rat in Entity.All.OfType<TushkanoNPC>())
			{
				if (this.Position.Distance(rat.Position) < this.MaxGroupFormDistance)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					rat.JoinGroup(this);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TushkanoGroup = rat.TushkanoGroup;
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TushkanoGroup = new TushkanoGroup();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TushkanoGroup.SetLeader(this);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002AE10 File Offset: 0x00029010
		public virtual void JoinGroup(NPCBase rat)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TushkanoGroup.AddMember(rat);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002AE23 File Offset: 0x00029023
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002AE3B File Offset: 0x0002903B
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002AE5C File Offset: 0x0002905C
		protected override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(NPCAnimParameters.MoveSpeed, this.Velocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(NPCAnimParameters.SpeedFraction, this.Velocity.WithZ(0f).Length / this.GetWishSpeed());
			RuntimeHelpers.EnsureSufficientExecutionStack();
			string turning = NPCAnimParameters.Turning;
			NPCMovementSlowTurn st = this.Movement as NPCMovementSlowTurn;
			base.SetAnimParameter(turning, st != null && st.IsStationaryTurning);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002AEF9 File Offset: 0x000290F9
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			TushkanoGroup tushkanoGroup = this.TushkanoGroup;
			if (tushkanoGroup != null)
			{
				tushkanoGroup.OnMemberDied(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x0400032B RID: 811
		public TushkanoIdleState IdleState;

		// Token: 0x0400032C RID: 812
		public TushkanoChaseState ChaseState;

		// Token: 0x0400032D RID: 813
		public TushkanoFleeState FleeState;

		// Token: 0x0400032E RID: 814
		public TushkanoMeleeState MeleeState;

		// Token: 0x0400032F RID: 815
		public TushkanoGroup TushkanoGroup;
	}
}
