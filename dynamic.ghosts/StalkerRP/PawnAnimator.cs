using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200002F RID: 47
	public abstract class PawnAnimator : PawnController
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000A7B2 File Offset: 0x000089B2
		public AnimatedEntity AnimPawn
		{
			get
			{
				return base.Pawn as AnimatedEntity;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000A7BF File Offset: 0x000089BF
		public AnimatedEntity FirstPersonPawn
		{
			get
			{
				StalkerPlayer stalkerPlayer = base.Pawn as StalkerPlayer;
				if (stalkerPlayer == null)
				{
					return null;
				}
				return stalkerPlayer.FirstPersonBody;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A7D8 File Offset: 0x000089D8
		[Description("We'll convert Position to a local position to the players eyes and set the param on the animgraph.")]
		public virtual void SetLookAt(string name, Vector3 Position)
		{
			Vector3 localPos = (Position - base.Pawn.EyePosition) * base.Rotation.Inverse;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter(name, localPos);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A817 File Offset: 0x00008A17
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, Vector3 val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn != null)
			{
				animPawn.SetAnimParameter(name, val);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity firstPersonPawn = this.FirstPersonPawn;
			if (firstPersonPawn == null)
			{
				return;
			}
			firstPersonPawn.SetAnimParameter(name, val);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A848 File Offset: 0x00008A48
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, float val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn != null)
			{
				animPawn.SetAnimParameter(name, val);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity firstPersonPawn = this.FirstPersonPawn;
			if (firstPersonPawn == null)
			{
				return;
			}
			firstPersonPawn.SetAnimParameter(name, val);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000A879 File Offset: 0x00008A79
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, bool val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn != null)
			{
				animPawn.SetAnimParameter(name, val);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity firstPersonPawn = this.FirstPersonPawn;
			if (firstPersonPawn == null)
			{
				return;
			}
			firstPersonPawn.SetAnimParameter(name, val);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A8AA File Offset: 0x00008AAA
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, int val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn != null)
			{
				animPawn.SetAnimParameter(name, val);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity firstPersonPawn = this.FirstPersonPawn;
			if (firstPersonPawn == null)
			{
				return;
			}
			firstPersonPawn.SetAnimParameter(name, val);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A8DB File Offset: 0x00008ADB
		[Description("Calls SetParam( name, true ). It's expected that your animgraph has a \"name\" param with the auto reset property set.")]
		public virtual void Trigger(string name)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter(name, true);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000A8EA File Offset: 0x00008AEA
		[Description("Resets all params to default values on the animgraph")]
		public virtual void ResetParameters()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn != null)
			{
				animPawn.ResetAnimParameters();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity firstPersonPawn = this.FirstPersonPawn;
			if (firstPersonPawn == null)
			{
				return;
			}
			firstPersonPawn.ResetAnimParameters();
		}
	}
}
