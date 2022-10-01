using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000188 RID: 392
	public class PawnController : BaseNetworkable
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0004998D File Offset: 0x00047B8D
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x00049995 File Offset: 0x00047B95
		public Entity Pawn { get; protected set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0004999E File Offset: 0x00047B9E
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x000499A6 File Offset: 0x00047BA6
		public Client Client { get; protected set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x000499AF File Offset: 0x00047BAF
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x000499B7 File Offset: 0x00047BB7
		public Vector3 Position { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x000499C0 File Offset: 0x00047BC0
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x000499C8 File Offset: 0x00047BC8
		public Rotation Rotation { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x000499D1 File Offset: 0x00047BD1
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x000499D9 File Offset: 0x00047BD9
		public Vector3 Velocity { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000499E2 File Offset: 0x00047BE2
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x000499EA File Offset: 0x00047BEA
		public Rotation EyeRotation { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x000499F3 File Offset: 0x00047BF3
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x000499FB File Offset: 0x00047BFB
		public Vector3 EyeLocalPosition { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00049A04 File Offset: 0x00047C04
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x00049A0C File Offset: 0x00047C0C
		public Vector3 BaseVelocity { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00049A15 File Offset: 0x00047C15
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00049A1D File Offset: 0x00047C1D
		public Entity GroundEntity { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00049A26 File Offset: 0x00047C26
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00049A2E File Offset: 0x00047C2E
		public Vector3 GroundNormal { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00049A37 File Offset: 0x00047C37
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x00049A3F File Offset: 0x00047C3F
		public Vector3 WishVelocity { get; set; }

		// Token: 0x060012FA RID: 4858 RVA: 0x00049A48 File Offset: 0x00047C48
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

		// Token: 0x060012FB RID: 4859 RVA: 0x00049AE0 File Offset: 0x00047CE0
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

		// Token: 0x060012FC RID: 4860 RVA: 0x00049BCC File Offset: 0x00047DCC
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

		// Token: 0x060012FD RID: 4861 RVA: 0x00049C50 File Offset: 0x00047E50
		[Description("This is what your logic should be going in")]
		public virtual void Simulate()
		{
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00049C52 File Offset: 0x00047E52
		[Description("This is called every frame on the client only")]
		public virtual void FrameSimulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("FrameSimulate");
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00049C64 File Offset: 0x00047E64
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

		// Token: 0x06001300 RID: 4864 RVA: 0x00049CD4 File Offset: 0x00047ED4
		[Description("An event has been triggered - maybe handle it")]
		public virtual void OnEvent(string name)
		{
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00049CD6 File Offset: 0x00047ED6
		[Description("Returns true if we have this event")]
		public bool HasEvent(string eventName)
		{
			return this.Events != null && this.Events.Contains(eventName);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00049CEE File Offset: 0x00047EEE
		public bool HasTag(string tagName)
		{
			return this.Tags != null && this.Tags.Contains(tagName);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00049D06 File Offset: 0x00047F06
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

		// Token: 0x06001304 RID: 4868 RVA: 0x00049D3C File Offset: 0x00047F3C
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

		// Token: 0x06001305 RID: 4869 RVA: 0x00049D77 File Offset: 0x00047F77
		[Description("Allow the controller to tweak input. Empty by default")]
		public virtual void BuildInput(InputBuilder input)
		{
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00049D7C File Offset: 0x00047F7C
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

		// Token: 0x06001307 RID: 4871 RVA: 0x00049E2C File Offset: 0x0004802C
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

		// Token: 0x0400060A RID: 1546
		internal HashSet<string> Events;

		// Token: 0x0400060B RID: 1547
		internal HashSet<string> Tags;
	}
}
