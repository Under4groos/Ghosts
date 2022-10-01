using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200008B RID: 139
	public class BurerTelekinesisState : NPCState<BurerNPC>
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001D006 File Offset: 0x0001B206
		private float HoverRadius
		{
			get
			{
				return 150f;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001D010 File Offset: 0x0001B210
		private Vector3 HoverOffset
		{
			get
			{
				return default(Vector3).WithZ(150f);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001D030 File Offset: 0x0001B230
		private float CoolDown
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001D037 File Offset: 0x0001B237
		public BurerTelekinesisState(BurerNPC host) : base(host)
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001D058 File Offset: 0x0001B258
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("ePsychicType", 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001D0CE File Offset: 0x0001B2CE
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DropObjects();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				if (!this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.IdleState);
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.FaceTarget();
			if (this.PhysObjects.Count > 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HoverObjects();
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001D185 File Offset: 0x0001B385
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ThrowObjects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.ChaseState);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001D1B2 File Offset: 0x0001B3B2
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DropObjects();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001D1BF File Offset: 0x0001B3BF
		public override void OnDestroyed()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DropObjects();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		[Description("Used to find new physics objects for the burers telekinesis. Note that we don't actually do anything with them in the NPC base, we just handle the collecting of them. It is expected for the different States to handle how we interact with these.")]
		public void FindNewPhysicsObjects()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PhysObjects.Clear();
			IEnumerable<Entity> entities = Entity.FindInSphere(this.Host.Position, this.Host.TelekinesisSearchRange);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PhysObjects.AddRange(entities.Where(delegate(Entity x)
			{
				if (x.Tags.Has(this.Host.TelekinesisTag))
				{
					return false;
				}
				Prop y = x as Prop;
				if (y == null)
				{
					return false;
				}
				if (y.PhysicsBody == null)
				{
					return false;
				}
				if (!this.Host.CanSee(y, false))
				{
					return false;
				}
				float mass = y.PhysicsBody.Mass;
				if (y.PhysicsGroup != null)
				{
					for (int i = 0; i < y.PhysicsGroup.BodyCount; i++)
					{
						y.PhysicsGroup.GetBody(i).GravityEnabled = true;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					mass = y.PhysicsGroup.Mass;
				}
				return mass < this.Host.TelekinesisMassLimit;
			}).Cast<ModelEntity>().Take(this.Host.TelekinesisMaxObjects));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PhysObjects.ForEach(delegate(ModelEntity x)
			{
				if (x.PhysicsGroup != null)
				{
					for (int i = 0; i < x.PhysicsGroup.BodyCount; i++)
					{
						x.PhysicsGroup.GetBody(i).GravityEnabled = false;
					}
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				x.PhysicsBody.GravityEnabled = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				x.PhysicsBody.ApplyAngularImpulse(Vector3.Random * 30f * x.PhysicsBody.Mass);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				x.Tags.Add(this.Host.TelekinesisTag);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AddHoverEffect(x);
			});
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001D258 File Offset: 0x0001B458
		private void HoverObjects()
		{
			List<ModelEntity> physObjs = this.PhysObjects;
			int count = (physObjs.Count > 1) ? 0 : 1;
			float degPerObj = 180f / (float)((physObjs.Count > 1) ? (physObjs.Count - 1) : 2);
			foreach (ModelEntity ent in physObjs.ToArray())
			{
				if (!ent.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					physObjs.Remove(ent);
				}
				else
				{
					Vector3 c = this.Host.Position + this.HoverOffset;
					Rotation rotation = Rotation.LookAt(this.Host.Rotation.Right, Vector3.Up);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					rotation = rotation.RotateAroundAxis(Vector3.Down, degPerObj * (float)count);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Vector3 c2 = c + rotation.Forward * this.HoverRadius;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Vector3 c3 = c2 + Vector3.Random * 50f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Vector3 dif = c3 + Vector3.Up * MathF.Sin(Time.Now * 5f) * 5f - ent.Position;
					float length = dif.Length;
					Vector3 dir = dif.Normal;
					float speed = (length / this.Host.TelekinesisSearchRange).Clamp(0.02f, 1f) * 3000f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.Velocity = ent.Velocity.LerpTo(dir * speed, Time.Delta * 5f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					count++;
				}
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001D404 File Offset: 0x0001B604
		private void ThrowObjects()
		{
			List<ModelEntity> physObjs = this.PhysObjects;
			foreach (ModelEntity ent in physObjs.ToArray())
			{
				if (!ent.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					physObjs.Remove(ent);
				}
				else
				{
					float mass = ent.PhysicsBody.Mass;
					if (ent.PhysicsGroup != null)
					{
						for (int i = 0; i < ent.PhysicsGroup.BodyCount; i++)
						{
							ent.PhysicsGroup.GetBody(i).GravityEnabled = true;
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						mass = ent.PhysicsGroup.Mass;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.PhysicsBody.GravityEnabled = true;
					Vector3 force = (base.Target.Position + Vector3.Up * 35f - ent.Position).Normal * this.Host.TelekinesisThrowForce * mass;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.ApplyAbsoluteImpulse(force);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.Tags.Remove(this.Host.TelekinesisTag);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveHoverEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			physObjs.Clear();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity("burer.teleattack", this.Host);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001D558 File Offset: 0x0001B758
		private void DropObjects()
		{
			List<ModelEntity> physObjs = this.PhysObjects;
			foreach (ModelEntity ent in physObjs.ToArray())
			{
				if (!ent.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					physObjs.Remove(ent);
				}
				else
				{
					if (ent.PhysicsGroup != null)
					{
						for (int i = 0; i < ent.PhysicsGroup.BodyCount; i++)
						{
							ent.PhysicsGroup.GetBody(i).GravityEnabled = true;
						}
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.PhysicsBody.GravityEnabled = true;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.Tags.Remove(this.Host.TelekinesisTag);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveHoverEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			physObjs.Clear();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001D61C File Offset: 0x0001B81C
		private void AddHoverEffect(ModelEntity obj)
		{
			Particles parts = Particles.Create("particles/stalker/burer_tele_hold.vpcf", obj, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			parts.SetPosition(0, obj.WorldSpaceBounds.Center);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HoldEffects.Add(parts);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001D662 File Offset: 0x0001B862
		private void RemoveHoverEffects()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HoldEffects.ForEach(delegate(Particles x)
			{
				x.Destroy(false);
			});
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001D693 File Offset: 0x0001B893
		public bool CanDoAttack()
		{
			if (this.TimeSinceExited <= this.CoolDown)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindNewPhysicsObjects();
			return this.PhysObjects.Count > 0;
		}

		// Token: 0x04000232 RID: 562
		private readonly List<Particles> HoldEffects = new List<Particles>();

		// Token: 0x04000233 RID: 563
		private readonly List<ModelEntity> PhysObjects = new List<ModelEntity>();
	}
}
