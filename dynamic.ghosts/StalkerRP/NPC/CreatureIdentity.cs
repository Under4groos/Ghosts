using System;

namespace StalkerRP.NPC
{
	// Token: 0x02000070 RID: 112
	[Flags]
	public enum CreatureIdentity
	{
		// Token: 0x040001CF RID: 463
		None = 0,
		// Token: 0x040001D0 RID: 464
		Player = 1,
		// Token: 0x040001D1 RID: 465
		NPC = 2,
		// Token: 0x040001D2 RID: 466
		BloodSucker = 4,
		// Token: 0x040001D3 RID: 467
		Zombie = 8,
		// Token: 0x040001D4 RID: 468
		Controller = 16,
		// Token: 0x040001D5 RID: 469
		Burer = 32,
		// Token: 0x040001D6 RID: 470
		PsySucker = 64,
		// Token: 0x040001D7 RID: 471
		PseudoGiant = 128,
		// Token: 0x040001D8 RID: 472
		Snork = 256,
		// Token: 0x040001D9 RID: 473
		Chimera = 512,
		// Token: 0x040001DA RID: 474
		Flesh = 1024,
		// Token: 0x040001DB RID: 475
		BlindDog = 2048,
		// Token: 0x040001DC RID: 476
		PseudoDog = 4096,
		// Token: 0x040001DD RID: 477
		PsyDog = 8192,
		// Token: 0x040001DE RID: 478
		Boar = 16384,
		// Token: 0x040001DF RID: 479
		Poltergeist = 32768,
		// Token: 0x040001E0 RID: 480
		Cat = 65536,
		// Token: 0x040001E1 RID: 481
		Karlik = 131072,
		// Token: 0x040001E2 RID: 482
		Izlom = 262144,
		// Token: 0x040001E3 RID: 483
		Tushkano = 524288
	}
}
