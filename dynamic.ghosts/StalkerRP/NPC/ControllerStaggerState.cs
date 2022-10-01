using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x02000099 RID: 153
	public class ControllerStaggerState : NPCState<ControllerNPC>
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001FB27 File Offset: 0x0001DD27
		private float Cooldown
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001FB2E File Offset: 0x0001DD2E
		public ControllerStaggerState(ControllerNPC host) : base(host)
		{
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001FB37 File Offset: 0x0001DD37
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bStagger", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001FB60 File Offset: 0x0001DD60
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bStagger", false);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001FB78 File Offset: 0x0001DD78
		public override void Update(float deltaTime)
		{
			if (this.TimeSinceEntered > 2f)
			{
				this.Host.SM.SetState(this.Host.ChaseState);
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001FBA7 File Offset: 0x0001DDA7
		public bool CanStagger()
		{
			return !this.HasEntered || this.TimeSinceExited > this.Cooldown;
		}
	}
}
