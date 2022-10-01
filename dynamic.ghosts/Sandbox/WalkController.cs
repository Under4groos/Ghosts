using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.Internal.Globals;

namespace Sandbox
{
	// Token: 0x0200018F RID: 399
	[Library]
	public class WalkController : BasePlayerController
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004AD9D File Offset: 0x00048F9D
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0004ADAB File Offset: 0x00048FAB
		[Net]
		[DefaultValue(320f)]
		public unsafe float SprintSpeed
		{
			get
			{
				return *this._repback__SprintSpeed.GetValue();
			}
			set
			{
				this._repback__SprintSpeed.SetValue(value);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004ADBA File Offset: 0x00048FBA
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0004ADC8 File Offset: 0x00048FC8
		[Net]
		[DefaultValue(150f)]
		public unsafe float WalkSpeed
		{
			get
			{
				return *this._repback__WalkSpeed.GetValue();
			}
			set
			{
				this._repback__WalkSpeed.SetValue(value);
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004ADD7 File Offset: 0x00048FD7
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0004ADE5 File Offset: 0x00048FE5
		[Net]
		[DefaultValue(190f)]
		public unsafe float DefaultSpeed
		{
			get
			{
				return *this._repback__DefaultSpeed.GetValue();
			}
			set
			{
				this._repback__DefaultSpeed.SetValue(value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0004ADF4 File Offset: 0x00048FF4
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x0004AE02 File Offset: 0x00049002
		[Net]
		[DefaultValue(10f)]
		public unsafe float Acceleration
		{
			get
			{
				return *this._repback__Acceleration.GetValue();
			}
			set
			{
				this._repback__Acceleration.SetValue(value);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0004AE11 File Offset: 0x00049011
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x0004AE1F File Offset: 0x0004901F
		[Net]
		[DefaultValue(50f)]
		public unsafe float AirAcceleration
		{
			get
			{
				return *this._repback__AirAcceleration.GetValue();
			}
			set
			{
				this._repback__AirAcceleration.SetValue(value);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004AE2E File Offset: 0x0004902E
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x0004AE3C File Offset: 0x0004903C
		[Net]
		[DefaultValue(-30f)]
		public unsafe float FallSoundZ
		{
			get
			{
				return *this._repback__FallSoundZ.GetValue();
			}
			set
			{
				this._repback__FallSoundZ.SetValue(value);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0004AE4B File Offset: 0x0004904B
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x0004AE59 File Offset: 0x00049059
		[Net]
		[DefaultValue(4f)]
		public unsafe float GroundFriction
		{
			get
			{
				return *this._repback__GroundFriction.GetValue();
			}
			set
			{
				this._repback__GroundFriction.SetValue(value);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0004AE68 File Offset: 0x00049068
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x0004AE76 File Offset: 0x00049076
		[Net]
		[DefaultValue(100f)]
		public unsafe float StopSpeed
		{
			get
			{
				return *this._repback__StopSpeed.GetValue();
			}
			set
			{
				this._repback__StopSpeed.SetValue(value);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0004AE85 File Offset: 0x00049085
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x0004AE93 File Offset: 0x00049093
		[Net]
		[DefaultValue(20f)]
		public unsafe float Size
		{
			get
			{
				return *this._repback__Size.GetValue();
			}
			set
			{
				this._repback__Size.SetValue(value);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0004AEA2 File Offset: 0x000490A2
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x0004AEB0 File Offset: 0x000490B0
		[Net]
		[DefaultValue(0.03125f)]
		public unsafe float DistEpsilon
		{
			get
			{
				return *this._repback__DistEpsilon.GetValue();
			}
			set
			{
				this._repback__DistEpsilon.SetValue(value);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0004AEBF File Offset: 0x000490BF
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x0004AECD File Offset: 0x000490CD
		[Net]
		[DefaultValue(46f)]
		public unsafe float GroundAngle
		{
			get
			{
				return *this._repback__GroundAngle.GetValue();
			}
			set
			{
				this._repback__GroundAngle.SetValue(value);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004AEDC File Offset: 0x000490DC
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x0004AEEA File Offset: 0x000490EA
		[Net]
		[DefaultValue(0f)]
		public unsafe float Bounce
		{
			get
			{
				return *this._repback__Bounce.GetValue();
			}
			set
			{
				this._repback__Bounce.SetValue(value);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004AEF9 File Offset: 0x000490F9
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x0004AF07 File Offset: 0x00049107
		[Net]
		[DefaultValue(1f)]
		public unsafe float MoveFriction
		{
			get
			{
				return *this._repback__MoveFriction.GetValue();
			}
			set
			{
				this._repback__MoveFriction.SetValue(value);
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0004AF16 File Offset: 0x00049116
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x0004AF24 File Offset: 0x00049124
		[Net]
		[DefaultValue(18f)]
		public unsafe float StepSize
		{
			get
			{
				return *this._repback__StepSize.GetValue();
			}
			set
			{
				this._repback__StepSize.SetValue(value);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0004AF33 File Offset: 0x00049133
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x0004AF41 File Offset: 0x00049141
		[Net]
		[DefaultValue(140f)]
		public unsafe float MaxNonJumpVelocity
		{
			get
			{
				return *this._repback__MaxNonJumpVelocity.GetValue();
			}
			set
			{
				this._repback__MaxNonJumpVelocity.SetValue(value);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0004AF50 File Offset: 0x00049150
		// (set) Token: 0x06001345 RID: 4933 RVA: 0x0004AF5E File Offset: 0x0004915E
		[Net]
		[DefaultValue(32f)]
		public unsafe float BodyGirth
		{
			get
			{
				return *this._repback__BodyGirth.GetValue();
			}
			set
			{
				this._repback__BodyGirth.SetValue(value);
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0004AF6D File Offset: 0x0004916D
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x0004AF7B File Offset: 0x0004917B
		[Net]
		[DefaultValue(72f)]
		public unsafe float BodyHeight
		{
			get
			{
				return *this._repback__BodyHeight.GetValue();
			}
			set
			{
				this._repback__BodyHeight.SetValue(value);
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0004AF8A File Offset: 0x0004918A
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x0004AF98 File Offset: 0x00049198
		[Net]
		[DefaultValue(64f)]
		public unsafe float EyeHeight
		{
			get
			{
				return *this._repback__EyeHeight.GetValue();
			}
			set
			{
				this._repback__EyeHeight.SetValue(value);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0004AFA7 File Offset: 0x000491A7
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x0004AFB5 File Offset: 0x000491B5
		[Net]
		[DefaultValue(800f)]
		public unsafe float Gravity
		{
			get
			{
				return *this._repback__Gravity.GetValue();
			}
			set
			{
				this._repback__Gravity.SetValue(value);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0004AFC4 File Offset: 0x000491C4
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x0004AFD2 File Offset: 0x000491D2
		[Net]
		[DefaultValue(30f)]
		public unsafe float AirControl
		{
			get
			{
				return *this._repback__AirControl.GetValue();
			}
			set
			{
				this._repback__AirControl.SetValue(value);
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0004AFE1 File Offset: 0x000491E1
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x0004AFE9 File Offset: 0x000491E9
		[DefaultValue(false)]
		public bool Swimming { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0004AFF2 File Offset: 0x000491F2
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x0004B000 File Offset: 0x00049200
		[Net]
		[DefaultValue(false)]
		public unsafe bool AutoJump
		{
			get
			{
				return *this._repback__AutoJump.GetValue();
			}
			set
			{
				this._repback__AutoJump.SetValue(value);
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004B010 File Offset: 0x00049210
		public WalkController()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Duck = new Duck(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Unstuck = new Unstuck(this);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004B194 File Offset: 0x00049394
		[Description("This is temporary, get the hull size for the player's collision")]
		public override BBox GetHull()
		{
			float girth = this.BodyGirth * 0.5f;
			Vector3 vector = new Vector3(-girth, -girth, 0f);
			Vector3 maxs = new Vector3(girth, girth, this.BodyHeight);
			return new BBox(vector, maxs);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0004B1D1 File Offset: 0x000493D1
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

		// Token: 0x06001355 RID: 4949 RVA: 0x0004B208 File Offset: 0x00049408
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

		// Token: 0x06001356 RID: 4950 RVA: 0x0004B28B File Offset: 0x0004948B
		public override void FrameSimulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FrameSimulate();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeRotation = Input.Rotation;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004B2A8 File Offset: 0x000494A8
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
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay7 = GlobalGameNamespace.DebugOverlay;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
				defaultInterpolatedStringHandler.AppendLiteral("    Speed: ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(base.Velocity.Length);
				debugOverlay7.ScreenText(defaultInterpolatedStringHandler.ToStringAndClear(), lineOffset + 6, 0f);
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004B8EC File Offset: 0x00049AEC
		public virtual float GetWishSpeed()
		{
			float ws = this.Duck.GetWishSpeed();
			if (ws >= 0f)
			{
				return ws;
			}
			if (Input.Down(InputButton.Run))
			{
				return this.SprintSpeed;
			}
			if (Input.Down(InputButton.Walk))
			{
				return this.WalkSpeed;
			}
			return this.DefaultSpeed;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004B93C File Offset: 0x00049B3C
		public virtual void WalkMove()
		{
			Vector3 wishdir = base.WishVelocity.Normal;
			float wishspeed = base.WishVelocity.Length;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity = base.WishVelocity.WithZ(0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity = base.WishVelocity.Normal * wishspeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = base.Velocity.WithZ(0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Accelerate(wishdir, wishspeed, 0f, this.Acceleration);
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
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = base.Velocity.Normal * MathF.Min(base.Velocity.Length, this.GetWishSpeed());
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004BB30 File Offset: 0x00049D30
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

		// Token: 0x0600135B RID: 4955 RVA: 0x0004BBD8 File Offset: 0x00049DD8
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

		// Token: 0x0600135C RID: 4956 RVA: 0x0004BC78 File Offset: 0x00049E78
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

		// Token: 0x0600135D RID: 4957 RVA: 0x0004BCE8 File Offset: 0x00049EE8
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

		// Token: 0x0600135E RID: 4958 RVA: 0x0004BD60 File Offset: 0x00049F60
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

		// Token: 0x0600135F RID: 4959 RVA: 0x0004BE4C File Offset: 0x0004A04C
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

		// Token: 0x06001360 RID: 4960 RVA: 0x0004BED4 File Offset: 0x0004A0D4
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

		// Token: 0x06001361 RID: 4961 RVA: 0x0004BF68 File Offset: 0x0004A168
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

		// Token: 0x06001362 RID: 4962 RVA: 0x0004C0E0 File Offset: 0x0004A2E0
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

		// Token: 0x06001363 RID: 4963 RVA: 0x0004C15C File Offset: 0x0004A35C
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

		// Token: 0x06001364 RID: 4964 RVA: 0x0004C2CC File Offset: 0x0004A4CC
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

		// Token: 0x06001365 RID: 4965 RVA: 0x0004C368 File Offset: 0x0004A568
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

		// Token: 0x06001366 RID: 4966 RVA: 0x0004C39F File Offset: 0x0004A59F
		[Description("Traces the current bbox and returns the result. liftFeet will move the start position up by this amount, while keeping the top of the bbox at the same position. This is good when tracing down because you won't be tracing through the ceiling above.")]
		public override TraceResult TraceBBox(Vector3 start, Vector3 end, float liftFeet = 0f)
		{
			return this.TraceBBox(start, end, this.mins, this.maxs, liftFeet);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0004C3B8 File Offset: 0x0004A5B8
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

		// Token: 0x06001368 RID: 4968 RVA: 0x0004C47D File Offset: 0x0004A67D
		private void RestoreGroundPos()
		{
			if (base.GroundEntity != null)
			{
				bool isWorld = base.GroundEntity.IsWorld;
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0004C493 File Offset: 0x0004A693
		private void SaveGroundPos()
		{
			if (base.GroundEntity != null)
			{
				bool isWorld = base.GroundEntity.IsWorld;
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0004C4AC File Offset: 0x0004A6AC
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<float>>(ref this._repback__SprintSpeed, "SprintSpeed", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__WalkSpeed, "WalkSpeed", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__DefaultSpeed, "DefaultSpeed", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Acceleration, "Acceleration", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__AirAcceleration, "AirAcceleration", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__FallSoundZ, "FallSoundZ", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__GroundFriction, "GroundFriction", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__StopSpeed, "StopSpeed", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Size, "Size", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__DistEpsilon, "DistEpsilon", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__GroundAngle, "GroundAngle", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Bounce, "Bounce", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MoveFriction, "MoveFriction", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__StepSize, "StepSize", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__MaxNonJumpVelocity, "MaxNonJumpVelocity", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__BodyGirth, "BodyGirth", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__BodyHeight, "BodyHeight", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__EyeHeight, "EyeHeight", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__Gravity, "Gravity", false, false);
			builder.Register<VarUnmanaged<float>>(ref this._repback__AirControl, "AirControl", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__AutoJump, "AutoJump", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000625 RID: 1573
		public Duck Duck;

		// Token: 0x04000626 RID: 1574
		public Unstuck Unstuck;

		// Token: 0x04000627 RID: 1575
		protected Vector3 mins;

		// Token: 0x04000628 RID: 1576
		protected Vector3 maxs;

		// Token: 0x04000629 RID: 1577
		protected float SurfaceFriction;

		// Token: 0x0400062A RID: 1578
		private bool IsTouchingLadder;

		// Token: 0x0400062B RID: 1579
		private Vector3 LadderNormal;

		// Token: 0x0400062C RID: 1580
		private VarUnmanaged<float> _repback__SprintSpeed = new VarUnmanaged<float>(320f);

		// Token: 0x0400062D RID: 1581
		private VarUnmanaged<float> _repback__WalkSpeed = new VarUnmanaged<float>(150f);

		// Token: 0x0400062E RID: 1582
		private VarUnmanaged<float> _repback__DefaultSpeed = new VarUnmanaged<float>(190f);

		// Token: 0x0400062F RID: 1583
		private VarUnmanaged<float> _repback__Acceleration = new VarUnmanaged<float>(10f);

		// Token: 0x04000630 RID: 1584
		private VarUnmanaged<float> _repback__AirAcceleration = new VarUnmanaged<float>(50f);

		// Token: 0x04000631 RID: 1585
		private VarUnmanaged<float> _repback__FallSoundZ = new VarUnmanaged<float>(-30f);

		// Token: 0x04000632 RID: 1586
		private VarUnmanaged<float> _repback__GroundFriction = new VarUnmanaged<float>(4f);

		// Token: 0x04000633 RID: 1587
		private VarUnmanaged<float> _repback__StopSpeed = new VarUnmanaged<float>(100f);

		// Token: 0x04000634 RID: 1588
		private VarUnmanaged<float> _repback__Size = new VarUnmanaged<float>(20f);

		// Token: 0x04000635 RID: 1589
		private VarUnmanaged<float> _repback__DistEpsilon = new VarUnmanaged<float>(0.03125f);

		// Token: 0x04000636 RID: 1590
		private VarUnmanaged<float> _repback__GroundAngle = new VarUnmanaged<float>(46f);

		// Token: 0x04000637 RID: 1591
		private VarUnmanaged<float> _repback__Bounce = new VarUnmanaged<float>(0f);

		// Token: 0x04000638 RID: 1592
		private VarUnmanaged<float> _repback__MoveFriction = new VarUnmanaged<float>(1f);

		// Token: 0x04000639 RID: 1593
		private VarUnmanaged<float> _repback__StepSize = new VarUnmanaged<float>(18f);

		// Token: 0x0400063A RID: 1594
		private VarUnmanaged<float> _repback__MaxNonJumpVelocity = new VarUnmanaged<float>(140f);

		// Token: 0x0400063B RID: 1595
		private VarUnmanaged<float> _repback__BodyGirth = new VarUnmanaged<float>(32f);

		// Token: 0x0400063C RID: 1596
		private VarUnmanaged<float> _repback__BodyHeight = new VarUnmanaged<float>(72f);

		// Token: 0x0400063D RID: 1597
		private VarUnmanaged<float> _repback__EyeHeight = new VarUnmanaged<float>(64f);

		// Token: 0x0400063E RID: 1598
		private VarUnmanaged<float> _repback__Gravity = new VarUnmanaged<float>(800f);

		// Token: 0x0400063F RID: 1599
		private VarUnmanaged<float> _repback__AirControl = new VarUnmanaged<float>(30f);

		// Token: 0x04000640 RID: 1600
		private VarUnmanaged<bool> _repback__AutoJump = new VarUnmanaged<bool>(false);
	}
}
