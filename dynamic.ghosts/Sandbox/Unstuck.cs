using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.Internal.Globals;

namespace Sandbox
{
	// Token: 0x0200018E RID: 398
	public class Unstuck
	{
		// Token: 0x06001324 RID: 4900 RVA: 0x0004AB0D File Offset: 0x00048D0D
		public Unstuck(BasePlayerController controller)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Controller = controller;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004AB24 File Offset: 0x00048D24
		public virtual bool TestAndFix()
		{
			TraceResult result = this.Controller.TraceBBox(this.Controller.Position, this.Controller.Position, 0f);
			if (!result.StartedSolid)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StuckTries = 0;
				return false;
			}
			if (result.StartedSolid && BasePlayerController.Debug)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay = GlobalGameNamespace.DebugOverlay;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
				defaultInterpolatedStringHandler.AppendLiteral("[stuck in ");
				defaultInterpolatedStringHandler.AppendFormatted<Entity>(result.Entity);
				defaultInterpolatedStringHandler.AppendLiteral("]");
				debugOverlay.Text(defaultInterpolatedStringHandler.ToStringAndClear(), this.Controller.Position, Color.Red, 0f, 1500f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Box(result.Entity, Color.Red, 0f);
			}
			if (Host.IsClient)
			{
				return true;
			}
			int AttemptsPerTick = 20;
			for (int i = 0; i < AttemptsPerTick; i++)
			{
				Vector3 pos = this.Controller.Position + Vector3.Random.Normal * ((float)this.StuckTries / 2f);
				if (i == 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					pos = this.Controller.Position + Vector3.Up * 5f;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				result = this.Controller.TraceBBox(pos, pos, 0f);
				if (!result.StartedSolid)
				{
					if (BasePlayerController.Debug)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						DebugOverlay debugOverlay2 = GlobalGameNamespace.DebugOverlay;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 2);
						defaultInterpolatedStringHandler.AppendLiteral("unstuck after ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.StuckTries);
						defaultInterpolatedStringHandler.AppendLiteral(" tries (");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.StuckTries * AttemptsPerTick);
						defaultInterpolatedStringHandler.AppendLiteral(" tests)");
						debugOverlay2.Text(defaultInterpolatedStringHandler.ToStringAndClear(), this.Controller.Position, Color.Green, 5f, 1500f);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						GlobalGameNamespace.DebugOverlay.Line(pos, this.Controller.Position, Color.Green, 5f, false);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Controller.Position = pos;
					return false;
				}
				if (BasePlayerController.Debug)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.DebugOverlay.Line(pos, this.Controller.Position, Color.Yellow, 0.5f, false);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StuckTries++;
			return true;
		}

		// Token: 0x04000621 RID: 1569
		public BasePlayerController Controller;

		// Token: 0x04000622 RID: 1570
		public bool IsActive;

		// Token: 0x04000623 RID: 1571
		internal int StuckTries;
	}
}
