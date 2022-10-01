using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200001A RID: 26
	public class ClientsideGib : ModelEntity
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00008A2B File Offset: 0x00006C2B
		private float LiveTime
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00008A32 File Offset: 0x00006C32
		private float FadeOutTime
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008A3C File Offset: 0x00006C3C
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("weapon");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel(ClientsideGib.GibModels[Rand.Int(ClientsideGib.GibModels.Length - 1)]);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RenderColor = new Color(50f, 15f, 15f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BloodSpray = Particles.Create("particles/stalker/gore_blood_trail.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.GravityScale = 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(this.LiveTime);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008AFC File Offset: 0x00006CFC
		public static void DoGibEffect(Vector3 pos, int numGibs, float speedMin, float speedMax, float rotSpeed)
		{
			for (int i = 0; i < numGibs; i++)
			{
				Vector3 velocity = Vector3.Random * Rand.Float(speedMin, speedMax);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				velocity = velocity.WithZ(Math.Abs(velocity.z));
				float scale = Rand.Float(0.8f, 1.6f);
				ClientsideGib clientsideGib = new ClientsideGib();
				clientsideGib.Position = pos;
				clientsideGib.Velocity = velocity;
				clientsideGib.Scale = scale;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				clientsideGib.PhysicsBody.AngularVelocity = Vector3.Random.Normal * 10f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("grav.body.gib", pos);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008BA4 File Offset: 0x00006DA4
		[Event.FrameAttribute]
		private void UpdateGibParticles()
		{
			if (this.BloodSpray != null && this.Velocity.Length < 20f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Particles bloodSpray = this.BloodSpray;
				if (bloodSpray != null)
				{
					bloodSpray.Destroy(false);
				}
			}
			float fadeOutFrac = this.TimeSinceCreated - (this.LiveTime - this.FadeOutTime);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			fadeOutFrac /= this.FadeOutTime;
			if (fadeOutFrac > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.RenderColor = base.RenderColor.WithAlpha(1f - fadeOutFrac);
			}
		}

		// Token: 0x04000053 RID: 83
		private static readonly string[] GibModels = new string[]
		{
			"models/sbox_props/watermelon/watermelon_gib06.vmdl",
			"models/sbox_props/watermelon/watermelon_gib07.vmdl",
			"models/sbox_props/watermelon/watermelon_gib08.vmdl",
			"models/sbox_props/watermelon/watermelon_gib09.vmdl"
		};

		// Token: 0x04000054 RID: 84
		private Particles BloodSpray;

		// Token: 0x04000055 RID: 85
		private TimeSince TimeSinceCreated = 0f;
	}
}
