using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000058 RID: 88
	public class StalkerProjectileBase : ModelEntity
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0001361E File Offset: 0x0001181E
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00013626 File Offset: 0x00011826
		[DefaultValue(false)]
		public bool HasTouchedWorld { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0001362F File Offset: 0x0001182F
		protected virtual string ModelPath
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00013636 File Offset: 0x00011836
		[Description("If this is false, the physics hull will be set up as a box with HullSize mins and maxs.")]
		protected virtual bool UsePhysicsModel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00013639 File Offset: 0x00011839
		protected virtual Vector3 HullSize
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00013645 File Offset: 0x00011845
		protected virtual float LiveTime
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0001364C File Offset: 0x0001184C
		[Description("Mass of the projectile in kilograms.")]
		protected virtual float Mass
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00013653 File Offset: 0x00011853
		private float FadeOutTime
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001365C File Offset: 0x0001185C
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("entity");
			if (!string.IsNullOrWhiteSpace(this.ModelPath))
			{
				base.SetModel(this.ModelPath);
			}
			if (this.UsePhysicsModel)
			{
				base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			}
			else
			{
				base.SetupPhysicsFromOBB(PhysicsMotionType.Dynamic, -this.HullSize, this.HullSize);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.Mass = this.Mass;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(this.LiveTime);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000136EB File Offset: 0x000118EB
		public virtual void OnThrown()
		{
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000136F0 File Offset: 0x000118F0
		[Event.FrameAttribute]
		private void UpdateGibParticles()
		{
			float fadeOutFrac = this.TimeSinceCreated - (this.LiveTime - this.FadeOutTime);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			fadeOutFrac /= this.FadeOutTime;
			if (fadeOutFrac > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.RenderColor = base.RenderColor.WithAlpha(1f - fadeOutFrac);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001374D File Offset: 0x0001194D
		public override void StartTouch(Entity other)
		{
			if (other.IsWorld)
			{
				this.HasTouchedWorld = true;
			}
		}

		// Token: 0x04000118 RID: 280
		private TimeSince TimeSinceCreated = 0f;
	}
}
