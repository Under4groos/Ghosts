using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000144 RID: 324
	public class ThirdPersonCamera : CameraMode
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0003BD14 File Offset: 0x00039F14
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x0003BCEC File Offset: 0x00039EEC
		[ConVar.ReplicatedAttribute(null)]
		public static bool thirdperson_orbit
		{
			get
			{
				return ThirdPersonCamera._repback__thirdperson_orbit;
			}
			set
			{
				if (ThirdPersonCamera._repback__thirdperson_orbit == value)
				{
					return;
				}
				ThirdPersonCamera._repback__thirdperson_orbit = value;
				if (Host.IsServer)
				{
					ConsoleSystem.SetValue("thirdperson_orbit", value);
				}
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0003BD43 File Offset: 0x00039F43
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x0003BD1B File Offset: 0x00039F1B
		[ConVar.ReplicatedAttribute(null)]
		public static bool thirdperson_collision
		{
			get
			{
				return ThirdPersonCamera._repback__thirdperson_collision;
			}
			set
			{
				if (ThirdPersonCamera._repback__thirdperson_collision == value)
				{
					return;
				}
				ThirdPersonCamera._repback__thirdperson_collision = value;
				if (Host.IsServer)
				{
					ConsoleSystem.SetValue("thirdperson_collision", value);
				}
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003BD4C File Offset: 0x00039F4C
		public override void Update()
		{
			AnimatedEntity pawn = Local.Pawn as AnimatedEntity;
			if (pawn == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = pawn.Position;
			Vector3 center = pawn.Position + Vector3.Up * 64f;
			Vector3 targetPos;
			if (ThirdPersonCamera.thirdperson_orbit)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Vector3 position = base.Position;
				Vector3 up = Vector3.Up;
				BBox collisionBounds = pawn.CollisionBounds;
				Vector3 vector = collisionBounds.Center;
				base.Position = position + up * (vector.z * pawn.Scale + this.orbitHeight);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Rotation = Rotation.From(this.orbitAngles);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetPos = base.Position + base.Rotation.Backward * this.orbitDistance;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = center;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Rotation = Rotation.FromAxis(Vector3.Up, 4f) * Input.Rotation;
				float distance = 130f * pawn.Scale;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Vector3 position2 = base.Position;
				Vector3 right = Input.Rotation.Right;
				BBox collisionBounds = pawn.CollisionBounds;
				targetPos = position2 + right * ((collisionBounds.Maxs.x + 15f) * pawn.Scale);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetPos += Input.Rotation.Forward * -distance;
			}
			if (ThirdPersonCamera.thirdperson_collision)
			{
				Vector3 vector = base.Position;
				Trace trace = Trace.Ray(vector, targetPos).WithAnyTags(new string[]
				{
					"solid"
				});
				Entity entity = pawn;
				bool flag = true;
				TraceResult tr = trace.Ignore(entity, flag).Radius(8f).Run();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = tr.EndPosition;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = targetPos;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = 70f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = null;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003BF64 File Offset: 0x0003A164
		public override void BuildInput(InputBuilder input)
		{
			if (ThirdPersonCamera.thirdperson_orbit && input.Down(InputButton.Walk))
			{
				if (input.Down(InputButton.PrimaryAttack))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitDistance += input.AnalogLook.pitch;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitDistance = this.orbitDistance.Clamp(0f, 1000f);
				}
				else if (input.Down(InputButton.SecondaryAttack))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitHeight += input.AnalogLook.pitch;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitHeight = this.orbitHeight.Clamp(-1000f, 1000f);
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitAngles.yaw = this.orbitAngles.yaw + input.AnalogLook.yaw;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitAngles.pitch = this.orbitAngles.pitch + input.AnalogLook.pitch;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitAngles = this.orbitAngles.Normal;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.orbitAngles.pitch = this.orbitAngles.pitch.Clamp(-89f, 89f);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				input.AnalogLook = Angles.Zero;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				input.Clear();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				input.StopProcessing = true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.BuildInput(input);
		}

		// Token: 0x040004A6 RID: 1190
		private Angles orbitAngles;

		// Token: 0x040004A7 RID: 1191
		private float orbitDistance = 150f;

		// Token: 0x040004A8 RID: 1192
		private float orbitHeight;

		// Token: 0x040004A9 RID: 1193
		public static bool _repback__thirdperson_orbit = false;

		// Token: 0x040004AA RID: 1194
		public static bool _repback__thirdperson_collision = true;
	}
}
