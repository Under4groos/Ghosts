using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A4 RID: 420
	[Library("post_processing_entity")]
	[HammerEntity]
	[PostProcessingVolume]
	[EditorSprite("editor/post_process.png")]
	[HideProperty("enable_shadows")]
	[Title("Post Processing Controller")]
	[Category("Effects")]
	[Icon("filter")]
	[Description("Applies given post processing effect to all players. Only the last activated entity will have an effect. (works like a stack) Overridden by <see cref=\"T:Sandbox.PostProcessingVolume\">post processing volume</see>.")]
	public class PostProcessingEntity : ModelEntity, IPostProcessEntity, ITonemapEntity
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00054C3C File Offset: 0x00052E3C
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x00054C49 File Offset: 0x00052E49
		[Property("postprocessing", null)]
		[Title("Post Processing Resource Name")]
		[Net]
		[ResourceType("vpost")]
		public string PostProcessingFile
		{
			get
			{
				return this._repback__PostProcessingFile.GetValue();
			}
			set
			{
				this._repback__PostProcessingFile.SetValue(value);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00054C57 File Offset: 0x00052E57
		// (set) Token: 0x0600151C RID: 5404 RVA: 0x00054C65 File Offset: 0x00052E65
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

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00054C74 File Offset: 0x00052E74
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x00054C82 File Offset: 0x00052E82
		[Property("fadetime", null)]
		[Net]
		[DefaultValue(1f)]
		[Description("Time to fade to these post-processing settings in seconds")]
		public unsafe float FadeTime
		{
			get
			{
				return *this._repback__FadeTime.GetValue();
			}
			set
			{
				this._repback__FadeTime.SetValue(value);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00054C91 File Offset: 0x00052E91
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x00054C9F File Offset: 0x00052E9F
		[Property("enableexposure", null)]
		[Category("Exposure")]
		[Net]
		[DefaultValue(true)]
		[Description("Use exposure settings for this entity? These will be overwritten by a tonemap controller if one is present.")]
		public unsafe bool EnableExposure
		{
			get
			{
				return *this._repback__EnableExposure.GetValue();
			}
			set
			{
				this._repback__EnableExposure.SetValue(value);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00054CAE File Offset: 0x00052EAE
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x00054CBC File Offset: 0x00052EBC
		[Property("minexposure", null)]
		[Category("Exposure")]
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

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00054CCB File Offset: 0x00052ECB
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x00054CD9 File Offset: 0x00052ED9
		[Property("maxexposure", null)]
		[Category("Exposure")]
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

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00054CE8 File Offset: 0x00052EE8
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x00054CF6 File Offset: 0x00052EF6
		[Property("exposurecompensation", null)]
		[Category("Exposure")]
		[Net]
		[DefaultValue(0f)]
		[Description("Number of stops to adjust auto-exposure by")]
		public unsafe float ExposureCompensation
		{
			get
			{
				return *this._repback__ExposureCompensation.GetValue();
			}
			set
			{
				this._repback__ExposureCompensation.SetValue(value);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00054D05 File Offset: 0x00052F05
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x00054D13 File Offset: 0x00052F13
		[Property("exposurespeedup", null)]
		[Category("Exposure")]
		[Net]
		[DefaultValue(1f)]
		public unsafe float ExposureFadeSpeedUp
		{
			get
			{
				return *this._repback__ExposureFadeSpeedUp.GetValue();
			}
			set
			{
				this._repback__ExposureFadeSpeedUp.SetValue(value);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00054D22 File Offset: 0x00052F22
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x00054D30 File Offset: 0x00052F30
		[Property("exposurespeeddown", null)]
		[Category("Exposure")]
		[Net]
		[DefaultValue(1f)]
		public unsafe float ExposureFadeSpeedDown
		{
			get
			{
				return *this._repback__ExposureFadeSpeedDown.GetValue();
			}
			set
			{
				this._repback__ExposureFadeSpeedDown.SetValue(value);
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00054D3F File Offset: 0x00052F3F
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TurnOnEffect();
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00054D57 File Offset: 0x00052F57
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00054D70 File Offset: 0x00052F70
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TurnOffEffect();
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00054D80 File Offset: 0x00052F80
		[ClientRpc]
		protected void TurnOnEffect()
		{
			if (!this.TurnOnEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			EnginePostProcess.AddPostProcessEntity(this);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00054DAC File Offset: 0x00052FAC
		[ClientRpc]
		protected void TurnOffEffect()
		{
			if (!this.TurnOffEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			EnginePostProcess.RemovePostProcessEntity(this);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00054DD6 File Offset: 0x00052FD6
		[Input]
		[Description("Enable this post processing effect.")]
		public void Enable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = true;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00054DE4 File Offset: 0x00052FE4
		[Input]
		[Description("Disable this post processing effect.")]
		public void Disable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = false;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00054DF4 File Offset: 0x00052FF4
		public SceneTonemapParameters GetTonemapParams()
		{
			SceneTonemapParameters param = new SceneTonemapParameters();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flAutoExposureMin = this.MinExposure;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flAutoExposureMax = this.MaxExposure;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flExposureCompensationScalar = (float)Math.Pow(2.0, (double)this.ExposureCompensation);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flRate = this.ExposureFadeSpeedUp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			param.m_flAccelerateExposureDown = this.ExposureFadeSpeedDown;
			return param;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00054E74 File Offset: 0x00053074
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
			using (NetWrite writer = NetWrite.StartRpc(-1978157295, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00054ED4 File Offset: 0x000530D4
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
			using (NetWrite writer = NetWrite.StartRpc(-1847619625, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00054F34 File Offset: 0x00053134
		protected void TurnOnEffect(To toTarget)
		{
			this.TurnOnEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00054F43 File Offset: 0x00053143
		protected void TurnOffEffect(To toTarget)
		{
			this.TurnOffEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00054F54 File Offset: 0x00053154
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1978157295)
			{
				if (!Prediction.WasPredicted("TurnOnEffect", Array.Empty<object>()))
				{
					this.TurnOnEffect();
				}
				return;
			}
			if (id != -1847619625)
			{
				base.OnCallRemoteProcedure(id, read);
				return;
			}
			if (!Prediction.WasPredicted("TurnOffEffect", Array.Empty<object>()))
			{
				this.TurnOffEffect();
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00054FAC File Offset: 0x000531AC
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__PostProcessingFile, "PostProcessingFile", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Enabled, "Enabled", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FadeTime, "FadeTime", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__EnableExposure, "EnableExposure", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MinExposure, "MinExposure", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MaxExposure, "MaxExposure", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__ExposureCompensation, "ExposureCompensation", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__ExposureFadeSpeedUp, "ExposureFadeSpeedUp", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__ExposureFadeSpeedDown, "ExposureFadeSpeedDown", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040006E4 RID: 1764
		private VarGeneric<string> _repback__PostProcessingFile = new VarGeneric<string>();

		// Token: 0x040006E5 RID: 1765
		private VarUnmanaged<bool> _repback__Enabled = new VarUnmanaged<bool>(true);

		// Token: 0x040006E6 RID: 1766
		private VarUnmanaged<float> _repback__FadeTime = new VarUnmanaged<float>(1f);

		// Token: 0x040006E7 RID: 1767
		private VarUnmanaged<bool> _repback__EnableExposure = new VarUnmanaged<bool>(true);

		// Token: 0x040006E8 RID: 1768
		private VarUnmanaged<float> _repback__MinExposure = new VarUnmanaged<float>(0.25f);

		// Token: 0x040006E9 RID: 1769
		private VarUnmanaged<float> _repback__MaxExposure = new VarUnmanaged<float>(8f);

		// Token: 0x040006EA RID: 1770
		private VarUnmanaged<float> _repback__ExposureCompensation = new VarUnmanaged<float>(0f);

		// Token: 0x040006EB RID: 1771
		private VarUnmanaged<float> _repback__ExposureFadeSpeedUp = new VarUnmanaged<float>(1f);

		// Token: 0x040006EC RID: 1772
		private VarUnmanaged<float> _repback__ExposureFadeSpeedDown = new VarUnmanaged<float>(1f);
	}
}
