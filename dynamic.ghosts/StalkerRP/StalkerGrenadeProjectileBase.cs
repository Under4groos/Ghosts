using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory;

namespace StalkerRP
{
	// Token: 0x02000057 RID: 87
	public class StalkerGrenadeProjectileBase : StalkerProjectileBase
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000133A3 File Offset: 0x000115A3
		protected virtual int ShrapnelCount
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000133A7 File Offset: 0x000115A7
		protected virtual float ShrapnelDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000133AE File Offset: 0x000115AE
		[Description("speed of shrapnel in meters per second")]
		protected virtual float ShrapnelSpeed
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000133B5 File Offset: 0x000115B5
		protected virtual string ShrapnelType
		{
			get
			{
				return "f1shrapnel";
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600036B RID: 875 RVA: 0x000133BC File Offset: 0x000115BC
		protected virtual float CookTime
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000133C3 File Offset: 0x000115C3
		protected virtual float BlastRadius
		{
			get
			{
				return 550f;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000133CA File Offset: 0x000115CA
		protected virtual float BlastDamage
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000133D1 File Offset: 0x000115D1
		protected virtual float BlastForce
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000133D8 File Offset: 0x000115D8
		protected virtual string BlastSound
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000133DF File Offset: 0x000115DF
		protected virtual string BlastEffect
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000133E8 File Offset: 0x000115E8
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.SetSurface("surfaces/metal.weapon.surface");
			if (!string.IsNullOrWhiteSpace(this.ShrapnelType))
			{
				this.ShrapnelData = StalkerResource.Get<AmmoItemResource>(this.ShrapnelType);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Detonate(this.CookTime);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00013444 File Offset: 0x00011644
		protected virtual void Detonate(float time)
		{
			StalkerGrenadeProjectileBase.<Detonate>d__22 <Detonate>d__;
			<Detonate>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Detonate>d__.<>4__this = this;
			<Detonate>d__.time = time;
			<Detonate>d__.<>1__state = -1;
			<Detonate>d__.<>t__builder.Start<StalkerGrenadeProjectileBase.<Detonate>d__22>(ref <Detonate>d__);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00013483 File Offset: 0x00011683
		protected virtual void DoExplosionEffect()
		{
			if (!string.IsNullOrWhiteSpace(this.BlastEffect))
			{
				Particles.Create(this.BlastEffect, this.Position);
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000134A4 File Offset: 0x000116A4
		protected virtual void DoExplosionDamage()
		{
			IEnumerable<Entity> enumerable = Entity.FindInSphere(this.Position, this.BlastRadius);
			Vector3 pos = this.Position;
			foreach (Entity ent in enumerable)
			{
				float distance = pos.Distance(ent.Position);
				float mult = 1f - distance / this.BlastRadius;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ent.TakeDamage(DamageInfo.Explosion(pos, this.BlastForce * mult, MathF.Max(mult * this.BlastDamage, 0f)));
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001354C File Offset: 0x0001174C
		protected virtual void DoShrapnel()
		{
			if (this.ShrapnelData == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("phys_bullet_tag");
			Vector3 origin = base.WorldSpaceBounds.Center + Vector3.Up * 3f;
			float speed = this.ShrapnelSpeed * 39.3701f;
			for (int i = 0; i < this.ShrapnelCount; i++)
			{
				Vector3 forward = Vector3.Random.Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				forward = forward.WithZ(Rand.Float(0.05f, 0.35f));
				BulletPhysBase bulletPhysBase = BulletPhysBase.Pool.Request();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bulletPhysBase.Fire(origin, forward, speed, this.Owner, this, this.ShrapnelData, 10f, this.ShrapnelDamage, true);
			}
		}

		// Token: 0x04000116 RID: 278
		private AmmoItemResource ShrapnelData;
	}
}
