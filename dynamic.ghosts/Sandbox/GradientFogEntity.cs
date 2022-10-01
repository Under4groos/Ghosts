using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A2 RID: 418
	[Library("env_gradient_fog")]
	[HammerEntity]
	[VisGroup(VisGroup.Lighting)]
	[Global("gradient_fog")]
	[EditorSprite("editor/env_gradient_fog.vmat")]
	[SimpleHelper("gradientfog")]
	[HideProperty("enable_shadows")]
	[Title("Gradient Fog")]
	[Category("Fog & Sky")]
	[Icon("gradient")]
	[Description("Specifies fog based on a color gradient")]
	public class GradientFogEntity : ModelEntity
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00053F54 File Offset: 0x00052154
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00053F62 File Offset: 0x00052162
		[Property("fogenabled", null)]
		[Title("Fog Enabled")]
		[Net]
		[DefaultValue(true)]
		[Description("Whether the fog is enabled or not.")]
		public unsafe bool FogEnabled
		{
			get
			{
				return *this._repback__FogEnabled.GetValue();
			}
			set
			{
				this._repback__FogEnabled.SetValue(value);
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00053F71 File Offset: 0x00052171
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x00053F7F File Offset: 0x0005217F
		[Property("fogstart", null)]
		[Title("Fog Start Distance")]
		[Net]
		[DefaultValue(0f)]
		[Description("For start distance.")]
		public unsafe float FogStartDistance
		{
			get
			{
				return *this._repback__FogStartDistance.GetValue();
			}
			set
			{
				this._repback__FogStartDistance.SetValue(value);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00053F8E File Offset: 0x0005218E
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x00053F9C File Offset: 0x0005219C
		[Property("fogend", null)]
		[Title("Fog End Distance")]
		[Net]
		[DefaultValue(4000f)]
		[Description("Fog end distance.")]
		public unsafe float FogEndDistance
		{
			get
			{
				return *this._repback__FogEndDistance.GetValue();
			}
			set
			{
				this._repback__FogEndDistance.SetValue(value);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00053FAB File Offset: 0x000521AB
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x00053FB9 File Offset: 0x000521B9
		[Property("fogstartheight", null)]
		[Title("Fog Start Height")]
		[Net]
		[DefaultValue(0f)]
		[Description("Fog start height.")]
		public unsafe float FogStartHeight
		{
			get
			{
				return *this._repback__FogStartHeight.GetValue();
			}
			set
			{
				this._repback__FogStartHeight.SetValue(value);
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00053FC8 File Offset: 0x000521C8
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00053FD6 File Offset: 0x000521D6
		[Property("fogendheight", null)]
		[Title("Fog End Height")]
		[Net]
		[DefaultValue(200f)]
		[Description("Fog end height.")]
		public unsafe float FogEndHeight
		{
			get
			{
				return *this._repback__FogEndHeight.GetValue();
			}
			set
			{
				this._repback__FogEndHeight.SetValue(value);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00053FE5 File Offset: 0x000521E5
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x00053FF3 File Offset: 0x000521F3
		[Property("fogmaxopacity", null)]
		[Title("Fog Maximum Opacity")]
		[Net]
		[DefaultValue(0.5f)]
		[Description("Set the maximum opacity at the base of the gradient fog.")]
		public unsafe float FogMaximumOpacity
		{
			get
			{
				return *this._repback__FogMaximumOpacity.GetValue();
			}
			set
			{
				this._repback__FogMaximumOpacity.SetValue(value);
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00054002 File Offset: 0x00052202
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x00054014 File Offset: 0x00052214
		[Property("fogcolor", null)]
		[Title("Fog Color (R G B)")]
		[DefaultValue("255 255 255 255")]
		[Net]
		[Description("Set the gradient fog color.")]
		public unsafe Color FogColor
		{
			get
			{
				return *this._repback__FogColor.GetValue();
			}
			set
			{
				this._repback__FogColor.SetValue(value);
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00054023 File Offset: 0x00052223
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x00054031 File Offset: 0x00052231
		[Property("fogstrength", null)]
		[Title("Fog Strength")]
		[Net]
		[DefaultValue(1f)]
		[Description("Fog strength.")]
		public unsafe float FogStrength
		{
			get
			{
				return *this._repback__FogStrength.GetValue();
			}
			set
			{
				this._repback__FogStrength.SetValue(value);
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00054040 File Offset: 0x00052240
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x0005404E File Offset: 0x0005224E
		[Property("fogfalloffexponent", null)]
		[Title("Distance Falloff Exponent")]
		[Net]
		[DefaultValue(2f)]
		[Description("Exponent for distance falloff.")]
		public unsafe float FogDistanceFalloffExponent
		{
			get
			{
				return *this._repback__FogDistanceFalloffExponent.GetValue();
			}
			set
			{
				this._repback__FogDistanceFalloffExponent.SetValue(value);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0005405D File Offset: 0x0005225D
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x0005406B File Offset: 0x0005226B
		[Property("fogverticalexponent", null)]
		[Title("Vertical Falloff Exponent")]
		[Net]
		[DefaultValue(1f)]
		[Description("\"Exponent for vertical falloff.\"")]
		public unsafe float FogVerticalFalloffExponent
		{
			get
			{
				return *this._repback__FogVerticalFalloffExponent.GetValue();
			}
			set
			{
				this._repback__FogVerticalFalloffExponent.SetValue(value);
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0005407A File Offset: 0x0005227A
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00054088 File Offset: 0x00052288
		[Property("fadetime", null)]
		[Title("Fade Time")]
		[Net]
		[DefaultValue(1f)]
		[Description("How much time it takes to fade in new values.")]
		public unsafe float FogFadeTime
		{
			get
			{
				return *this._repback__FogFadeTime.GetValue();
			}
			set
			{
				this._repback__FogFadeTime.SetValue(value);
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00054097 File Offset: 0x00052297
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000540B0 File Offset: 0x000522B0
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Current = GlobalGameNamespace.Map.Scene.GradientFog;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired = this.Current;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(true);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00054100 File Offset: 0x00052300
		[ClientRpc]
		public void UpdateFogState(bool isForceSet = false)
		{
			if (!this.UpdateFogState__RpcProxy(isForceSet, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Current.Enabled = this.FogEnabled;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.Enabled = this.FogEnabled;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.StartDistance = this.FogStartDistance;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.EndDistance = this.FogEndDistance;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.StartHeight = this.FogStartHeight;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.EndHeight = this.FogEndHeight;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.MaximumOpacity = this.FogMaximumOpacity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.DistanceFalloffExponent = this.FogDistanceFalloffExponent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.VerticalFalloffExponent = this.FogVerticalFalloffExponent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desired.Color = this.FogColor;
			Color color;
			Vector4 vector;
			Vector3 vector2;
			Vector4 vector3;
			if (!isForceSet && this.FogEnabled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FadeEndTime = this.FogFadeTime;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsFading = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Current = GlobalGameNamespace.Map.Scene.GradientFog;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				color = this.Current.Color;
				vector = color;
				vector2 = ((this.CurrentStrength == 0f) ? 1f : (1f / this.CurrentStrength));
				vector3 = new Vector4(ref vector2, 1f);
				this.Current.Color = vector * vector3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LastStrength = this.CurrentStrength;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Current = this.Desired;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LastStrength = this.FogStrength;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentStrength = this.FogStrength;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			color = this.Current.Color;
			vector = color;
			vector2 = this.FogStrength;
			vector3 = new Vector4(ref vector2, 1f);
			this.Current.Color = vector * vector3;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Map.Scene.GradientFog = this.Current;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00054350 File Offset: 0x00052550
		[Event.FrameAttribute]
		protected void TickFrame()
		{
			if (!this.IsFading)
			{
				return;
			}
			Color color;
			Vector4 vector;
			Vector3 vector2;
			Vector4 vector3;
			if (this.FadeEndTime)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Current = this.Desired;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				color = this.Current.Color;
				vector = color;
				vector2 = this.FogStrength;
				vector3 = new Vector4(ref vector2, 1f);
				this.Current.Color = vector * vector3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Map.Scene.GradientFog = this.Current;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LastStrength = this.FogStrength;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CurrentStrength = this.FogStrength;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsFading = false;
				return;
			}
			float fraction = this.FadeEndTime.Fraction;
			GradientFogController state = this.Current.LerpTo(this.Desired, fraction, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			state.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentStrength = this.LastStrength.LerpTo(this.FogStrength, fraction, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			color = state.Color;
			vector = color;
			vector2 = this.CurrentStrength;
			vector3 = new Vector4(ref vector2, 1f);
			state.Color = vector * vector3;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Map.Scene.GradientFog = state;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x000544BC File Offset: 0x000526BC
		[Input]
		[Description("Set Fog Start Distance")]
		public void SetFogStartDistance(float fogDistance)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogStartDistance = fogDistance;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000544D6 File Offset: 0x000526D6
		[Input]
		[Description("Set Fog End Distance")]
		public void SetFogEndDistance(float fogDistance)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogEndDistance = fogDistance;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000544F0 File Offset: 0x000526F0
		[Input]
		[Description("Set Fog Start Height")]
		public void SetFogStartHeight(float height)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogStartHeight = height;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0005450A File Offset: 0x0005270A
		[Input]
		[Description("Set Fog End Height")]
		public void SetFogEndHeight(float height)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogEndHeight = height;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00054524 File Offset: 0x00052724
		[Input]
		[Description("Set Fog Max Opacity")]
		public void SetFogMaxOpacity(float maxOpacity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogMaximumOpacity = maxOpacity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0005453E File Offset: 0x0005273E
		[Input]
		[Description("Set Fog Falloff Exponent")]
		public void SetFogFalloffExponent(float exponent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogDistanceFalloffExponent = exponent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00054558 File Offset: 0x00052758
		[Input]
		[Description("Set Fog Vertical Exponent")]
		public void SetFogVerticalExponent(float exponent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogVerticalFalloffExponent = exponent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00054572 File Offset: 0x00052772
		[Input]
		[Description("Set Fog Color")]
		public void SetFogColor(Color fogColor)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogColor = fogColor;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0005458C File Offset: 0x0005278C
		[Input]
		[Description("Set Fog Strength")]
		public void SetFogStrength(float strength)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FogStrength = strength;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFogState(false);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000545A8 File Offset: 0x000527A8
		protected bool UpdateFogState__RpcProxy(bool isForceSet = false, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("UpdateFogState", new object[]
				{
					isForceSet
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-643791300, this))
			{
				if (!NetRead.IsSupported(isForceSet))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] UpdateFogState is not allowed to use Boolean for the parameter 'isForceSet'!");
					return false;
				}
				writer.Write<bool>(isForceSet);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0005463C File Offset: 0x0005283C
		public void UpdateFogState(To toTarget, bool isForceSet = false)
		{
			this.UpdateFogState__RpcProxy(isForceSet, new To?(toTarget));
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0005464C File Offset: 0x0005284C
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -643791300)
			{
				bool __isForceSet = false;
				__isForceSet = read.ReadData<bool>(__isForceSet);
				if (!Prediction.WasPredicted("UpdateFogState", new object[]
				{
					__isForceSet
				}))
				{
					this.UpdateFogState(__isForceSet);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00054698 File Offset: 0x00052898
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__FogEnabled, "FogEnabled", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogStartDistance, "FogStartDistance", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogEndDistance, "FogEndDistance", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogStartHeight, "FogStartHeight", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogEndHeight, "FogEndHeight", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogMaximumOpacity, "FogMaximumOpacity", false, false);
			builder.Register<VarUnmanaged<Color>>(ref this._repback__FogColor, "FogColor", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogStrength, "FogStrength", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogDistanceFalloffExponent, "FogDistanceFalloffExponent", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogVerticalFalloffExponent, "FogVerticalFalloffExponent", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FogFadeTime, "FogFadeTime", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040006C3 RID: 1731
		private GradientFogController Current;

		// Token: 0x040006C4 RID: 1732
		private GradientFogController Desired;

		// Token: 0x040006C5 RID: 1733
		private float LastStrength;

		// Token: 0x040006C6 RID: 1734
		private float CurrentStrength;

		// Token: 0x040006C7 RID: 1735
		private TimeUntil FadeEndTime;

		// Token: 0x040006C8 RID: 1736
		private bool IsFading;

		// Token: 0x040006C9 RID: 1737
		private VarUnmanaged<bool> _repback__FogEnabled = new VarUnmanaged<bool>(true);

		// Token: 0x040006CA RID: 1738
		private VarUnmanaged<float> _repback__FogStartDistance = new VarUnmanaged<float>(0f);

		// Token: 0x040006CB RID: 1739
		private VarUnmanaged<float> _repback__FogEndDistance = new VarUnmanaged<float>(4000f);

		// Token: 0x040006CC RID: 1740
		private VarUnmanaged<float> _repback__FogStartHeight = new VarUnmanaged<float>(0f);

		// Token: 0x040006CD RID: 1741
		private VarUnmanaged<float> _repback__FogEndHeight = new VarUnmanaged<float>(200f);

		// Token: 0x040006CE RID: 1742
		private VarUnmanaged<float> _repback__FogMaximumOpacity = new VarUnmanaged<float>(0.5f);

		// Token: 0x040006CF RID: 1743
		private VarUnmanaged<Color> _repback__FogColor = new VarUnmanaged<Color>();

		// Token: 0x040006D0 RID: 1744
		private VarUnmanaged<float> _repback__FogStrength = new VarUnmanaged<float>(1f);

		// Token: 0x040006D1 RID: 1745
		private VarUnmanaged<float> _repback__FogDistanceFalloffExponent = new VarUnmanaged<float>(2f);

		// Token: 0x040006D2 RID: 1746
		private VarUnmanaged<float> _repback__FogVerticalFalloffExponent = new VarUnmanaged<float>(1f);

		// Token: 0x040006D3 RID: 1747
		private VarUnmanaged<float> _repback__FogFadeTime = new VarUnmanaged<float>(1f);
	}
}
