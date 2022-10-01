using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000030 RID: 48
	public class PawnController : BaseNetworkable
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000A91F File Offset: 0x00008B1F
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000A927 File Offset: 0x00008B27
		public Entity Pawn { get; protected set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000A930 File Offset: 0x00008B30
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000A938 File Offset: 0x00008B38
		public Client Client { get; protected set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000A941 File Offset: 0x00008B41
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000A949 File Offset: 0x00008B49
		public Vector3 Position { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000A952 File Offset: 0x00008B52
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000A95A File Offset: 0x00008B5A
		public Rotation Rotation { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000A963 File Offset: 0x00008B63
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000A96B File Offset: 0x00008B6B
		public Vector3 Velocity { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000A974 File Offset: 0x00008B74
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000A97C File Offset: 0x00008B7C
		public Rotation EyeRotation { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000A985 File Offset: 0x00008B85
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000A98D File Offset: 0x00008B8D
		public Vector3 EyeLocalPosition { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000A996 File Offset: 0x00008B96
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000A99E File Offset: 0x00008B9E
		public Vector3 BaseVelocity { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000A9A7 File Offset: 0x00008BA7
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000A9AF File Offset: 0x00008BAF
		public Entity GroundEntity { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public Vector3 GroundNormal { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000A9C9 File Offset: 0x00008BC9
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000A9D1 File Offset: 0x00008BD1
		public Vector3 WishVelocity { get; set; }

		// Token: 0x06000184 RID: 388 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public void UpdateFromEntity(Entity entity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = entity.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = entity.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = entity.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EyeRotation = entity.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EyeLocalPosition = entity.EyeLocalPosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BaseVelocity = entity.BaseVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GroundEntity = entity.GroundEntity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WishVelocity = entity.Velocity;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000AA74 File Offset: 0x00008C74
		public void UpdateFromController(PawnController controller)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Pawn = controller.Pawn;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Client = controller.Client;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = controller.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = controller.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = controller.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EyeRotation = controller.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GroundEntity = controller.GroundEntity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BaseVelocity = controller.BaseVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EyeLocalPosition = controller.EyeLocalPosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WishVelocity = controller.WishVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GroundNormal = controller.GroundNormal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Events = controller.Events;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags = controller.Tags;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000AB60 File Offset: 0x00008D60
		public void Finalize(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.Position = this.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.Velocity = this.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.Rotation = this.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.GroundEntity = this.GroundEntity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.BaseVelocity = this.BaseVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.EyeLocalPosition = this.EyeLocalPosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			target.EyeRotation = this.EyeRotation;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		[Description("This is what your logic should be going in")]
		public virtual void Simulate()
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000ABE6 File Offset: 0x00008DE6
		[Description("This is called every frame on the client only")]
		public virtual void FrameSimulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("FrameSimulate");
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		[Description("Call OnEvent for each event")]
		public virtual void RunEvents(PawnController additionalController)
		{
			if (this.Events == null)
			{
				return;
			}
			foreach (string e in this.Events)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnEvent(e);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (additionalController != null)
				{
					additionalController.OnEvent(e);
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000AC68 File Offset: 0x00008E68
		[Description("An event has been triggered - maybe handle it")]
		public virtual void OnEvent(string name)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000AC6A File Offset: 0x00008E6A
		[Description("Returns true if we have this event")]
		public bool HasEvent(string eventName)
		{
			return this.Events != null && this.Events.Contains(eventName);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000AC82 File Offset: 0x00008E82
		public bool HasTag(string tagName)
		{
			return this.Tags != null && this.Tags.Contains(tagName);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000AC9A File Offset: 0x00008E9A
		[Description("Allows the controller to pass events to other systems while staying abstracted. For example, it could pass a \"jump\" event, which could then be picked up by the playeranimator to trigger a jump animation, and picked up by the player to play a jump sound.")]
		public void AddEvent(string eventName)
		{
			if (this.Events == null)
			{
				this.Events = new HashSet<string>();
			}
			if (this.Events.Contains(eventName))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Events.Add(eventName);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public void SetTag(string tagName)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (this.Tags == null)
			{
				this.Tags = new HashSet<string>();
			}
			if (this.Tags.Contains(tagName))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add(tagName);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000AD0B File Offset: 0x00008F0B
		[Description("Allow the controller to tweak input. Empty by default")]
		public virtual void BuildInput(InputBuilder input)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000AD10 File Offset: 0x00008F10
		public void Simulate(Client client, Entity pawn, PawnController additional)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HashSet<string> events = this.Events;
			if (events != null)
			{
				events.Clear();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HashSet<string> tags = this.Tags;
			if (tags != null)
			{
				tags.Clear();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Pawn = pawn;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Client = client;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFromEntity(pawn);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Simulate();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (additional != null)
			{
				additional.UpdateFromController(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (additional != null)
			{
				additional.Simulate();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RunEvents(additional);
			if (additional != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				additional.Finalize(pawn);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Finalize(pawn);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		public void FrameSimulate(Client client, Entity pawn, PawnController additional)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Pawn = pawn;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Client = client;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFromEntity(pawn);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FrameSimulate();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (additional != null)
			{
				additional.UpdateFromController(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (additional != null)
			{
				additional.FrameSimulate();
			}
			if (additional != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				additional.Finalize(pawn);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Finalize(pawn);
		}

		// Token: 0x04000089 RID: 137
		internal HashSet<string> Events;

		// Token: 0x0400008A RID: 138
		internal HashSet<string> Tags;
	}
}
