using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000198 RID: 408
	public class WaterController
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x000527DA File Offset: 0x000509DA
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x000527E2 File Offset: 0x000509E2
		public Entity WaterEntity { get; set; }

		// Token: 0x06001459 RID: 5209 RVA: 0x000527EB File Offset: 0x000509EB
		public void StartTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnEnterWater(other as ModelEntity);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x000527FE File Offset: 0x000509FE
		public void EndTouch(Entity other)
		{
			if (other.WaterEntity != this.WaterEntity)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnLeaveWater(other as ModelEntity);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00052820 File Offset: 0x00050A20
		public virtual void OnEnterWater(ModelEntity other)
		{
			if (other == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			other.WaterEntity = this.WaterEntity;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00052838 File Offset: 0x00050A38
		public virtual void OnLeaveWater(ModelEntity other)
		{
			if (other == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			other.WaterLevel = 0f;
			if (other.PhysicsGroup == null)
			{
				return;
			}
			int bodyCount = other.PhysicsGroup.BodyCount;
			for (int i = 0; i < bodyCount; i++)
			{
				PhysicsBody body = other.PhysicsGroup.GetBody(i);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body.GravityScale = 1f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body.LinearDrag = 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body.AngularDrag = 0f;
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x000528B8 File Offset: 0x00050AB8
		public void Touch(Entity other)
		{
			if (other.WaterEntity != this.WaterEntity)
			{
				return;
			}
			ModelEntity me = other as ModelEntity;
			if (me != null && me.PhysicsGroup.IsValid())
			{
				int bodyCount = me.PhysicsGroup.BodyCount;
				for (int i = 0; i < bodyCount; i++)
				{
					PhysicsBody body = me.PhysicsGroup.GetBody(i);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateBody(other, body);
					if (bodyCount == 1)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						me.WaterLevel = body.WaterLevel;
					}
				}
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00052934 File Offset: 0x00050B34
		private void UpdateBody(Entity ent, PhysicsBody body)
		{
			int waterDensity = 1000;
			float oldLevel = body.WaterLevel;
			float density = body.Density;
			Vector3 waterSurface = this.WaterEntity.Position + (this.WaterEntity as ModelEntity).CollisionBounds.Maxs;
			BBox bounds = body.GetBounds();
			Vector3 velocity = body.Velocity;
			Vector3 pos = bounds.Center;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pos.z = waterSurface.z;
			float densityDiff = density - (float)waterDensity;
			float volume = bounds.Volume;
			float level = waterSurface.z.LerpInverse(bounds.Mins.z, bounds.Maxs.z, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			body.WaterLevel = level;
			if (ent.IsClientOnly == Host.IsClient)
			{
				float bouyancy = densityDiff.LerpInverse(0f, -300f, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bouyancy = MathF.Pow(bouyancy, 0.1f);
				if (bouyancy <= 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					body.GravityScale = 1f - body.WaterLevel * 0.8f;
				}
				else
				{
					Vector3 point = bounds.Center;
					if (level < 1f)
					{
						point.z = bounds.Mins.z - 100f;
					}
					Vector3 closestpoint = body.FindClosestPoint(point);
					float depth = (waterSurface.z - bounds.Maxs.z) / 100f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					depth = depth.Clamp(1f, 10f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					body.ApplyForceAt(closestpoint, Vector3.Up * volume * level * bouyancy * 0.05f * depth);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					body.GravityScale = 1f - MathF.Pow(body.WaterLevel.Clamp(0f, 0.5f) * 2f, 0.5f);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body.LinearDrag = body.WaterLevel * this.WaterThickness;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body.AngularDrag = body.WaterLevel * this.WaterThickness * 0.5f;
			}
			if (Host.IsClient)
			{
				if (oldLevel == 0f)
				{
					return;
				}
				if (MathF.Abs(oldLevel - level) > 0.001f && body.LastWaterEffect > 0.2f)
				{
					if (oldLevel < 0.3f && level >= 0.35f)
					{
						Particles particles = Particles.Create("particles/water_splash.vpcf", pos);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						particles.SetForward(0, Vector3.Up);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						body.LastWaterEffect = 0f;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sound.FromWorld("water_splash_medium", pos);
					}
					if (velocity.Length > 2f && velocity.z > -10f && velocity.z < 10f)
					{
						Particles particles2 = Particles.Create("particles/water_bob.vpcf", pos);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						particles2.SetForward(0, Vector3.Up);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						body.LastWaterEffect = 0f;
					}
				}
			}
		}

		// Token: 0x04000691 RID: 1681
		public float WaterThickness = 80f;

		// Token: 0x04000692 RID: 1682
		public TimeSince TimeSinceLastEffect = 0f;
	}
}
