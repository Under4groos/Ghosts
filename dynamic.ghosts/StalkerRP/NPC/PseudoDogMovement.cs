using System;

namespace StalkerRP.NPC
{
	// Token: 0x020000C3 RID: 195
	public class PseudoDogMovement : NPCMovementSlowTurn
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0002564F File Offset: 0x0002384F
		protected override float StationaryTurnDotThreshold
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00025656 File Offset: 0x00023856
		[Description("If the dot product of the targets new direction withs its current forward direction is greater than this value it will end its stationary turn and begin accelerating.")]
		protected override float StationaryTurnEndDotThreshold
		{
			get
			{
				return 0.7f;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0002565D File Offset: 0x0002385D
		protected override float TurnSpeedMin
		{
			get
			{
				return 0.9f;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00025664 File Offset: 0x00023864
		protected override float TurnSpeedMax
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0002566B File Offset: 0x0002386B
		protected override float Acceleration
		{
			get
			{
				return 4.5f;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00025672 File Offset: 0x00023872
		public PseudoDogMovement(NPCBase npc) : base(npc)
		{
		}
	}
}
