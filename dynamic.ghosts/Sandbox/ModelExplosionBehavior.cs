using System;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x02000151 RID: 337
	[GameData("explosion_behavior")]
	[Sphere("explosive_radius", "")]
	[Description("Defines the model as explosive. Support for this depends on the entity.")]
	public class ModelExplosionBehavior
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0003DC69 File Offset: 0x0003BE69
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0003DC71 File Offset: 0x0003BE71
		[JsonPropertyName("explosion_custom_sound")]
		[FGDType("sound", "", "")]
		[Description("Sound override for when the prop explodes.")]
		public string Sound { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0003DC7A File Offset: 0x0003BE7A
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0003DC82 File Offset: 0x0003BE82
		[JsonPropertyName("explosion_custom_effect")]
		[ResourceType("vpcf")]
		[Description("Particle effect override for when the problem explodes.")]
		public string Effect { get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003DC8B File Offset: 0x0003BE8B
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0003DC93 File Offset: 0x0003BE93
		[JsonPropertyName("explosive_damage")]
		[DefaultValue(-1)]
		[Description("Amount of damage to do at the center on the explosion. It will falloff over distance.")]
		public float Damage { get; set; } = -1f;

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003DC9C File Offset: 0x0003BE9C
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0003DCA4 File Offset: 0x0003BEA4
		[JsonPropertyName("explosive_radius")]
		[DefaultValue(-1)]
		[Description("Range of explosion's damage.")]
		public float Radius { get; set; } = -1f;

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003DCAD File Offset: 0x0003BEAD
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0003DCB5 File Offset: 0x0003BEB5
		[JsonPropertyName("explosive_force")]
		[Title("Force Scale")]
		[DefaultValue(-1)]
		[Description("Scale of the force applied to entities damaged by the explosion and the models break pieces.")]
		public float Force { get; set; } = -1f;
	}
}
