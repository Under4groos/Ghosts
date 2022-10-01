using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000CF RID: 207
	public class PsyDogDodgeState : NPCState<PsyDogNPC>
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x000273D8 File Offset: 0x000255D8
		public PsyDogDodgeState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000273F8 File Offset: 0x000255F8
		private static float Cooldown
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000273FF File Offset: 0x000255FF
		private float MaximumImpactRange
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00027408 File Offset: 0x00025608
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.DodgeType, (int)this.DodgeDirection);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Dodge, true);
			float oldZ = this.Host.Velocity.z;
			if (this.DodgeDirection == PsyDogDodgeState.DodgeType.Left)
			{
				this.InitialSpeed = (this.Host.Rotation.Forward * 300f + this.Host.Rotation.Right * 120f).WithZ(oldZ);
				return;
			}
			this.InitialSpeed = (this.Host.Rotation.Forward * 300f + this.Host.Rotation.Right * -120f).WithZ(oldZ);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000274FF File Offset: 0x000256FF
		public override void OnStateExited()
		{
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00027504 File Offset: 0x00025704
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

		// Token: 0x06000900 RID: 2304 RVA: 0x000275A0 File Offset: 0x000257A0
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

		// Token: 0x06000901 RID: 2305 RVA: 0x00027614 File Offset: 0x00025814
		public void CalculateDodgeType(Vector3 bulletImpactPosition)
		{
			Vector3 forward = this.Host.Rotation.Right.Normal;
			Vector3 dir = (bulletImpactPosition - this.Host.Position).Normal;
			float dot = forward.Dot(dir);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DodgeDirection = ((dot > 0f) ? PsyDogDodgeState.DodgeType.Right : PsyDogDodgeState.DodgeType.Left);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002767C File Offset: 0x0002587C
		public bool ShouldDodge(Vector3 bulletImpactPosition)
		{
			return this.Host.IsValid() && this.CanDodge() && this.RandomDodgeCheck() && this.Host.Position.Distance(bulletImpactPosition) < this.MaximumImpactRange;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000276C8 File Offset: 0x000258C8
		private bool RandomDodgeCheck()
		{
			return Rand.Int(this.DodgeOnceInEveryX) == this.DodgeOnceInEveryX;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000276DD File Offset: 0x000258DD
		private bool CanDodge()
		{
			return !this.IsActive && (!this.HasEntered || this.TimeSinceExited > PsyDogDodgeState.Cooldown);
		}

		// Token: 0x040002E5 RID: 741
		private int DodgeOnceInEveryX = 4;

		// Token: 0x040002E6 RID: 742
		private PsyDogDodgeState.DodgeType DodgeDirection;

		// Token: 0x040002E7 RID: 743
		private Vector3 InitialSpeed;

		// Token: 0x040002E8 RID: 744
		private bool HasBegunDodge;

		// Token: 0x040002E9 RID: 745
		private TimeSince TimeSinceDodgeBegan = 0f;

		// Token: 0x02000205 RID: 517
		private enum DodgeType
		{
			// Token: 0x0400086F RID: 2159
			Left,
			// Token: 0x04000870 RID: 2160
			Right
		}
	}
}
