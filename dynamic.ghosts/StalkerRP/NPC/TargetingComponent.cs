using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200006F RID: 111
	public class TargetingComponent : EntityComponent, ISingletonComponent
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000184FC File Offset: 0x000166FC
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00018504 File Offset: 0x00016704
		public TargetComponent Target
		{
			get
			{
				return this.target;
			}
			set
			{
				TargetComponent oldValue = this.target;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.target = value;
				if (this.target != oldValue)
				{
					this.Host.OnTargetAcquired((value != null) ? value.Entity : null);
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00018544 File Offset: 0x00016744
		public Entity TargetEntity
		{
			get
			{
				TargetComponent targetComponent = this.Target;
				if (targetComponent == null)
				{
					return null;
				}
				return targetComponent.Entity;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00018558 File Offset: 0x00016758
		public float DistanceToTarget
		{
			get
			{
				TargetComponent targetComponent = this.Target;
				if (((targetComponent != null) ? targetComponent.Entity : null) == null)
				{
					return 0f;
				}
				return this.TargetEntity.Position.Distance(base.Entity.Position);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001859D File Offset: 0x0001679D
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x000185A5 File Offset: 0x000167A5
		[Description("Forces entities in the hash to be considered friendly.")]
		private HashSet<TargetComponent> ForcedFriendlies { get; set; } = new HashSet<TargetComponent>();

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000185AE File Offset: 0x000167AE
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x000185B6 File Offset: 0x000167B6
		[Description("Forces entities in the has to be considered enemies.")]
		private HashSet<TargetComponent> ForcedEnemies { get; set; } = new HashSet<TargetComponent>();

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000185BF File Offset: 0x000167BF
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x000185C7 File Offset: 0x000167C7
		[DefaultValue(CreatureIdentity.None)]
		[Description("The identity of this targeting component.")]
		private CreatureIdentity Identity { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000185D0 File Offset: 0x000167D0
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x000185D8 File Offset: 0x000167D8
		[DefaultValue(CreatureIdentity.None)]
		[Description("A mask of creature identities that will be targeted.")]
		private CreatureIdentity EnemyIdentities { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000185E1 File Offset: 0x000167E1
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000185E9 File Offset: 0x000167E9
		[DefaultValue(CreatureIdentity.None)]
		[Description("A mask of creature identities that will be ignored.")]
		private CreatureIdentity IgnoreIdentities { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000185F2 File Offset: 0x000167F2
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x000185FA File Offset: 0x000167FA
		[DefaultValue(false)]
		[Description("If this is true, it will ignore entities of the same type as the Host. ForcedEnemies takes priority over this.")]
		private bool IgnoreOwnIdentity { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00018603 File Offset: 0x00016803
		[Description("Targets within this range will generate the maximum value of threat from RecalculateThreatLevelsByDistance().")]
		private float DistanceForMaxThreatGeneration
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001860A File Offset: 0x0001680A
		[Description("Enemies further away than this will have their threat set to the minimum value by RecalculateThreatLevelsByDistance().")]
		private float DistanceForMinimumThreat
		{
			get
			{
				return 4000f;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00018611 File Offset: 0x00016811
		private float MaximumThreatGeneratedFromDistance
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00018618 File Offset: 0x00016818
		private float MaximumThreatLostFromDistance
		{
			get
			{
				return -100f;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001861F File Offset: 0x0001681F
		private float MaximumRangeSquared
		{
			get
			{
				return MathF.Pow(this.DistanceForMinimumThreat, 2f);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00018631 File Offset: 0x00016831
		private float MinimumRangeSquared
		{
			get
			{
				return MathF.Pow(this.DistanceForMaxThreatGeneration, 2f);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00018643 File Offset: 0x00016843
		[Description("Scale the threat generated with the creatures speed. Maximum threat at this value.")]
		private float SpeedForMaximumThreat
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001864A File Offset: 0x0001684A
		[Description("The minimum speed a hostile needs to be registered as a threat. Useful for motion sensor type enemies.")]
		private float MinimumSpeedForThreat
		{
			get
			{
				return 80f;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00018651 File Offset: 0x00016851
		private float MaximumThreatGeneratedFromSpeed
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00018658 File Offset: 0x00016858
		private float MaximumThreatLostFromSpeed
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001865F File Offset: 0x0001685F
		private float DamageToThreatMultiplier
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00018666 File Offset: 0x00016866
		protected override void OnActivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host = (base.Entity as NPCBase);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			TargetingComponent.All.Add(this);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001868E File Offset: 0x0001688E
		protected override void OnDeactivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			TargetingComponent.All.Remove(this);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000186A4 File Offset: 0x000168A4
		public void LoadFromAsset(NPCResource resource)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Identity = resource.Identity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EnemyIdentities = resource.EnemyIdentities;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IgnoreIdentities = resource.IgnoreIdentities;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IgnoreOwnIdentity = resource.IgnoreOwnIdentity;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000186F5 File Offset: 0x000168F5
		public void AddForcedEnemy(TargetComponent targetComponent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ForcedEnemies.Add(targetComponent);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00018709 File Offset: 0x00016909
		public void AddForcedFriendly(TargetComponent targetComponent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ForcedFriendlies.Add(targetComponent);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001871D File Offset: 0x0001691D
		public void RemovePotentialTarget(TargetComponent targetComponent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PotentialTargets.Remove(targetComponent);
			if (this.Target == targetComponent)
			{
				this.Target = null;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00018744 File Offset: 0x00016944
		public bool TrySetTarget(Entity target)
		{
			if (target == null)
			{
				return false;
			}
			TargetComponent targetComponent;
			if (!target.Components.TryGet<TargetComponent>(out targetComponent, false))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetThreat(targetComponent, 1000f);
			return true;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001877A File Offset: 0x0001697A
		public void Reset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Target = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PotentialTargets.Clear();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018798 File Offset: 0x00016998
		public bool TryEnsureTarget()
		{
			if (!this.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetTargetFromPotentialTargets();
			}
			return this.ValidateTarget();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000187B3 File Offset: 0x000169B3
		public bool ValidateTarget()
		{
			if (this.Target == null)
			{
				return false;
			}
			if (this.TargetEntity.IsValid() && this.TargetEntity.LifeState == LifeState.Alive)
			{
				return true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Target = null;
			return false;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000187E8 File Offset: 0x000169E8
		public bool ValidateTarget(Entity entity)
		{
			return entity != null && (entity.IsValid() && entity.LifeState == LifeState.Alive);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00018804 File Offset: 0x00016A04
		public bool CanTarget(TargetComponent target)
		{
			return !target.Entity.Tags.Has("stalker_ghost_mode") && target.Entity != base.Entity && target.Entity.LifeState == LifeState.Alive && !this.ForcedFriendlies.Contains(target) && (this.ForcedEnemies.Contains(target) || ((!this.IgnoreOwnIdentity || (this.Identity & target.Identity) != this.Identity) && (target.Identity & this.IgnoreIdentities) == CreatureIdentity.None && (target.Identity & this.EnemyIdentities) > CreatureIdentity.None));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000188A9 File Offset: 0x00016AA9
		public bool CanSeeTarget()
		{
			return this.TargetEntity.IsValid() && base.Entity.CanSee(this.TargetEntity);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000188CC File Offset: 0x00016ACC
		public IEnumerable<TargetComponent> GetValidTargetsInRange(float range)
		{
			List<TargetComponent> list = new List<TargetComponent>();
			foreach (TargetComponent targetComponent in TargetComponent.All)
			{
				if (targetComponent.Entity.DistanceToSquared(targetComponent.Entity) <= this.MaximumRangeSquared && this.CanTarget(targetComponent))
				{
					list.Add(targetComponent);
				}
			}
			return list;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018948 File Offset: 0x00016B48
		public void RecalculateThreatBasedOnMovement(bool ignoreLineOfSight = true)
		{
			foreach (TargetComponent targetComponent in TargetComponent.All)
			{
				if (targetComponent.Entity.DistanceToSquared(targetComponent.Entity) > this.MaximumRangeSquared)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SetThreat(targetComponent, 0f);
				}
				else if (this.CanTarget(targetComponent) && (ignoreLineOfSight || base.Entity.CanSee(targetComponent.Entity)))
				{
					float normal = (targetComponent.Entity.Velocity.Length - this.MinimumSpeedForThreat) / this.SpeedForMaximumThreat;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					normal = normal.Clamp(0f, 1f);
					float threat = this.GetThreatForNormalizedSpeed(normal);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AddThreat(targetComponent, threat);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetTargetFromPotentialTargets();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018A40 File Offset: 0x00016C40
		public void RecalculateThreatLevelsByDistance(bool ignoreLineOfSight = false)
		{
			foreach (TargetComponent targetComponent in TargetComponent.All)
			{
				float distSqr = base.Entity.DistanceToSquared(targetComponent.Entity);
				if (this.CanTarget(targetComponent))
				{
					if (!ignoreLineOfSight && !base.Entity.CanSee(targetComponent.Entity))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.AddThreat(targetComponent, this.MaximumThreatLostFromDistance / 3f);
					}
					else
					{
						float normal = (distSqr - this.MinimumRangeSquared) / this.MaximumRangeSquared;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						normal = normal.Clamp(0f, 1f);
						float threat = this.GetThreatForNormalizedDistance(normal);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.AddThreat(targetComponent, threat);
					}
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetTargetFromPotentialTargets();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00018B24 File Offset: 0x00016D24
		private void SetTargetFromPotentialTargets()
		{
			TargetComponent currentTarget = null;
			float highestThreat = 0f;
			foreach (KeyValuePair<TargetComponent, float> keyValuePair in this.PotentialTargets)
			{
				TargetComponent targetComponent;
				float num;
				keyValuePair.Deconstruct(out targetComponent, out num);
				TargetComponent target = targetComponent;
				float threat = num;
				if (threat >= highestThreat)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					highestThreat = threat;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					currentTarget = target;
				}
			}
			if (highestThreat > 0f && this.Target == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetThreat(currentTarget, 1000f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Target = ((highestThreat <= 0f) ? null : currentTarget);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00018BD8 File Offset: 0x00016DD8
		public void OnDamageTaken(DamageInfo info)
		{
			if (!info.Attacker.IsValid())
			{
				return;
			}
			if (info.Attacker.Tags.Has("stalker_ghost_mode"))
			{
				return;
			}
			TargetComponent targetComponent;
			if (!info.Attacker.Components.TryGet<TargetComponent>(out targetComponent, false))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddForcedEnemy(targetComponent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddThreat(targetComponent, info.Damage * this.DamageToThreatMultiplier);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018C4A File Offset: 0x00016E4A
		private float GetThreatForNormalizedDistance(float n)
		{
			return n * this.MaximumThreatLostFromDistance + this.MaximumThreatGeneratedFromDistance;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018C5B File Offset: 0x00016E5B
		private float GetThreatForNormalizedSpeed(float n)
		{
			return n * this.MaximumThreatGeneratedFromSpeed + this.MaximumThreatLostFromSpeed;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00018C6C File Offset: 0x00016E6C
		private float GetThreatForTarget(TargetComponent targetComponent)
		{
			return this.PotentialTargets.GetValueOrDefault(targetComponent, 0f);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00018C80 File Offset: 0x00016E80
		private void AddThreat(TargetComponent targetComponent, float threat)
		{
			float newThreat = (this.GetThreatForTarget(targetComponent) + threat).Clamp(0f, 1000f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PotentialTargets[targetComponent] = newThreat;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018CB8 File Offset: 0x00016EB8
		public void SetThreat(TargetComponent targetComponent, float threat)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PotentialTargets[targetComponent] = threat;
		}

		// Token: 0x040001C2 RID: 450
		public const float MaximumThreat = 1000f;

		// Token: 0x040001C3 RID: 451
		public const float MinimumThreat = 0f;

		// Token: 0x040001C4 RID: 452
		public static List<TargetingComponent> All = new List<TargetingComponent>();

		// Token: 0x040001C5 RID: 453
		public NPCBase Host;

		// Token: 0x040001C6 RID: 454
		private TargetComponent target;

		// Token: 0x040001C7 RID: 455
		private readonly Dictionary<TargetComponent, float> PotentialTargets = new Dictionary<TargetComponent, float>();
	}
}
