using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.Inventory
{
	// Token: 0x02000100 RID: 256
	public class HeadGearEntity : ModelEntity
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002F14F File Offset: 0x0002D34F
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0002F15D File Offset: 0x0002D35D
		[Net]
		[Local]
		public unsafe bool Enabled
		{
			get
			{
				return *this._repback__Enabled.GetValue();
			}
			set
			{
				this._repback__Enabled.SetValue(value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002F16C File Offset: 0x0002D36C
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0002F17A File Offset: 0x0002D37A
		[Net]
		[Local]
		[Predicted]
		public unsafe bool Active
		{
			get
			{
				return *this._repback__Active.GetValue();
			}
			set
			{
				this._repback__Active.SetValue(value);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002F189 File Offset: 0x0002D389
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0002F196 File Offset: 0x0002D396
		[Net]
		[Local]
		private HeadGearResource HeadGearResource
		{
			get
			{
				return this._repback__HeadGearResource.GetValue();
			}
			set
			{
				this._repback__HeadGearResource.SetValue(value);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002F1A4 File Offset: 0x0002D3A4
		private Vector3 LightOffset
		{
			get
			{
				return Vector3.Forward * 10f;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002F1B5 File Offset: 0x0002D3B5
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002F1C4 File Offset: 0x0002D3C4
		public void Initialize(Entity owner)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = owner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetParent(owner, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateWorldLight();
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002F1FE File Offset: 0x0002D3FE
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateClientLight();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002F20B File Offset: 0x0002D40B
		public void SetLightEnabled(bool b)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.worldLight.Enabled = b;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetClientLightActive(b);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002F22A File Offset: 0x0002D42A
		public void SetHeadGearData(HeadGearResource data)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearResource = data;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002F238 File Offset: 0x0002D438
		public void AttachToPlayer(StalkerPlayer player)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetParent(player, "eyes", new Transform?(new Transform(this.LightOffset)));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.worldLight.SetParent(player, "hat", new Transform?(new Transform(this.LightOffset)));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalPosition = 0f;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002F2A1 File Offset: 0x0002D4A1
		private void CreateWorldLight()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.worldLight = this.CreateLight();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.worldLight.EnableHideInFirstPerson = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.worldLight.Enabled = false;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002F2D8 File Offset: 0x0002D4D8
		[ClientRpc]
		private void CreateClientLight()
		{
			if (!this.CreateClientLight__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.viewLight = this.CreateLight();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.viewLight.Enabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.viewLight.EnableViewmodelRendering = true;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002F32C File Offset: 0x0002D52C
		[ClientRpc]
		private void SetClientLightActive(bool active)
		{
			if (!this.SetClientLightActive__RpcProxy(active, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.viewLight.Enabled = active;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002F360 File Offset: 0x0002D560
		private SpotLightEntity CreateLight()
		{
			return new SpotLightEntity
			{
				Enabled = false,
				DynamicShadows = true,
				Range = 1024f,
				Falloff = 1f,
				LinearAttenuation = 0f,
				QuadraticAttenuation = 1f,
				Brightness = 2f,
				Color = Color.White,
				InnerConeAngle = 20f,
				OuterConeAngle = 40f,
				FogStrength = 1f,
				Owner = this,
				LightCookie = Texture.Load("materials/effects/lightcookie.vtex", true)
			};
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002F3FB File Offset: 0x0002D5FB
		private float swayMagnitude
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002F402 File Offset: 0x0002D602
		private float swayFrequency
		{
			get
			{
				return 9f;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002F40C File Offset: 0x0002D60C
		private Rotation GetHeadSway()
		{
			float noiseY = Noise.Simplex(Time.Now * this.swayFrequency) - 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			noiseY *= 2f;
			float num = Noise.Simplex(0f, Time.Now * this.swayFrequency) - 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Rotation rot = Rotation.From(num * 2f * this.swayMagnitude, noiseY * this.swayMagnitude, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.swayRot = Rotation.Slerp(this.swayRot, rot, Time.Delta * 7f, true);
			return rot;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		[Event.FrameAttribute]
		private void UpdateViewLight()
		{
			if (!this.viewLight.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.currentHeadLightRotation = Rotation.Slerp(this.currentHeadLightRotation, CurrentView.Rotation, Time.Delta * 30f, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.currentHeadLightRotation *= this.GetHeadSway();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.viewLight.Position = CurrentView.Position + CurrentView.Rotation.Up * 3f + CurrentView.Rotation.Forward * 5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.viewLight.Rotation = this.currentHeadLightRotation;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002F565 File Offset: 0x0002D765
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x0002F573 File Offset: 0x0002D773
		[Net]
		[Predicted]
		private unsafe bool IsToggling
		{
			get
			{
				return *this._repback__IsToggling.GetValue();
			}
			set
			{
				this._repback__IsToggling.SetValue(value);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002F582 File Offset: 0x0002D782
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0002F594 File Offset: 0x0002D794
		[Net]
		[Predicted]
		[DefaultValue(0)]
		private unsafe TimeSince TimeSinceToggleBegan
		{
			get
			{
				return *this._repback__TimeSinceToggleBegan.GetValue();
			}
			set
			{
				this._repback__TimeSinceToggleBegan.SetValue(value);
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002F5A4 File Offset: 0x0002D7A4
		public override void Simulate(Client cl)
		{
			if (cl == null)
			{
				return;
			}
			if (!this.Enabled)
			{
				return;
			}
			if (this.IsToggling && this.TimeSinceToggleBegan > 0.5f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoHeadlightToggle();
				return;
			}
			bool flag;
			if (!this.IsToggling && Input.Pressed(InputButton.Grenade))
			{
				HeadGearResource headGearResource = this.HeadGearResource;
				flag = (headGearResource != null && headGearResource.HasFlashlight);
			}
			else
			{
				flag = false;
			}
			bool toggle = flag;
			if (this.timeSinceLightToggled <= 0.9f || !toggle)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsToggling = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceToggleBegan = 0f;
			StalkerPlayer player = this.Owner as StalkerPlayer;
			if (player != null)
			{
				BaseCarriable baseCarriable = player.ActiveChild as BaseCarriable;
				if (baseCarriable != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					BaseViewModel viewModelEntity = baseCarriable.ViewModelEntity;
					if (viewModelEntity == null)
					{
						return;
					}
					viewModelEntity.SetAnimParameter("bHeadInteract", true);
				}
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002F680 File Offset: 0x0002D880
		private void DoHeadlightToggle()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsToggling = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Active = !this.Active;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound(this.Active ? "flashlight-on" : "flashlight-off");
			if (this.worldLight.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.worldLight.Enabled = this.Active;
			}
			if (this.viewLight.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.viewLight.Enabled = this.Active;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLightToggled = 0f;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002F728 File Offset: 0x0002D928
		protected bool CreateClientLight__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("CreateClientLight", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(452795833, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002F788 File Offset: 0x0002D988
		protected bool SetClientLightActive__RpcProxy(bool active, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("SetClientLightActive", new object[]
				{
					active
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1856849629, this))
			{
				if (!NetRead.IsSupported(active))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] SetClientLightActive is not allowed to use Boolean for the parameter 'active'!");
					return false;
				}
				writer.Write<bool>(active);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002F81C File Offset: 0x0002DA1C
		private void CreateClientLight(To toTarget)
		{
			this.CreateClientLight__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002F82B File Offset: 0x0002DA2B
		private void SetClientLightActive(To toTarget, bool active)
		{
			this.SetClientLightActive__RpcProxy(active, new To?(toTarget));
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002F83C File Offset: 0x0002DA3C
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == 452795833)
			{
				if (!Prediction.WasPredicted("CreateClientLight", Array.Empty<object>()))
				{
					this.CreateClientLight();
				}
				return;
			}
			if (id != 1856849629)
			{
				base.OnCallRemoteProcedure(id, read);
				return;
			}
			bool __active = false;
			__active = read.ReadData<bool>(__active);
			if (!Prediction.WasPredicted("SetClientLightActive", new object[]
			{
				__active
			}))
			{
				this.SetClientLightActive(__active);
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002F8AC File Offset: 0x0002DAAC
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Enabled, "Enabled", false, true);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Active, "Active", true, true);
			builder.Register<VarGeneric<HeadGearResource>>(ref this._repback__HeadGearResource, "HeadGearResource", false, true);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsToggling, "IsToggling", true, false);
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceToggleBegan, "TimeSinceToggleBegan", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040003A2 RID: 930
		private SpotLightEntity worldLight;

		// Token: 0x040003A3 RID: 931
		private SpotLightEntity viewLight;

		// Token: 0x040003A4 RID: 932
		private TimeSince timeSinceLightToggled;

		// Token: 0x040003A5 RID: 933
		private Rotation swayRot;

		// Token: 0x040003A6 RID: 934
		private Rotation currentHeadLightRotation;

		// Token: 0x040003A7 RID: 935
		private VarUnmanaged<bool> _repback__Enabled = new VarUnmanaged<bool>();

		// Token: 0x040003A8 RID: 936
		private VarUnmanaged<bool> _repback__Active = new VarUnmanaged<bool>();

		// Token: 0x040003A9 RID: 937
		private VarGeneric<HeadGearResource> _repback__HeadGearResource = new VarGeneric<HeadGearResource>();

		// Token: 0x040003AA RID: 938
		private VarUnmanaged<bool> _repback__IsToggling = new VarUnmanaged<bool>();

		// Token: 0x040003AB RID: 939
		private VarUnmanaged<TimeSince> _repback__TimeSinceToggleBegan = new VarUnmanaged<TimeSince>(0f);
	}
}
