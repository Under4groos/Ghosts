using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.UI;

namespace StalkerRP
{
	// Token: 0x02000040 RID: 64
	public class StaminaComponent : EntityComponent<StalkerPlayer>
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000EDA3 File Offset: 0x0000CFA3
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000EDB1 File Offset: 0x0000CFB1
		[Net]
		[Local]
		[Predicted]
		public unsafe float Stamina
		{
			get
			{
				return *this._repback__Stamina.GetValue();
			}
			set
			{
				this._repback__Stamina.SetValue(value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000EDCE File Offset: 0x0000CFCE
		[Net]
		[Local]
		[DefaultValue(1)]
		public unsafe float MaxStaminaMultiplier
		{
			get
			{
				return *this._repback__MaxStaminaMultiplier.GetValue();
			}
			set
			{
				this._repback__MaxStaminaMultiplier.SetValue(value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000EDDD File Offset: 0x0000CFDD
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000EDEF File Offset: 0x0000CFEF
		[Net]
		[Local]
		[Predicted]
		public unsafe TimeSince TimeSinceStartedSprinting
		{
			get
			{
				return *this._repback__TimeSinceStartedSprinting.GetValue();
			}
			set
			{
				this._repback__TimeSinceStartedSprinting.SetValue(value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000EDFE File Offset: 0x0000CFFE
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000EE10 File Offset: 0x0000D010
		[Net]
		[Local]
		[Predicted]
		public unsafe TimeSince TimeSinceStoppedSprinting
		{
			get
			{
				return *this._repback__TimeSinceStoppedSprinting.GetValue();
			}
			set
			{
				this._repback__TimeSinceStoppedSprinting.SetValue(value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000EE1F File Offset: 0x0000D01F
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000EE31 File Offset: 0x0000D031
		[Net]
		[Local]
		[Predicted]
		private unsafe TimeSince TimeSinceStaminaDecreased
		{
			get
			{
				return *this._repback__TimeSinceStaminaDecreased.GetValue();
			}
			set
			{
				this._repback__TimeSinceStaminaDecreased.SetValue(value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000EE40 File Offset: 0x0000D040
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000EE4E File Offset: 0x0000D04E
		[Net]
		[Predicted]
		public unsafe bool IsRunning
		{
			get
			{
				return *this._repback__IsRunning.GetValue();
			}
			set
			{
				this._repback__IsRunning.SetValue(value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000EE5D File Offset: 0x0000D05D
		public float Fraction
		{
			get
			{
				return this.Stamina.LerpInverse(0f, this.MaxStamina, true);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000EE78 File Offset: 0x0000D078
		public float MaxStamina
		{
			get
			{
				return base.Entity.Stats.Stamina.Max * this.MaxStaminaMultiplier;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		protected override void OnActivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stamina = base.Entity.Stats.Stamina.Max;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnActivate();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		public void Reset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MaxStaminaMultiplier = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stamina = base.Entity.Stats.Stamina.Max;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceStaminaDecreased = 0f;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EF38 File Offset: 0x0000D138
		public void Simulate(Client client)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateRunning();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateStamina();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stamina = this.Stamina.Clamp(0f, this.MaxStamina);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StaminaBar instance = StaminaBar.Instance;
			if (instance != null)
			{
				instance.SetFraction(this.Fraction);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StaminaBar instance2 = StaminaBar.Instance;
			if (instance2 == null)
			{
				return;
			}
			instance2.SetBrokenFraction(1f - this.MaxStaminaMultiplier);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000EFB7 File Offset: 0x0000D1B7
		public void AddStaminaMultiplier(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MaxStaminaMultiplier += n;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		private float secondWindForgivenessWindow
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		private void UpdateRunning()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.oldIsRunning = this.IsRunning;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsRunning = (Input.Down(InputButton.Run) && this.CanRun() && base.Entity.Velocity.Length > 200f);
			if (!this.IsRunning || this.oldIsRunning == this.IsRunning)
			{
				if (!this.IsRunning && this.oldIsRunning != this.IsRunning)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TimeSinceStoppedSprinting = 0f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.oldTimeSinceStartedSprintingTime = this.TimeSinceStartedSprinting.Relative;
				}
				return;
			}
			if (this.TimeSinceStoppedSprinting < this.secondWindForgivenessWindow)
			{
				this.TimeSinceStartedSprinting = this.oldTimeSinceStartedSprintingTime;
				return;
			}
			this.TimeSinceStartedSprinting = 0f;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		private void UpdateStamina()
		{
			if (this.IsRunning)
			{
				if (this.TimeSinceStartedSprinting < base.Entity.Stats.Stamina.SecondWindDelay)
				{
					this.Stamina -= base.Entity.Stats.Stamina.SprintCost * Time.Delta;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceStaminaDecreased = 0f;
				return;
			}
			if (this.TimeSinceStaminaDecreased > base.Entity.Stats.Stamina.RechargeDelay)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Stamina += base.Entity.Stats.Stamina.RechargeRate * Time.Delta;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F18C File Offset: 0x0000D38C
		public bool CanRun()
		{
			return (this.IsRunning || (double)this.Fraction >= 0.1) && this.Stamina > 0f && base.Entity.GroundEntity != null;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		public bool CanJump()
		{
			return this.Stamina >= base.Entity.Stats.Stamina.JumpCost;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public void Jump()
		{
			if (base.Entity.Stats.Movement.AutoJump)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stamina -= base.Entity.Stats.Stamina.JumpCost;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceStaminaDecreased = 0f;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000F260 File Offset: 0x0000D460
		public void OnTakeDamage(DamageInfo info)
		{
			if (info.Flags.HasFlag(DamageFlags.Direct))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stamina -= info.Damage * base.Entity.Stats.Stamina.DamageToStaminaLossRatio;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceStaminaDecreased = 0f;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<float>>(ref this._repback__Stamina, "Stamina", true, true);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MaxStaminaMultiplier, "MaxStaminaMultiplier", false, true);
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceStartedSprinting, "TimeSinceStartedSprinting", true, true);
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceStoppedSprinting, "TimeSinceStoppedSprinting", true, true);
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceStaminaDecreased, "TimeSinceStaminaDecreased", true, true);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsRunning, "IsRunning", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040000CF RID: 207
		private float oldTimeSinceStartedSprintingTime;

		// Token: 0x040000D0 RID: 208
		private bool oldIsRunning;

		// Token: 0x040000D1 RID: 209
		private VarUnmanaged<float> _repback__Stamina = new VarUnmanaged<float>();

		// Token: 0x040000D2 RID: 210
		private VarUnmanaged<float> _repback__MaxStaminaMultiplier = new VarUnmanaged<float>(1f);

		// Token: 0x040000D3 RID: 211
		private VarUnmanaged<TimeSince> _repback__TimeSinceStartedSprinting = new VarUnmanaged<TimeSince>();

		// Token: 0x040000D4 RID: 212
		private VarUnmanaged<TimeSince> _repback__TimeSinceStoppedSprinting = new VarUnmanaged<TimeSince>();

		// Token: 0x040000D5 RID: 213
		private VarUnmanaged<TimeSince> _repback__TimeSinceStaminaDecreased = new VarUnmanaged<TimeSince>();

		// Token: 0x040000D6 RID: 214
		private VarUnmanaged<bool> _repback__IsRunning = new VarUnmanaged<bool>();
	}
}
