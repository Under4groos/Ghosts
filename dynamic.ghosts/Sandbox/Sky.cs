using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000149 RID: 329
	[ClassName("env_sky")]
	[HammerEntity]
	[Skybox]
	[EditorSprite("editor/env_sky.vmat")]
	[Title("Sky")]
	[Category("Fog & Sky")]
	[Icon("cloud_circle")]
	[Description("Controls the sky.")]
	public class Sky : Entity
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0003CF90 File Offset: 0x0003B190
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0003CF9D File Offset: 0x0003B19D
		[Property("skyname", null)]
		[Net]
		[Title("Sky Material")]
		[ResourceType("vmat")]
		[DefaultValue("materials/dev/default_sky.vmat")]
		public string Skyname
		{
			get
			{
				return this._repback__Skyname.GetValue();
			}
			set
			{
				this._repback__Skyname.SetValue(value);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0003CFAB File Offset: 0x0003B1AB
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0003CFBD File Offset: 0x0003B1BD
		[Property("tint_color", null)]
		[Net]
		[Title("Skybox Tint Color")]
		[Description("Tint the skybox")]
		public unsafe Color TintColor
		{
			get
			{
				return *this._repback__TintColor.GetValue();
			}
			set
			{
				this._repback__TintColor.SetValue(value);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0003CFCC File Offset: 0x0003B1CC
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x0003CFDA File Offset: 0x0003B1DA
		[Property("fog_type", null)]
		[Net]
		[Title("Fog Type")]
		[DefaultValue(SceneSkyBox.FogType.Distance)]
		public unsafe SceneSkyBox.FogType FogType
		{
			get
			{
				return *this._repback__FogType.GetValue();
			}
			set
			{
				this._repback__FogType.SetValue(value);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0003CFE9 File Offset: 0x0003B1E9
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0003CFF7 File Offset: 0x0003B1F7
		[Property("angular_fog_min_start", null)]
		[Net]
		[Title("Fog Min Angle Start")]
		[DefaultValue(-25f)]
		public unsafe float FogMinStart
		{
			get
			{
				return *this._repback__FogMinStart.GetValue();
			}
			set
			{
				this._repback__FogMinStart.SetValue(value);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0003D006 File Offset: 0x0003B206
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0003D014 File Offset: 0x0003B214
		[Property("angular_fog_min_end", null)]
		[Net]
		[Title("Fog Min Angle End")]
		[DefaultValue(-35f)]
		public unsafe float FogMinEnd
		{
			get
			{
				return *this._repback__FogMinEnd.GetValue();
			}
			set
			{
				this._repback__FogMinEnd.SetValue(value);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0003D023 File Offset: 0x0003B223
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0003D031 File Offset: 0x0003B231
		[Property("angular_fog_max_start", null)]
		[Net]
		[Title("Fog Max Angle Start")]
		[DefaultValue(25f)]
		public unsafe float FogMaxStart
		{
			get
			{
				return *this._repback__FogMaxStart.GetValue();
			}
			set
			{
				this._repback__FogMaxStart.SetValue(value);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0003D040 File Offset: 0x0003B240
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0003D04E File Offset: 0x0003B24E
		[Property("angular_fog_max_end", null)]
		[Net]
		[Title("Fog Max Angle End")]
		[DefaultValue(35f)]
		public unsafe float FogMaxEnd
		{
			get
			{
				return *this._repback__FogMaxEnd.GetValue();
			}
			set
			{
				this._repback__FogMaxEnd.SetValue(value);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0003D05D File Offset: 0x0003B25D
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0003D065 File Offset: 0x0003B265
		public Material SkyMaterial { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0003D06E File Offset: 0x0003B26E
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x0003D076 File Offset: 0x0003B276
		public SceneSkyBox SkyObject { get; set; }

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003D080 File Offset: 0x0003B280
		public Sky()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003D10B File Offset: 0x0003B30B
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateSky();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003D124 File Offset: 0x0003B324
		private void CreateSky()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("CreateSky");
			if (string.IsNullOrWhiteSpace(this.Skyname))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SkyMaterial = Material.Load(this.Skyname);
			if (this.SkyMaterial == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SkyObject = new SceneSkyBox(base.Scene, this.SkyMaterial)
			{
				Transform = new Transform(Vector3.Zero, this.LocalRotation, 1f),
				SkyTint = this.TintColor,
				FogParams = new SceneSkyBox.FogParamInfo
				{
					FogType = this.FogType,
					FogMinStart = this.FogMinStart,
					FogMinEnd = this.FogMinEnd,
					FogMaxStart = this.FogMaxStart,
					FogMaxEnd = this.FogMaxEnd
				}
			};
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003D200 File Offset: 0x0003B400
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__Skyname, "Skyname", false, false);
			builder.Register<VarUnmanaged<Color>>(ref this._repback__TintColor, "TintColor", false, false);
			builder.Register<VarUnmanaged<SceneSkyBox.FogType>>(ref this._repback__FogType, "FogType", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogMinStart, "FogMinStart", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogMinEnd, "FogMinEnd", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogMaxStart, "FogMaxStart", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogMaxEnd, "FogMaxEnd", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040004C0 RID: 1216
		private VarGeneric<string> _repback__Skyname = new VarGeneric<string>("materials/dev/default_sky.vmat");

		// Token: 0x040004C1 RID: 1217
		private VarUnmanaged<Color> _repback__TintColor = new VarUnmanaged<Color>(Color.White);

		// Token: 0x040004C2 RID: 1218
		private VarUnmanaged<SceneSkyBox.FogType> _repback__FogType = new VarUnmanaged<SceneSkyBox.FogType>(SceneSkyBox.FogType.Distance);

		// Token: 0x040004C3 RID: 1219
		private VarUnmanaged<float> _repback__FogMinStart = new VarUnmanaged<float>(-25f);

		// Token: 0x040004C4 RID: 1220
		private VarUnmanaged<float> _repback__FogMinEnd = new VarUnmanaged<float>(-35f);

		// Token: 0x040004C5 RID: 1221
		private VarUnmanaged<float> _repback__FogMaxStart = new VarUnmanaged<float>(25f);

		// Token: 0x040004C6 RID: 1222
		private VarUnmanaged<float> _repback__FogMaxEnd = new VarUnmanaged<float>(35f);
	}
}
