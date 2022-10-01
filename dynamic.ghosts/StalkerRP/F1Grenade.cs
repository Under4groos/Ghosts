using System;
using Sandbox;
using SandboxEditor;

namespace StalkerRP
{
	// Token: 0x02000054 RID: 84
	[Title("F1 Grenade")]
	[Category("Guns")]
	[EditorModel("models/stalker/weapons/wpn_svd_m1/wpn_svd_m1_world.vmdl", "white", "white")]
	[HammerEntity]
	[Spawnable]
	public class F1Grenade : StalkerThrownWeaponBase
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00013281 File Offset: 0x00011481
		public override string ViewModelPath
		{
			get
			{
				return "models/stalker/weapons/wpn_f1_grenade/wpn_f1_grenade_hud.vmdl";
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00013288 File Offset: 0x00011488
		public override string ProjectileName
		{
			get
			{
				return "ent_f1_grenade_projectile";
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001328F File Offset: 0x0001148F
		public override float ThrowReleaseProjectileDelay
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00013298 File Offset: 0x00011498
		public override Vector3 ThrowOrigin
		{
			get
			{
				return this.Owner.EyePosition + this.Owner.EyeRotation.Forward * 30f + this.Owner.EyeRotation.Right * 4.5f;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000132F4 File Offset: 0x000114F4
		public override float ThrowVelocity
		{
			get
			{
				return 700f;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000132FB File Offset: 0x000114FB
		public override float ThrowUpVelocity
		{
			get
			{
				return 120f;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00013304 File Offset: 0x00011504
		public override Vector3 ThrowAngularVelocity
		{
			get
			{
				return Vector3.Random.Normal * Rand.Float(30f, 60f);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00013332 File Offset: 0x00011532
		public override Angles ThrowStartingAngles
		{
			get
			{
				return Angles.Random * 2000f;
			}
		}
	}
}
