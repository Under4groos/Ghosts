using System;

namespace StalkerRP.NPC
{
	// Token: 0x020000AB RID: 171
	public class FleshMovement : NPCMovementSlowTurn
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00022193 File Offset: 0x00020393
		protected override float StationaryTurnDotThreshold
		{
			get
			{
				return 0.45f;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0002219A File Offset: 0x0002039A
		[Description("If the dot product of the targets new direction withs its current forward direction is greater than this value it will end its stationary turn and begin accelerating.")]
		protected override float StationaryTurnEndDotThreshold
		{
			get
			{
				return 0.8f;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000221A1 File Offset: 0x000203A1
		protected override float TurnSpeedMin
		{
			get
			{
				return 0.8f;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x000221A8 File Offset: 0x000203A8
		protected override float TurnSpeedMax
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000221AF File Offset: 0x000203AF
		protected override float Acceleration
		{
			get
			{
				return 4.5f;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000221B6 File Offset: 0x000203B6
		public FleshMovement(NPCBase npc) : base(npc)
		{
		}
	}
}
