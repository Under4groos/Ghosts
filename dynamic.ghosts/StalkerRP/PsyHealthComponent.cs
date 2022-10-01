using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.PostProcessing;

namespace StalkerRP
{
	// Token: 0x0200003F RID: 63
	public class PsyHealthComponent : EntityComponent<StalkerPlayer>, ISingletonComponent
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000EC1A File Offset: 0x0000CE1A
		[Net]
		[Local]
		[Predicted]
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		public float Fraction
		{
			get
			{
				return this.Health / base.Entity.Stats.Health.Psy;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000EC58 File Offset: 0x0000CE58
		protected override void OnActivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Reset();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000EC68 File Offset: 0x0000CE68
		public void Reset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health = base.Entity.Stats.Health.Psy;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000EC98 File Offset: 0x0000CE98
		public void Simulate(Client client)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health += base.Entity.Stats.Health.PsyRegenRate * Time.Delta;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health = this.Health.Clamp(0f, base.Entity.Stats.Health.Psy);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PsyPostProcessManager instance = PsyPostProcessManager.Instance;
			if (instance == null)
			{
				return;
			}
			instance.SetPsyHealthFraction(this.Fraction);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000ED24 File Offset: 0x0000CF24
		public void TakeDamage(float n)
		{
			if (base.Entity.Tags.Has("stalker_godmode"))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Health -= n;
			if (this.Health <= 0f)
			{
				base.Entity.OnKilled();
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<float>>(ref this._repback__Health, "Health", true, true);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040000CE RID: 206
		private VarUnmanaged<float> _repback__Health = new VarUnmanaged<float>();
	}
}
