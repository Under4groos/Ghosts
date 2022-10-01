using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A6 RID: 422
	[Library("func_precipitation")]
	[HammerEntity]
	[Solid]
	[HideProperty("enable_shadows")]
	[AutoApplyMaterial("materials/tools/toolsprecipitation.vmat")]
	[Title("Precipitation Volume")]
	[Category("Effects")]
	[Icon("grain")]
	[Description("A solid entity that creates rain and snow inside its volume.")]
	public class PrecipitationEntity : ModelEntity
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000551CF File Offset: 0x000533CF
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x000551DC File Offset: 0x000533DC
		[Property]
		[Title("Particle Effect Inner Near")]
		[ResourceType("vpcf")]
		[Category("Particle System")]
		[Net]
		[DefaultValue("particles/precipitation/rain_inner.vpcf")]
		[Description("Particle effect to be placed at \"Inner Near Distance\" away from the view")]
		public string InnerNearEffect
		{
			get
			{
				return this._repback__InnerNearEffect.GetValue();
			}
			set
			{
				this._repback__InnerNearEffect.SetValue(value);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x000551EA File Offset: 0x000533EA
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x000551F7 File Offset: 0x000533F7
		[Property]
		[Title("Particle Effect Inner Far")]
		[ResourceType("vpcf")]
		[Category("Particle System")]
		[Net]
		[DefaultValue("particles/precipitation/rain_inner.vpcf")]
		[Description("Particle effect to be placed at \"Inner Far Distance\" away from the view")]
		public string InnerFarEffect
		{
			get
			{
				return this._repback__InnerFarEffect.GetValue();
			}
			set
			{
				this._repback__InnerFarEffect.SetValue(value);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00055205 File Offset: 0x00053405
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x00055212 File Offset: 0x00053412
		[Property]
		[Title("Particle Effect Outer")]
		[ResourceType("vpcf")]
		[Category("Particle System")]
		[Net]
		[DefaultValue("particles/precipitation/rain_outer.vpcf")]
		[Description("Particle effect to be placed at the current view's position")]
		public string OuterEffect
		{
			get
			{
				return this._repback__OuterEffect.GetValue();
			}
			set
			{
				this._repback__OuterEffect.SetValue(value);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00055220 File Offset: 0x00053420
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0005522E File Offset: 0x0005342E
		[Property]
		[Category("Distance")]
		[Net]
		[DefaultValue(32f)]
		[Description("How far in front of player's view to place the \"near\" effect.")]
		public unsafe float InnerNearDistance
		{
			get
			{
				return *this._repback__InnerNearDistance.GetValue();
			}
			set
			{
				this._repback__InnerNearDistance.SetValue(value);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0005523D File Offset: 0x0005343D
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0005524B File Offset: 0x0005344B
		[Property]
		[Category("Distance")]
		[Net]
		[DefaultValue(180f)]
		[Description("How far in front of player's view to place the \"far\" effect.")]
		public unsafe float InnerFarDistance
		{
			get
			{
				return *this._repback__InnerFarDistance.GetValue();
			}
			set
			{
				this._repback__InnerFarDistance.SetValue(value);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0005525A File Offset: 0x0005345A
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0005526C File Offset: 0x0005346C
		[Property]
		[DefaultValue("255 255 255 255")]
		[Net]
		[Description("Set the Tint of the Particle, which will set control point 4 on the particle system.")]
		public unsafe Color ParticleTint
		{
			get
			{
				return *this._repback__ParticleTint.GetValue();
			}
			set
			{
				this._repback__ParticleTint.SetValue(value);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0005527B File Offset: 0x0005347B
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x00055289 File Offset: 0x00053489
		[Property]
		[Title("Start On")]
		[Net]
		[DefaultValue(true)]
		[Description("Sets if the particles are running by default")]
		public unsafe bool IsRunning
		{
			get
			{
				return *this._repback__IsRunning.GetValue();
			}
			set
			{
				this._repback__IsRunning.SetValue(value);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00055298 File Offset: 0x00053498
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x000552A6 File Offset: 0x000534A6
		[Property]
		[Title("Fade Time")]
		[Net]
		[DefaultValue(0.5f)]
		[Description("Sets the time particles take to fade in or out when turned on or off")]
		public unsafe float FadingTime
		{
			get
			{
				return *this._repback__FadingTime.GetValue();
			}
			set
			{
				this._repback__FadingTime.SetValue(value);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000552B5 File Offset: 0x000534B5
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x000552C3 File Offset: 0x000534C3
		[Property]
		[Net]
		[DefaultValue(100f)]
		[Description("Set the Density of the Particle, which will set control point 3 on the particle system.")]
		public unsafe float Density
		{
			get
			{
				return *this._repback__Density.GetValue();
			}
			set
			{
				this._repback__Density.SetValue(value);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000552FA File Offset: 0x000534FA
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000552D2 File Offset: 0x000534D2
		[ConVar.ReplicatedAttribute("precipitation_density")]
		public static float DensityScale
		{
			get
			{
				return PrecipitationEntity._repback__DensityScale;
			}
			set
			{
				if (PrecipitationEntity._repback__DensityScale == value)
				{
					return;
				}
				PrecipitationEntity._repback__DensityScale = value;
				if (Host.IsServer)
				{
					ConsoleSystem.SetValue("precipitation_density", value);
				}
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00055301 File Offset: 0x00053501
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("trigger");
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00055324 File Offset: 0x00053524
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Static, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouch = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InnerNearParticles = Particles.Create(this.InnerNearEffect);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InnerFarParticles = Particles.Create(this.InnerFarEffect);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OuterParticles = Particles.Create(this.OuterEffect);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000553A4 File Offset: 0x000535A4
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles innerNearParticles = this.InnerNearParticles;
			if (innerNearParticles != null)
			{
				innerNearParticles.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles innerFarParticles = this.InnerFarParticles;
			if (innerFarParticles != null)
			{
				innerFarParticles.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles outerParticles = this.OuterParticles;
			if (outerParticles == null)
			{
				return;
			}
			outerParticles.Destroy(false);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000553F5 File Offset: 0x000535F5
		public override void StartTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StartTouch(other);
			if (other != Local.Pawn)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetActive(true);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00055418 File Offset: 0x00053618
		public override void EndTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EndTouch(other);
			if (other != Local.Pawn)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetActive(false);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0005543C File Offset: 0x0005363C
		[ClientRpc]
		public void SetActive(bool isActive)
		{
			if (!this.SetActive__RpcProxy(isActive, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = isActive;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ParticleFadeTime = this.FadingTime;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsFading = true;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0005548A File Offset: 0x0005368A
		[Input]
		[Description("Change the particle density.")]
		public void ChangeDensity(float densitychanged)
		{
			this.Density = densitychanged;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00055493 File Offset: 0x00053693
		[Input]
		[Description("Start the particles.")]
		public void Start()
		{
			if (!this.IsRunning)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsRunning = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetActive(true);
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000554B5 File Offset: 0x000536B5
		[Input]
		[Description("Stop the particles.")]
		public void Stop()
		{
			if (this.IsRunning)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsRunning = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetActive(false);
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x000554D7 File Offset: 0x000536D7
		[Input]
		[Description("Freezes particles in current state.")]
		public void FreezeParticles()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetParticleState(false);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000554E5 File Offset: 0x000536E5
		[Input]
		[Description("UnFreezes particles if they were previously frozen.")]
		public void UnFreezeParticles()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetParticleState(true);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000554F3 File Offset: 0x000536F3
		[Input]
		[Description("Slow or speed up particles. 1 = normal, 0.5 = half speed, 2 = twice the speed.")]
		public void ParticleTimeScale(float particletimescale)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetParticleTimeScale(particletimescale);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00055504 File Offset: 0x00053704
		[Event.Tick.ClientAttribute]
		protected void ClientTick()
		{
			Vector3 forward = CurrentView.Rotation.Forward.WithZ(0f).Normal;
			float density = this.Density * PrecipitationEntity.DensityScale;
			float densityModifier = 1f;
			if (this.IsFading)
			{
				if (!this.IsActive)
				{
					densityModifier = 1f - this.ParticleFadeTime.Fraction;
				}
				else
				{
					densityModifier = this.ParticleFadeTime.Fraction;
				}
				if (this.ParticleFadeTime && this.IsActive)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IsFading = false;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			density *= densityModifier;
			if (this.OuterParticles != null)
			{
				if (Local.Pawn.IsValid())
				{
					this.OuterParticles.SetEntity(2, Local.Pawn, true);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OuterParticles.SetPosition(1, CurrentView.Position + Vector3.Up * 180f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OuterParticles.SetPosition(3, Vector3.Forward * density);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OuterParticles.SetPosition(4, this.ParticleTint * 255f);
			}
			if (this.InnerNearParticles != null)
			{
				if (Local.Pawn.IsValid())
				{
					this.InnerNearParticles.SetEntity(2, Local.Pawn, true);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerNearParticles.SetPosition(1, CurrentView.Position + Vector3.Up * 180f + forward * this.InnerNearDistance);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerNearParticles.SetPosition(3, Vector3.Forward * density);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerNearParticles.SetPosition(4, this.ParticleTint * 255f);
			}
			if (this.InnerFarParticles != null)
			{
				if (Local.Pawn.IsValid())
				{
					this.InnerFarParticles.SetEntity(2, Local.Pawn, true);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerFarParticles.SetPosition(1, CurrentView.Position + Vector3.Up * 180f + forward * this.InnerFarDistance);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerFarParticles.SetPosition(3, Vector3.Forward * density);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerFarParticles.SetPosition(4, this.ParticleTint * 255f);
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00055784 File Offset: 0x00053984
		[ClientRpc]
		public void SetParticleState(bool newState)
		{
			if (!this.SetParticleState__RpcProxy(newState, null))
			{
				return;
			}
			if (this.OuterParticles != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OuterParticles.Simulating = newState;
			}
			if (this.InnerNearParticles != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerNearParticles.Simulating = newState;
			}
			if (this.InnerFarParticles != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerFarParticles.Simulating = newState;
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000557F0 File Offset: 0x000539F0
		[ClientRpc]
		public void SetParticleTimeScale(float timeScale)
		{
			if (!this.SetParticleTimeScale__RpcProxy(timeScale, null))
			{
				return;
			}
			if (this.OuterParticles != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OuterParticles.TimeScale = timeScale;
			}
			if (this.InnerNearParticles != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerNearParticles.TimeScale = timeScale;
			}
			if (this.InnerFarParticles != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InnerFarParticles.TimeScale = timeScale;
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0005585C File Offset: 0x00053A5C
		protected bool SetActive__RpcProxy(bool isActive, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("SetActive", new object[]
				{
					isActive
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(440800226, this))
			{
				if (!NetRead.IsSupported(isActive))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] SetActive is not allowed to use Boolean for the parameter 'isActive'!");
					return false;
				}
				writer.Write<bool>(isActive);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000558F0 File Offset: 0x00053AF0
		protected bool SetParticleState__RpcProxy(bool newState, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("SetParticleState", new object[]
				{
					newState
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1528666685, this))
			{
				if (!NetRead.IsSupported(newState))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] SetParticleState is not allowed to use Boolean for the parameter 'newState'!");
					return false;
				}
				writer.Write<bool>(newState);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00055984 File Offset: 0x00053B84
		protected bool SetParticleTimeScale__RpcProxy(float timeScale, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("SetParticleTimeScale", new object[]
				{
					timeScale
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(11123601, this))
			{
				if (!NetRead.IsSupported(timeScale))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] SetParticleTimeScale is not allowed to use Single for the parameter 'timeScale'!");
					return false;
				}
				writer.Write<float>(timeScale);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00055A18 File Offset: 0x00053C18
		public void SetActive(To toTarget, bool isActive)
		{
			this.SetActive__RpcProxy(isActive, new To?(toTarget));
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00055A28 File Offset: 0x00053C28
		public void SetParticleState(To toTarget, bool newState)
		{
			this.SetParticleState__RpcProxy(newState, new To?(toTarget));
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00055A38 File Offset: 0x00053C38
		public void SetParticleTimeScale(To toTarget, float timeScale)
		{
			this.SetParticleTimeScale__RpcProxy(timeScale, new To?(toTarget));
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00055A48 File Offset: 0x00053C48
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == 11123601)
			{
				float __timeScale = 0f;
				__timeScale = read.ReadData<float>(__timeScale);
				if (!Prediction.WasPredicted("SetParticleTimeScale", new object[]
				{
					__timeScale
				}))
				{
					this.SetParticleTimeScale(__timeScale);
				}
				return;
			}
			if (id == 440800226)
			{
				bool __isActive = false;
				__isActive = read.ReadData<bool>(__isActive);
				if (!Prediction.WasPredicted("SetActive", new object[]
				{
					__isActive
				}))
				{
					this.SetActive(__isActive);
				}
				return;
			}
			if (id != 1528666685)
			{
				base.OnCallRemoteProcedure(id, read);
				return;
			}
			bool __newState = false;
			__newState = read.ReadData<bool>(__newState);
			if (!Prediction.WasPredicted("SetParticleState", new object[]
			{
				__newState
			}))
			{
				this.SetParticleState(__newState);
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00055B08 File Offset: 0x00053D08
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__InnerNearEffect, "InnerNearEffect", false, false);
			builder.Register<VarGeneric<string>>(ref this._repback__InnerFarEffect, "InnerFarEffect", false, false);
			builder.Register<VarGeneric<string>>(ref this._repback__OuterEffect, "OuterEffect", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__InnerNearDistance, "InnerNearDistance", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__InnerFarDistance, "InnerFarDistance", false, false);
			builder.Register<VarUnmanaged<Color>>(ref this._repback__ParticleTint, "ParticleTint", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsRunning, "IsRunning", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FadingTime, "FadingTime", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Density, "Density", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040006ED RID: 1773
		private Particles InnerNearParticles;

		// Token: 0x040006EE RID: 1774
		private Particles InnerFarParticles;

		// Token: 0x040006EF RID: 1775
		private Particles OuterParticles;

		// Token: 0x040006F0 RID: 1776
		private TimeUntil ParticleFadeTime;

		// Token: 0x040006F1 RID: 1777
		private bool IsFading;

		// Token: 0x040006F2 RID: 1778
		private bool IsActive;

		// Token: 0x040006F3 RID: 1779
		private const float HeightOffset = 180f;

		// Token: 0x040006F4 RID: 1780
		public static float _repback__DensityScale = 1f;

		// Token: 0x040006F5 RID: 1781
		private VarGeneric<string> _repback__InnerNearEffect = new VarGeneric<string>("particles/precipitation/rain_inner.vpcf");

		// Token: 0x040006F6 RID: 1782
		private VarGeneric<string> _repback__InnerFarEffect = new VarGeneric<string>("particles/precipitation/rain_inner.vpcf");

		// Token: 0x040006F7 RID: 1783
		private VarGeneric<string> _repback__OuterEffect = new VarGeneric<string>("particles/precipitation/rain_outer.vpcf");

		// Token: 0x040006F8 RID: 1784
		private VarUnmanaged<float> _repback__InnerNearDistance = new VarUnmanaged<float>(32f);

		// Token: 0x040006F9 RID: 1785
		private VarUnmanaged<float> _repback__InnerFarDistance = new VarUnmanaged<float>(180f);

		// Token: 0x040006FA RID: 1786
		private VarUnmanaged<Color> _repback__ParticleTint = new VarUnmanaged<Color>();

		// Token: 0x040006FB RID: 1787
		private VarUnmanaged<bool> _repback__IsRunning = new VarUnmanaged<bool>(true);

		// Token: 0x040006FC RID: 1788
		private VarUnmanaged<float> _repback__FadingTime = new VarUnmanaged<float>(0.5f);

		// Token: 0x040006FD RID: 1789
		private VarUnmanaged<float> _repback__Density = new VarUnmanaged<float>(100f);
	}
}
