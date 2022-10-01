using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.camera;
using StalkerRP;
using StalkerRP.Camera;

namespace Sandbox
{
	// Token: 0x020001B1 RID: 433
	public class StalkerBaseCamera : CameraMode
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00056C31 File Offset: 0x00054E31
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x00056C39 File Offset: 0x00054E39
		public Vector3 CurrentShake { get; set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00056C42 File Offset: 0x00054E42
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00056C4A File Offset: 0x00054E4A
		public Rotation CurrentFlinch { get; set; }

		// Token: 0x060015E0 RID: 5600 RVA: 0x00056C53 File Offset: 0x00054E53
		public StalkerBaseCamera()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerBaseCamera.Instance = this;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00056C7C File Offset: 0x00054E7C
		public override void Update()
		{
			Vector3 offset = this.CalculateShakeOffset();
			Rotation rotation = this.CalculateFlinchRotation();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			offset += this.GetHeadMotionOffset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			offset += this.GetLimpOffset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentShake = offset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentFlinch = rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position += offset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation *= rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation *= HeadMovementComponent.Instance.GetCameraRotation();
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00056D24 File Offset: 0x00054F24
		private Rotation GetHeadRotation()
		{
			StalkerPlayer player = base.Entity as StalkerPlayer;
			if (player == null)
			{
				return Rotation.Identity;
			}
			return player.HeadMovementComponent.GetCameraRotation();
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00056D54 File Offset: 0x00054F54
		private Vector3 GetHeadMotionOffset()
		{
			StalkerPlayer player = base.Entity as StalkerPlayer;
			if (player == null)
			{
				return Vector3.Zero;
			}
			return player.HeadMovementComponent.GetOffset();
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00056D84 File Offset: 0x00054F84
		private Vector3 GetLimpOffset()
		{
			StalkerPlayer player = base.Entity as StalkerPlayer;
			if (player == null)
			{
				return Vector3.Zero;
			}
			return player.HeadMovementComponent.GetLimpOffset();
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00056DB4 File Offset: 0x00054FB4
		private Vector3 CalculateShakeOffset()
		{
			Vector3 offset = Vector3.Zero;
			for (int i = 0; i < this.cameraShakes.Count; i++)
			{
				CameraShake shake = this.cameraShakes[i];
				if (shake.IsFinished)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.cameraShakes.RemoveAt(i);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					i--;
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					offset += shake.UpdateShake();
				}
			}
			return offset;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00056E24 File Offset: 0x00055024
		private Rotation CalculateFlinchRotation()
		{
			Rotation rotation = Rotation.Identity;
			for (int i = 0; i < this.hitFlinches.Count; i++)
			{
				HitFlinch flinch = this.hitFlinches[i];
				if (flinch.IsFinished)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.hitFlinches.RemoveAt(i);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					i--;
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					rotation *= flinch.UpdateRotation();
				}
			}
			return rotation;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00056E91 File Offset: 0x00055091
		public void AddShake(float magnitude, float roughness, float fadeInDuration = 0.1f, float fadeOutDuration = 0.5f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.cameraShakes.Add(new CameraShake(magnitude, roughness, fadeInDuration, fadeOutDuration));
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00056EAD File Offset: 0x000550AD
		public void AddHitFlinch(float vertMagnitude, float horizontalMagnitude, float fadeIn, float fadeOut)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hitFlinches.Add(new HitFlinch(vertMagnitude, horizontalMagnitude, fadeIn, fadeOut));
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00056ECC File Offset: 0x000550CC
		public void SnapToEyePosition()
		{
			Entity pawn = Local.Pawn;
			if (pawn == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = pawn.EyePosition;
		}

		// Token: 0x0400071B RID: 1819
		public static StalkerBaseCamera Instance;

		// Token: 0x0400071C RID: 1820
		private List<CameraShake> cameraShakes = new List<CameraShake>();

		// Token: 0x0400071D RID: 1821
		private List<HitFlinch> hitFlinches = new List<HitFlinch>();
	}
}
