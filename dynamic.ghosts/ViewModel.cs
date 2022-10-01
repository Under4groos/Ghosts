using System;
using System.Runtime.CompilerServices;
using Sandbox;

// Token: 0x0200000D RID: 13
public class ViewModel : BaseViewModel
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000074 RID: 116 RVA: 0x00006B18 File Offset: 0x00004D18
	protected float SwingInfluence
	{
		get
		{
			return 0.05f;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000075 RID: 117 RVA: 0x00006B1F File Offset: 0x00004D1F
	protected float ReturnSpeed
	{
		get
		{
			return 5f;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000076 RID: 118 RVA: 0x00006B26 File Offset: 0x00004D26
	protected float MaxOffsetLength
	{
		get
		{
			return 10f;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000077 RID: 119 RVA: 0x00006B2D File Offset: 0x00004D2D
	protected float BobCycleTime
	{
		get
		{
			return 7f;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000078 RID: 120 RVA: 0x00006B34 File Offset: 0x00004D34
	protected Vector3 BobDirection
	{
		get
		{
			return new Vector3(0f, 1f, 0.5f);
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000079 RID: 121 RVA: 0x00006B4A File Offset: 0x00004D4A
	// (set) Token: 0x0600007A RID: 122 RVA: 0x00006B52 File Offset: 0x00004D52
	public float YawInertia { get; private set; }

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600007B RID: 123 RVA: 0x00006B5B File Offset: 0x00004D5B
	// (set) Token: 0x0600007C RID: 124 RVA: 0x00006B63 File Offset: 0x00004D63
	public float PitchInertia { get; private set; }

	// Token: 0x0600007D RID: 125 RVA: 0x00006B6C File Offset: 0x00004D6C
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
			this.YawInertia = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PitchInertia = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.activated = true;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Position = camSetup.Position;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Rotation = camSetup.Rotation;
		int cameraBoneIndex = base.GetBoneIndex("camera");
		if (cameraBoneIndex != -1)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			camSetup.Rotation *= this.Rotation.Inverse * base.GetBoneTransform(cameraBoneIndex, true).Rotation;
		}
		float newPitch = this.Rotation.Pitch();
		float newYaw = this.Rotation.Yaw();
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.PitchInertia = Angles.NormalizeAngle(newPitch - this.lastPitch);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.YawInertia = Angles.NormalizeAngle(this.lastYaw - newYaw);
		if (this.EnableSwingAndBob)
		{
			Vector3 playerVelocity = Local.Pawn.Velocity;
			Player player = Local.Pawn as Player;
			if (player != null)
			{
				PawnController controller = player.GetActiveController();
				if (controller != null && controller.HasTag("noclip"))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					playerVelocity = Vector3.Zero;
				}
			}
			float verticalDelta = playerVelocity.z * Time.Delta;
			Vector3 viewDown = Rotation.FromPitch(newPitch).Up * -1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			verticalDelta *= 1f - MathF.Abs(viewDown.Cross(Vector3.Down).y);
			float pitchDelta = this.PitchInertia - verticalDelta * 1f;
			float yawDelta = this.YawInertia;
			Vector3 offset = this.CalcSwingOffset(pitchDelta, yawDelta);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			offset += this.CalcBobbingOffset(playerVelocity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position += this.Rotation * offset;
		}
		else
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter("aim_yaw_inertia", this.YawInertia);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter("aim_pitch_inertia", this.PitchInertia);
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.lastPitch = newPitch;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.lastYaw = newYaw;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00006DF4 File Offset: 0x00004FF4
	protected Vector3 CalcSwingOffset(float pitchDelta, float yawDelta)
	{
		Vector3 swingVelocity = new Vector3(0f, yawDelta, pitchDelta);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.swingOffset -= this.swingOffset * this.ReturnSpeed * Time.Delta;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.swingOffset += swingVelocity * this.SwingInfluence;
		if (this.swingOffset.Length > this.MaxOffsetLength)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.swingOffset = this.swingOffset.Normal * this.MaxOffsetLength;
		}
		return this.swingOffset;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00006E9C File Offset: 0x0000509C
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
		Vector3 offset = this.BobDirection * (speed * 0.005f) * MathF.Cos(this.bobAnim);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		offset = offset.WithZ(-MathF.Abs(offset.z));
		return offset;
	}

	// Token: 0x0400002C RID: 44
	private Vector3 swingOffset;

	// Token: 0x0400002D RID: 45
	private float lastPitch;

	// Token: 0x0400002E RID: 46
	private float lastYaw;

	// Token: 0x0400002F RID: 47
	private float bobAnim;

	// Token: 0x04000030 RID: 48
	private bool activated;

	// Token: 0x04000031 RID: 49
	public bool EnableSwingAndBob = true;
}
