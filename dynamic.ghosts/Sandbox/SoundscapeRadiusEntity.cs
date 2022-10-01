using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000185 RID: 389
	[HammerEntity]
	[ClassName("snd_soundscape")]
	[Title("Soundscape Radius")]
	[Category("Sound")]
	[Icon("spatial_tracking")]
	[EditorSprite("materials/editor/env_soundscape.vmat")]
	[Sphere("radius", 255, 255, 255, false)]
	[Description("Declares a sphere in which the specified soundscape will be active. If the ear is within two radiuses, we'll use the closest. The entity needs to be within the PVS for it to be active.")]
	public class SoundscapeRadiusEntity : Entity
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00048F18 File Offset: 0x00047118
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00048F26 File Offset: 0x00047126
		[Net]
		[Property]
		[DefaultValue(true)]
		[Description("Whether the soundscape is active or not.")]
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

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00048F35 File Offset: 0x00047135
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x00048F42 File Offset: 0x00047142
		[Net]
		[Property]
		[ResourceType("sndscape")]
		public string Soundscape
		{
			get
			{
				return this._repback__Soundscape.GetValue();
			}
			set
			{
				this._repback__Soundscape.SetValue(value);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00048F50 File Offset: 0x00047150
		// (set) Token: 0x060012CC RID: 4812 RVA: 0x00048F5E File Offset: 0x0004715E
		[Net]
		[Property]
		[DefaultValue(128f)]
		[Description("Radius of this soundscape.")]
		public unsafe float Radius
		{
			get
			{
				return *this._repback__Radius.GetValue();
			}
			set
			{
				this._repback__Radius.SetValue(value);
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00048F6D File Offset: 0x0004716D
		[Input]
		[Description("Become enabled")]
		protected void Enable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = true;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00048F7B File Offset: 0x0004717B
		[Input]
		[Description("Become disabled")]
		protected void Disable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = false;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00048F89 File Offset: 0x00047189
		[Input]
		[Description("Toggle between enabled and disabled")]
		protected void Toggle()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = !this.Enabled;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00048FA0 File Offset: 0x000471A0
		[Description("Returns true if the soundscape is enabled and audible from this location")]
		public bool TestPosition(Vector3 position)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("TestPosition");
			return this.Enabled && (position - this.Position).Length < this.Radius;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00048FE4 File Offset: 0x000471E4
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Enabled, "Enabled", false, false);
			builder.Register<VarGeneric<string>>(ref this._repback__Soundscape, "Soundscape", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Radius, "Radius", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000601 RID: 1537
		private VarUnmanaged<bool> _repback__Enabled = new VarUnmanaged<bool>(true);

		// Token: 0x04000602 RID: 1538
		private VarGeneric<string> _repback__Soundscape = new VarGeneric<string>();

		// Token: 0x04000603 RID: 1539
		private VarUnmanaged<float> _repback__Radius = new VarUnmanaged<float>(128f);
	}
}
