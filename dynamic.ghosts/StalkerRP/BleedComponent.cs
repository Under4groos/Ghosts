using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x0200001B RID: 27
	public class BleedComponent : EntityComponent, ISingletonComponent
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00008C7A File Offset: 0x00006E7A
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00008C88 File Offset: 0x00006E88
		[Net]
		[DefaultValue(0)]
		[Description("How much health is lost per second.")]
		public unsafe float HealthLossRate
		{
			get
			{
				return *this._repback__HealthLossRate.GetValue();
			}
			set
			{
				this._repback__HealthLossRate.SetValue(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00008C97 File Offset: 0x00006E97
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00008CA5 File Offset: 0x00006EA5
		[Net]
		[DefaultValue(0.05f)]
		[Description("The rate at which bleeding naturally slows down per second.")]
		public unsafe float HealRate
		{
			get
			{
				return *this._repback__HealRate.GetValue();
			}
			set
			{
				this._repback__HealRate.SetValue(value);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public void Reset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthLossRate = 0f;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008CC8 File Offset: 0x00006EC8
		[Description("Update how much the entity is bleeding from the damage info. A portion of the damage will be converted into health loss.")]
		public void UpdateBleedFromDamage(DamageInfo info)
		{
			if (info.Damage <= 0f)
			{
				return;
			}
			float mult = 0f;
			if (this.damageFlagBleedMultipliers.ContainsKey(info.Flags))
			{
				mult = this.damageFlagBleedMultipliers[info.Flags];
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthLossRate += mult * info.Damage;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008D2C File Offset: 0x00006F2C
		[Event.Tick.ServerAttribute]
		private void Update()
		{
			if (!this.Enabled || this.HealthLossRate <= 0f || base.Entity.LifeState != LifeState.Alive)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Entity.Health -= this.HealthLossRate * Time.Delta;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthLossRate = MathF.Max(this.HealthLossRate - this.HealRate * Time.Delta, 0f);
			if (base.Entity.Health <= 0f)
			{
				base.Entity.OnKilled();
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<float>>(ref this._repback__HealthLossRate, "HealthLossRate", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__HealRate, "HealRate", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000056 RID: 86
		private Dictionary<DamageFlags, float> damageFlagBleedMultipliers = new Dictionary<DamageFlags, float>
		{
			{
				DamageFlags.Buckshot,
				0.008f
			},
			{
				DamageFlags.Bullet,
				0.015f
			},
			{
				DamageFlags.Blunt,
				0.002f
			},
			{
				DamageFlags.Slash,
				0.025f
			},
			{
				DamageFlags.Blast,
				0.01f
			}
		};

		// Token: 0x04000057 RID: 87
		private VarUnmanaged<float> _repback__HealthLossRate = new VarUnmanaged<float>(0f);

		// Token: 0x04000058 RID: 88
		private VarUnmanaged<float> _repback__HealRate = new VarUnmanaged<float>(0.05f);
	}
}
