using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x02000037 RID: 55
	public class HealthComponent : EntityComponent<StalkerPlayer>
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000DB5D File Offset: 0x0000BD5D
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000DB6A File Offset: 0x0000BD6A
		[Net]
		[Local]
		public IList<LimbBase> Limbs
		{
			get
			{
				return this._repback__Limbs.GetValue();
			}
			set
			{
				this._repback__Limbs.SetValue(value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000DB78 File Offset: 0x0000BD78
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000DB85 File Offset: 0x0000BD85
		[Net]
		[Local]
		public LimbHead Head
		{
			get
			{
				return this._repback__Head.GetValue();
			}
			set
			{
				this._repback__Head.SetValue(value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000DB93 File Offset: 0x0000BD93
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		[Net]
		[Local]
		public LimbTorso Torso
		{
			get
			{
				return this._repback__Torso.GetValue();
			}
			set
			{
				this._repback__Torso.SetValue(value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000DBAE File Offset: 0x0000BDAE
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000DBBB File Offset: 0x0000BDBB
		[Net]
		[Local]
		public LimbStomach Stomach
		{
			get
			{
				return this._repback__Stomach.GetValue();
			}
			set
			{
				this._repback__Stomach.SetValue(value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000DBC9 File Offset: 0x0000BDC9
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000DBD6 File Offset: 0x0000BDD6
		[Net]
		[Local]
		public LimbArm ArmLeft
		{
			get
			{
				return this._repback__ArmLeft.GetValue();
			}
			set
			{
				this._repback__ArmLeft.SetValue(value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		[Net]
		[Local]
		public LimbArm ArmRight
		{
			get
			{
				return this._repback__ArmRight.GetValue();
			}
			set
			{
				this._repback__ArmRight.SetValue(value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		[Net]
		[Local]
		public LimbLeg LegLeft
		{
			get
			{
				return this._repback__LegLeft.GetValue();
			}
			set
			{
				this._repback__LegLeft.SetValue(value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000DC1A File Offset: 0x0000BE1A
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000DC27 File Offset: 0x0000BE27
		[Net]
		[Local]
		public LimbLeg LegRight
		{
			get
			{
				return this._repback__LegRight.GetValue();
			}
			set
			{
				this._repback__LegRight.SetValue(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000DC35 File Offset: 0x0000BE35
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000DC43 File Offset: 0x0000BE43
		[Net]
		[Local]
		private unsafe float TotalHealth
		{
			get
			{
				return *this._repback__TotalHealth.GetValue();
			}
			set
			{
				this._repback__TotalHealth.SetValue(value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000DC52 File Offset: 0x0000BE52
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000DC60 File Offset: 0x0000BE60
		[Net]
		[Local]
		[Change(null)]
		private unsafe float CurrentHealth
		{
			get
			{
				return *this._repback__CurrentHealth.GetValue();
			}
			set
			{
				this._repback__CurrentHealth.SetValue(value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000DC6F File Offset: 0x0000BE6F
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000DC81 File Offset: 0x0000BE81
		[Net]
		[Local]
		[DefaultValue(0)]
		public unsafe TimeSince TimeSinceLastDamaged
		{
			get
			{
				return *this._repback__TimeSinceLastDamaged.GetValue();
			}
			set
			{
				this._repback__TimeSinceLastDamaged.SetValue(value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000DC90 File Offset: 0x0000BE90
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000DC9E File Offset: 0x0000BE9E
		[Net]
		[Local]
		public unsafe int BrokenLegs
		{
			get
			{
				return *this._repback__BrokenLegs.GetValue();
			}
			set
			{
				this._repback__BrokenLegs.SetValue(value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000DCAD File Offset: 0x0000BEAD
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000DCBB File Offset: 0x0000BEBB
		[Net]
		[Local]
		public unsafe int BrokenArms
		{
			get
			{
				return *this._repback__BrokenArms.GetValue();
			}
			set
			{
				this._repback__BrokenArms.SetValue(value);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000DCCA File Offset: 0x0000BECA
		protected override void OnActivate()
		{
			if (!this.initialised)
			{
				this.Initialise();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		private void Initialise()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.initialised = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Head = new LimbHead();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Head.Initialise(this, base.Entity, base.Entity.Stats.Health.Head);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Limbs.Add(this.Head);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Torso = new LimbTorso();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Torso.Initialise(this, base.Entity, base.Entity.Stats.Health.Torso);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Limbs.Add(this.Torso);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stomach = new LimbStomach();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stomach.Initialise(this, base.Entity, base.Entity.Stats.Health.Stomach);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Limbs.Add(this.Stomach);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ArmLeft = new LimbArm();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ArmLeft.Initialise(this, base.Entity, base.Entity.Stats.Health.Arm);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Limbs.Add(this.ArmLeft);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ArmRight = new LimbArm();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ArmRight.Initialise(this, base.Entity, base.Entity.Stats.Health.Arm);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Limbs.Add(this.ArmRight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LegLeft = new LimbLeg();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LegLeft.Initialise(this, base.Entity, base.Entity.Stats.Health.Leg);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Limbs.Add(this.LegLeft);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LegRight = new LimbLeg();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LegRight.Initialise(this, base.Entity, base.Entity.Stats.Health.Leg);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Limbs.Add(this.LegRight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TotalHealth = this.Limbs.Sum((LimbBase x) => x.MaxHealth);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentHealth = this.TotalHealth;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000DF90 File Offset: 0x0000C190
		public void Simulate(Client client)
		{
			using (Prediction.Off())
			{
				if (base.Entity.IsServer)
				{
					this.UpdateSprintDamage();
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		private float sprintDamageInterval
		{
			get
			{
				return 4f;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		private void UpdateSprintDamage()
		{
			if (this.BrokenLegs <= 0)
			{
				return;
			}
			if (base.Entity.StaminaComponent.IsRunning)
			{
				this.timeSpentSprinting += Time.Delta;
			}
			if (this.timeSpentSprinting > this.sprintDamageInterval)
			{
				DamageInfo damage = DamageInfo.Generic(base.Entity.Stats.Health.SprintBrokenDamage * (float)this.BrokenLegs * this.sprintDamageInterval);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				damage.WithFlag(DamageFlags.Cook);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				damage.WithFlag(DamageFlags.Direct);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Entity.TakeDamage(damage);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSpentSprinting = 0f;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E098 File Offset: 0x0000C298
		public LimbBase GetLimbFromHitGroup(HitGroup group)
		{
			LimbBase result;
			switch (group)
			{
			case HitGroup.Head:
				result = this.Head;
				break;
			case HitGroup.Chest:
				result = this.Torso;
				break;
			case HitGroup.Stomach:
				result = this.Stomach;
				break;
			case HitGroup.LeftArm:
				result = this.ArmLeft;
				break;
			case HitGroup.RightArm:
				result = this.ArmRight;
				break;
			case HitGroup.LeftLeg:
				result = this.LegLeft;
				break;
			case HitGroup.RightLeg:
				result = this.LegRight;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E110 File Offset: 0x0000C310
		public void TakeDamage(DamageInfo info)
		{
			HitGroup hitGroup = (HitGroup)base.Entity.GetHitboxGroup(info.HitboxIndex);
			if (info.Flags.HasFlag(DamageFlags.Cook))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TakeFullBodyDamage(info.Damage);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				LimbBase limbFromHitGroup = this.GetLimbFromHitGroup(hitGroup);
				if (limbFromHitGroup != null)
				{
					limbFromHitGroup.TakeDamage(info);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastDamaged = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateCurrentHealth();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000E19C File Offset: 0x0000C39C
		public void OnLegBroken()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			int brokenLegs = this.BrokenLegs;
			this.BrokenLegs = brokenLegs + 1;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public void OnArmBroken()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			int brokenArms = this.BrokenArms;
			this.BrokenArms = brokenArms + 1;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		private float swayMagnitude
		{
			get
			{
				return 0.75f;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000E1E9 File Offset: 0x0000C3E9
		private float swayFrequency
		{
			get
			{
				return 9f;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public Rotation GetArmsSway()
		{
			float noiseY = Noise.Simplex(Time.Now * this.swayFrequency * (float)this.BrokenArms) - 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			noiseY *= 2f;
			float num = Noise.Simplex(0f, Time.Now * this.swayFrequency * (float)this.BrokenArms) - 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Rotation rot = Rotation.From(num * 2f * this.swayMagnitude * (float)this.BrokenArms, noiseY * this.swayMagnitude * (float)this.BrokenArms, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.swayRot = Rotation.Slerp(this.swayRot, rot, Time.Delta * 7f, true);
			return rot;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		public float GetRecoilMultiplier()
		{
			if (this.BrokenArms <= 0)
			{
				return 1f;
			}
			return 1f * base.Entity.Stats.Health.BrokenArmRecoilMultiplier;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public void TakeFullBodyDamage(float amount)
		{
			List<LimbBase> unbrokenLimbs = new List<LimbBase>();
			float unbrokenMaxHealth = 0f;
			foreach (LimbBase limb in this.Limbs)
			{
				if (!limb.IsBroken)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					unbrokenLimbs.Add(limb);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					unbrokenMaxHealth += limb.MaxHealth;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			unbrokenLimbs.ForEach(delegate(LimbBase x)
			{
				float limbDamage = amount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				limbDamage *= x.MaxHealth / unbrokenMaxHealth;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				x.TakeDamage(limbDamage);
			});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000E38C File Offset: 0x0000C58C
		public void Reset()
		{
			foreach (LimbBase limbBase in this.Limbs)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				limbBase.Reset();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSpentSprinting = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BrokenLegs = 0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BrokenArms = 0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateCurrentHealth();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000E410 File Offset: 0x0000C610
		public float GetEffectiveHealthFraction()
		{
			return new float[]
			{
				this.CurrentHealth / this.TotalHealth,
				this.Torso.Health / this.Torso.MaxHealth,
				this.Head.Health / this.Head.MaxHealth
			}.Min();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000E46C File Offset: 0x0000C66C
		private void UpdateCurrentHealth()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentHealth = 0f;
			foreach (LimbBase limb in this.Limbs)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CurrentHealth += limb.Health;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Entity.SetHealthFraction(To.Single(base.Entity.Client), this.GetEffectiveHealthFraction());
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000E500 File Offset: 0x0000C700
		private void OnCurrentHealthChanged(float oldHealth, float newHealth)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000E504 File Offset: 0x0000C704
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarEmbedList<LimbBase>>(ref this._repback__Limbs, "Limbs", false, true);
			builder.Register<VarClass<LimbHead>>(ref this._repback__Head, "Head", false, true);
			builder.Register<VarClass<LimbTorso>>(ref this._repback__Torso, "Torso", false, true);
			builder.Register<VarClass<LimbStomach>>(ref this._repback__Stomach, "Stomach", false, true);
			builder.Register<VarClass<LimbArm>>(ref this._repback__ArmLeft, "ArmLeft", false, true);
			builder.Register<VarClass<LimbArm>>(ref this._repback__ArmRight, "ArmRight", false, true);
			builder.Register<VarClass<LimbLeg>>(ref this._repback__LegLeft, "LegLeft", false, true);
			builder.Register<VarClass<LimbLeg>>(ref this._repback__LegRight, "LegRight", false, true);
			builder.Register<VarUnmanaged<float>>(ref this._repback__TotalHealth, "TotalHealth", false, true);
			builder.Register<VarUnmanaged<float>>(ref this._repback__CurrentHealth, "CurrentHealth", false, true);
			this._repback__CurrentHealth.SetCallback<float>(new Action<float, float>(this.OnCurrentHealthChanged));
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceLastDamaged, "TimeSinceLastDamaged", false, true);
			builder.Register<VarUnmanaged<int>>(ref this._repback__BrokenLegs, "BrokenLegs", false, true);
			builder.Register<VarUnmanaged<int>>(ref this._repback__BrokenArms, "BrokenArms", false, true);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040000B5 RID: 181
		private Sound heartbeat;

		// Token: 0x040000B6 RID: 182
		private bool initialised;

		// Token: 0x040000B7 RID: 183
		private float timeSpentSprinting;

		// Token: 0x040000B8 RID: 184
		private Rotation swayRot;

		// Token: 0x040000B9 RID: 185
		private VarEmbedList<LimbBase> _repback__Limbs = new VarEmbedList<LimbBase>(new List<LimbBase>());

		// Token: 0x040000BA RID: 186
		private VarClass<LimbHead> _repback__Head = new VarClass<LimbHead>();

		// Token: 0x040000BB RID: 187
		private VarClass<LimbTorso> _repback__Torso = new VarClass<LimbTorso>();

		// Token: 0x040000BC RID: 188
		private VarClass<LimbStomach> _repback__Stomach = new VarClass<LimbStomach>();

		// Token: 0x040000BD RID: 189
		private VarClass<LimbArm> _repback__ArmLeft = new VarClass<LimbArm>();

		// Token: 0x040000BE RID: 190
		private VarClass<LimbArm> _repback__ArmRight = new VarClass<LimbArm>();

		// Token: 0x040000BF RID: 191
		private VarClass<LimbLeg> _repback__LegLeft = new VarClass<LimbLeg>();

		// Token: 0x040000C0 RID: 192
		private VarClass<LimbLeg> _repback__LegRight = new VarClass<LimbLeg>();

		// Token: 0x040000C1 RID: 193
		private VarUnmanaged<float> _repback__TotalHealth = new VarUnmanaged<float>();

		// Token: 0x040000C2 RID: 194
		private VarUnmanaged<float> _repback__CurrentHealth = new VarUnmanaged<float>();

		// Token: 0x040000C3 RID: 195
		private VarUnmanaged<TimeSince> _repback__TimeSinceLastDamaged = new VarUnmanaged<TimeSince>(0f);

		// Token: 0x040000C4 RID: 196
		private VarUnmanaged<int> _repback__BrokenLegs = new VarUnmanaged<int>();

		// Token: 0x040000C5 RID: 197
		private VarUnmanaged<int> _repback__BrokenArms = new VarUnmanaged<int>();
	}
}
