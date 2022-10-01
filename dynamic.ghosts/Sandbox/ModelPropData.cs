using System;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x02000150 RID: 336
	[GameData("prop_data")]
	[Description("Generic prop settings. Support for this depends on the entity.")]
	public class ModelPropData
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003DBC2 File Offset: 0x0003BDC2
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x0003DBCA File Offset: 0x0003BDCA
		[Title("Bake Lighting As Static Prop")]
		[DefaultValue(true)]
		[Description("When this model is used as prop_static, it will bake lighting by default depending on this value.")]
		public bool BakeLighting { get; set; } = true;

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003DBD3 File Offset: 0x0003BDD3
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x0003DBDB File Offset: 0x0003BDDB
		[JsonPropertyName("spawn_motion_disabled")]
		[Title("Spawn as Motion-Disabled")]
		[DefaultValue(false)]
		[Description("TODO: Implement/Delete")]
		public bool SpawnMotionDisabled { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003DBE4 File Offset: 0x0003BDE4
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		[DefaultValue(-1)]
		[Description("When this model is used as prop_physics, it's health will be set to this value.")]
		public float Health { get; set; } = -1f;

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0003DBF5 File Offset: 0x0003BDF5
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0003DBFD File Offset: 0x0003BDFD
		[JsonPropertyName("min_impact_damage_speed")]
		[Title("Minimum Impact Damage Speed")]
		[DefaultValue(-1)]
		[Description("TODO: Implement/Delete")]
		public float MinImpactDamageSpeed { get; set; } = -1f;

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0003DC06 File Offset: 0x0003BE06
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0003DC0E File Offset: 0x0003BE0E
		[JsonPropertyName("impact_damage")]
		[DefaultValue(-1)]
		[Description("TODO: Implement/Delete")]
		public float ImpactDamage { get; set; } = -1f;

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003DC17 File Offset: 0x0003BE17
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0003DC1F File Offset: 0x0003BE1F
		[JsonPropertyName("parent_bodygroup_name")]
		[Description("TODO: Implement/Delete")]
		public string ParentBodygroupName { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0003DC28 File Offset: 0x0003BE28
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0003DC30 File Offset: 0x0003BE30
		[JsonPropertyName("parent_bodygroup_value")]
		[Description("TODO: Implement/Delete")]
		public int ParentBodygroupValue { get; set; }
	}
}
