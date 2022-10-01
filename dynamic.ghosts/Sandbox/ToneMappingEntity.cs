using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A8 RID: 424
	[Library("env_tonemap_controller")]
	[Title("Tonemap Controller")]
	[Category("Effects")]
	[Icon("contrast")]
	[HammerEntity]
	[Global("tonemap")]
	[EditorSprite("editor/env_tonemap_controller.vmat")]
	[ToneMap]
	[Description("An entity that controls the HDR tonemapping for all players on the map. Think of it as a method of controlling the exposure of the player's eyes. Only the last activated entity will affect the players. (works like a stack)")]
	public class ToneMappingEntity : Entity, ITonemapEntity
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x000560B9 File Offset: 0x000542B9
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x000560C7 File Offset: 0x000542C7
		[Property]
		[Net]
		[DefaultValue(true)]
		[Description("Whether this entity is enabled.")]
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

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x000560D6 File Offset: 0x000542D6
		// (set) Token: 0x0600158C RID: 5516 RVA: 0x000560E4 File Offset: 0x000542E4
		[Property("minexposure", null)]
		[Net]
		[DefaultValue(0.25f)]
		[Description("Minimum auto exposure scale")]
		public unsafe float MinExposure
		{
			get
			{
				return *this._repback__MinExposure.GetValue();
			}
			set
			{
				this._repback__MinExposure.SetValue(value);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x000560F3 File Offset: 0x000542F3
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x00056101 File Offset: 0x00054301
		[Property("maxexposure", null)]
		[Net]
		[DefaultValue(8f)]
		[Description("Maximum auto exposure scale")]
		public unsafe float MaxExposure
		{
			get
			{
				return *this._repback__MaxExposure.GetValue();
			}
			set
			{
				this._repback__MaxExposure.SetValue(value);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00056110 File Offset: 0x00054310
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x0005611E File Offset: 0x0005431E
		[Property("percent_bright_pixels", null)]
		[Net]
		[DefaultValue(-1f)]
		[Description("Set a target for percentage of pixels above a certain brightness. (-1 for default engine behavior)")]
		public unsafe float PercentBrightPixels
		{
			get
			{
				return *this._repback__PercentBrightPixels.GetValue();
			}
			set
			{
				this._repback__PercentBrightPixels.SetValue(value);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0005612D File Offset: 0x0005432D
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x0005613B File Offset: 0x0005433B
		[Property("percent_target", null)]
		[Net]
		[DefaultValue(-1f)]
		[Description("Set a custom brightness target to go along with 'Target Bright Pixel Percentage'. (-1 for default engine behavior)")]
		public unsafe float PercentTarget
		{
			get
			{
				return *this._repback__PercentTarget.GetValue();
			}
			set
			{
				this._repback__PercentTarget.SetValue(value);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x0005614A File Offset: 0x0005434A
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x00056158 File Offset: 0x00054358
		[Property("rate", null)]
		[Net]
		[DefaultValue(1f)]
		public unsafe float Rate
		{
			get
			{
				return *this._repback__Rate.GetValue();
			}
			set
			{
				this._repback__Rate.SetValue(value);
			}
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00056167 File Offset: 0x00054367
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			if (this.Enabled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TurnOnEffect();
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00056187 File Offset: 0x00054387
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000561A0 File Offset: 0x000543A0
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TurnOffEffect();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000561B0 File Offset: 0x000543B0
		[ClientRpc]
		protected void TurnOnEffect()
		{
			if (!this.TurnOnEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			EnginePostProcess.AddToneMappingEntity(this);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x000561DC File Offset: 0x000543DC
		[ClientRpc]
		protected void TurnOffEffect()
		{
			if (!this.TurnOffEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			EnginePostProcess.RemoveToneMappingEntity(this);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00056206 File Offset: 0x00054406
		[Input]
		[Description("Enable the tonemapping entity/effect.")]
		public void Enable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = true;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00056214 File Offset: 0x00054414
		[Input]
		[Description("Disable the tonemapping entity/effect.")]
		public void Disable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = false;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00056224 File Offset: 0x00054424
		public SceneTonemapParameters GetTonemapParams()
		{
			SceneTonemapParameters param = new SceneTonemapParameters();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flAutoExposureMin = this.MinExposure;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flAutoExposureMax = this.MaxExposure;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flTonemapPercentTarget = this.PercentTarget;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flTonemapPercentBrightPixels = this.PercentBrightPixels;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flTonemapMinAvgLum = -1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flRate = this.Rate;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flAccelerateExposureDown = -1f;
			return param;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x000562B8 File Offset: 0x000544B8
		protected bool TurnOnEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("TurnOnEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(103459177, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00056318 File Offset: 0x00054518
		protected bool TurnOffEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("TurnOffEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1558069217, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00056378 File Offset: 0x00054578
		protected void TurnOnEffect(To toTarget)
		{
			this.TurnOnEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00056387 File Offset: 0x00054587
		protected void TurnOffEffect(To toTarget)
		{
			this.TurnOffEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00056398 File Offset: 0x00054598
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1558069217)
			{
				if (!Prediction.WasPredicted("TurnOffEffect", Array.Empty<object>()))
				{
					this.TurnOffEffect();
				}
				return;
			}
			if (id == 103459177)
			{
				if (!Prediction.WasPredicted("TurnOnEffect", Array.Empty<object>()))
				{
					this.TurnOnEffect();
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x000563F0 File Offset: 0x000545F0
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Enabled, "Enabled", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MinExposure, "MinExposure", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MaxExposure, "MaxExposure", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__PercentBrightPixels, "PercentBrightPixels", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__PercentTarget, "PercentTarget", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Rate, "Rate", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000706 RID: 1798
		private VarUnmanaged<bool> _repback__Enabled = new VarUnmanaged<bool>(true);

		// Token: 0x04000707 RID: 1799
		private VarUnmanaged<float> _repback__MinExposure = new VarUnmanaged<float>(0.25f);

		// Token: 0x04000708 RID: 1800
		private VarUnmanaged<float> _repback__MaxExposure = new VarUnmanaged<float>(8f);

		// Token: 0x04000709 RID: 1801
		private VarUnmanaged<float> _repback__PercentBrightPixels = new VarUnmanaged<float>(-1f);

		// Token: 0x0400070A RID: 1802
		private VarUnmanaged<float> _repback__PercentTarget = new VarUnmanaged<float>(-1f);

		// Token: 0x0400070B RID: 1803
		private VarUnmanaged<float> _repback__Rate = new VarUnmanaged<float>(1f);
	}
}
