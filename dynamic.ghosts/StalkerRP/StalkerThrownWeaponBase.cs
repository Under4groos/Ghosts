using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x0200005B RID: 91
	public class StalkerThrownWeaponBase : StalkerWeaponBase
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00013D9D File Offset: 0x00011F9D
		[Description("This is the minimum amount of time a throw must be held for. Basically, it wont do the release animation until this much time has passed.")]
		public virtual float ThrowMinimumHoldTime
		{
			get
			{
				return 0.4f;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00013DA4 File Offset: 0x00011FA4
		[Description("The delay in seconds after the player releases the throw the weapon waits until the projectile is spawned.")]
		public virtual float ThrowReleaseProjectileDelay
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00013DAB File Offset: 0x00011FAB
		public virtual string ProjectileName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00013DB2 File Offset: 0x00011FB2
		[Description("How much force the projectile is thrown with.")]
		public virtual float ThrowVelocity
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00013DB9 File Offset: 0x00011FB9
		[Description("Extra force added to the Z value of the throw velocity. Nice for making arcs, 0 by default.")]
		public virtual float ThrowUpVelocity
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public virtual Vector3 ThrowAngularVelocity
		{
			get
			{
				return Vector3.Zero;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00013DC7 File Offset: 0x00011FC7
		public virtual Angles ThrowStartingAngles
		{
			get
			{
				return Angles.Zero;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00013DD0 File Offset: 0x00011FD0
		[Description("The position the projectile should start at when thrown.")]
		public virtual Vector3 ThrowOrigin
		{
			get
			{
				return this.Owner.EyePosition + this.Owner.EyeRotation.Forward * 100f;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00013E0A File Offset: 0x0001200A
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00013E18 File Offset: 0x00012018
		[Net]
		[Predicted]
		public unsafe bool IsThrowing
		{
			get
			{
				return *this._repback__IsThrowing.GetValue();
			}
			set
			{
				this._repback__IsThrowing.SetValue(value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00013E27 File Offset: 0x00012027
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00013E35 File Offset: 0x00012035
		[Net]
		[Predicted]
		public unsafe bool IsFinishingThrow
		{
			get
			{
				return *this._repback__IsFinishingThrow.GetValue();
			}
			set
			{
				this._repback__IsFinishingThrow.SetValue(value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00013E44 File Offset: 0x00012044
		protected override bool IsRunning
		{
			get
			{
				return base.IsRunning && !this.IsThrowing && !this.IsFinishingThrow;
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00013E61 File Offset: 0x00012061
		public override void Deploy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Deploy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity == null)
			{
				return;
			}
			animatedEntity.SetAnimParameter("holdtype_attack", 1);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013E8E File Offset: 0x0001208E
		public override void Holster()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Holster();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity == null)
			{
				return;
			}
			animatedEntity.SetAnimParameter("holdtype_attack", 0);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00013EBB File Offset: 0x000120BB
		public override void ActiveStart(Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ActiveStart(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsThrowing = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsFinishingThrow = false;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00013EE1 File Offset: 0x000120E1
		public override bool CanReload()
		{
			return false;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00013EE4 File Offset: 0x000120E4
		public virtual bool CanStartThrow()
		{
			if (this.IsThrowing)
			{
				return false;
			}
			if (this.IsFinishingThrow)
			{
				return false;
			}
			if (this.IsRunning)
			{
				return false;
			}
			if (!this.Owner.IsValid())
			{
				return false;
			}
			float rate = this.PrimaryRate;
			return rate <= 0f || (!(base.Resource.Automatic ? (!Input.Down(InputButton.PrimaryAttack)) : (!Input.Pressed(InputButton.PrimaryAttack))) && this.TimeSinceThrowReleased >= 1f / rate);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013F75 File Offset: 0x00012175
		public virtual bool CanReleaseThrow()
		{
			return this.IsThrowing && this.TimeSinceThrowStarted >= this.ThrowMinimumHoldTime && !Input.Down(InputButton.PrimaryAttack);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013FA4 File Offset: 0x000121A4
		public virtual void StartThrow()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsThrowing = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("bStartThrow", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity != null)
			{
				animatedEntity.SetAnimParameter("b_attack", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceThrowStarted = 0f;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00014010 File Offset: 0x00012210
		public virtual void ReleaseThrow()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsThrowing = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsFinishingThrow = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("bReleaseThrow", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity != null)
			{
				animatedEntity.SetAnimParameter("b_attack", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceThrowReleased = 0f;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00014087 File Offset: 0x00012287
		public virtual bool CanFinishThrow()
		{
			return this.IsFinishingThrow && this.TimeSinceThrowReleased > this.ThrowReleaseProjectileDelay;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000140A8 File Offset: 0x000122A8
		[Description("Throws the actual projectile. Serverside only.")]
		public virtual void ThrowProjectile()
		{
			StalkerProjectileBase stalkerProjectileBase = GlobalGameNamespace.TypeLibrary.Create<StalkerProjectileBase>(this.ProjectileName, true);
			Vector3 dir = this.Owner.EyeRotation.Forward;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			stalkerProjectileBase.Position = this.ThrowOrigin;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			stalkerProjectileBase.Velocity = dir * this.ThrowVelocity + Vector3.Up * this.ThrowUpVelocity + this.Owner.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			stalkerProjectileBase.PhysicsBody.AngularVelocity = this.ThrowAngularVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			stalkerProjectileBase.Rotation = this.ThrowStartingAngles.ToRotation();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			stalkerProjectileBase.Owner = this.Owner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			stalkerProjectileBase.OnThrown();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00014174 File Offset: 0x00012374
		public override void Simulate(Client owner)
		{
			if (this.IsLocalPawn && base.ViewModelEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bRunning", this.IsRunning);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bMoving", this.Owner.Velocity.Length > 10f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bCrouching", Input.Down(InputButton.Duck));
			}
			if (this.CanFinishThrow())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsFinishingThrow = false;
				if (base.IsServer)
				{
					this.ThrowProjectile();
				}
				return;
			}
			if (this.CanStartThrow())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StartThrow();
			}
			if (this.CanReleaseThrow())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ReleaseThrow();
			}
			if (!this.Owner.IsValid())
			{
				return;
			}
			if (this.CanSecondaryAttack())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AttackSecondary();
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00014262 File Offset: 0x00012462
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsThrowing, "IsThrowing", true, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsFinishingThrow, "IsFinishingThrow", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0400011E RID: 286
		private TimeSince TimeSinceThrowReleased = 0f;

		// Token: 0x0400011F RID: 287
		private TimeSince TimeSinceThrowStarted = 0f;

		// Token: 0x04000120 RID: 288
		private VarUnmanaged<bool> _repback__IsThrowing = new VarUnmanaged<bool>();

		// Token: 0x04000121 RID: 289
		private VarUnmanaged<bool> _repback__IsFinishingThrow = new VarUnmanaged<bool>();
	}
}
