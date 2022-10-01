using System;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.NPC
{
	// Token: 0x0200006B RID: 107
	[GameResource("NPC", "npc", "NPC stats.")]
	public class NPCResource : StalkerResource
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001810B File Offset: 0x0001630B
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00018113 File Offset: 0x00016313
		[Title("Health")]
		[Category("Health")]
		[MinMax(0f, 5000f)]
		[DefaultValue(100f)]
		public float MaxHealth { get; set; } = 100f;

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001811C File Offset: 0x0001631C
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00018124 File Offset: 0x00016324
		[Title("Critical Multiplier")]
		[Category("Health")]
		[MinMax(0f, 100f)]
		[DefaultValue(10f)]
		public float CriticalMultiplier { get; set; } = 10f;

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001812D File Offset: 0x0001632D
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00018135 File Offset: 0x00016335
		[Category("Movement")]
		[MinMax(0f, 1000f)]
		[DefaultValue(100f)]
		public float RunSpeed { get; set; } = 100f;

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001813E File Offset: 0x0001633E
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00018146 File Offset: 0x00016346
		[Category("Movement")]
		[MinMax(0f, 1000f)]
		[DefaultValue(100f)]
		public float CrippledSpeed { get; set; } = 100f;

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001814F File Offset: 0x0001634F
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00018157 File Offset: 0x00016357
		[Category("Movement")]
		[MinMax(0f, 1000f)]
		[DefaultValue(100f)]
		public float Acceleration { get; set; } = 100f;

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00018160 File Offset: 0x00016360
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00018168 File Offset: 0x00016368
		[Category("Model")]
		[ResourceType("vmdl")]
		public string Model { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00018171 File Offset: 0x00016371
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00018179 File Offset: 0x00016379
		[Title("Ragdoll On Death")]
		[Category("Model")]
		[DefaultValue(true)]
		public bool ShouldCreateRagdollOnDeath { get; set; } = true;

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00018182 File Offset: 0x00016382
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001818A File Offset: 0x0001638A
		[Category("Sounds")]
		[ResourceType("sound")]
		public string IdleSounds { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00018193 File Offset: 0x00016393
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0001819B File Offset: 0x0001639B
		[Category("Sounds")]
		[ResourceType("sound")]
		public string PainSounds { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000181A4 File Offset: 0x000163A4
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x000181AC File Offset: 0x000163AC
		[Category("Sounds")]
		[ResourceType("sound")]
		public string DeathSounds { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000181B5 File Offset: 0x000163B5
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000181BD File Offset: 0x000163BD
		[Category("Sounds")]
		[MinMax(0f, 1f)]
		[DefaultValue(1)]
		public float FootStepVolume { get; set; } = 1f;

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000181C6 File Offset: 0x000163C6
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x000181CE File Offset: 0x000163CE
		[Category("Targeting")]
		[DefaultValue(true)]
		public bool IgnoreOwnIdentity { get; set; } = true;

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000181D7 File Offset: 0x000163D7
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x000181DF File Offset: 0x000163DF
		[Category("Targeting")]
		[BitFlags]
		[DefaultValue(CreatureIdentity.None)]
		public CreatureIdentity Identity { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000181E8 File Offset: 0x000163E8
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x000181F0 File Offset: 0x000163F0
		[Category("Targeting")]
		[BitFlags]
		[DefaultValue(CreatureIdentity.None)]
		public CreatureIdentity EnemyIdentities { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000181F9 File Offset: 0x000163F9
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00018201 File Offset: 0x00016401
		[Category("Targeting")]
		[BitFlags]
		[DefaultValue(CreatureIdentity.None)]
		public CreatureIdentity IgnoreIdentities { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001820A File Offset: 0x0001640A
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00018212 File Offset: 0x00016412
		[Category("Targeting")]
		[ResourceType("limbwgt")]
		public string LimbWeightPath { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001821B File Offset: 0x0001641B
		[HideInEditor]
		public LimbTargetResource LimbWeightResource
		{
			get
			{
				ResourceLibrary resourceLibrary = GlobalGameNamespace.ResourceLibrary;
				if (resourceLibrary == null)
				{
					return null;
				}
				return resourceLibrary.Get<LimbTargetResource>(this.LimbWeightPath);
			}
		}
	}
}
