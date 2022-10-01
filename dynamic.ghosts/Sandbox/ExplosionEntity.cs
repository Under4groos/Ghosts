using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A1 RID: 417
	[Library("ent_explosion")]
	[HammerEntity]
	[EditorSprite("editor/env_explosion.vmat")]
	[Sphere("radius", 255, 255, 255, false)]
	[Title("Explosion")]
	[Category("Effects")]
	[Icon("radar")]
	[Description("An entity that creates an explosion at its center.")]
	public class ExplosionEntity : Entity
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00053C46 File Offset: 0x00051E46
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00053C4D File Offset: 0x00051E4D
		[ConVar.ServerAttribute(null)]
		[DefaultValue(false)]
		private static bool debug_prop_explosion { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00053C55 File Offset: 0x00051E55
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00053C5D File Offset: 0x00051E5D
		[Property]
		[DefaultValue(100f)]
		[Description("Radius of the explosion.")]
		public float Radius { get; set; } = 100f;

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00053C66 File Offset: 0x00051E66
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00053C6E File Offset: 0x00051E6E
		[Property]
		[DefaultValue(100f)]
		[Description("Damage the explosion should do at the center. The damage will reduce the farther the target is from the center of the explosion.")]
		public float Damage { get; set; } = 100f;

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00053C77 File Offset: 0x00051E77
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x00053C7F File Offset: 0x00051E7F
		[Property]
		[DefaultValue(1f)]
		[Description("Scale explosion induced physics forces by this amount.")]
		public float ForceScale { get; set; } = 1f;

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00053C88 File Offset: 0x00051E88
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x00053C90 File Offset: 0x00051E90
		[ResourceType("vpcf")]
		[Property]
		[Description("If set, will override the default explosion particle effect.")]
		public string ParticleOverride { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00053C99 File Offset: 0x00051E99
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x00053CA1 File Offset: 0x00051EA1
		[FGDType("sound", "", "")]
		[Property]
		[Description("If set, will override the default explosion sound.")]
		public string SoundOverride { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00053CAA File Offset: 0x00051EAA
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00053CB2 File Offset: 0x00051EB2
		[Property]
		[DefaultValue(true)]
		[Description("Delete this entity when it is triggered via the Explode input?")]
		public bool RemoveOnExplode { get; set; } = true;

		// Token: 0x060014C2 RID: 5314 RVA: 0x00053CBC File Offset: 0x00051EBC
		[Input]
		[Description("Perform the explosion.")]
		public void Explode(Entity activator)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld(string.IsNullOrWhiteSpace(this.SoundOverride) ? "rust_pumpshotgun.shootdouble" : this.SoundOverride, this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create(string.IsNullOrWhiteSpace(this.ParticleOverride) ? "particles/explosion/barrel_explosion/explosion_barrel.vpcf" : this.ParticleOverride, this.Position);
			IEnumerable<Entity> enumerable = Entity.FindInSphere(this.Position, this.Radius);
			if (ExplosionEntity.debug_prop_explosion)
			{
				GlobalGameNamespace.DebugOverlay.Sphere(this.Position, this.Radius, Color.Orange, 5f, true);
			}
			foreach (Entity entity in enumerable)
			{
				ModelEntity ent = entity as ModelEntity;
				if (ent != null && ent.IsValid() && ent.LifeState == LifeState.Alive && ent.PhysicsBody.IsValid() && !ent.IsWorld)
				{
					Vector3 targetPos = ent.PhysicsBody.MassCenter;
					float dist = Vector3.DistanceBetween(this.Position, targetPos);
					if (dist <= this.Radius)
					{
						Vector3 vector = this.Position;
						Trace trace = Trace.Ray(vector, targetPos);
						bool flag = true;
						TraceResult tr = trace.Ignore(activator, flag).WorldOnly().Run();
						if (tr.Fraction < 0.95f)
						{
							if (ExplosionEntity.debug_prop_explosion)
							{
								GlobalGameNamespace.DebugOverlay.Line(this.Position, tr.EndPosition, Color.Red, 5f, true);
							}
						}
						else
						{
							if (ExplosionEntity.debug_prop_explosion)
							{
								GlobalGameNamespace.DebugOverlay.Line(this.Position, targetPos, 5f, true);
							}
							float distanceMul = 1f - Math.Clamp(dist / this.Radius, 0f, 1f);
							float dmg = this.Damage * distanceMul;
							float force = this.ForceScale * distanceMul * ent.PhysicsBody.Mass;
							vector = targetPos - this.Position;
							Vector3 forceDir = vector.Normal;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							ent.TakeDamage(DamageInfo.Explosion(this.Position, forceDir * force, dmg).WithAttacker(activator, null));
						}
					}
				}
			}
			if (this.RemoveOnExplode)
			{
				base.Delete();
			}
		}
	}
}
