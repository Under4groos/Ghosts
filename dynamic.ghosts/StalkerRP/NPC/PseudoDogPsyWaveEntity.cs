using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.NPC
{
	// Token: 0x020000C5 RID: 197
	[Library("ent_pseudodog_psywave", Title = "Pseudodog PsyWave")]
	[Spawnable]
	public sealed class PseudoDogPsyWaveEntity : ModelEntity
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00025B2B File Offset: 0x00023D2B
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00025B39 File Offset: 0x00023D39
		[Net]
		public unsafe float Radius
		{
			get
			{
				return *this._repback__Radius.GetValue();
			}
			set
			{
				this._repback__Radius.SetValue(value);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00025B48 File Offset: 0x00023D48
		private float LiveTime
		{
			get
			{
				return 6f;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00025B4F File Offset: 0x00023D4F
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00025B57 File Offset: 0x00023D57
		public float Speed { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00025B60 File Offset: 0x00023D60
		private float PsyOverlayAlphaMax
		{
			get
			{
				return 0.13f;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00025B67 File Offset: 0x00023D67
		private float PsyOverlayDistanceMax
		{
			get
			{
				return 800f;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00025B70 File Offset: 0x00023D70
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("trigger");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromSphere(PhysicsMotionType.Dynamic, Vector3.Zero, 10f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.GravityEnabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel("models/stalker/monsters/pseudodog/pseudodog_psy_blast.vmdl");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RenderColor = base.RenderColor.WithAlpha(0.05f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(this.LiveTime);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00025C15 File Offset: 0x00023E15
		public override void Touch(Entity other)
		{
			if (base.IsClient)
			{
				return;
			}
			if (other == this.Owner)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DealDamage(other);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00025C44 File Offset: 0x00023E44
		public void Fire()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = this.Direction * this.Speed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = Rotation.LookAt(this.Direction);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles effectSparks = this.EffectSparks;
			if (effectSparks != null)
			{
				effectSparks.Destroy(true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EffectSparks = Particles.Create("particles/stalker/monsters/pdog_psy_blast.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EffectSparks.SetPosition(1, -this.Direction);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity("pdog.psy_effect", this);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00025CDF File Offset: 0x00023EDF
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles effectSparks = this.EffectSparks;
			if (effectSparks != null)
			{
				effectSparks.Destroy(false);
			}
			if (base.IsClient)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoDestroyEffect();
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00025D0B File Offset: 0x00023F0B
		private void DoDestroyEffect()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/burer_gravi_wave_burst.vpcf", this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("burer.wave", this.Position);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00025D3C File Offset: 0x00023F3C
		private void DealDamage(Entity ent)
		{
			DamageInfo info = DamageInfo.Generic(this.Damage);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			info.Attacker = this.Owner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			info.Force = this.Velocity.Normal * 150f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			info.Position = this.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.TakeDamage(info);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00025DA9 File Offset: 0x00023FA9
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<float>>(ref this._repback__Radius, "Radius", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040002CF RID: 719
		public float Damage;

		// Token: 0x040002D0 RID: 720
		public Vector3 Direction;

		// Token: 0x040002D1 RID: 721
		private Particles EffectSparks;

		// Token: 0x040002D2 RID: 722
		private VarUnmanaged<float> _repback__Radius = new VarUnmanaged<float>();
	}
}
