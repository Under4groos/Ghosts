using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using Sandbox.Internal.Globals;

namespace StalkerRP
{
	// Token: 0x02000033 RID: 51
	public class Unstuck
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000BE65 File Offset: 0x0000A065
		public Unstuck(BasePlayerController controller)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Controller = controller;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BE7C File Offset: 0x0000A07C
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

		// Token: 0x040000A2 RID: 162
		public BasePlayerController Controller;

		// Token: 0x040000A3 RID: 163
		public bool IsActive;

		// Token: 0x040000A4 RID: 164
		internal int StuckTries;
	}
}
