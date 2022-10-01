using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000EB RID: 235
	[DebugSpawnable(Name = "Zombie", Category = "Mutants")]
	[Title("Zombie")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/zombie_anomaly/zombie_anomaly.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class ZombieNPC : NPCBase
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002B643 File Offset: 0x00029843
		public float FakeDeathTime
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002B64A File Offset: 0x0002984A
		public float AttackRange
		{
			get
			{
				return 70f;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002B651 File Offset: 0x00029851
		public float AttackDamage
		{
			get
			{
				return 35f;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002B658 File Offset: 0x00029858
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0002B660 File Offset: 0x00029860
		public bool Aggravated { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002B669 File Offset: 0x00029869
		protected string ReviveSound
		{
			get
			{
				return "zombie.revive";
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002B670 File Offset: 0x00029870
		protected string AlertSound
		{
			get
			{
				return "zombie.alert";
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002B677 File Offset: 0x00029877
		protected string AttackSound
		{
			get
			{
				return "zombie.attack";
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002B67E File Offset: 0x0002987E
		protected string HitSound
		{
			get
			{
				return "zombie.attack_hit";
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002B685 File Offset: 0x00029885
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 9f;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002B68C File Offset: 0x0002988C
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 14f;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002B693 File Offset: 0x00029893
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002B696 File Offset: 0x00029896
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0002B69D File Offset: 0x0002989D
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(70f, 11f);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002B6AE File Offset: 0x000298AE
		protected override string NPCAssetID
		{
			get
			{
				return "zombie";
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002B6B8 File Offset: 0x000298B8
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup(Rand.Int(base.MaterialGroupCount));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 52f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementSlowTurn(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new ZombieIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new ZombieChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FakeDeathState = new ZombieFakeDeathState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002B750 File Offset: 0x00029950
		protected override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			Vector3 dir = this.InputVelocity.Normal;
			float dot = this.Rotation.Left.Dot(dir);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot = MathF.Acos(dot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot /= 3.1415927f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot *= 2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot -= 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter("fTargetDirectionDotProduct", base.GetAnimParameterFloat("fTargetDirectionDotProduct").LerpTo(dot, Time.Delta * 10f, true));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter("fMoveSpeed", this.Velocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			string name = "bTurning";
			NPCMovementSlowTurn npcmovementSlowTurn = this.Movement as NPCMovementSlowTurn;
			base.SetAnimParameter(name, npcmovementSlowTurn != null && npcmovementSlowTurn.IsStationaryTurning);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002B842 File Offset: 0x00029A42
		public override float GetWishSpeed()
		{
			if (!this.Aggravated)
			{
				return base.Resource.CrippledSpeed;
			}
			return base.Resource.RunSpeed;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002B863 File Offset: 0x00029A63
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002B87C File Offset: 0x00029A7C
		[ClientRpc]
		public void PlayReviveSound()
		{
			if (!this.PlayReviveSound__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.ReviveSound, this);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002B8B0 File Offset: 0x00029AB0
		[ClientRpc]
		public void PlayAlertSound()
		{
			if (!this.PlayAlertSound__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.AlertSound, this);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		[ClientRpc]
		public void PlayAttackSound()
		{
			if (!this.PlayAttackSound__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.AttackSound, this);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002B918 File Offset: 0x00029B18
		[ClientRpc]
		public void PlayAttackHitSound(Vector3 pos)
		{
			if (!this.PlayAttackHitSound__RpcProxy(pos, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld(this.HitSound, pos);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002B94C File Offset: 0x00029B4C
		protected bool PlayReviveSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("PlayReviveSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-476149282, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002B9AC File Offset: 0x00029BAC
		protected bool PlayAlertSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("PlayAlertSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(828098307, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002BA0C File Offset: 0x00029C0C
		protected bool PlayAttackSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("PlayAttackSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1422380701, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002BA6C File Offset: 0x00029C6C
		protected bool PlayAttackHitSound__RpcProxy(Vector3 pos, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("PlayAttackHitSound", new object[]
				{
					pos
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-181397838, this))
			{
				if (!NetRead.IsSupported(pos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] PlayAttackHitSound is not allowed to use Vector3 for the parameter 'pos'!");
					return false;
				}
				writer.Write<Vector3>(pos);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002BB00 File Offset: 0x00029D00
		public void PlayReviveSound(To toTarget)
		{
			this.PlayReviveSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002BB0F File Offset: 0x00029D0F
		public void PlayAlertSound(To toTarget)
		{
			this.PlayAlertSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002BB1E File Offset: 0x00029D1E
		public void PlayAttackSound(To toTarget)
		{
			this.PlayAttackSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002BB2D File Offset: 0x00029D2D
		public void PlayAttackHitSound(To toTarget, Vector3 pos)
		{
			this.PlayAttackHitSound__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002BB40 File Offset: 0x00029D40
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= -181397838)
			{
				if (id == -476149282)
				{
					if (!Prediction.WasPredicted("PlayReviveSound", Array.Empty<object>()))
					{
						this.PlayReviveSound();
					}
					return;
				}
				if (id == -181397838)
				{
					Vector3 __pos = read.ReadData<Vector3>(default(Vector3));
					if (!Prediction.WasPredicted("PlayAttackHitSound", new object[]
					{
						__pos
					}))
					{
						this.PlayAttackHitSound(__pos);
					}
					return;
				}
			}
			else
			{
				if (id == 828098307)
				{
					if (!Prediction.WasPredicted("PlayAlertSound", Array.Empty<object>()))
					{
						this.PlayAlertSound();
					}
					return;
				}
				if (id == 1422380701)
				{
					if (!Prediction.WasPredicted("PlayAttackSound", Array.Empty<object>()))
					{
						this.PlayAttackSound();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x04000337 RID: 823
		public ZombieIdleState IdleState;

		// Token: 0x04000338 RID: 824
		public ZombieChaseState ChaseState;

		// Token: 0x04000339 RID: 825
		public ZombieFakeDeathState FakeDeathState;
	}
}
