using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x0200004C RID: 76
	public class StateMachine
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00012144 File Offset: 0x00010344
		public virtual void SetState(State newState)
		{
			if (this.ActiveState != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActiveState.OnStateExited();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActiveState.TimeSinceExited = 0f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveState = newState;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveState.TimeSinceEntered = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveState.HasEntered = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveState.OnStateEntered();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000121CA File Offset: 0x000103CA
		public virtual void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			State activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.Update(deltaTime);
		}

		// Token: 0x040000F4 RID: 244
		protected State ActiveState;
	}
}
