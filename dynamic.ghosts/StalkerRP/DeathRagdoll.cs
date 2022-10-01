using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x02000025 RID: 37
	public class DeathRagdoll : ModelEntity
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000998E File Offset: 0x00007B8E
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00009996 File Offset: 0x00007B96
		[DefaultValue(15f)]
		private float LifeTime { get; set; } = 15f;

		// Token: 0x0600012A RID: 298 RVA: 0x000099A0 File Offset: 0x00007BA0
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("weapon");
			if (base.IsServer)
			{
				base.DeleteAsync(this.LifeTime);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000099FC File Offset: 0x00007BFC
		public new void CopyFrom(ModelEntity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = ent.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = ent.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = ent.Scale;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = ent.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel(ent.GetModelName());
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDecalsFrom(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CopyBonesFrom(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetRagdollVelocityFrom(ent, 0.1f, 1f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetRagdollVelocity(ent.Velocity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup(ent.GetMaterialGroup());
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009AC4 File Offset: 0x00007CC4
		public void SetRagdollVelocity(Vector3 velocity)
		{
			for (int i = 0; i < base.BoneCount; i++)
			{
				PhysicsBody body = base.GetBonePhysicsBody(i);
				if (body != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					body.Velocity = velocity;
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009AFC File Offset: 0x00007CFC
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			PhysicsBody bone = base.GetBonePhysicsBody(info.BoneIndex);
			if (bone != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bone.ApplyImpulse(info.Force);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009B38 File Offset: 0x00007D38
		protected override void OnPhysicsCollision(CollisionEventData eventData)
		{
			if (this.IsValid() && eventData.Speed > 1400f && !this.HasGibbed)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Gib();
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009B64 File Offset: 0x00007D64
		[Event.FrameAttribute]
		private void FadeOut()
		{
			if (this.TimeSinceCreated > this.LifeTime - this.FadeOutTime)
			{
				float frac = (this.LifeTime - this.TimeSinceCreated) / this.FadeOutTime;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.RenderColor = base.RenderColor.WithAlpha(frac);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009BBF File Offset: 0x00007DBF
		public void Gib()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GibEffect(this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasGibbed = true;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009BEC File Offset: 0x00007DEC
		[ClientRpc]
		public void GibEffect(Vector3 pos)
		{
			if (!this.GibEffect__RpcProxy(pos, null))
			{
				return;
			}
			int numGibs = Rand.Int(12, 22);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientsideGib.DoGibEffect(pos, numGibs, 300f, 1500f, 20f);
			Particles.Create("particles/stalker/anomalies/grav_gore_crush.vpcf", pos);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009C40 File Offset: 0x00007E40
		protected bool GibEffect__RpcProxy(Vector3 pos, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("GibEffect", new object[]
				{
					pos
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1664659091, this))
			{
				if (!NetRead.IsSupported(pos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] GibEffect is not allowed to use Vector3 for the parameter 'pos'!");
					return false;
				}
				writer.Write<Vector3>(pos);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public void GibEffect(To toTarget, Vector3 pos)
		{
			this.GibEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009CE4 File Offset: 0x00007EE4
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == 1664659091)
			{
				Vector3 __pos = read.ReadData<Vector3>(default(Vector3));
				if (!Prediction.WasPredicted("GibEffect", new object[]
				{
					__pos
				}))
				{
					this.GibEffect(__pos);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x04000074 RID: 116
		private readonly TimeSince TimeSinceCreated = 0f;

		// Token: 0x04000075 RID: 117
		private float FadeOutTime = 1f;

		// Token: 0x04000076 RID: 118
		private bool HasGibbed;
	}
}
