using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox.Internal;
using Sandbox.Internal.Globals;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200017A RID: 378
	[Library("ent_button")]
	[HammerEntity]
	[SupportsSolid]
	[DoorHelper("movedir", "movedir_islocal", "movedir_type", "distance")]
	[RenderFields]
	[VisGroup(VisGroup.Dynamic)]
	[Model(Archetypes = (ModelArchetype)3)]
	[Title("Button")]
	[Category("Gameplay")]
	[Icon("radio_button_checked")]
	[Description("A generic button that is useful to control other map entities via inputs/outputs.")]
	public class ButtonEntity : KeyframeEntity, IUse
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00044D0F File Offset: 0x00042F0F
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x00044D17 File Offset: 0x00042F17
		[Property(Title = "Move Direction")]
		[Description("Specifies the direction to move in when the button is used, or axis of rotation for rotating buttons.")]
		public Angles MoveDir { get; set; } = new Angles(0f, 0f, 0f);

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00044D20 File Offset: 0x00042F20
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x00044D28 File Offset: 0x00042F28
		[Property("movedir_islocal", null, Title = "Move Direction is Expressed in Local Space")]
		[global::DefaultValue(true)]
		[Description("If checked, the movement direction angle is in local space and should be rotated by the entity's angles after spawning.")]
		public bool MoveDirIsLocal { get; set; } = true;

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00044D31 File Offset: 0x00042F31
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x00044D39 File Offset: 0x00042F39
		[Property("movedir_type", null, Title = "Movement Type")]
		[global::DefaultValue(ButtonEntity.ButtonMoveType.Moving)]
		[Description("Movement type of the button.")]
		public ButtonEntity.ButtonMoveType MoveDirType { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00044D42 File Offset: 0x00042F42
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x00044D4A File Offset: 0x00042F4A
		[Property]
		[global::DefaultValue(1f)]
		[Description("Moving button: The amount, in inches, of the button to leave sticking out of the wall it recedes into when pressed. Negative values make the button recede even further into the wall. Rotating button: The amount, in degrees, that the button should rotate when it's pressed.")]
		public float Distance { get; set; } = 1f;

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00044D53 File Offset: 0x00042F53
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x00044D5B File Offset: 0x00042F5B
		[Property]
		[global::DefaultValue(100f)]
		[Description("The speed at which the button moves, in inches per second or degrees per second.")]
		public float Speed { get; set; } = 100f;

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00044D64 File Offset: 0x00042F64
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x00044D6C File Offset: 0x00042F6C
		[Property]
		[global::DefaultValue(0)]
		[Description("The speed at which the button returns to the initial position. 0 or less will function as the 'forward' move speed.")]
		public float ReturnSpeed { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00044D75 File Offset: 0x00042F75
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x00044D7D File Offset: 0x00042F7D
		[Property("reset_delay", null, Title = "Reset Delay (-1 stay)")]
		[global::DefaultValue(1f)]
		[Description("Amount of time, in seconds, after the button has been fully pressed before it starts to return to the starting position. Once it has returned, it can be used again. If the value is set to -1, the button never returns.")]
		public float ResetDelay { get; set; } = 1f;

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00044D86 File Offset: 0x00042F86
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x00044D8E File Offset: 0x00042F8E
		[Property("unlocked_sound", null, Title = "Activation Sound")]
		[FGDType("sound", "", "")]
		[Description("Sound played when the button is pressed and is unlocked")]
		public string UnlockedSound { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00044D97 File Offset: 0x00042F97
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x00044D9F File Offset: 0x00042F9F
		[Property("locked_sound", null, Title = "Locked Activation Sound")]
		[FGDType("sound", "", "")]
		[Description("Sound played when the button is pressed and is locked")]
		public string LockedSound { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00044DA8 File Offset: 0x00042FA8
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00044DB0 File Offset: 0x00042FB0
		[Property]
		[global::DefaultValue(false)]
		[Description("If enabled, the button will have to be activated again to return to initial position.")]
		public bool Toggle { get; set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00044DB9 File Offset: 0x00042FB9
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x00044DC1 File Offset: 0x00042FC1
		[Property]
		[global::DefaultValue(false)]
		[Description("If enabled, the button will have to be held to reach the pressed in state.")]
		public bool Momentary { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00044DCA File Offset: 0x00042FCA
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x00044DD2 File Offset: 0x00042FD2
		[Description("Whether the button is locked or not.")]
		public bool Locked { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00044DDB File Offset: 0x00042FDB
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x00044DE3 File Offset: 0x00042FE3
		[Property]
		[global::DefaultValue(ButtonEntity.ActivationFlags.UseActivates)]
		[Description("How this button can be activated")]
		public ButtonEntity.ActivationFlags ActivationSettings { get; set; } = ButtonEntity.ActivationFlags.UseActivates;

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00044DEC File Offset: 0x00042FEC
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x00044DF4 File Offset: 0x00042FF4
		[Property]
		[Description("Settings that are only relevant on spawn")]
		public ButtonEntity.Flags SpawnSettings { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00044DFD File Offset: 0x00042FFD
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x00044E05 File Offset: 0x00043005
		[Description("Fired when the button position changes, carries 0...1 as argument.")]
		protected Entity.Output<float> OnProgress { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00044E49 File Offset: 0x00043049
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x00044E10 File Offset: 0x00043010
		public float Progress
		{
			get
			{
				return this._progress;
			}
			protected set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._progress = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnProgress.Fire(this, this._progress, 0f);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00044E54 File Offset: 0x00043054
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			if (base.SetupPhysicsFromModel(PhysicsMotionType.Keyframed, false) == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("{0} has a model {1} with no physics!", new object[]
				{
					this,
					base.Model
				}));
			}
			if (this.SpawnSettings.HasFlag(ButtonEntity.Flags.NonSolid))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableSolidCollisions = false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Locked = this.SpawnSettings.HasFlag(ButtonEntity.Flags.StartsLocked);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionStart = this.LocalPosition;
			Vector3 dir = this.MoveDir.Direction;
			Vector3 boundSize = base.CollisionBounds.Size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionEnd = this.PositionStart + dir * (MathF.Abs(boundSize.Dot(dir)) - this.Distance);
			if (this.MoveDirIsLocal)
			{
				Vector3 dir_world = base.Transform.NormalToWorld(dir);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PositionEnd = this.PositionStart + dir_world * (MathF.Abs(boundSize.Dot(dir)) - this.Distance);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RotationStart = this.LocalRotation;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00044FA2 File Offset: 0x000431A2
		public virtual bool IsUsable(Entity user)
		{
			return !this.Moving && this.ActivationSettings.HasFlag(ButtonEntity.ActivationFlags.UseActivates);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00044FC4 File Offset: 0x000431C4
		private Task FireRelease()
		{
			ButtonEntity.<FireRelease>d__76 <FireRelease>d__;
			<FireRelease>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FireRelease>d__.<>4__this = this;
			<FireRelease>d__.<>1__state = -1;
			<FireRelease>d__.<>t__builder.Start<ButtonEntity.<FireRelease>d__76>(ref <FireRelease>d__);
			return <FireRelease>d__.<>t__builder.Task;
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00045007 File Offset: 0x00043207
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x0004500F File Offset: 0x0004320F
		[Description("Fired when the button is used while locked")]
		protected Entity.Output OnUseLocked { get; set; }

		// Token: 0x06001196 RID: 4502 RVA: 0x00045018 File Offset: 0x00043218
		[Description("A player has pressed this")]
		public virtual bool OnUse(Entity user)
		{
			if (!this.ActivationSettings.HasFlag(ButtonEntity.ActivationFlags.UseActivates))
			{
				return false;
			}
			if (this.Locked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnUseLocked.Fire(user, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.LockedSound);
				return false;
			}
			if (this.LastUsed > 0.1f)
			{
				this.DoPress(user);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LastUsed = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FireRelease();
			return this.Momentary;
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x000450B5 File Offset: 0x000432B5
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x000450BD File Offset: 0x000432BD
		[Description("Fired when the button is damaged")]
		protected Entity.Output OnDamaged { get; set; }

		// Token: 0x06001199 RID: 4505 RVA: 0x000450C8 File Offset: 0x000432C8
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnDamaged.Fire(info.Attacker, 0f);
			if (this.ActivationSettings.HasFlag(ButtonEntity.ActivationFlags.DamageActivates))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoPress(info.Attacker);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnReleased.Fire(this, 0f);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00045145 File Offset: 0x00043345
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x0004514D File Offset: 0x0004334D
		[Description("Fired when the button is pressed by any means. This is not the same as button reaching its pressed in position.")]
		protected Entity.Output OnPressed { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00045156 File Offset: 0x00043356
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x0004515E File Offset: 0x0004335E
		[Description("Fired when the button was released. This is not the same as button reaching its initial position.")]
		protected Entity.Output OnReleased { get; set; }

		// Token: 0x0600119E RID: 4510 RVA: 0x00045168 File Offset: 0x00043368
		private void DoPress(Entity activator)
		{
			if (this.Moving)
			{
				return;
			}
			if (this.Locked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.LockedSound);
				return;
			}
			bool targetState = !this.State;
			if (this.Momentary && !this.Toggle)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetState = true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound(this.UnlockedSound);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnPressed.Fire(activator, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MoveToPosition(targetState, activator);
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x000451F4 File Offset: 0x000433F4
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x000451FC File Offset: 0x000433FC
		[Description("Fired when the button reaches the in/pressed position")]
		protected Entity.Output OnIn { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00045205 File Offset: 0x00043405
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x0004520D File Offset: 0x0004340D
		[Description("Fired when the button reaches the out/released position")]
		protected Entity.Output OnOut { get; set; }

		// Token: 0x060011A3 RID: 4515 RVA: 0x00045218 File Offset: 0x00043418
		[Event.Tick.ServerAttribute]
		private void Think()
		{
			if (this.Momentary && !this.Toggle && this.LastUsed > this.ResetDelay && !this.Moving && this.Progress > 0f && !this.Locked)
			{
				float progDir = -1f;
				if (this.MoveDirType == ButtonEntity.ButtonMoveType.Moving)
				{
					Vector3 targetPos = this.PositionStart;
					float timeToTake = Math.Abs(Vector3.DistanceBetween(this.LocalPosition, targetPos) / Math.Max((this.ReturnSpeed > 0f) ? this.ReturnSpeed : this.Speed, 0.1f));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Progress = Math.Clamp(this.Progress + (Time.Now - this.lastThinkTime) / timeToTake * progDir, 0f, 1f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.LocalPosition = this.PositionStart.LerpTo(this.PositionEnd, this.Progress);
				}
				else if (this.MoveDirType == ButtonEntity.ButtonMoveType.Rotating)
				{
					float timeToTake2 = Math.Abs(this.Distance / Math.Max((this.ReturnSpeed > 0f) ? this.ReturnSpeed : this.Speed, 0.1f));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Progress = Math.Clamp(this.Progress + (Time.Now - this.lastThinkTime) / timeToTake2 * progDir, 0f, 1f);
					Vector3 axis = Rotation.From(this.MoveDir).Up;
					if (!this.MoveDirIsLocal)
					{
						axis = base.Transform.NormalToLocal(axis);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.LocalRotation = this.RotationStart.RotateAroundAxis(axis, this.Distance * this.Progress);
				}
				if (this.Progress == 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.OnOut.Fire(this, 0f);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastThinkTime = Time.Now;
			if (base.DebugFlags.HasFlag(EntityDebugFlags.Text))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay = GlobalGameNamespace.DebugOverlay;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
				defaultInterpolatedStringHandler.AppendLiteral("State: ");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(this.State);
				defaultInterpolatedStringHandler.AppendLiteral("\nProgress: ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.Progress);
				debugOverlay.Text(defaultInterpolatedStringHandler.ToStringAndClear(), base.WorldSpaceBounds.Center, 10, Color.White, 0f, 1500f);
				Vector3 dir_world = this.MoveDir.Direction;
				if (this.MoveDirIsLocal)
				{
					dir_world = base.Transform.NormalToWorld(this.MoveDir.Direction);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Line(this.Position, this.Position + dir_world * 100f, 0f, true);
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00045518 File Offset: 0x00043718
		private Task MoveToPosition(bool state, Entity activator)
		{
			ButtonEntity.<MoveToPosition>d__106 <MoveToPosition>d__;
			<MoveToPosition>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MoveToPosition>d__.<>4__this = this;
			<MoveToPosition>d__.state = state;
			<MoveToPosition>d__.activator = activator;
			<MoveToPosition>d__.<>1__state = -1;
			<MoveToPosition>d__.<>t__builder.Start<ButtonEntity.<MoveToPosition>d__106>(ref <MoveToPosition>d__);
			return <MoveToPosition>d__.<>t__builder.Task;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004556B File Offset: 0x0004376B
		[Input]
		[Description("Become locked")]
		protected void Lock()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Locked = true;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00045579 File Offset: 0x00043779
		[Input]
		[Description("Become unlocked")]
		protected void Unlock()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Locked = false;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00045588 File Offset: 0x00043788
		[Input]
		[Description("Simulates the button being pressed")]
		protected void Press(Entity activator)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoPress(activator);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnReleased.Fire(this, 0f);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000455BC File Offset: 0x000437BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnProgress = new Entity.Output<float>(this, "OnProgress");
			this.OnUseLocked = new Entity.Output(this, "OnUseLocked");
			this.OnDamaged = new Entity.Output(this, "OnDamaged");
			this.OnPressed = new Entity.Output(this, "OnPressed");
			this.OnReleased = new Entity.Output(this, "OnReleased");
			this.OnIn = new Entity.Output(this, "OnIn");
			this.OnOut = new Entity.Output(this, "OnOut");
			base.CreateHammerOutputs();
		}

		// Token: 0x04000575 RID: 1397
		private float _progress;

		// Token: 0x04000576 RID: 1398
		private TimeSince LastUsed;

		// Token: 0x04000577 RID: 1399
		private bool State;

		// Token: 0x04000578 RID: 1400
		private bool Moving;

		// Token: 0x04000579 RID: 1401
		private Vector3 PositionStart;

		// Token: 0x0400057A RID: 1402
		private Vector3 PositionEnd;

		// Token: 0x0400057B RID: 1403
		private Rotation RotationStart;

		// Token: 0x0400057C RID: 1404
		private int globalTimers;

		// Token: 0x04000583 RID: 1411
		private float lastThinkTime;

		// Token: 0x02000238 RID: 568
		public enum ButtonMoveType
		{
			// Token: 0x04000958 RID: 2392
			Moving,
			// Token: 0x04000959 RID: 2393
			Rotating,
			// Token: 0x0400095A RID: 2394
			NotMoving
		}

		// Token: 0x02000239 RID: 569
		[Flags]
		public enum ActivationFlags
		{
			// Token: 0x0400095C RID: 2396
			UseActivates = 1,
			// Token: 0x0400095D RID: 2397
			DamageActivates = 2
		}

		// Token: 0x0200023A RID: 570
		[Flags]
		public enum Flags
		{
			// Token: 0x0400095F RID: 2399
			StartsLocked = 1,
			// Token: 0x04000960 RID: 2400
			NonSolid = 2
		}
	}
}
