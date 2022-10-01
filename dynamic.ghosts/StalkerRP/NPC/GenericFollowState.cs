using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000072 RID: 114
	public class GenericFollowState : GenericNPCState
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00018E42 File Offset: 0x00017042
		private float followDistance
		{
			get
			{
				return 150f;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018E49 File Offset: 0x00017049
		public GenericFollowState(NPCBase host) : base(host)
		{
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018E54 File Offset: 0x00017054
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018EA2 File Offset: 0x000170A2
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00018ED0 File Offset: 0x000170D0
		public void SetFollowTarget(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.followTarget = target;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018EE0 File Offset: 0x000170E0
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Update(deltaTime);
			if (!this.CheckFollowTargetValid() || base.Target.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetToIdleState();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.followTarget = null;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			if (!this.Host.Position.AlmostEqual(this.followTarget.Position, this.followDistance))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Target = this.followTarget.Position;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.Host.Position;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00018FAD File Offset: 0x000171AD
		protected virtual bool CheckFollowTargetValid()
		{
			return this.followTarget.IsValid() && this.followTarget.LifeState == LifeState.Alive;
		}

		// Token: 0x040001E6 RID: 486
		private Entity followTarget;
	}
}
