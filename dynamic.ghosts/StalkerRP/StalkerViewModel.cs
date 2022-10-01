using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200005C RID: 92
	public class StalkerViewModel : BaseViewModel
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000142CF File Offset: 0x000124CF
		// (set) Token: 0x060003BE RID: 958 RVA: 0x000142D6 File Offset: 0x000124D6
		[ConVar.ClientAttribute(null)]
		[DefaultValue(70f)]
		public static float stalker_viewmodel_fov { get; set; } = 70f;

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000142DE File Offset: 0x000124DE
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x000142E6 File Offset: 0x000124E6
		public Vector3 PositionOffset { get; set; } = Vector3.Zero;

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000142EF File Offset: 0x000124EF
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x000142F7 File Offset: 0x000124F7
		public Angles RotationOffset { get; set; } = Angles.Zero;

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00014300 File Offset: 0x00012500
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00014308 File Offset: 0x00012508
		[DefaultValue(0)]
		public float ViewModelFOVReduction { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00014311 File Offset: 0x00012511
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00014319 File Offset: 0x00012519
		[DefaultValue(false)]
		public bool ReduceSwing { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00014322 File Offset: 0x00012522
		protected float SwingInfluence
		{
			get
			{
				return 0.02f;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00014329 File Offset: 0x00012529
		protected float ReturnSpeed
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00014330 File Offset: 0x00012530
		protected float MaxOffsetLength
		{
			get
			{
				return 6f;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00014337 File Offset: 0x00012537
		protected float BobCycleTime
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001433E File Offset: 0x0001253E
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00014346 File Offset: 0x00012546
		public Vector3 BobDirection { get; set; }

		// Token: 0x060003CD RID: 973 RVA: 0x00014350 File Offset: 0x00012550
		public override void PostCameraSetup(ref CameraSetup camSetup)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostCameraSetup(ref camSetup);
			if (!Local.Pawn.IsValid())
			{
				return;
			}
			if (!this.activated)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lastPitch = camSetup.Rotation.Pitch();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lastYaw = camSetup.Rotation.Yaw();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.activated = true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = camSetup.Rotation * this.RotationOffset.ToRotation();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = camSetup.Position + this.Rotation.Forward * this.PositionOffset.y + this.Rotation.Right * this.PositionOffset.x + this.Rotation.Up * this.PositionOffset.z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			camSetup.ViewModel.FieldOfView = StalkerViewModel.stalker_viewmodel_fov - this.ViewModelFOVReduction;
			int cameraBoneIndex = base.GetBoneIndex("camera");
			if (cameraBoneIndex != -1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				camSetup.Rotation *= this.Rotation.Inverse * base.GetBoneTransform(cameraBoneIndex, true).Rotation;
			}
			Vector3 playerVelocity = Local.Pawn.Velocity;
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player != null)
			{
				PawnController controller = player.GetActiveController();
				if (controller != null && controller.HasTag("noclip"))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					playerVelocity = Vector3.Zero;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Position += player.HeadMovementComponent.GetLimpOffset() / 5f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Rotation *= player.HealthComponent.GetArmsSway();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Rotation *= HeadMovementComponent.Instance.GetCameraRotation().Inverse / 2f;
			}
			float newPitch = this.Rotation.Pitch();
			float newYaw = this.Rotation.Yaw();
			float pitchDelta = Angles.NormalizeAngle(newPitch - this.lastPitch);
			float yawDelta = Angles.NormalizeAngle(this.lastYaw - newYaw);
			float verticalDelta = playerVelocity.z * Time.Delta;
			Vector3 viewDown = Rotation.FromPitch(newPitch).Up * -1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			verticalDelta *= 1f - MathF.Abs(viewDown.Cross(Vector3.Down).y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pitchDelta -= verticalDelta * 1f;
			Vector3 offset = this.CalcSwingOffset(pitchDelta, yawDelta);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			offset += this.CalcBobbingOffset(playerVelocity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position += this.Rotation * offset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPitch = newPitch;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastYaw = newYaw;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00014690 File Offset: 0x00012890
		protected Vector3 CalcSwingOffset(float pitchDelta, float yawDelta)
		{
			Vector3 swingVelocity = new Vector3(0f, yawDelta, pitchDelta);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.swingOffset -= this.swingOffset * this.ReturnSpeed * Time.Delta * (this.ReduceSwing ? 15f : 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.swingOffset += swingVelocity * this.SwingInfluence;
			if (this.swingOffset.Length > this.MaxOffsetLength)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.swingOffset = this.swingOffset.Normal * this.MaxOffsetLength;
			}
			return this.swingOffset;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00014754 File Offset: 0x00012954
		protected Vector3 CalcBobbingOffset(Vector3 velocity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.bobAnim += Time.Delta * this.BobCycleTime;
			float twoPI = 6.2831855f;
			if (this.bobAnim > twoPI)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.bobAnim -= twoPI;
			}
			float speed = new Vector2(velocity.x, velocity.y).Length;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			speed = (((double)speed > 10.0) ? speed : 0f);
			Vector3 offset = this.BobDirection * (this.ReduceSwing ? 0.3f : 1f) * (speed * 0.005f) * MathF.Cos(this.bobAnim);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			offset = offset.WithZ(-MathF.Abs(offset.z));
			return offset;
		}

		// Token: 0x04000128 RID: 296
		private Vector3 swingOffset;

		// Token: 0x04000129 RID: 297
		private float lastPitch;

		// Token: 0x0400012A RID: 298
		private float lastYaw;

		// Token: 0x0400012B RID: 299
		private float bobAnim;

		// Token: 0x0400012C RID: 300
		private bool activated;
	}
}
