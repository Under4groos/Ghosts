using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000017 RID: 23
	public class ThirdPersonCamera : StalkerBaseCamera
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00008639 File Offset: 0x00006839
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00008611 File Offset: 0x00006811
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00008668 File Offset: 0x00006868
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00008640 File Offset: 0x00006840
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

		// Token: 0x060000D7 RID: 215 RVA: 0x00008670 File Offset: 0x00006870
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
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Update();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008894 File Offset: 0x00006A94
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

		// Token: 0x0400003D RID: 61
		private Angles orbitAngles;

		// Token: 0x0400003E RID: 62
		private float orbitDistance = 150f;

		// Token: 0x0400003F RID: 63
		private float orbitHeight;

		// Token: 0x04000040 RID: 64
		public static bool _repback__thirdperson_orbit = false;

		// Token: 0x04000041 RID: 65
		public static bool _repback__thirdperson_collision = true;
	}
}
