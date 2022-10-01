using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200017D RID: 381
	[Library("ent_door")]
	[HammerEntity]
	[SupportsSolid]
	[Model(Archetypes = ModelArchetype.animated_model)]
	[DoorHelper("movedir", "movedir_islocal", "movedir_type", "distance")]
	[RenderFields]
	[VisGroup(VisGroup.Dynamic)]
	[Title("Door")]
	[Category("Gameplay")]
	[Icon("door_front")]
	[Description("A basic door entity that can move or rotate. It can be a model or a mesh entity. The door will rotate around the model's origin. For Hammer meshes the mesh origin can be set via the Pivot Tool.")]
	public class DoorEntity : KeyframeEntity, IUse
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00045796 File Offset: 0x00043996
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x0004579E File Offset: 0x0004399E
		[Property("spawnsettings", null, Title = "Spawn Settings")]
		[global::DefaultValue(DoorEntity.Flags.UseOpens)]
		[Description("Settings that are only applicable when the entity spawns")]
		public DoorEntity.Flags SpawnSettings { get; set; } = DoorEntity.Flags.UseOpens;

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x000457A7 File Offset: 0x000439A7
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x000457AF File Offset: 0x000439AF
		[Property("movedir", null, Title = "Move Direction (Pitch Yaw Roll)")]
		[Description("The direction the door will move, when it opens.")]
		public Angles MoveDir { get; set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000457B8 File Offset: 0x000439B8
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x000457C0 File Offset: 0x000439C0
		[Property("movedir_islocal", null, Title = "Move Direction is Expressed in Local Space")]
		[global::DefaultValue(true)]
		[Description("If checked, the movement direction angle is in local space and should be rotated by the entity's angles after spawning.")]
		public bool MoveDirIsLocal { get; set; } = true;

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000457C9 File Offset: 0x000439C9
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x000457D1 File Offset: 0x000439D1
		[Property("movedir_type", null, Title = "Movement Type")]
		[global::DefaultValue(DoorEntity.DoorMoveType.Moving)]
		[Description("Movement type of the door.")]
		public DoorEntity.DoorMoveType MoveDirType { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000457DA File Offset: 0x000439DA
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x000457E2 File Offset: 0x000439E2
		[Property]
		[global::DefaultValue(0)]
		[Description("Moving door: The amount, in inches, of the door to leave sticking out of the wall it recedes into when pressed. Negative values make the door recede even further into the wall. Rotating door: The amount, in degrees, that the door should rotate when it's pressed.")]
		public float Distance { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x000457EB File Offset: 0x000439EB
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x000457F3 File Offset: 0x000439F3
		[Property("initial_position", null)]
		[Category("Spawn Settings")]
		[MinMax(0f, 100f)]
		[global::DefaultValue(0)]
		[Description("How far the door should be open on spawn where 0% = closed and 100% = fully open.")]
		public float InitialPosition { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x000457FC File Offset: 0x000439FC
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x00045804 File Offset: 0x00043A04
		[Property("open_away", null, Title = "Open Away From Player")]
		[Category("Spawn Settings")]
		[global::DefaultValue(false)]
		[Description("If checked, rotating doors will try to open away from the activator")]
		public bool OpenAwayFromPlayer { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0004580D File Offset: 0x00043A0D
		// (set) Token: 0x060011CF RID: 4559 RVA: 0x00045815 File Offset: 0x00043A15
		[Property]
		[global::DefaultValue(100)]
		[Description("The speed at which the door moves.")]
		public float Speed { get; set; } = 100f;

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004581E File Offset: 0x00043A1E
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x00045826 File Offset: 0x00043A26
		[Property("close_delay", null, Title = "Auto Close Delay (-1 stay)")]
		[global::DefaultValue(4)]
		[Description("Amount of time, in seconds, after the door has opened before it closes automatically. If the value is set to -1, the door never closes itself.")]
		public float TimeBeforeReset { get; set; } = 4f;

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004582F File Offset: 0x00043A2F
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x00045837 File Offset: 0x00043A37
		[Property("other_doors_to_open", null)]
		[FGDType("target_destination", "", "")]
		[global::DefaultValue("")]
		[Description("If set, opening this door will open all doors with given entity name. You can also simply name all doors the same for this to work.")]
		public string OtherDoorsToOpen { get; set; } = "";

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00045840 File Offset: 0x00043A40
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x00045848 File Offset: 0x00043A48
		[Property]
		[global::DefaultValue(false)]
		[Description("If the door model supports break pieces and has prop_data with health, this option can be used to allow the door to break like a normal prop would.")]
		public bool Breakable { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00045851 File Offset: 0x00043A51
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x00045859 File Offset: 0x00043A59
		[Property("open_sound", null, Title = "Start Opening Sound")]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[global::DefaultValue("")]
		[Description("Sound to play when the door starts to open.")]
		public string OpenSound { get; set; } = "";

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00045862 File Offset: 0x00043A62
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0004586A File Offset: 0x00043A6A
		[Property("fully_open_sound", null, Title = "Fully Open Sound")]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[global::DefaultValue("")]
		[Description("Sound to play when the door reaches it's fully open position.")]
		public string FullyOpenSound { get; set; } = "";

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00045873 File Offset: 0x00043A73
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0004587B File Offset: 0x00043A7B
		[Property("close_sound", null, Title = "Start Closing Sound")]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[global::DefaultValue("")]
		[Description("Sound to play when the door starts to close.")]
		public string CloseSound { get; set; } = "";

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00045884 File Offset: 0x00043A84
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0004588C File Offset: 0x00043A8C
		[Property("fully_closed_sound", null, Title = "Fully Closed Sound")]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[global::DefaultValue("")]
		[Description("Sound to play when the door reaches it's fully closed position.")]
		public string FullyClosedSound { get; set; } = "";

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00045895 File Offset: 0x00043A95
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0004589D File Offset: 0x00043A9D
		[Property("locked_sound", null, Title = "Locked Sound")]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[global::DefaultValue("")]
		[Description("Sound to play when the door is attempted to be opened, but is locked.")]
		public string LockedSound { get; set; } = "";

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000458A6 File Offset: 0x00043AA6
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x000458AE File Offset: 0x00043AAE
		[Property("moving_sound", null, Title = "Moving Sound")]
		[FGDType("sound", "", "")]
		[Category("Sounds")]
		[global::DefaultValue("")]
		[Description("Sound to play while the door is moving. Typically this should be looping or very long.")]
		public string MovingSound { get; set; } = "";

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x000458B7 File Offset: 0x00043AB7
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x000458BF File Offset: 0x00043ABF
		[Property("open_ease", null, Title = "Ease Function")]
		[Description("Used to override the open/close animation of moving and rotating doors. X axis (input, left to right) is the animation, Y axis (output, bottom to top) is how open the door is at that point in the animation.")]
		public FGDCurve OpenCurve { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x000458C8 File Offset: 0x00043AC8
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x000458D6 File Offset: 0x00043AD6
		[Net]
		[Description("Whether this door is locked or not.")]
		public unsafe bool Locked
		{
			get
			{
				return *this._repback__Locked.GetValue();
			}
			set
			{
				this._repback__Locked.SetValue(value);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x000458E5 File Offset: 0x00043AE5
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x000458ED File Offset: 0x00043AED
		[Description("The easing function for both movement and rotation TODO: Expose to hammer in a nice way")]
		public Easing.Function Ease { get; set; } = new Easing.Function(Easing.EaseOut);

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x000458F6 File Offset: 0x00043AF6
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x00045904 File Offset: 0x00043B04
		[Net]
		[global::DefaultValue(DoorEntity.DoorState.Open)]
		[Description("Which position the door is in.")]
		public unsafe DoorEntity.DoorState State
		{
			get
			{
				return *this._repback__State.GetValue();
			}
			set
			{
				this._repback__State.SetValue(value);
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00045914 File Offset: 0x00043B14
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Keyframed, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionA = this.LocalPosition;
			Vector3 dir = this.MoveDir.Direction;
			Vector3 boundSize = base.CollisionBounds.Size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionB = this.PositionA + dir * (MathF.Abs(boundSize.Dot(dir)) - this.Distance);
			if (this.MoveDirIsLocal)
			{
				Vector3 dir_world = base.Transform.NormalToWorld(dir);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PositionB = this.PositionA + dir_world * (MathF.Abs(boundSize.Dot(dir)) - this.Distance);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RotationA = this.LocalRotation;
			Vector3 axis = Rotation.From(this.MoveDir).Up;
			if (!this.MoveDirIsLocal)
			{
				axis = base.Transform.NormalToLocal(axis);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RotationB_Opposite = this.RotationA.RotateAroundAxis(axis, -this.Distance);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RotationB_Normal = this.RotationA.RotateAroundAxis(axis, this.Distance);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RotationB = this.RotationB_Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.State = DoorEntity.DoorState.Closed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Locked = this.SpawnSettings.HasFlag(DoorEntity.Flags.StartLocked);
			if (this.InitialPosition > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetPosition(this.InitialPosition / 100f);
			}
			if (this.OpenCurve != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Ease = ((float x) => this.OpenCurve.GetNormalized(x, true));
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00045AE4 File Offset: 0x00043CE4
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

		// Token: 0x060011EC RID: 4588 RVA: 0x00045B34 File Offset: 0x00043D34
		[Input]
		[Description("Sets the door's position to given percentage. The expected input range is 0..1")]
		private void SetPosition(float progress)
		{
			if (this.MoveDirType == DoorEntity.DoorMoveType.Moving)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LocalPosition = this.PositionA.LerpTo(this.PositionB, progress);
			}
			else if (this.MoveDirType == DoorEntity.DoorMoveType.Rotating)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LocalRotation = Rotation.Lerp(this.RotationA, this.RotationB, progress, true);
			}
			else if (this.MoveDirType == DoorEntity.DoorMoveType.AnimatingOnly)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter("initial_position", progress);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter("update_position", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateAnimGraph(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.State = DoorEntity.DoorState.Closed;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("{0}: Unknown door move type {1}!", new object[]
				{
					this,
					this.MoveDirType
				}));
			}
			if (progress >= 1f)
			{
				this.State = DoorEntity.DoorState.Open;
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00045C19 File Offset: 0x00043E19
		private void UpdateAnimGraph(bool open)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter("open", open);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00045C2C File Offset: 0x00043E2C
		protected override void OnAnimGraphCreated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnimGraphCreated();
			if (this.State == DoorEntity.DoorState.Open)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateAnimGraph(true);
			}
			if (this.InitialPosition > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter("initial_position", this.InitialPosition / 100f);
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00045C81 File Offset: 0x00043E81
		public virtual bool IsUsable(Entity user)
		{
			return this.SpawnSettings.HasFlag(DoorEntity.Flags.UseOpens);
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00045C99 File Offset: 0x00043E99
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x00045CA1 File Offset: 0x00043EA1
		[Description("Fired when a player tries to open/close this door with +use, but it's locked")]
		protected Entity.Output OnLockedUse { get; set; }

		// Token: 0x060011F2 RID: 4594 RVA: 0x00045CAC File Offset: 0x00043EAC
		public virtual bool OnUse(Entity user)
		{
			if (this.Locked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.LockedSound);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter("locked", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnLockedUse.Fire(this, 0f);
				return false;
			}
			if (this.SpawnSettings.HasFlag(DoorEntity.Flags.UseOpens))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Toggle(user);
			}
			return false;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00045D28 File Offset: 0x00043F28
		public override void OnNewModel(Model model)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnNewModel(model);
			if (model == null || model.IsError)
			{
				return;
			}
			if (!base.IsServer)
			{
				return;
			}
			ModelDoorSounds sounds;
			if (model.TryGetData<ModelDoorSounds>(out sounds))
			{
				if (string.IsNullOrEmpty(this.MovingSound))
				{
					this.MovingSound = sounds.MovingSound;
				}
				if (string.IsNullOrEmpty(this.CloseSound))
				{
					this.CloseSound = sounds.CloseSound;
				}
				if (string.IsNullOrEmpty(this.FullyClosedSound))
				{
					this.FullyClosedSound = sounds.FullyClosedSound;
				}
				if (string.IsNullOrEmpty(this.OpenSound))
				{
					this.OpenSound = sounds.OpenSound;
				}
				if (string.IsNullOrEmpty(this.FullyOpenSound))
				{
					this.FullyOpenSound = sounds.FullyOpenSound;
				}
				if (string.IsNullOrEmpty(this.LockedSound))
				{
					this.LockedSound = sounds.LockedSound;
				}
			}
			ModelPropData propInfo;
			if (model.TryGetData<ModelPropData>(out propInfo))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Health = propInfo.Health;
			}
			if (base.Health <= 0f)
			{
				base.Health = -1f;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00045E2C File Offset: 0x0004402C
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x00045E34 File Offset: 0x00044034
		[Description("Fired when the entity gets damaged, even if it is unbreakable.")]
		protected Entity.Output OnDamaged { get; set; }

		// Token: 0x060011F6 RID: 4598 RVA: 0x00045E40 File Offset: 0x00044040
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnDamaged.Fire(this, 0f);
			if (!this.Breakable)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LastDamage = info;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00045E88 File Offset: 0x00044088
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00045E90 File Offset: 0x00044090
		[Description("Fired when the entity gets destroyed.")]
		protected Entity.Output OnBreak { get; set; }

		// Token: 0x060011F9 RID: 4601 RVA: 0x00045E9C File Offset: 0x0004409C
		public override void OnKilled()
		{
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			Breakables.Result result = new Breakables.Result();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			result.CopyParamsFrom(this.LastDamage);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Breakables.Break(this, result);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnBreak.Fire(this.LastDamage.Attacker, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00045F04 File Offset: 0x00044104
		[Input]
		[Description("Causes this prop to break, regardless if it is actually breakable or not. (i.e. ignores health and whether the model has gibs)")]
		public void Break()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnKilled();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Dead;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00045F28 File Offset: 0x00044128
		[Input]
		[Description("Toggle the open state of the door. Obeys locked state.")]
		public void Toggle(Entity activator = null)
		{
			if (this.State == DoorEntity.DoorState.Open || this.State == DoorEntity.DoorState.Opening)
			{
				this.Close(activator);
				return;
			}
			if (this.State == DoorEntity.DoorState.Closed || this.State == DoorEntity.DoorState.Closing)
			{
				this.Open(activator);
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00045F5C File Offset: 0x0004415C
		[Input]
		[Description("Open the door. Obeys locked state.")]
		public void Open(Entity activator = null)
		{
			if (this.Locked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.LockedSound);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter("locked", true);
				return;
			}
			if (this.State == DoorEntity.DoorState.Closed)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.OpenSound);
			}
			if (this.State == DoorEntity.DoorState.Closed || this.State == DoorEntity.DoorState.Closing)
			{
				this.State = DoorEntity.DoorState.Opening;
			}
			if (activator != null && this.MoveDirType == DoorEntity.DoorMoveType.Rotating && this.OpenAwayFromPlayer && this.State != DoorEntity.DoorState.Open)
			{
				Vector3 axis = Rotation.From(this.MoveDir).Up;
				if (!this.MoveDirIsLocal)
				{
					axis = base.Transform.NormalToLocal(axis);
				}
				Vector3 Dir = (base.WorldSpaceBounds.Center.WithZ(this.Position.z) - this.Position).Normal;
				Vector3 Pos = this.Position + Rotation.FromAxis(Dir, 0f).RotateAroundAxis(axis, this.Distance) * Dir * 24f;
				Vector3 Pos2 = this.Position + Rotation.FromAxis(Dir, 0f).RotateAroundAxis(axis, -this.Distance) * Dir * 24f;
				Vector3 PlyPos = activator.Position;
				if (PlyPos.Distance(Pos2) < PlyPos.Distance(Pos))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.RotationB = this.RotationB_Normal;
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.RotationB = this.RotationB_Opposite;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateAnimGraph(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateState();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OpenOtherDoors(true, activator);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0004612C File Offset: 0x0004432C
		[Input]
		[Description("Close the door. Obeys locked state.")]
		public void Close(Entity activator = null)
		{
			if (this.Locked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.LockedSound);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter("locked", true);
				return;
			}
			if (this.State == DoorEntity.DoorState.Open)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.CloseSound);
			}
			if (this.State == DoorEntity.DoorState.Open || this.State == DoorEntity.DoorState.Opening)
			{
				this.State = DoorEntity.DoorState.Closing;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateAnimGraph(false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateState();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OpenOtherDoors(false, activator);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000461BB File Offset: 0x000443BB
		[Input]
		[Description("Locks the door so it cannot be opened or closed.")]
		public void Lock()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Locked = true;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000461C9 File Offset: 0x000443C9
		[Input]
		[Description("Unlocks the door.")]
		public void Unlock()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Locked = false;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000461D7 File Offset: 0x000443D7
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x000461DF File Offset: 0x000443DF
		[Description("Fired when the door starts to open. This can be called multiple times during a single \"door opening\"")]
		protected Entity.Output OnOpen { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000461E8 File Offset: 0x000443E8
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x000461F0 File Offset: 0x000443F0
		[Description("Fired when the door starts to close. This can be called multiple times during a single \"door closing\"")]
		protected Entity.Output OnClose { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x000461F9 File Offset: 0x000443F9
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x00046201 File Offset: 0x00044401
		[Description("Called when the door fully opens.")]
		protected Entity.Output OnFullyOpen { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0004620A File Offset: 0x0004440A
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x00046212 File Offset: 0x00044412
		[Description("Called when the door fully closes.")]
		protected Entity.Output OnFullyClosed { get; set; }

		// Token: 0x06001208 RID: 4616 RVA: 0x0004621C File Offset: 0x0004441C
		private void OpenOtherDoors(bool open, Entity activator)
		{
			if (!this.ShouldPropagateState)
			{
				return;
			}
			List<Entity> ents = new List<Entity>();
			if (!string.IsNullOrEmpty(base.Name))
			{
				ents.AddRange(Entity.FindAllByName(base.Name));
			}
			if (!string.IsNullOrEmpty(this.OtherDoorsToOpen))
			{
				ents.AddRange(Entity.FindAllByName(this.OtherDoorsToOpen));
			}
			foreach (Entity ent in ents)
			{
				if (ent != this && ent is DoorEntity)
				{
					DoorEntity door = (DoorEntity)ent;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					door.ShouldPropagateState = false;
					if (open)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						door.Open(activator);
					}
					else
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						door.Close(activator);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					door.ShouldPropagateState = true;
				}
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000462F8 File Offset: 0x000444F8
		private void UpdateState()
		{
			bool open = this.State == DoorEntity.DoorState.Opening || this.State == DoorEntity.DoorState.Open;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMove(open);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00046328 File Offset: 0x00044528
		private Task DoMove(bool state)
		{
			DoorEntity.<DoMove>d__142 <DoMove>d__;
			<DoMove>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoMove>d__.<>4__this = this;
			<DoMove>d__.state = state;
			<DoMove>d__.<>1__state = -1;
			<DoMove>d__.<>t__builder.Start<DoorEntity.<DoMove>d__142>(ref <DoMove>d__);
			return <DoMove>d__.<>t__builder.Task;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00046373 File Offset: 0x00044573
		protected override void OnAnimGraphTag(string tag, AnimatedEntity.AnimGraphTagEvent fireMode)
		{
			if (tag == "AnimationFinished" && fireMode != AnimatedEntity.AnimGraphTagEvent.End)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AnimGraphFinished = true;
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00046392 File Offset: 0x00044592
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Locked, "Locked", false, false);
			builder.Register<VarUnmanaged<DoorEntity.DoorState>>(ref this._repback__State, "State", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000463C4 File Offset: 0x000445C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnLockedUse = new Entity.Output(this, "OnLockedUse");
			this.OnDamaged = new Entity.Output(this, "OnDamaged");
			this.OnBreak = new Entity.Output(this, "OnBreak");
			this.OnOpen = new Entity.Output(this, "OnOpen");
			this.OnClose = new Entity.Output(this, "OnClose");
			this.OnFullyOpen = new Entity.Output(this, "OnFullyOpen");
			this.OnFullyClosed = new Entity.Output(this, "OnFullyClosed");
			base.CreateHammerOutputs();
		}

		// Token: 0x040005A1 RID: 1441
		private Vector3 PositionA;

		// Token: 0x040005A2 RID: 1442
		private Vector3 PositionB;

		// Token: 0x040005A3 RID: 1443
		private Rotation RotationA;

		// Token: 0x040005A4 RID: 1444
		private Rotation RotationB;

		// Token: 0x040005A5 RID: 1445
		private Rotation RotationB_Normal;

		// Token: 0x040005A6 RID: 1446
		private Rotation RotationB_Opposite;

		// Token: 0x040005A8 RID: 1448
		private DamageInfo LastDamage;

		// Token: 0x040005AF RID: 1455
		internal bool ShouldPropagateState = true;

		// Token: 0x040005B0 RID: 1456
		private int movement;

		// Token: 0x040005B1 RID: 1457
		private Sound? MoveSoundInstance;

		// Token: 0x040005B2 RID: 1458
		private bool AnimGraphFinished;

		// Token: 0x040005B3 RID: 1459
		private VarUnmanaged<bool> _repback__Locked = new VarUnmanaged<bool>();

		// Token: 0x040005B4 RID: 1460
		private VarUnmanaged<DoorEntity.DoorState> _repback__State = new VarUnmanaged<DoorEntity.DoorState>(DoorEntity.DoorState.Open);

		// Token: 0x0200023D RID: 573
		[Flags]
		public enum Flags
		{
			// Token: 0x04000971 RID: 2417
			UseOpens = 1,
			// Token: 0x04000972 RID: 2418
			StartLocked = 2
		}

		// Token: 0x0200023E RID: 574
		public enum DoorMoveType
		{
			// Token: 0x04000974 RID: 2420
			Moving,
			// Token: 0x04000975 RID: 2421
			Rotating,
			// Token: 0x04000976 RID: 2422
			AnimatingOnly
		}

		// Token: 0x0200023F RID: 575
		public enum DoorState
		{
			// Token: 0x04000978 RID: 2424
			Open,
			// Token: 0x04000979 RID: 2425
			Closed,
			// Token: 0x0400097A RID: 2426
			Opening,
			// Token: 0x0400097B RID: 2427
			Closing
		}
	}
}
