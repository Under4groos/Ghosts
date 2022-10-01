using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000184 RID: 388
	[Library("ent_platform")]
	[HammerEntity]
	[SupportsSolid]
	[Model]
	[RenderFields]
	[VisGroup(VisGroup.Dynamic)]
	[DoorHelper("movedir", "movedir_islocal", "movedir_type", "movedistance")]
	[Title("Platform")]
	[Category("Gameplay")]
	[Icon("trending_flat")]
	[Description("A simple platform that moves between two locations and can be controlled through Entity IO.")]
	public class PlatformEntity : KeyframeEntity
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000486EF File Offset: 0x000468EF
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x000486F7 File Offset: 0x000468F7
		[Property("movedir", null, Title = "Move Direction")]
		[Description("Specifies the direction to move in when the platform is used, or axis of rotation for rotating platforms.")]
		public Angles MoveDir { get; set; } = new Angles(0f, 0f, 0f);

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00048700 File Offset: 0x00046900
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x00048708 File Offset: 0x00046908
		[Property("movedir_islocal", null, Title = "Move Direction is Expressed in Local Space")]
		[global::DefaultValue(true)]
		[Description("If checked, the movement direction angle is in local space and should be rotated by the entity's angles after spawning.")]
		public bool MoveDirIsLocal { get; set; } = true;

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00048711 File Offset: 0x00046911
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x00048719 File Offset: 0x00046919
		[Property("movedir_type", null, Title = "Movement Type")]
		[global::DefaultValue(PlatformEntity.PlatformMoveType.Moving)]
		[Description("Movement type of the platform.<br /><b>Moving</b>: Moving linearly and reversing direction at final position if Looping is enabled.<br /><b>Rotating</b>: Rotating and reversing direction at final rotation if Looping is enabled.<br /><b>Rotating Continuous</b>: Rotating continuously past Move Distance. OnReached outputs are fired every Move Distance degrees.<br />")]
		public PlatformEntity.PlatformMoveType MoveDirType { get; set; } = PlatformEntity.PlatformMoveType.Moving;

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00048722 File Offset: 0x00046922
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x0004872A File Offset: 0x0004692A
		[Property]
		[global::DefaultValue(100f)]
		[Description("How much to move in the move direction, or rotate around the axis for rotating move type.")]
		public float MoveDistance { get; set; } = 100f;

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00048733 File Offset: 0x00046933
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x0004873B File Offset: 0x0004693B
		[Property]
		[global::DefaultValue(64f)]
		[Description("The speed to move/rotate with.")]
		public float Speed { get; protected set; } = 64f;

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00048744 File Offset: 0x00046944
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x0004874C File Offset: 0x0004694C
		[Property]
		[global::DefaultValue(0f)]
		[Description("If set to above 0 and <b>Loop Movement</b> is enabled, the amount of time to wait before automatically toggling direction.")]
		public float TimeToHold { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00048755 File Offset: 0x00046955
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x0004875D File Offset: 0x0004695D
		[Property]
		[global::DefaultValue(false)]
		[Description("If set, the platform will automatically go back upon reaching either end of the movement range.")]
		public bool LoopMovement { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00048766 File Offset: 0x00046966
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x0004876E File Offset: 0x0004696E
		[Property]
		[Category("Spawn Settings")]
		[global::DefaultValue(true)]
		[Description("If set, the platform will start moving on spawn.")]
		public bool StartsMoving { get; set; } = true;

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00048777 File Offset: 0x00046977
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x0004877F File Offset: 0x0004697F
		[Property]
		[Category("Spawn Settings")]
		[MinMax(0f, 100f)]
		[global::DefaultValue(0)]
		[Description("At what percentage between start and end positions should the platform spawn in")]
		public float StartPosition { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00048788 File Offset: 0x00046988
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00048790 File Offset: 0x00046990
		[Property]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[Description("Sound to play when starting to move")]
		public string StartMoveSound { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00048799 File Offset: 0x00046999
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x000487A1 File Offset: 0x000469A1
		[Property]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[Description("Sound to play when we stopped moving")]
		public string StopMoveSound { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x000487AA File Offset: 0x000469AA
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x000487B2 File Offset: 0x000469B2
		[Property("moving_sound", null)]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[Description("Sound to play while platform is moving.")]
		public string MovingSound { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x000487BB File Offset: 0x000469BB
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x000487C3 File Offset: 0x000469C3
		[Description("Whether the platform is moving or not")]
		public bool IsMoving { get; protected set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x000487CC File Offset: 0x000469CC
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x000487D4 File Offset: 0x000469D4
		[Description("The platform's move direction.")]
		public bool IsMovingForwards { get; protected set; }

		// Token: 0x060012B1 RID: 4785 RVA: 0x000487E0 File Offset: 0x000469E0
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Keyframed, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionA = this.LocalPosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionB = this.PositionA + this.MoveDir.Direction * this.MoveDistance;
			if (this.MoveDirIsLocal)
			{
				Vector3 dir_world = base.Transform.NormalToWorld(this.MoveDir.Direction);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PositionB = this.PositionA + dir_world * this.MoveDistance;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RotationA = this.LocalRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMoving = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMovingForwards = true;
			if (this.StartPosition > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetPosition(this.StartPosition / 100f);
			}
			if (this.StartsMoving)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StartMoving();
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000488F0 File Offset: 0x00046AF0
		protected override void OnDestroy()
		{
			if (this.MoveSoundInstance != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MoveSoundInstance.Value.Stop();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MoveSoundInstance = null;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00048940 File Offset: 0x00046B40
		private Vector3 GetRotationAxis()
		{
			Vector3 axis = Rotation.From(this.MoveDir).Up;
			if (!this.MoveDirIsLocal)
			{
				axis = base.Transform.NormalToLocal(axis);
			}
			return axis;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0004897A File Offset: 0x00046B7A
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00048982 File Offset: 0x00046B82
		[Description("Fired when the platform reaches its beginning location")]
		protected Entity.Output OnReachedStart { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0004898B File Offset: 0x00046B8B
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x00048993 File Offset: 0x00046B93
		[Description("Fired when the platform reaches its end location (startPos + dir * distance)")]
		protected Entity.Output OnReachedEnd { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0004899C File Offset: 0x00046B9C
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x000489A4 File Offset: 0x00046BA4
		[global::DefaultValue(0)]
		[Description("Contains the current rotation of the platform in degrees.")]
		public float CurrentRotation { get; protected set; }

		// Token: 0x060012BA RID: 4794 RVA: 0x000489B0 File Offset: 0x00046BB0
		[Event.Tick.ServerAttribute]
		private void DebugOverlayTick()
		{
			if (!base.DebugFlags.HasFlag(EntityDebugFlags.Text))
			{
				return;
			}
			if (this.MoveDirType == PlatformEntity.PlatformMoveType.Moving)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Box((this.Parent != null) ? this.Parent.Transform.PointToWorld(this.PositionA) : this.PositionA, this.Rotation, base.CollisionBounds.Mins, base.CollisionBounds.Maxs, Color.Green, 0f, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Box((this.Parent != null) ? this.Parent.Transform.PointToWorld(this.PositionB) : this.PositionB, this.Rotation, base.CollisionBounds.Mins, base.CollisionBounds.Maxs, Color.Red, 0f, true);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.DebugOverlay.Line(this.Position, this.Position + base.Transform.NormalToWorld(this.GetRotationAxis()) * 5f, Color.Green, 0f, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.DebugOverlay.Line(this.Position, this.Position + this.LocalRotation.Forward * 20f, Color.Cyan, 0f, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.DebugOverlay.Line(this.Position, this.Position + this.LocalRotation.Up * 10f, Color.Yellow, 0f, false);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00048B71 File Offset: 0x00046D71
		public override void MoveFinished()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalVelocity = Vector3.Zero;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AngularVelocity = Angles.Zero;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00048B94 File Offset: 0x00046D94
		private Task DoMove()
		{
			PlatformEntity.<DoMove>d__81 <DoMove>d__;
			<DoMove>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoMove>d__.<>4__this = this;
			<DoMove>d__.<>1__state = -1;
			<DoMove>d__.<>t__builder.Start<PlatformEntity.<DoMove>d__81>(ref <DoMove>d__);
			return <DoMove>d__.<>t__builder.Task;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00048BD8 File Offset: 0x00046DD8
		[Input]
		[Description("Sets the platforms's position to given percentage between start and end positions. The expected input range is 0..1")]
		private void SetPosition(float progress)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			progress = Math.Clamp(progress, 0f, 1f);
			if (this.MoveDirType == PlatformEntity.PlatformMoveType.Moving)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LocalPosition = Vector3.Lerp(this.PositionA, this.PositionB, progress, true);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalRotation = Rotation.Lerp(this.RotationA, this.RotationA.RotateAroundAxis(this.GetRotationAxis(), (this.MoveDistance != 0f) ? this.MoveDistance : 360f), progress, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentRotation = 0f.LerpTo((this.MoveDistance != 0f) ? this.MoveDistance : 360f, progress, true);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00048C98 File Offset: 0x00046E98
		[Input]
		[Description("Start moving in platform's current move direction")]
		public void StartMoving()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMove();
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00048CA8 File Offset: 0x00046EA8
		[Input]
		[Description("Set the move direction to forwards and start moving")]
		public void StartMovingForward()
		{
			bool oldDir = this.IsMovingForwards;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMovingForwards = true;
			if (this.IsMovingForwards != oldDir && this.IsMoving)
			{
				this.StartMoving();
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00048CE0 File Offset: 0x00046EE0
		[Input]
		[Description("Set the move direction to backwards and start moving")]
		public void StartMovingBackwards()
		{
			bool oldDir = this.IsMovingForwards;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMovingForwards = false;
			if (this.IsMovingForwards != oldDir && this.IsMoving)
			{
				this.StartMoving();
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00048D17 File Offset: 0x00046F17
		[Input]
		[Description("Reverse current move direction. Will NOT start moving if stopped")]
		public void ReverseMoving()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMovingForwards = !this.IsMovingForwards;
			if (this.IsMoving)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.StartMoveSound);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StartMoving();
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00048D54 File Offset: 0x00046F54
		[Input]
		[Description("Stop moving, preserving move direction")]
		public void StopMoving()
		{
			if (!this.IsMoving && !this.isWaitingToMove)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.movement++;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LocalKeyframeTo(this.LocalPosition, 0f, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LocalRotateKeyframeTo(this.LocalRotation, 0f, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalVelocity = Vector3.Zero;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AngularVelocity = Angles.Zero;
			if (!this.IsMoving)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMoving = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound(this.StopMoveSound);
			if (this.MoveSoundInstance != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MoveSoundInstance.Value.Stop();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MoveSoundInstance = null;
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00048E33 File Offset: 0x00047033
		[Input]
		[Description("Toggle moving, preserving move direction")]
		public void ToggleMoving()
		{
			if (this.IsMoving || this.isWaitingToMove)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StopMoving();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartMoving();
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00048E5C File Offset: 0x0004705C
		[Input]
		[Description("Sets the move speed")]
		public void SetSpeed(float speed)
		{
			float oldSpeed = this.Speed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Speed = speed;
			if (this.IsMoving && oldSpeed != this.Speed)
			{
				this.StartMoving();
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00048E93 File Offset: 0x00047093
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnReachedStart = new Entity.Output(this, "OnReachedStart");
			this.OnReachedEnd = new Entity.Output(this, "OnReachedEnd");
			base.CreateHammerOutputs();
		}

		// Token: 0x040005F7 RID: 1527
		private Vector3 PositionA;

		// Token: 0x040005F8 RID: 1528
		private Vector3 PositionB;

		// Token: 0x040005F9 RID: 1529
		private Rotation RotationA;

		// Token: 0x040005FD RID: 1533
		private bool isWaitingToMove;

		// Token: 0x040005FE RID: 1534
		private int movement;

		// Token: 0x040005FF RID: 1535
		private int movementTick;

		// Token: 0x04000600 RID: 1536
		private Sound? MoveSoundInstance;

		// Token: 0x02000249 RID: 585
		public enum PlatformMoveType
		{
			// Token: 0x0400099A RID: 2458
			Moving = 3,
			// Token: 0x0400099B RID: 2459
			Rotating = 1,
			// Token: 0x0400099C RID: 2460
			RotatingContinious = 4
		}
	}
}
