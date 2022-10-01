using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using Sandbox.Internal.Globals;

namespace StalkerRP
{
	// Token: 0x02000034 RID: 52
	public class WalkController : BasePlayerController
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public float SprintSpeed
		{
			get
			{
				return this.Player.Stats.Movement.SprintSpeed;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000C120 File Offset: 0x0000A320
		public float SprintForwardProportionality
		{
			get
			{
				return this.Player.Stats.Movement.SprintForwardProportionality;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000C148 File Offset: 0x0000A348
		public float WalkSpeed
		{
			get
			{
				return this.Player.Stats.Movement.WalkSpeed;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000C170 File Offset: 0x0000A370
		public float DefaultSpeed
		{
			get
			{
				return this.Player.Stats.Movement.DefaultSpeed;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000C198 File Offset: 0x0000A398
		public float Acceleration
		{
			get
			{
				return this.Player.Stats.Movement.Acceleration;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public float AccelerationForwardProportionality
		{
			get
			{
				return this.Player.Stats.Movement.AccelerationForwardProportionality;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		public float AirAcceleration
		{
			get
			{
				return this.Player.Stats.Movement.AirAcceleration;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000C210 File Offset: 0x0000A410
		public float GroundFriction
		{
			get
			{
				return this.Player.Stats.Movement.GroundFriction;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000C238 File Offset: 0x0000A438
		public float StopSpeed
		{
			get
			{
				return this.Player.Stats.Movement.StopSpeed;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000C260 File Offset: 0x0000A460
		public float GroundAngle
		{
			get
			{
				return this.Player.Stats.Movement.GroundAngle;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000C288 File Offset: 0x0000A488
		public float StepSize
		{
			get
			{
				return this.Player.Stats.Movement.StepSize;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public float MaxNonJumpVelocity
		{
			get
			{
				return this.Player.Stats.Movement.MaxNonJumpVelocity;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public float BodyGirth
		{
			get
			{
				return this.Player.Stats.Movement.BodyGirth;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000C300 File Offset: 0x0000A500
		public float BodyHeight
		{
			get
			{
				return this.Player.Stats.Movement.BodyHeight;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000C328 File Offset: 0x0000A528
		public float EyeHeight
		{
			get
			{
				return this.Player.Stats.Movement.EyeHeight;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000C350 File Offset: 0x0000A550
		public float Gravity
		{
			get
			{
				return this.Player.Stats.Movement.Gravity;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000C378 File Offset: 0x0000A578
		public float AirControl
		{
			get
			{
				return this.Player.Stats.Movement.AirControl;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public bool AutoJump
		{
			get
			{
				return this.Player.Stats.Movement.AutoJump;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000C3CD File Offset: 0x0000A5CD
		[DefaultValue(false)]
		public bool Swimming { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public StalkerPlayer Player
		{
			get
			{
				return base.Pawn as StalkerPlayer;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C3E3 File Offset: 0x0000A5E3
		public WalkController()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Duck = new Duck(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Unstuck = new Unstuck(this);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C410 File Offset: 0x0000A610
		[Description("This is temporary, get the hull size for the player's collision")]
		public override BBox GetHull()
		{
			float girth = this.BodyGirth * 0.5f;
			Vector3 vector = new Vector3(-girth, -girth, 0f);
			Vector3 maxs = new Vector3(girth, girth, this.BodyHeight);
			return new BBox(vector, maxs);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C44D File Offset: 0x0000A64D
		public virtual void SetBBox(Vector3 mins, Vector3 maxs)
		{
			if (this.mins == mins && this.maxs == maxs)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.mins = mins;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.maxs = maxs;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C484 File Offset: 0x0000A684
		[Description("Update the size of the bbox. We should really trigger some shit if this changes.")]
		public virtual void UpdateBBox()
		{
			float girth = this.BodyGirth * 0.5f;
			Vector3 mins = new Vector3(-girth, -girth, 0f) * base.Pawn.Scale;
			Vector3 maxs = new Vector3(girth, girth, this.BodyHeight) * base.Pawn.Scale;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Duck.UpdateBBox(ref mins, ref maxs, base.Pawn.Scale);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetBBox(mins, maxs);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C507 File Offset: 0x0000A707
		public override void FrameSimulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FrameSimulate();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeRotation = Input.Rotation;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C524 File Offset: 0x0000A724
		public override void Simulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * (this.EyeHeight * base.Pawn.Scale);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateBBox();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition += this.TraceOffset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeRotation = Input.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RestoreGroundPos();
			if (this.Unstuck.TestAndFix())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CheckLadder();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Swimming = (base.Pawn.WaterLevel > 0.6f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CheckFalling();
			if (!this.Swimming && !this.IsTouchingLadder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity -= new Vector3(0f, 0f, this.Gravity * 0.5f) * Time.Delta;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity += new Vector3(0f, 0f, base.BaseVelocity.z) * Time.Delta;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.BaseVelocity = base.BaseVelocity.WithZ(0f);
			}
			if (this.AutoJump ? Input.Down(InputButton.Jump) : Input.Pressed(InputButton.Jump))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CheckJumpButton();
			}
			if (base.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity = base.Velocity.WithZ(0f);
				if (base.GroundEntity != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.ApplyFriction(this.GroundFriction * this.SurfaceFriction);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity = new Vector3(Input.Forward, Input.Left, 0f);
			float inSpeed = base.WishVelocity.Length.Clamp(0f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity *= Input.Rotation.Angles().WithPitch(0f).ToRotation();
			if (!this.Swimming && !this.IsTouchingLadder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.WishVelocity = base.WishVelocity.WithZ(0f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity = base.WishVelocity.Normal * inSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity *= this.GetWishSpeed();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Duck.PreTick();
			bool bStayOnGround = false;
			if (this.Swimming)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ApplyFriction(1f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.WaterMove();
			}
			else if (this.IsTouchingLadder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetTag("climbing");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LadderMove();
			}
			else if (base.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bStayOnGround = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.WalkMove();
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AirMove();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CategorizePosition(bStayOnGround);
			if (!this.Swimming && !this.IsTouchingLadder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity -= new Vector3(0f, 0f, this.Gravity * 0.5f) * Time.Delta;
			}
			if (base.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity = base.Velocity.WithZ(0f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SaveGroundPos();
			if (BasePlayerController.Debug)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Box(base.Position + this.TraceOffset, this.mins, this.maxs, Color.Red, 0f, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Box(base.Position, this.mins, this.maxs, Color.Blue, 0f, true);
				int lineOffset = 0;
				if (Host.IsServer)
				{
					lineOffset = 10;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay = GlobalGameNamespace.DebugOverlay;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
				defaultInterpolatedStringHandler.AppendLiteral("        Position: ");
				defaultInterpolatedStringHandler.AppendFormatted<Vector3>(base.Position);
				debugOverlay.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), lineOffset, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay2 = GlobalGameNamespace.DebugOverlay;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
				defaultInterpolatedStringHandler.AppendLiteral("        Velocity: ");
				defaultInterpolatedStringHandler.AppendFormatted<Vector3>(base.Velocity);
				debugOverlay2.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), lineOffset + 1, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay3 = GlobalGameNamespace.DebugOverlay;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
				defaultInterpolatedStringHandler.AppendLiteral("    BaseVelocity: ");
				defaultInterpolatedStringHandler.AppendFormatted<Vector3>(base.BaseVelocity);
				debugOverlay3.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), lineOffset + 2, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay4 = GlobalGameNamespace.DebugOverlay;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 2);
				defaultInterpolatedStringHandler.AppendLiteral("    GroundEntity: ");
				defaultInterpolatedStringHandler.AppendFormatted<Entity>(base.GroundEntity);
				defaultInterpolatedStringHandler.AppendLiteral(" [");
				Entity groundEntity = base.GroundEntity;
				defaultInterpolatedStringHandler.AppendFormatted<Vector3?>((groundEntity != null) ? new Vector3?(groundEntity.Velocity) : null);
				defaultInterpolatedStringHandler.AppendLiteral("]");
				debugOverlay4.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), lineOffset + 3, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay5 = GlobalGameNamespace.DebugOverlay;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
				defaultInterpolatedStringHandler.AppendLiteral(" SurfaceFriction: ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.SurfaceFriction);
				debugOverlay5.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), lineOffset + 4, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay6 = GlobalGameNamespace.DebugOverlay;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
				defaultInterpolatedStringHandler.AppendLiteral("    WishVelocity: ");
				defaultInterpolatedStringHandler.AppendFormatted<Vector3>(base.WishVelocity);
				debugOverlay6.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), lineOffset + 5, 0f);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000CB26 File Offset: 0x0000AD26
		protected virtual void CheckFalling()
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public virtual float GetWishSpeed()
		{
			float ws = this.Duck.GetWishSpeed();
			if (ws >= 0f)
			{
				return ws;
			}
			if (Input.Down(InputButton.Run) && this.Player.StaminaComponent.CanRun())
			{
				return this.SprintSpeed;
			}
			if (Input.Down(InputButton.Walk) || this.Player.InSights)
			{
				return this.WalkSpeed;
			}
			return this.DefaultSpeed;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000CB98 File Offset: 0x0000AD98
		public virtual void WalkMove()
		{
			Vector3 wishdir = base.WishVelocity.Normal;
			float wishspeed = base.WishVelocity.Length;
			float accel = this.Acceleration;
			Vector3 forwardDir = Input.Rotation.Angles().WithPitch(0f).ToRotation().Forward;
			float frac = 1f - wishdir.Dot(forwardDir).Clamp(0f, 1f);
			if (wishspeed > this.DefaultSpeed && Input.Down(InputButton.Run))
			{
				float sprintProp = 1f - (1f - this.SprintForwardProportionality) * frac;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				wishspeed *= sprintProp;
			}
			float accelProp = 1f - (1f - this.AccelerationForwardProportionality) * frac;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			accel *= accelProp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity = base.WishVelocity.WithZ(0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity = base.WishVelocity.Normal * wishspeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = base.Velocity.WithZ(0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Accelerate(wishdir, wishspeed, 0f, accel);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = base.Velocity.WithZ(0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity += base.BaseVelocity;
			try
			{
				if (base.Velocity.Length < 1f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Velocity = Vector3.Zero;
					return;
				}
				Vector3 dest = (base.Position + base.Velocity * Time.Delta).WithZ(base.Position.z);
				TraceResult pm = this.TraceBBox(base.Position, dest, 0f);
				if (pm.Fraction == 1f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Position = pm.EndPosition;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.StayOnGround();
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StepMove();
			}
			finally
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity -= base.BaseVelocity;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StayOnGround();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000CE04 File Offset: 0x0000B004
		public virtual void StepMove()
		{
			MoveHelper mover = new MoveHelper(base.Position, base.Velocity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Trace trace = mover.Trace.Size(this.mins, this.maxs);
			Entity pawn = base.Pawn;
			bool flag = true;
			mover.Trace = trace.Ignore(pawn, flag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mover.MaxStandableAngle = this.GroundAngle;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mover.TryMoveWithStep(Time.Delta, this.StepSize);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = mover.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = mover.Velocity;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		public virtual void Move()
		{
			MoveHelper mover = new MoveHelper(base.Position, base.Velocity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Trace trace = mover.Trace.Size(this.mins, this.maxs);
			Entity pawn = base.Pawn;
			bool flag = true;
			mover.Trace = trace.Ignore(pawn, flag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mover.MaxStandableAngle = this.GroundAngle;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mover.TryMove(Time.Delta);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = mover.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = mover.Velocity;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000CF4C File Offset: 0x0000B14C
		[Description("Add our wish direction and speed onto our velocity")]
		public virtual void Accelerate(Vector3 wishdir, float wishspeed, float speedLimit, float acceleration)
		{
			if (speedLimit > 0f && wishspeed > speedLimit)
			{
				wishspeed = speedLimit;
			}
			float currentspeed = base.Velocity.Dot(wishdir);
			float addspeed = wishspeed - currentspeed;
			if (addspeed <= 0f)
			{
				return;
			}
			float accelspeed = acceleration * Time.Delta * wishspeed * this.SurfaceFriction;
			if (accelspeed > addspeed)
			{
				accelspeed = addspeed;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity += wishdir * accelspeed;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		[Description("Remove ground friction from velocity")]
		public virtual void ApplyFriction(float frictionAmount = 1f)
		{
			float speed = base.Velocity.Length;
			if (speed < 0.1f)
			{
				return;
			}
			float drop = ((speed < this.StopSpeed) ? this.StopSpeed : speed) * Time.Delta * frictionAmount;
			float newspeed = speed - drop;
			if (newspeed < 0f)
			{
				newspeed = 0f;
			}
			if (newspeed != speed)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				newspeed /= speed;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity *= newspeed;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D034 File Offset: 0x0000B234
		public virtual void CheckJumpButton()
		{
			if (this.Swimming)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ClearGroundEntity();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Velocity = base.Velocity.WithZ(100f);
				return;
			}
			if (base.GroundEntity == null)
			{
				return;
			}
			if (!this.Player.StaminaComponent.CanJump())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Player.StaminaComponent.Jump();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ClearGroundEntity();
			float flGroundFactor = 1f;
			float flMul = 321.9938f;
			float startz = base.Velocity.z;
			if (this.Duck.IsActive)
			{
				flMul *= 0.8f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = base.Velocity.WithZ(startz + flMul * flGroundFactor);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity -= new Vector3(0f, 0f, this.Gravity * 0.5f) * Time.Delta;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddEvent("jump");
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D148 File Offset: 0x0000B348
		public virtual void AirMove()
		{
			Vector3 wishdir = base.WishVelocity.Normal;
			float wishspeed = base.WishVelocity.Length;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Accelerate(wishdir, wishspeed, this.AirControl, this.AirAcceleration);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity += base.BaseVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Move();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity -= base.BaseVelocity;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public virtual void WaterMove()
		{
			Vector3 wishdir = base.WishVelocity.Normal;
			float wishspeed = base.WishVelocity.Length;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			wishspeed *= 0.8f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Accelerate(wishdir, wishspeed, 100f, this.Acceleration);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity += base.BaseVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Move();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity -= base.BaseVelocity;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D264 File Offset: 0x0000B464
		public virtual void CheckLadder()
		{
			Vector3 wishvel = new Vector3(Input.Forward, Input.Left, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			wishvel *= Input.Rotation.Angles().WithPitch(0f).ToRotation();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			wishvel = wishvel.Normal;
			if (this.IsTouchingLadder)
			{
				if (Input.Pressed(InputButton.Jump))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Velocity = this.LadderNormal * 100f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IsTouchingLadder = false;
					return;
				}
				if (base.GroundEntity != null && this.LadderNormal.Dot(wishvel) > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IsTouchingLadder = false;
					return;
				}
			}
			Vector3 start = base.Position;
			Vector3 end = start + (this.IsTouchingLadder ? (this.LadderNormal * -1f) : wishvel) * 1f;
			Trace trace = Trace.Ray(start, end).Size(this.mins, this.maxs).WithTag("ladder");
			Entity pawn = base.Pawn;
			bool flag = true;
			TraceResult pm = trace.Ignore(pawn, flag).Run();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsTouchingLadder = false;
			if (pm.Hit)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsTouchingLadder = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LadderNormal = pm.Normal;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public virtual void LadderMove()
		{
			Vector3 velocity = base.WishVelocity;
			float normalDot = velocity.Dot(this.LadderNormal);
			Vector3 cross = this.LadderNormal * normalDot;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = velocity - cross + -normalDot * this.LadderNormal.Cross(Vector3.Up.Cross(this.LadderNormal).Normal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Move();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D458 File Offset: 0x0000B658
		public virtual void CategorizePosition(bool bStayOnGround)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SurfaceFriction = 1f;
			Vector3 point = base.Position - Vector3.Up * 2f;
			Vector3 vBumpOrigin = base.Position;
			bool flag = base.Velocity.z > this.MaxNonJumpVelocity;
			float z = base.Velocity.z;
			bool bMoveToEndPos = false;
			if (base.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bMoveToEndPos = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				point.z -= this.StepSize;
			}
			else if (bStayOnGround)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bMoveToEndPos = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				point.z -= this.StepSize;
			}
			if (flag || this.Swimming)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ClearGroundEntity();
				return;
			}
			TraceResult pm = this.TraceBBox(vBumpOrigin, point, 4f);
			if (pm.Entity == null || Vector3.GetAngle(Vector3.Up, pm.Normal) > this.GroundAngle)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ClearGroundEntity();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bMoveToEndPos = false;
				if (base.Velocity.z > 0f)
				{
					this.SurfaceFriction = 0.25f;
				}
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateGroundEntity(pm);
			}
			if (bMoveToEndPos && !pm.StartedSolid && pm.Fraction > 0f && pm.Fraction < 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = pm.EndPosition;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		[Description("We have a new ground entity")]
		public virtual void UpdateGroundEntity(TraceResult tr)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.GroundNormal = tr.Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SurfaceFriction = tr.Surface.Friction * 1.25f;
			if (this.SurfaceFriction > 1f)
			{
				this.SurfaceFriction = 1f;
			}
			if (base.GroundEntity != null)
			{
				Vector3 velocity = base.GroundEntity.Velocity;
			}
			Entity groundEntity = base.GroundEntity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.GroundEntity = tr.Entity;
			if (base.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.BaseVelocity = base.GroundEntity.Velocity;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D664 File Offset: 0x0000B864
		[Description("We're no longer on the ground, remove it")]
		public virtual void ClearGroundEntity()
		{
			if (base.GroundEntity == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.GroundEntity = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.GroundNormal = Vector3.Up;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SurfaceFriction = 1f;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D69B File Offset: 0x0000B89B
		[Description("Traces the current bbox and returns the result. liftFeet will move the start position up by this amount, while keeping the top of the bbox at the same position. This is good when tracing down because you won't be tracing through the ceiling above.")]
		public override TraceResult TraceBBox(Vector3 start, Vector3 end, float liftFeet = 0f)
		{
			return this.TraceBBox(start, end, this.mins, this.maxs, liftFeet);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		[Description("Try to keep a walking player on the ground when running down slopes etc")]
		public virtual void StayOnGround()
		{
			Vector3 start = base.Position + Vector3.Up * 2f;
			Vector3 end = base.Position + Vector3.Down * this.StepSize;
			TraceResult trace = this.TraceBBox(base.Position, start, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			start = trace.EndPosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			trace = this.TraceBBox(start, end, 0f);
			if (trace.Fraction <= 0f)
			{
				return;
			}
			if (trace.Fraction >= 1f)
			{
				return;
			}
			if (trace.StartedSolid)
			{
				return;
			}
			if (Vector3.GetAngle(Vector3.Up, trace.Normal) > this.GroundAngle)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = trace.EndPosition;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D779 File Offset: 0x0000B979
		private void RestoreGroundPos()
		{
			if (base.GroundEntity != null)
			{
				bool isWorld = base.GroundEntity.IsWorld;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000D78F File Offset: 0x0000B98F
		private void SaveGroundPos()
		{
			if (base.GroundEntity != null)
			{
				bool isWorld = base.GroundEntity.IsWorld;
			}
		}

		// Token: 0x040000A6 RID: 166
		public Duck Duck;

		// Token: 0x040000A7 RID: 167
		public Unstuck Unstuck;

		// Token: 0x040000A8 RID: 168
		protected Vector3 mins;

		// Token: 0x040000A9 RID: 169
		protected Vector3 maxs;

		// Token: 0x040000AA RID: 170
		protected float SurfaceFriction;

		// Token: 0x040000AB RID: 171
		private bool IsTouchingLadder;

		// Token: 0x040000AC RID: 172
		private Vector3 LadderNormal;
	}
}
