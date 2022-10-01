using System;
using System.Runtime.CompilerServices;
using Kryz.Tweening;
using Sandbox;
using StalkerRP.PostProcessing;

namespace StalkerRP
{
	// Token: 0x02000014 RID: 20
	public class FirstPersonCamera : StalkerBaseCamera
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00007F9B File Offset: 0x0000619B
		private float psyBoltZoomInTime
		{
			get
			{
				return 0.75f;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00007FA2 File Offset: 0x000061A2
		private float psyBoltZoomOutTime
		{
			get
			{
				return 0.3f;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00007FA9 File Offset: 0x000061A9
		private float psyBoltHoverTime
		{
			get
			{
				return 0.3f;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007FB0 File Offset: 0x000061B0
		public override void Activated()
		{
			Entity pawn = Local.Pawn;
			if (pawn == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.absolutePosition = pawn.EyePosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = pawn.EyePosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = pawn.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPos = base.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ZNear = 5f;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000801B File Offset: 0x0000621B
		public override void Update()
		{
			if (this.doingPsyboltEffect)
			{
				this.PsyBoltUpdate();
			}
			else
			{
				this.DefaultUpdate();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Update();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00008040 File Offset: 0x00006240
		private void DefaultUpdate()
		{
			Entity pawn = Local.Pawn;
			if (pawn == null)
			{
				return;
			}
			Vector3 eyePos = pawn.EyePosition;
			if (eyePos.Distance(this.lastPos) < 300f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.absolutePosition = Vector3.Lerp(eyePos.WithZ(this.lastPos.z), eyePos, 40f * Time.Delta, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = Vector3.Lerp(eyePos.WithZ(this.lastPos.z), eyePos, 40f * Time.Delta, true);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.absolutePosition = eyePos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = eyePos;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = this.absolutePosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = pawn.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFromLean();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = pawn;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPos = this.absolutePosition;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00008134 File Offset: 0x00006334
		private void UpdateFromLean()
		{
			Entity pawn = Local.Pawn;
			StalkerPlayer player = pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position += player.LeanComponent.GetLeanOffset(pawn.EyeRotation);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation *= player.LeanComponent.GetLeanRotation();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000819C File Offset: 0x0000639C
		private void PsyBoltUpdate()
		{
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player == null || this.psyBoltTarget == null)
			{
				return;
			}
			if (this.timeSincePsyBolyStarted < this.psyBoltZoomInTime)
			{
				float frac = EasingFunctions.OutCubic(this.timeSincePsyBolyStarted / this.psyBoltZoomInTime);
				Vector3 targetPos = this.psyBoltTarget.EyePosition + this.psyBoltTarget.Rotation.Forward * 150f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = this.positionStart.LerpTo(targetPos, frac);
				Rotation newRot = Rotation.LookAt((this.psyBoltTarget.EyePosition - base.Position).Normal);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Rotation = Rotation.Lerp(base.Rotation, newRot, Time.Delta * 12f, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				player.EyeRotation = base.Rotation;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				PsyPostProcessManager.Instance.SetControllerVignette(frac, 1.9f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				PsyPostProcessManager.Instance.SetControllerContrast(1f + 0.09f * frac);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.FieldOfView = 150f.LerpTo(30f, frac, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Viewer = null;
			}
			else if (this.timeSincePsyBolyStarted > this.psyBoltZoomInTime + this.psyBoltHoverTime)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.doingPsyboltEffect = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.FieldOfView = 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				AnimatedEntity cameraAnimationEntity = HeadMovementComponent.CameraAnimationEntity;
				if (cameraAnimationEntity != null)
				{
					cameraAnimationEntity.SetAnimParameter("bHeadShot", true);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				PsyPostProcessManager.Instance.SetControllerVignette(0f, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				PsyPostProcessManager.Instance.SetControllerBlur(1f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				PsyPostProcessManager.Instance.SetControllerContrast(1f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.velocity = (base.Position - this.lastPos) / Time.Delta;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPos = base.Position;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000083B8 File Offset: 0x000065B8
		public void DoPsyBoltZoom(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.positionStart = base.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyBoltTarget = target;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.doingPsyboltEffect = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSincePsyBolyStarted = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddShake(15f, 5f, -1f, this.psyBoltZoomInTime + 0.125f);
		}

		// Token: 0x04000035 RID: 53
		private Vector3 lastPos;

		// Token: 0x04000036 RID: 54
		private bool doingPsyboltEffect;

		// Token: 0x04000037 RID: 55
		private Entity psyBoltTarget;

		// Token: 0x04000038 RID: 56
		private Vector3 velocity;

		// Token: 0x04000039 RID: 57
		private TimeSince timeSincePsyBolyStarted;

		// Token: 0x0400003A RID: 58
		private Vector3 absolutePosition;

		// Token: 0x0400003B RID: 59
		private Vector3 positionStart;
	}
}
