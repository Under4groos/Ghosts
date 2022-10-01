using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory;

namespace StalkerRP
{
	// Token: 0x02000053 RID: 83
	[Library("weapon_bolt", Title = "Bolt")]
	[Spawnable]
	public class Bolt : StalkerThrownWeaponBase
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00013195 File Offset: 0x00011395
		public override string ViewModelPath
		{
			get
			{
				return "models/stalker/weapons/wpn_bolt/wpn_bolt_hud.vmdl";
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0001319C File Offset: 0x0001139C
		public override string ProjectileName
		{
			get
			{
				return "ent_bolt_projectile";
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000131A3 File Offset: 0x000113A3
		public override float ThrowReleaseProjectileDelay
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000131AC File Offset: 0x000113AC
		public override Vector3 ThrowOrigin
		{
			get
			{
				return this.Owner.EyePosition + this.Owner.EyeRotation.Forward * 30f + this.Owner.EyeRotation.Right * 4.5f;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00013208 File Offset: 0x00011408
		public override float ThrowVelocity
		{
			get
			{
				return 700f;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0001320F File Offset: 0x0001140F
		public override float ThrowUpVelocity
		{
			get
			{
				return 120f;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00013218 File Offset: 0x00011418
		public override Vector3 ThrowAngularVelocity
		{
			get
			{
				return Vector3.Random.Normal * Rand.Float(30f, 60f);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00013246 File Offset: 0x00011446
		public override Angles ThrowStartingAngles
		{
			get
			{
				return Angles.Random * 2000f;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00013257 File Offset: 0x00011457
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Init(StalkerResource.Get<WeaponItemResource>("item_weapon_bolt"));
		}
	}
}
