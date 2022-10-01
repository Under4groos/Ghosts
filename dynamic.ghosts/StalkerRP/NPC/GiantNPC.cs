using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000CA RID: 202
	[DebugSpawnable(Name = "Pseudogiant", Category = "Mutants")]
	[Title("Pseudogiant")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/pseudogiant/pseudogiant.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class GiantNPC : NPCBase
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x000265C9 File Offset: 0x000247C9
		public float AttackDamage
		{
			get
			{
				return 80f;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000265D0 File Offset: 0x000247D0
		public float AttackRange
		{
			get
			{
				return 120f;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x000265D7 File Offset: 0x000247D7
		public float AttackForce
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000265DE File Offset: 0x000247DE
		public float StompDamage
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000265E5 File Offset: 0x000247E5
		public float StompFallOffRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000265EC File Offset: 0x000247EC
		public float StompForce
		{
			get
			{
				return 1200f;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000265F3 File Offset: 0x000247F3
		public string AggroSound
		{
			get
			{
				return "giant.aggro";
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000265FA File Offset: 0x000247FA
		public string BreathingSounds
		{
			get
			{
				return "giant.breathing";
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00026601 File Offset: 0x00024801
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00026608 File Offset: 0x00024808
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002660B File Offset: 0x0002480B
		protected override float FootStepVolume
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00026612 File Offset: 0x00024812
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(42f, 52f);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00026623 File Offset: 0x00024823
		protected override string NPCAssetID
		{
			get
			{
				return "pseudogiant";
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002662C File Offset: 0x0002482C
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 42f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new GiantIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new GiantChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StompState = new GiantStompState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002669C File Offset: 0x0002489C
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000266BD File Offset: 0x000248BD
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000266D8 File Offset: 0x000248D8
		public override void OnAnimEventFootstep(Vector3 pos, int foot, float volume)
		{
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			if (!base.IsClient)
			{
				return;
			}
			Vector3 vector = pos + Vector3.Down * 20f;
			Trace trace = Trace.Ray(pos, vector).Radius(1f);
			Entity entity = this;
			bool flag = true;
			TraceResult tr = trace.Ignore(entity, flag).Run();
			if (!tr.Hit)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoStompFX(tr.EndPosition);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00026757 File Offset: 0x00024957
		private float maxStompRange
		{
			get
			{
				return 2000f;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0002675E File Offset: 0x0002495E
		private float shakeLength
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00026765 File Offset: 0x00024965
		private float maxSpeed
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0002676C File Offset: 0x0002496C
		private float maxSize
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00026774 File Offset: 0x00024974
		private void DoStompFX(Vector3 pos)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("giant.stomp", pos);
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			if (player.GroundEntity == null || !player.GroundEntity.IsWorld)
			{
				return;
			}
			float distance = player.Position.Distance(this.Position);
			if (distance > this.maxStompRange)
			{
				return;
			}
			float frac = 1f - distance / this.maxStompRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			player.Camera.AddShake(this.maxSize * frac, this.maxSpeed * frac, 0.05f, this.shakeLength);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00026810 File Offset: 0x00024A10
		private float maxStompAttackRange
		{
			get
			{
				return 3000f;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00026817 File Offset: 0x00024A17
		private float attackShakeLength
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0002681E File Offset: 0x00024A1E
		private float attackMaxSpeed
		{
			get
			{
				return 900f;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00026825 File Offset: 0x00024A25
		private float attackMaxSize
		{
			get
			{
				return 55f;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002682C File Offset: 0x00024A2C
		[ClientRpc]
		public void DoStompAttackFX(Vector3 pos)
		{
			if (!this.DoStompAttackFX__RpcProxy(pos, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("giant.stomp2", pos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("giant.stomp0", pos);
			Vector3 handPos = this.Rotation.Forward * 100f + pos + this.Rotation.Right * 5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/giant_stomp_outer.vpcf", handPos);
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			if (player.GroundEntity == null || !player.GroundEntity.IsWorld)
			{
				return;
			}
			float distance = player.Position.Distance(this.Position);
			if (distance > this.maxStompAttackRange)
			{
				return;
			}
			float frac = 1f - distance / this.maxStompAttackRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			player.Camera.AddShake(this.attackMaxSize * frac, this.attackMaxSpeed * frac, 0.05f, this.attackShakeLength);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002693D File Offset: 0x00024B3D
		public override void OnTargetAcquired(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StompState.SetStompCooldown(this.StompState.Cooldown / 2f);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00026960 File Offset: 0x00024B60
		protected bool DoStompAttackFX__RpcProxy(Vector3 pos, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoStompAttackFX", new object[]
				{
					pos
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1431959536, this))
			{
				if (!NetRead.IsSupported(pos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoStompAttackFX is not allowed to use Vector3 for the parameter 'pos'!");
					return false;
				}
				writer.Write<Vector3>(pos);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000269F4 File Offset: 0x00024BF4
		public void DoStompAttackFX(To toTarget, Vector3 pos)
		{
			this.DoStompAttackFX__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00026A04 File Offset: 0x00024C04
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == 1431959536)
			{
				Vector3 __pos = read.ReadData<Vector3>(default(Vector3));
				if (!Prediction.WasPredicted("DoStompAttackFX", new object[]
				{
					__pos
				}))
				{
					this.DoStompAttackFX(__pos);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x040002DA RID: 730
		public GiantIdleState IdleState;

		// Token: 0x040002DB RID: 731
		public GiantChaseState ChaseState;

		// Token: 0x040002DC RID: 732
		public GiantStompState StompState;
	}
}
