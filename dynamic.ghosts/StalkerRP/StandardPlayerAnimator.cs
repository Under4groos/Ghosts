using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000032 RID: 50
	public class StandardPlayerAnimator : PawnAnimator
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000B908 File Offset: 0x00009B08
		public override void Simulate()
		{
			Player player = base.Pawn as Player;
			Rotation idealRotation = Rotation.LookAt(Input.Rotation.Forward.WithZ(0f), Vector3.Up);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoRotation(idealRotation);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoWalk();
			bool sitting = base.HasTag("sitting");
			bool noclip = base.HasTag("noclip") && !sitting;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("b_grounded", base.GroundEntity != null || noclip || sitting);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("b_noclip", noclip);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("b_sit", sitting);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("b_swim", base.Pawn.WaterLevel > 0.5f && !sitting);
			if (Host.IsClient && base.Client.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetAnimParameter("voice", (base.Client.TimeSinceLastVoice < 0.5f) ? base.Client.VoiceLevel : 0f);
			}
			Vector3 aimPos = base.Pawn.EyePosition + Input.Rotation.Forward * 200f;
			Vector3 lookPos = aimPos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetLookAt("aim_eyes", lookPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetLookAt("aim_head", lookPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetLookAt("aim_body", aimPos);
			if (base.HasTag("ducked"))
			{
				this.duck = this.duck.LerpTo(1f, Time.Delta * 10f, true);
			}
			else
			{
				this.duck = this.duck.LerpTo(0f, Time.Delta * 5f, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("duck", this.duck);
			if (player != null)
			{
				BaseCarriable carry = player.ActiveChild as BaseCarriable;
				if (carry != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					carry.SimulateAnimator(this);
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("holdtype", 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("aim_body_weight", 0.5f);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public virtual void DoRotation(Rotation idealRotation)
		{
			Player player = base.Pawn as Player;
			int allowYawDiff = (((player != null) ? player.ActiveChild : null) == null) ? 90 : 50;
			float turnSpeed = 0.01f;
			if (base.HasTag("ducked"))
			{
				turnSpeed = 0.1f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = Rotation.Slerp(base.Rotation, idealRotation, base.WishVelocity.Length * Time.Delta * turnSpeed, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float change;
			base.Rotation = base.Rotation.Clamp(idealRotation, (float)allowYawDiff, out change);
			if (change > 1f && base.WishVelocity.Length <= 1f)
			{
				this.TimeSinceFootShuffle = 0f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("b_shuffle", (double)this.TimeSinceFootShuffle < 0.1);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000BC34 File Offset: 0x00009E34
		private void DoWalk()
		{
			Vector3 dir = base.Velocity;
			float forward = base.Rotation.Forward.Dot(dir);
			float sideward = base.Rotation.Right.Dot(dir);
			float angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("move_direction", angle);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("move_speed", base.Velocity.Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("move_groundspeed", base.Velocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("move_y", sideward);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("move_x", forward);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("move_z", base.Velocity.z);
			Vector3 dir2 = base.WishVelocity;
			float forward2 = base.Rotation.Forward.Dot(dir2);
			float sideward2 = base.Rotation.Right.Dot(dir2);
			float angle2 = MathF.Atan2(sideward2, forward2).RadianToDegree().NormalizeDegrees();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("wish_direction", angle2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("wish_speed", base.WishVelocity.Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("wish_groundspeed", base.WishVelocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("wish_y", sideward2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("wish_x", forward2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter("wish_z", base.WishVelocity.z);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000BE22 File Offset: 0x0000A022
		public override void OnEvent(string name)
		{
			if (name == "jump")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Trigger("b_jump");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnEvent(name);
		}

		// Token: 0x040000A0 RID: 160
		private TimeSince TimeSinceFootShuffle = 60f;

		// Token: 0x040000A1 RID: 161
		private float duck;
	}
}
