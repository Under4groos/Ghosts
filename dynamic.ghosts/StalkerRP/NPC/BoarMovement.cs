using System;

namespace StalkerRP.NPC
{
	// Token: 0x0200007F RID: 127
	public class BoarMovement : NPCMovementSlowTurn
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001B0A7 File Offset: 0x000192A7
		protected override float StationaryTurnDotThreshold
		{
			get
			{
				return 0.45f;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001B0AE File Offset: 0x000192AE
		[Description("If the dot product of the targets new direction withs its current forward direction is greater than this value it will end its stationary turn and begin accelerating.")]
		protected override float StationaryTurnEndDotThreshold
		{
			get
			{
				return 0.8f;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001B0B5 File Offset: 0x000192B5
		protected override float TurnSpeedMin
		{
			get
			{
				return 0.8f;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001B0BC File Offset: 0x000192BC
		protected override float TurnSpeedMax
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001B0C3 File Offset: 0x000192C3
		protected override float Acceleration
		{
			get
			{
				return 4.5f;
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001B0CA File Offset: 0x000192CA
		public BoarMovement(NPCBase npc) : base(npc)
		{
		}
	}
}
