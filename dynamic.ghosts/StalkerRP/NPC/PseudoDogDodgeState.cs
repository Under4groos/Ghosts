using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000BE RID: 190
	public class PseudoDogDodgeState : NPCState<PseudoDogNPC>
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x00024811 File Offset: 0x00022A11
		public PseudoDogDodgeState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00024831 File Offset: 0x00022A31
		private static float Cooldown
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00024838 File Offset: 0x00022A38
		private float MaximumImpactRange
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00024840 File Offset: 0x00022A40
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.DodgeType, (int)this.DodgeDirection);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Dodge, true);
			float oldZ = this.Host.Velocity.z;
			if (this.DodgeDirection == PseudoDogDodgeState.DodgeType.Left)
			{
				this.InitialSpeed = (this.Host.Rotation.Forward * 300f + this.Host.Rotation.Right * 120f).WithZ(oldZ);
				return;
			}
			this.InitialSpeed = (this.Host.Rotation.Forward * 300f + this.Host.Rotation.Right * -120f).WithZ(oldZ);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00024937 File Offset: 0x00022B37
		public override void OnStateExited()
		{
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002493C File Offset: 0x00022B3C
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCBeginDodgeEvent"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasBegunDodge = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceDodgeBegan = 0f;
			}
			if (name.Equals("NPCEndDodgeEvent"))
			{
				if (this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.ChargeState);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000249D8 File Offset: 0x00022BD8
		public override void Update(float deltaTime)
		{
			if (!this.HasBegunDodge)
			{
				return;
			}
			Vector3 forwardVel = this.Host.Rotation.Forward.Normal * 300f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Velocity = this.InitialSpeed.LerpTo(forwardVel, this.TimeSinceDodgeBegan / this.Host.CurrentSequence.Duration);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00024A4C File Offset: 0x00022C4C
		public void CalculateDodgeType(Vector3 bulletImpactPosition)
		{
			Vector3 forward = this.Host.Rotation.Right.Normal;
			Vector3 dir = (bulletImpactPosition - this.Host.Position).Normal;
			float dot = forward.Dot(dir);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DodgeDirection = ((dot > 0f) ? PseudoDogDodgeState.DodgeType.Right : PseudoDogDodgeState.DodgeType.Left);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00024AB4 File Offset: 0x00022CB4
		public bool ShouldDodge(Vector3 bulletImpactPosition)
		{
			return this.Host.IsValid() && this.CanDodge() && this.RandomDodgeCheck() && this.Host.Position.Distance(bulletImpactPosition) < this.MaximumImpactRange;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00024B00 File Offset: 0x00022D00
		private bool RandomDodgeCheck()
		{
			return Rand.Int(this.DodgeOnceInEveryX) == this.DodgeOnceInEveryX;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00024B15 File Offset: 0x00022D15
		private bool CanDodge()
		{
			return !this.IsActive && (!this.HasEntered || this.TimeSinceExited > PseudoDogDodgeState.Cooldown);
		}

		// Token: 0x040002B4 RID: 692
		private int DodgeOnceInEveryX = 4;

		// Token: 0x040002B5 RID: 693
		private PseudoDogDodgeState.DodgeType DodgeDirection;

		// Token: 0x040002B6 RID: 694
		private Vector3 InitialSpeed;

		// Token: 0x040002B7 RID: 695
		private bool HasBegunDodge;

		// Token: 0x040002B8 RID: 696
		private TimeSince TimeSinceDodgeBegan = 0f;

		// Token: 0x02000204 RID: 516
		private enum DodgeType
		{
			// Token: 0x0400086C RID: 2156
			Left,
			// Token: 0x0400086D RID: 2157
			Right
		}
	}
}
