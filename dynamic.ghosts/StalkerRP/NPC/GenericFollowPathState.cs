using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000071 RID: 113
	public class GenericFollowPathState : GenericNPCState
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00018D01 File Offset: 0x00016F01
		public GenericFollowPathState(NPCBase host) : base(host)
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00018D0C File Offset: 0x00016F0C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00018D5A File Offset: 0x00016F5A
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00018D9C File Offset: 0x00016F9C
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Update(deltaTime);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			if (this.Host.Position.AlmostEqual(this.finalTargetPos, 30f))
			{
				this.Host.SetToIdleState();
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00018DFC File Offset: 0x00016FFC
		public void SetPath(GenericPathEntity pathEntity)
		{
			List<Vector3> points = global::NavPath.GeneratePointsFromPath(this.Host.Position, pathEntity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.OverridePath(points);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.finalTargetPos = points.Last<Vector3>();
		}

		// Token: 0x040001E4 RID: 484
		private GenericPathEntity path;

		// Token: 0x040001E5 RID: 485
		private Vector3 finalTargetPos;
	}
}
