using System;

namespace StalkerRP.NPC
{
	// Token: 0x020000A1 RID: 161
	public class BlindDogMovement : NPCMovementSlowTurn
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00020E33 File Offset: 0x0001F033
		protected override float StationaryTurnDotThreshold
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00020E3A File Offset: 0x0001F03A
		[Description("If the dot product of the targets new direction withs its current forward direction is greater than this value it will end its stationary turn and begin accelerating.")]
		protected override float StationaryTurnEndDotThreshold
		{
			get
			{
				return 0.7f;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00020E41 File Offset: 0x0001F041
		protected override float TurnSpeedMin
		{
			get
			{
				return 0.9f;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00020E48 File Offset: 0x0001F048
		protected override float TurnSpeedMax
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00020E4F File Offset: 0x0001F04F
		protected override float Acceleration
		{
			get
			{
				return 4.5f;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00020E56 File Offset: 0x0001F056
		public BlindDogMovement(NPCBase npc) : base(npc)
		{
		}
	}
}
