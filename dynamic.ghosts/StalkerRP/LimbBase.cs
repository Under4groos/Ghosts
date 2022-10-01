using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x0200003A RID: 58
	public class LimbBase : BaseNetworkable
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000E93D File Offset: 0x0000CB3D
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000E945 File Offset: 0x0000CB45
		public virtual float MaxHealth { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000E94E File Offset: 0x0000CB4E
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000E956 File Offset: 0x0000CB56
		[DefaultValue(1)]
		protected virtual float SpillOverDamageMultiplier { get; set; } = 1f;

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000E95F File Offset: 0x0000CB5F
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000E978 File Offset: 0x0000CB78
		[Net]
		[Local]
		public unsafe StalkerPlayer Host
		{
			get
			{
				return *this._repback__Host.GetValue();
			}
			set
			{
				VarUnmanaged<EntityHandle<StalkerPlayer>> repback__Host = this._repback__Host;
				EntityHandle<StalkerPlayer> entityHandle = value;
				repback__Host.SetValue(entityHandle);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000E999 File Offset: 0x0000CB99
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000E9A7 File Offset: 0x0000CBA7
		[Net]
		[Local]
		[Change(null)]
		public unsafe float Health
		{
			get
			{
				return *this._repback__Health.GetValue();
			}
			set
			{
				this._repback__Health.SetValue(value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000E9B6 File Offset: 0x0000CBB6
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		[Net]
		[Local]
		public unsafe bool IsBroken
		{
			get
			{
				return *this._repback__IsBroken.GetValue();
			}
			set
			{
				this._repback__IsBroken.SetValue(value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000E9D3 File Offset: 0x0000CBD3
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000E9DB File Offset: 0x0000CBDB
		public HealthComponent Container { get; set; }

		// Token: 0x0600024D RID: 589 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public void Initialise(HealthComponent container, StalkerPlayer host, float maxHealth)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Container = container;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host = host;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MaxHealth = maxHealth;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health = this.MaxHealth;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000EA1B File Offset: 0x0000CC1B
		public void Reset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsBroken = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health = this.MaxHealth;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000EA3A File Offset: 0x0000CC3A
		public virtual void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TakeDamage(info.Damage);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public virtual void TakeDamage(float damage)
		{
			float dif = damage - this.Health;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health -= damage;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health = this.Health.Clamp(0f, this.MaxHealth);
			if (this.Health > 0f)
			{
				return;
			}
			if (!this.IsBroken)
			{
				this.OnLimbBreak();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsBroken = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Container.TakeFullBodyDamage(dif * this.SpillOverDamageMultiplier);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000EADA File Offset: 0x0000CCDA
		protected virtual void OnLimbBreak()
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000EADC File Offset: 0x0000CCDC
		protected virtual void OnHealthChanged(float oldValue, float newValue)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000EADE File Offset: 0x0000CCDE
		public float GetHealthFraction()
		{
			return this.Health / this.MaxHealth;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<EntityHandle<StalkerPlayer>>>(ref this._repback__Host, "Host", false, true);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Health, "Health", false, true);
			this._repback__Health.SetCallback<float>(new Action<float, float>(this.OnHealthChanged));
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsBroken, "IsBroken", false, true);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040000CB RID: 203
		private VarUnmanaged<EntityHandle<StalkerPlayer>> _repback__Host = new VarUnmanaged<EntityHandle<StalkerPlayer>>();

		// Token: 0x040000CC RID: 204
		private VarUnmanaged<float> _repback__Health = new VarUnmanaged<float>();

		// Token: 0x040000CD RID: 205
		private VarUnmanaged<bool> _repback__IsBroken = new VarUnmanaged<bool>();
	}
}
