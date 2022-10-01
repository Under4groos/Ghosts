using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP;

// Token: 0x0200000E RID: 14
[Spawnable]
[Library("weapon_fists", Title = "Fists")]
internal class Fists : Weapon
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000081 RID: 129 RVA: 0x00006F6D File Offset: 0x0000516D
	public override string ViewModelPath
	{
		get
		{
			return "models/first_person/first_person_arms.vmdl";
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000082 RID: 130 RVA: 0x00006F74 File Offset: 0x00005174
	public override float PrimaryRate
	{
		get
		{
			return 2f;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000083 RID: 131 RVA: 0x00006F7B File Offset: 0x0000517B
	public override float SecondaryRate
	{
		get
		{
			return 2f;
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00006F82 File Offset: 0x00005182
	public override bool CanReload()
	{
		return false;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00006F85 File Offset: 0x00005185
	public override bool CanPrimaryAttack()
	{
		return !(this.Owner as StalkerPlayer).StaminaComponent.IsRunning && base.CanPrimaryAttack();
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00006FA6 File Offset: 0x000051A6
	public override bool CanSecondaryAttack()
	{
		return !(this.Owner as StalkerPlayer).StaminaComponent.IsRunning && base.CanSecondaryAttack();
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00006FC8 File Offset: 0x000051C8
	private void Attack(bool leftHand)
	{
		if (this.MeleeAttack())
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnMeleeHit(leftHand);
		}
		else
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnMeleeMiss(leftHand);
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
		if (animatedEntity == null)
		{
			return;
		}
		animatedEntity.SetAnimParameter("b_attack", true);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00007017 File Offset: 0x00005217
	public override void AttackPrimary()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Attack(true);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00007025 File Offset: 0x00005225
	public override void AttackSecondary()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Attack(false);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00007033 File Offset: 0x00005233
	public override void OnCarryDrop(Entity dropper)
	{
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00007038 File Offset: 0x00005238
	public override void SimulateAnimator(StalkerRP.PawnAnimator anim)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		anim.SetAnimParameter("b_sprint", (this.Owner as StalkerPlayer).StaminaComponent.IsRunning);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		anim.SetAnimParameter("holdtype", 4);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		anim.SetAnimParameter("aim_body_weight", 1f);
		if (this.Owner.IsValid() && base.ViewModelEntity.IsValid())
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("b_grounded", this.Owner.GroundEntity.IsValid());
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("aim_pitch", this.Owner.EyeRotation.Pitch());
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("b_jump", anim.HasEvent("jump"));
			Vector3 dir = this.Owner.Velocity;
			float forward = this.Owner.Rotation.Forward.Dot(dir);
			float sideward = this.Owner.Rotation.Right.Dot(dir);
			float speed = dir.WithZ(0f).Length;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("move_groundspeed", (speed / 320f * 2f).Clamp(0f, 2f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("move_y", (sideward / 320f * 2f).Clamp(-2f, 2f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("move_x", (forward / 320f * 2f).Clamp(-2f, 2f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("move_z", (dir.z / 320f * 2f).Clamp(-2f, 2f));
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00007248 File Offset: 0x00005448
	public override void CreateViewModel()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		Host.AssertClient("CreateViewModel");
		if (string.IsNullOrEmpty(this.ViewModelPath))
		{
			return;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.ViewModelEntity = new ViewModel
		{
			Position = this.Position,
			Owner = this.Owner,
			EnableViewmodelRendering = true,
			EnableSwingAndBob = false
		};
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.ViewModelEntity.SetModel(this.ViewModelPath);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.ViewModelEntity.SetAnimGraph("models/first_person/first_person_arms_punching.vanmgrph");
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000072D4 File Offset: 0x000054D4
	private bool MeleeAttack()
	{
		Vector3 forward = this.Owner.EyeRotation.Forward;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		forward = forward.Normal;
		bool hit = false;
		foreach (TraceResult tr in this.TraceBullet(this.Owner.EyePosition, this.Owner.EyePosition + forward * 80f, 20f))
		{
			if (tr.Entity.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tr.Surface.DoBulletImpact(tr);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				hit = true;
				if (base.IsServer)
				{
					using (Prediction.Off())
					{
						DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPosition, forward * 100f, 25f).UsingTraceResult(tr).WithAttacker(this.Owner, null).WithWeapon(this);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						tr.Entity.TakeDamage(damageInfo);
					}
				}
			}
		}
		return hit;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000741C File Offset: 0x0000561C
	[ClientRpc]
	private void OnMeleeMiss(bool leftHand)
	{
		if (!this.OnMeleeMiss__RpcProxy(leftHand, null))
		{
			return;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		Host.AssertClient("OnMeleeMiss");
		RuntimeHelpers.EnsureSufficientExecutionStack();
		BaseViewModel viewModelEntity = base.ViewModelEntity;
		if (viewModelEntity != null)
		{
			viewModelEntity.SetAnimParameter("attack_has_hit", false);
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		BaseViewModel viewModelEntity2 = base.ViewModelEntity;
		if (viewModelEntity2 != null)
		{
			viewModelEntity2.SetAnimParameter("attack", true);
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		BaseViewModel viewModelEntity3 = base.ViewModelEntity;
		if (viewModelEntity3 == null)
		{
			return;
		}
		viewModelEntity3.SetAnimParameter("holdtype_attack", leftHand ? 2 : 1);
	}

	// Token: 0x0600008F RID: 143 RVA: 0x000074A4 File Offset: 0x000056A4
	[ClientRpc]
	private void OnMeleeHit(bool leftHand)
	{
		if (!this.OnMeleeHit__RpcProxy(leftHand, null))
		{
			return;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		Host.AssertClient("OnMeleeHit");
		RuntimeHelpers.EnsureSufficientExecutionStack();
		BaseViewModel viewModelEntity = base.ViewModelEntity;
		if (viewModelEntity != null)
		{
			viewModelEntity.SetAnimParameter("attack_has_hit", true);
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		BaseViewModel viewModelEntity2 = base.ViewModelEntity;
		if (viewModelEntity2 != null)
		{
			viewModelEntity2.SetAnimParameter("attack", true);
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		BaseViewModel viewModelEntity3 = base.ViewModelEntity;
		if (viewModelEntity3 == null)
		{
			return;
		}
		viewModelEntity3.SetAnimParameter("holdtype_attack", leftHand ? 2 : 1);
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000752C File Offset: 0x0000572C
	protected bool OnMeleeMiss__RpcProxy(bool leftHand, To? toTarget = null)
	{
		if (!Prediction.FirstTime)
		{
			return false;
		}
		if (!Host.IsServer)
		{
			Prediction.Watch("OnMeleeMiss", new object[]
			{
				leftHand
			});
			return true;
		}
		using (NetWrite writer = NetWrite.StartRpc(-134903247, this))
		{
			if (!NetRead.IsSupported(leftHand))
			{
				GlobalGameNamespace.Log.Error("[ClientRpc] OnMeleeMiss is not allowed to use Boolean for the parameter 'leftHand'!");
				return false;
			}
			writer.Write<bool>(leftHand);
			writer.SendRpc(toTarget, this);
		}
		return false;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x000075C0 File Offset: 0x000057C0
	protected bool OnMeleeHit__RpcProxy(bool leftHand, To? toTarget = null)
	{
		if (!Prediction.FirstTime)
		{
			return false;
		}
		if (!Host.IsServer)
		{
			Prediction.Watch("OnMeleeHit", new object[]
			{
				leftHand
			});
			return true;
		}
		using (NetWrite writer = NetWrite.StartRpc(1988073504, this))
		{
			if (!NetRead.IsSupported(leftHand))
			{
				GlobalGameNamespace.Log.Error("[ClientRpc] OnMeleeHit is not allowed to use Boolean for the parameter 'leftHand'!");
				return false;
			}
			writer.Write<bool>(leftHand);
			writer.SendRpc(toTarget, this);
		}
		return false;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00007654 File Offset: 0x00005854
	private void OnMeleeMiss(To toTarget, bool leftHand)
	{
		this.OnMeleeMiss__RpcProxy(leftHand, new To?(toTarget));
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00007664 File Offset: 0x00005864
	private void OnMeleeHit(To toTarget, bool leftHand)
	{
		this.OnMeleeHit__RpcProxy(leftHand, new To?(toTarget));
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00007674 File Offset: 0x00005874
	protected override void OnCallRemoteProcedure(int id, NetRead read)
	{
		if (id == -134903247)
		{
			bool __leftHand = false;
			__leftHand = read.ReadData<bool>(__leftHand);
			if (!Prediction.WasPredicted("OnMeleeMiss", new object[]
			{
				__leftHand
			}))
			{
				this.OnMeleeMiss(__leftHand);
			}
			return;
		}
		if (id != 1988073504)
		{
			base.OnCallRemoteProcedure(id, read);
			return;
		}
		bool __leftHand2 = false;
		__leftHand2 = read.ReadData<bool>(__leftHand2);
		if (!Prediction.WasPredicted("OnMeleeHit", new object[]
		{
			__leftHand2
		}))
		{
			this.OnMeleeHit(__leftHand2);
		}
	}
}
