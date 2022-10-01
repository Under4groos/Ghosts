using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200016C RID: 364
	public struct CitizenAnimationHelper
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x00042040 File Offset: 0x00040240
		public CitizenAnimationHelper(AnimatedEntity entity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = entity;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00042050 File Offset: 0x00040250
		[Description("Have the player look at this point in the world")]
		public void WithLookAt(Vector3 look, float eyesWeight = 1f, float headWeight = 1f, float bodyWeight = 1f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimLookAt("aim_eyes", look);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimLookAt("aim_head", look);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimLookAt("aim_body", look);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("aim_eyes_weight", eyesWeight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("aim_head_weight", headWeight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("aim_body_weight", bodyWeight);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000420E4 File Offset: 0x000402E4
		public void WithVelocity(Vector3 Velocity)
		{
			Vector3 dir = Velocity;
			float forward = this.Owner.Rotation.Forward.Dot(dir);
			float sideward = this.Owner.Rotation.Right.Dot(dir);
			float angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("move_direction", angle);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("move_speed", Velocity.Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("move_groundspeed", Velocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("move_y", sideward);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("move_x", forward);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("move_z", Velocity.z);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000421E8 File Offset: 0x000403E8
		public void WithWishVelocity(Vector3 Velocity)
		{
			Vector3 dir = Velocity;
			float forward = this.Owner.Rotation.Forward.Dot(dir);
			float sideward = this.Owner.Rotation.Right.Dot(dir);
			float angle = MathF.Atan2(sideward, forward).RadianToDegree().NormalizeDegrees();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("wish_direction", angle);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("wish_speed", Velocity.Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("wish_groundspeed", Velocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("wish_y", sideward);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("wish_x", forward);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("wish_z", Velocity.z);
		}

		// Token: 0x17000487 RID: 1159
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x000422EC File Offset: 0x000404EC
		public Rotation AimAngle
		{
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value = this.Owner.Rotation.Inverse * value;
				Angles ang = value.Angles();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Owner.SetAnimParameter("aim_body_pitch", ang.pitch);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Owner.SetAnimParameter("aim_body_yaw", ang.yaw);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00042357 File Offset: 0x00040557
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x00042369 File Offset: 0x00040569
		public float AimEyesWeight
		{
			get
			{
				return this.Owner.GetAnimParameterFloat("aim_eyes_weight");
			}
			set
			{
				this.Owner.SetAnimParameter("aim_eyes_weight", value);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0004237C File Offset: 0x0004057C
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x0004238E File Offset: 0x0004058E
		public float AimHeadWeight
		{
			get
			{
				return this.Owner.GetAnimParameterFloat("aim_head_weight");
			}
			set
			{
				this.Owner.SetAnimParameter("aim_head_weight", value);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x000423A1 File Offset: 0x000405A1
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x000423B3 File Offset: 0x000405B3
		public float AimBodyWeight
		{
			get
			{
				return this.Owner.GetAnimParameterFloat("aim_body_weight");
			}
			set
			{
				this.Owner.SetAnimParameter("aim_headaim_body_weight_weight", value);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x000423C6 File Offset: 0x000405C6
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x000423D8 File Offset: 0x000405D8
		public float FootShuffle
		{
			get
			{
				return this.Owner.GetAnimParameterFloat("move_shuffle");
			}
			set
			{
				this.Owner.SetAnimParameter("move_shuffle", value);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x000423EB File Offset: 0x000405EB
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x000423FD File Offset: 0x000405FD
		public float DuckLevel
		{
			get
			{
				return this.Owner.GetAnimParameterFloat("duck");
			}
			set
			{
				this.Owner.SetAnimParameter("duck", value);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00042410 File Offset: 0x00040610
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x00042422 File Offset: 0x00040622
		public float VoiceLevel
		{
			get
			{
				return this.Owner.GetAnimParameterFloat("voice");
			}
			set
			{
				this.Owner.SetAnimParameter("voice", value);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00042435 File Offset: 0x00040635
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x00042447 File Offset: 0x00040647
		public bool IsSitting
		{
			get
			{
				return this.Owner.GetAnimParameterBool("b_sit");
			}
			set
			{
				this.Owner.SetAnimParameter("b_sit", value);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0004245A File Offset: 0x0004065A
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x0004246C File Offset: 0x0004066C
		public bool IsGrounded
		{
			get
			{
				return this.Owner.GetAnimParameterBool("b_grounded");
			}
			set
			{
				this.Owner.SetAnimParameter("b_grounded", value);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0004247F File Offset: 0x0004067F
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x00042491 File Offset: 0x00040691
		public bool IsSwimming
		{
			get
			{
				return this.Owner.GetAnimParameterBool("b_swim");
			}
			set
			{
				this.Owner.SetAnimParameter("b_swim", value);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x000424A4 File Offset: 0x000406A4
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x000424B6 File Offset: 0x000406B6
		public bool IsClimbing
		{
			get
			{
				return this.Owner.GetAnimParameterBool("b_climbing");
			}
			set
			{
				this.Owner.SetAnimParameter("b_climbing", value);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000424C9 File Offset: 0x000406C9
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x000424DB File Offset: 0x000406DB
		public bool IsNoclipping
		{
			get
			{
				return this.Owner.GetAnimParameterBool("b_noclip");
			}
			set
			{
				this.Owner.SetAnimParameter("b_noclip", value);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x000424EE File Offset: 0x000406EE
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x00042500 File Offset: 0x00040700
		public bool IsWeaponLowered
		{
			get
			{
				return this.Owner.GetAnimParameterBool("b_weapon_lower");
			}
			set
			{
				this.Owner.SetAnimParameter("b_weapon_lower", value);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00042513 File Offset: 0x00040713
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x00042525 File Offset: 0x00040725
		public CitizenAnimationHelper.HoldTypes HoldType
		{
			get
			{
				return (CitizenAnimationHelper.HoldTypes)this.Owner.GetAnimParameterInt("holdtype");
			}
			set
			{
				this.Owner.SetAnimParameter("holdtype", (int)value);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00042538 File Offset: 0x00040738
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x0004254A File Offset: 0x0004074A
		public CitizenAnimationHelper.Hand Handedness
		{
			get
			{
				return (CitizenAnimationHelper.Hand)this.Owner.GetAnimParameterInt("holdtype_handedness");
			}
			set
			{
				this.Owner.SetAnimParameter("holdtype_handedness", (int)value);
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004255D File Offset: 0x0004075D
		public void TriggerJump()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("b_jump", true);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00042575 File Offset: 0x00040775
		public void TriggerDeploy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner.SetAnimParameter("b_deploy", true);
		}

		// Token: 0x04000535 RID: 1333
		private AnimatedEntity Owner;

		// Token: 0x02000233 RID: 563
		public enum HoldTypes
		{
			// Token: 0x0400092B RID: 2347
			None,
			// Token: 0x0400092C RID: 2348
			Pistol,
			// Token: 0x0400092D RID: 2349
			Rifle,
			// Token: 0x0400092E RID: 2350
			Shotgun,
			// Token: 0x0400092F RID: 2351
			HoldItem,
			// Token: 0x04000930 RID: 2352
			Punch,
			// Token: 0x04000931 RID: 2353
			Swing
		}

		// Token: 0x02000234 RID: 564
		public enum Hand
		{
			// Token: 0x04000933 RID: 2355
			Both,
			// Token: 0x04000934 RID: 2356
			Right,
			// Token: 0x04000935 RID: 2357
			Left
		}
	}
}
