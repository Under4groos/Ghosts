using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200001E RID: 30
	public class NPCStateMachine : EntityComponent, ISingletonComponent
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00008ED8 File Offset: 0x000070D8
		public virtual void SetState(BaseNPCState newState)
		{
			if (this.ActiveState != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActiveState.OnStateExited();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActiveState.IsActive = false;
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
			this.ActiveState.IsActive = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveState.OnStateEntered();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008F80 File Offset: 0x00007180
		public virtual void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseNPCState activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.Update(deltaTime);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008F98 File Offset: 0x00007198
		public virtual void OnCrit(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseNPCState activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.OnCrit(info);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008FB0 File Offset: 0x000071B0
		public virtual void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseNPCState activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.TakeDamage(info);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008FC8 File Offset: 0x000071C8
		public virtual void OnAnimAttackEvent(int intData, float floatData, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseNPCState activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.OnAnimAttackEvent(intData, floatData, vectorData, stringData);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008FE4 File Offset: 0x000071E4
		public virtual void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseNPCState activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.OnAnimEventGeneric(name, intData, floatData, vectorData, stringData);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00009002 File Offset: 0x00007202
		public virtual void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseNPCState activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.OnKilled();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00009019 File Offset: 0x00007219
		public virtual void OnDestroyed()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseNPCState activeState = this.ActiveState;
			if (activeState == null)
			{
				return;
			}
			activeState.OnDestroyed();
		}

		// Token: 0x0400005D RID: 93
		protected BaseNPCState ActiveState;
	}
}
