using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Debug.settings;
using Sandbox.Internal;
using StalkerRP.NPC;

namespace StalkerRP
{
	// Token: 0x02000026 RID: 38
	public class NPCSpawnManager : Entity
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009D63 File Offset: 0x00007F63
		private float SpawnsPerMinute
		{
			get
			{
				return 30f * ServerSettings.stalker_setting_mutant_spawn_rate;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009D70 File Offset: 0x00007F70
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CacheNPCs();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009D89 File Offset: 0x00007F89
		[Event.Tick.ServerAttribute]
		private void Update()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CleanseNPCs();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TrySpawnNPC();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00009DA1 File Offset: 0x00007FA1
		private void CleanseNPCs()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.npcList.RemoveAll((NPCBase x) => !x.IsValid());
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009DD4 File Offset: 0x00007FD4
		private void TrySpawnNPC()
		{
			if (this.timeUntilNextSpawn > 0f)
			{
				return;
			}
			if (this.npcList.Count >= ServerSettings.stalker_setting_max_mutants)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SpawnNPC();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeUntilNextSpawn = 60f / this.SpawnsPerMinute;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009E30 File Offset: 0x00008030
		private void SpawnNPC()
		{
			Vector3? pos;
			if (NPCSpawnPoint.SpawnPoints.Count <= 0)
			{
				pos = NavMesh.GetClosestPoint(Vector3.Random * 10000f * Noise.Simplex(Time.Now));
			}
			else
			{
				pos = new Vector3?(NPCSpawnPoint.SpawnPoints.GetRandom<Entity>().Position);
			}
			if (pos == null)
			{
				return;
			}
			Type type = this.npcTypes.GetRandom<Type>();
			Entity ent = GlobalGameNamespace.TypeLibrary.Create<Entity>(type, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.Position = pos.Value;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.npcList.Add(ent as NPCBase);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009ED4 File Offset: 0x000080D4
		private void CacheNPCs()
		{
			foreach (TypeDescription type in GlobalGameNamespace.TypeLibrary.GetDescriptions<Entity>())
			{
				if (type.GetAttribute<NPCAttribute>() != null)
				{
					this.npcTypes.Add(type.GetType());
				}
			}
		}

		// Token: 0x04000077 RID: 119
		private readonly List<NPCBase> npcList = new List<NPCBase>();

		// Token: 0x04000078 RID: 120
		private readonly List<Type> npcTypes = new List<Type>();

		// Token: 0x04000079 RID: 121
		private TimeUntil timeUntilNextSpawn;
	}
}
