using System;
using System.Runtime.CompilerServices;
using Kryz.Tweening;
using Sandbox;
using Sandbox.player.assets;

namespace StalkerRP
{
	// Token: 0x02000036 RID: 54
	public class HeadMovementComponent : EntityComponent<StalkerPlayer>
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000D81D File Offset: 0x0000BA1D
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000D824 File Offset: 0x0000BA24
		public static AnimatedEntity CameraAnimationEntity { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000D82C File Offset: 0x0000BA2C
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000D834 File Offset: 0x0000BA34
		[ConVar.ClientAttribute(null)]
		[DefaultValue(true)]
		public bool stalker_viewbob_enabled { get; set; } = true;

		// Token: 0x060001FE RID: 510 RVA: 0x0000D83D File Offset: 0x0000BA3D
		public HeadMovementComponent()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HeadMovementComponent.Instance = this;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D86D File Offset: 0x0000BA6D
		protected override void OnActivate()
		{
			if (base.Entity.IsClient)
			{
				HeadMovementComponent.CameraAnimationEntity = new AnimatedEntity("models/stalker/camera_animations/camera_headshot.vmdl");
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public Rotation GetCameraRotation()
		{
			if (HeadMovementComponent.CameraAnimationEntity == null)
			{
				return Rotation.Identity;
			}
			Transform? transform = HeadMovementComponent.CameraAnimationEntity.GetAttachment("camera", true);
			if (transform != null)
			{
				return transform.Value.Rotation;
			}
			return Rotation.Identity;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		private float GetHeadBobFraction()
		{
			return (base.Entity.Velocity.Length / base.Entity.Stats.CameraMotion.ViewBobSpeedThreshold).Clamp(0f, 1f);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000D91C File Offset: 0x0000BB1C
		public Vector3 GetLimpOffset()
		{
			if (base.Entity.HealthComponent.BrokenLegs <= 0)
			{
				return Vector3.Zero;
			}
			float frac = this.GetHeadBobFraction();
			CameraMotion camMotion = base.Entity.Stats.CameraMotion;
			bool isRunning = base.Entity.StaminaComponent.IsRunning;
			float y = Time.Now % this.limpCycle;
			if (y > this.limpDuration || base.Entity.GroundEntity == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.limpOffset = this.limpOffset.LerpTo(0f, Time.Delta * 10f);
				return this.limpOffset;
			}
			float limpFraction = y / this.limpDuration;
			float sinOffset;
			if (limpFraction < 0.5f)
			{
				sinOffset = -EasingFunctions.InQuad(limpFraction / 0.5f);
			}
			else
			{
				sinOffset = -EasingFunctions.OutQuad((1f - limpFraction) / 0.5f);
			}
			Vector3 z = Vector3.Up * sinOffset * camMotion.ViewBobMagnitude * frac * 7f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.limpOffset = this.limpOffset.LerpTo(z, Time.Delta * 10f);
			return this.limpOffset;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000DA54 File Offset: 0x0000BC54
		[Description("Called every camere build for stalker base cameras.")]
		public Vector3 GetOffset()
		{
			if (!this.stalker_viewbob_enabled || base.Entity.GroundEntity == null)
			{
				return Vector3.Zero;
			}
			float frac = this.GetHeadBobFraction();
			CameraMotion camMotion = base.Entity.Stats.CameraMotion;
			float runningMult = base.Entity.StaminaComponent.IsRunning ? camMotion.ViewBobRunningMultiplier : 1f;
			float sinOffset = MathF.Sin(Time.Now * camMotion.ViewBobFrequency * 2f * runningMult);
			float sinOffset2 = MathF.Sin(Time.Now * camMotion.ViewBobFrequency * runningMult) / 2f;
			Vector3 c = Vector3.Up * sinOffset * camMotion.ViewBobMagnitude * frac;
			Vector3 y = base.Entity.EyeRotation.Left.WithZ(0f).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= sinOffset2 * camMotion.ViewBobMagnitude * frac;
			return (c + y) * runningMult;
		}

		// Token: 0x040000AE RID: 174
		public static HeadMovementComponent Instance;

		// Token: 0x040000B1 RID: 177
		private float limpCycle = 1f;

		// Token: 0x040000B2 RID: 178
		private float limpDuration = 0.5f;

		// Token: 0x040000B3 RID: 179
		private Vector3 limpOffset;

		// Token: 0x040000B4 RID: 180
		private Vector3 offset;
	}
}
