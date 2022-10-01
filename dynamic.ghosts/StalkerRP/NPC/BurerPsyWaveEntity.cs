using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.NPC
{
	// Token: 0x02000088 RID: 136
	[Library("ent_burer_psywave", Title = "Burer PsyWave")]
	[Spawnable]
	public sealed class BurerPsyWaveEntity : ModelEntity
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001CA64 File Offset: 0x0001AC64
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001CA72 File Offset: 0x0001AC72
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

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001CA81 File Offset: 0x0001AC81
		private float LiveTime
		{
			get
			{
				return 6f;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001CA88 File Offset: 0x0001AC88
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public float Speed { get; set; }

		// Token: 0x06000626 RID: 1574 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("entity");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromSphere(PhysicsMotionType.Dynamic, Vector3.Zero, 15f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.GravityEnabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(this.LiveTime);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001CB03 File Offset: 0x0001AD03
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttachParticles();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001CB10 File Offset: 0x0001AD10
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

		// Token: 0x06000629 RID: 1577 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		public void Fire()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = this.Direction * this.Speed;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		private void AttachParticles()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EffectSparks = Particles.Create("particles/stalker/burer_gravi_wave.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EffectOrb = Particles.Create("particles/stalker/burer_gravi_sphere.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity("burer.wavelaunch", this);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001CBAA File Offset: 0x0001ADAA
		protected override void OnDestroy()
		{
			if (base.IsClient)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EffectSparks.Destroy(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EffectOrb.Destroy(true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoDestroyEffect();
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001CBE1 File Offset: 0x0001ADE1
		private void DoDestroyEffect()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/burer_gravi_wave_burst.vpcf", this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("burer.wave", this.Position);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001CC10 File Offset: 0x0001AE10
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

		// Token: 0x0600062E RID: 1582 RVA: 0x0001CC7D File Offset: 0x0001AE7D
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<float>>(ref this._repback__Radius, "Radius", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0400022C RID: 556
		public float Damage;

		// Token: 0x0400022D RID: 557
		public Vector3 Direction;

		// Token: 0x0400022E RID: 558
		private Particles EffectSparks;

		// Token: 0x0400022F RID: 559
		private Particles EffectOrb;

		// Token: 0x04000230 RID: 560
		private VarUnmanaged<float> _repback__Radius = new VarUnmanaged<float>();
	}
}
