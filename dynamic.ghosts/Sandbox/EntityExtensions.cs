using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x0200014B RID: 331
	public static class EntityExtensions
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x0003D2A4 File Offset: 0x0003B4A4
		[Description("Sets the procedural hit creation parameters for the animgraph node, which makes the  model twitch according to where it got hit.   The parameters set are  \tbool hit \tint hit_bone \tvector hit_offset \tvector hit_direction \tvector hit_strength")]
		public static void ProceduralHitReaction(this AnimatedEntity self, DamageInfo info, float damageScale = 1f)
		{
			Vector3 localToBone = self.GetBoneTransform(info.BoneIndex, true).PointToLocal(info.Position);
			if (localToBone == Vector3.Zero)
			{
				localToBone = Vector3.One;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.SetAnimParameter("hit", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.SetAnimParameter("hit_bone", info.BoneIndex);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.SetAnimParameter("hit_offset", localToBone);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.SetAnimParameter("hit_direction", info.Force.Normal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.SetAnimParameter("hit_strength", info.Force.Length / 1000f * damageScale);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003D361 File Offset: 0x0003B561
		[Description("Copy the bones from the target entity, but at the current entity's position and rotation")]
		public static void CopyBonesFrom(this Entity self, Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.CopyBonesFrom(ent, self.Position, self.Rotation, 1f);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003D380 File Offset: 0x0003B580
		[Description("Copy the bones from the target entity, but at this position and rotation instead of the target entity's")]
		public static void CopyBonesFrom(this Entity self, Entity ent, Vector3 pos, Rotation rot, float scale = 1f)
		{
			ModelEntity to = self as ModelEntity;
			if (to != null)
			{
				ModelEntity me = ent as ModelEntity;
				if (me != null)
				{
					if (to.BoneCount != me.BoneCount)
					{
						GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("CopyBonesFrom: Bone count doesn't match - {0} vs {1}", new object[]
						{
							me.BoneCount,
							to.BoneCount
						}));
					}
					Vector3 localPos = me.Position;
					Rotation localRot = me.Rotation.Inverse;
					int bones = Math.Min(to.BoneCount, me.BoneCount);
					for (int i = 0; i < bones; i++)
					{
						Transform tx = me.GetBoneTransform(i, true);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						tx.Position = (tx.Position - localPos) * localRot * rot + pos;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						tx.Rotation = rot * (localRot * tx.Rotation);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						tx.Scale = scale;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						to.SetBoneTransform(i, tx, true);
					}
				}
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003D49C File Offset: 0x0003B69C
		[Description("Set the velocity of the ragdoll entity by working out the bone positions of from delta seconds ago")]
		public static void SetRagdollVelocityFrom(this Entity self, Entity fromEnt, float delta = 0.1f, float linearAmount = 1f, float angularAmount = 1f)
		{
			if (delta == 0f)
			{
				return;
			}
			ModelEntity to = self as ModelEntity;
			if (to != null)
			{
				ModelEntity from = fromEnt as ModelEntity;
				if (from != null)
				{
					Transform[] bonesNow = from.ComputeBones(0f);
					Transform[] bonesThn = from.ComputeBones(-delta);
					for (int i = 0; i < from.BoneCount; i++)
					{
						PhysicsBody body = to.GetBonePhysicsBody(i);
						if (body != null)
						{
							if (linearAmount > 0f)
							{
								Vector3 center = body.LocalMassCenter;
								Vector3 c0 = bonesThn[i].TransformVector(center);
								Vector3 vLinearVelocity = (bonesNow[i].TransformVector(center) - c0) * (linearAmount / delta);
								RuntimeHelpers.EnsureSufficientExecutionStack();
								body.Velocity = vLinearVelocity;
							}
							if (angularAmount > 0f)
							{
								Rotation diff = Rotation.Difference(bonesThn[i].Rotation, bonesNow[i].Rotation);
								RuntimeHelpers.EnsureSufficientExecutionStack();
								body.AngularVelocity = new Vector3(diff.x, diff.y, diff.z) * (angularAmount / delta);
							}
						}
					}
				}
			}
		}
	}
}
