using System;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.Inventory
{
	// Token: 0x020000F9 RID: 249
	[GameResource("Weapon Resource", "weapon", "Contains information about a weapon.")]
	public class WeaponItemResource : ItemResource
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002D996 File Offset: 0x0002BB96
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0002D99E File Offset: 0x0002BB9E
		[Category("Weapon Stats")]
		public float Damage { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002D9A7 File Offset: 0x0002BBA7
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002D9AF File Offset: 0x0002BBAF
		[Category("Weapon Stats")]
		public bool Automatic { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002D9C0 File Offset: 0x0002BBC0
		[Category("Weapon Stats")]
		public float RPM { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002D9C9 File Offset: 0x0002BBC9
		[HideInEditor]
		public float PrimaryFireDelay
		{
			get
			{
				return 60f / this.RPM;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002D9D7 File Offset: 0x0002BBD7
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0002D9DF File Offset: 0x0002BBDF
		[Category("Weapon Stats")]
		public float ReloadTime { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002D9E8 File Offset: 0x0002BBE8
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0002D9F0 File Offset: 0x0002BBF0
		[Category("Weapon Stats")]
		public float ReloadTimeEmpty { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0002D9F9 File Offset: 0x0002BBF9
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0002DA01 File Offset: 0x0002BC01
		[Category("Weapon Stats")]
		[DefaultValue(0.5f)]
		public float HolsterDelay { get; set; } = 0.5f;

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0002DA0A File Offset: 0x0002BC0A
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0002DA12 File Offset: 0x0002BC12
		[Category("Weapon Stats")]
		[DefaultValue(0.5f)]
		public float DrawDelay { get; set; } = 0.5f;

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002DA1B File Offset: 0x0002BC1B
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0002DA23 File Offset: 0x0002BC23
		[Category("Weapon Stats")]
		[DefaultValue(false)]
		public bool HasSights { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002DA2C File Offset: 0x0002BC2C
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0002DA34 File Offset: 0x0002BC34
		[Category("Weapon Stats")]
		[DefaultValue(22f)]
		public float EnterSightsSpeed { get; set; } = 22f;

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002DA3D File Offset: 0x0002BC3D
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x0002DA45 File Offset: 0x0002BC45
		[Category("Weapon Stats")]
		[DefaultValue(20f)]
		public float SightsFOVReduction { get; set; } = 20f;

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0002DA4E File Offset: 0x0002BC4E
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x0002DA56 File Offset: 0x0002BC56
		[Category("Weapon Recoil")]
		[DefaultValue(5f)]
		public float VerticalRecoil { get; set; } = 5f;

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002DA5F File Offset: 0x0002BC5F
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x0002DA67 File Offset: 0x0002BC67
		[Category("Weapon Recoil")]
		[DefaultValue(5f)]
		public float HorizontalRecoil { get; set; } = 5f;

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002DA70 File Offset: 0x0002BC70
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x0002DA78 File Offset: 0x0002BC78
		[Category("Weapon Recoil")]
		[DefaultValue(25f)]
		public float RecoilDecayRate { get; set; } = 25f;

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0002DA81 File Offset: 0x0002BC81
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x0002DA89 File Offset: 0x0002BC89
		[Category("Weapon Recoil")]
		[DefaultValue(1f)]
		[Description("This 'shield' must first hit zero before recoil is actually applied to your gun. Regenerates when not firing. Allows for short taps with accuracy.")]
		public float RecoilShield { get; set; } = 1f;

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002DA92 File Offset: 0x0002BC92
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0002DA9A File Offset: 0x0002BC9A
		[Category("Weapon Recoil")]
		[DefaultValue(5f)]
		public float CameraShakeMagnitude { get; set; } = 5f;

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002DAA3 File Offset: 0x0002BCA3
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0002DAAB File Offset: 0x0002BCAB
		[Category("Weapon Recoil")]
		[DefaultValue(5f)]
		public float CameraShakeRoughness { get; set; } = 5f;

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002DAB4 File Offset: 0x0002BCB4
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0002DABC File Offset: 0x0002BCBC
		[Category("Weapon Spread")]
		public float SpreadMinimum { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002DAC5 File Offset: 0x0002BCC5
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0002DACD File Offset: 0x0002BCCD
		[Category("Weapon Spread")]
		public float SpreadIncreasePerShot { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002DAD6 File Offset: 0x0002BCD6
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0002DADE File Offset: 0x0002BCDE
		[Category("Weapon Spread")]
		[Description("This caps the spread when increased by the per shot number.")]
		public float SpreadSoftMaximum { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0002DAE7 File Offset: 0x0002BCE7
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0002DAEF File Offset: 0x0002BCEF
		[Category("Weapon Spread")]
		public float SpreadSightsAdd { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002DAF8 File Offset: 0x0002BCF8
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0002DB00 File Offset: 0x0002BD00
		[Category("Weapon Spread")]
		[DefaultValue(0.6f)]
		[Description("Multiplier from moving. Will be added to 1 when multiplied with your current spread. e.g. 0.6 is a 60% increase.")]
		public float SpreadMovingAdd { get; set; } = 0.6f;

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002DB09 File Offset: 0x0002BD09
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0002DB11 File Offset: 0x0002BD11
		[Category("Weapon Spread")]
		[DefaultValue(1f)]
		[Description("Multiplier from moving. Will be added to 1 when multiplied with your current spread.")]
		public float SpreadJumpingAdd { get; set; } = 1f;

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002DB1A File Offset: 0x0002BD1A
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0002DB22 File Offset: 0x0002BD22
		[Category("Weapon Spread")]
		[DefaultValue(-0.2f)]
		[Description("Multiplier from moving. Will be added to 1 when multiplied with your current spread.")]
		public float SpreadCrouchingAdd { get; set; } = -0.2f;

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002DB2B File Offset: 0x0002BD2B
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x0002DB33 File Offset: 0x0002BD33
		[Category("Weapon View Model")]
		[DefaultValue(0)]
		public Vector3 ViewModelOffset { get; set; } = 0f;

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002DB3C File Offset: 0x0002BD3C
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0002DB44 File Offset: 0x0002BD44
		[Category("Weapon View Model")]
		public Angles ViewModelRotationOffset { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002DB4D File Offset: 0x0002BD4D
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0002DB55 File Offset: 0x0002BD55
		[Category("Weapon Stats")]
		[DefaultValue(0.8f)]
		public float SightsRecoilMult { get; set; } = 0.8f;

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002DB5E File Offset: 0x0002BD5E
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x0002DB66 File Offset: 0x0002BD66
		[Category("Weapon Stats")]
		public float Force { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002DB6F File Offset: 0x0002BD6F
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0002DB77 File Offset: 0x0002BD77
		[Category("Weapon Stats")]
		public float MuzzleVelocity { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002DB80 File Offset: 0x0002BD80
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0002DB88 File Offset: 0x0002BD88
		[Category("Weapon Stats")]
		[DefaultValue(1)]
		public int NumBullets { get; set; } = 1;

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002DB91 File Offset: 0x0002BD91
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0002DB99 File Offset: 0x0002BD99
		[Category("Weapon Stats")]
		public float BulletSize { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002DBA2 File Offset: 0x0002BDA2
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0002DBAA File Offset: 0x0002BDAA
		[Category("Weapon Stats")]
		[ResourceType("ammo")]
		public string AmmoResourcePath { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002DBB3 File Offset: 0x0002BDB3
		[HideInEditor]
		public AmmoItemResource AmmoResource
		{
			get
			{
				ResourceLibrary resourceLibrary = GlobalGameNamespace.ResourceLibrary;
				if (resourceLibrary == null)
				{
					return null;
				}
				return resourceLibrary.Get<AmmoItemResource>(this.AmmoResourcePath);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002DBCB File Offset: 0x0002BDCB
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x0002DBD3 File Offset: 0x0002BDD3
		[Category("Weapon Stats")]
		public int MagazinePrimarySize { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002DBDC File Offset: 0x0002BDDC
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
		[Category("Weapon Stats")]
		[ResourceType("vpcf")]
		[DefaultValue("particles/pistol_muzzleflash.vpcf")]
		public string MuzzleEffectPath { get; set; } = "particles/pistol_muzzleflash.vpcf";

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002DBED File Offset: 0x0002BDED
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0002DBF5 File Offset: 0x0002BDF5
		[Category("Weapon Stats")]
		[ResourceType("sound")]
		public string FireSound { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002DBFE File Offset: 0x0002BDFE
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0002DC06 File Offset: 0x0002BE06
		[Category("Weapon Stats")]
		[ResourceType("sound")]
		public string DryFireSound { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002DC0F File Offset: 0x0002BE0F
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0002DC17 File Offset: 0x0002BE17
		[Category("Weapon Stats")]
		[ResourceType("vmdl")]
		public string ViewModelPath { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002DC20 File Offset: 0x0002BE20
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0002DC28 File Offset: 0x0002BE28
		[Category("Weapon Stats")]
		public WeaponItemResource.WeaponType Type { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002DC31 File Offset: 0x0002BE31
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0002DC39 File Offset: 0x0002BE39
		[Category("Weapon Stats")]
		public HoldType HoldType { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002DC42 File Offset: 0x0002BE42
		[HideInEditor]
		public override ItemCategory Category
		{
			get
			{
				return ItemCategory.Weapon;
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002DC48 File Offset: 0x0002BE48
		public StalkerWeaponBase CreateWeaponEntity()
		{
			StalkerWeaponBase wep;
			switch (this.Type)
			{
			case WeaponItemResource.WeaponType.Regular:
				wep = new StalkerWeaponBase();
				wep.Init(this);
				break;
			case WeaponItemResource.WeaponType.Shotgun:
				wep = new StalkerShotgunBase();
				wep.Init(this);
				break;
			case WeaponItemResource.WeaponType.Scoped:
				wep = new StalkerScopedWeaponBase();
				wep.Init(this);
				break;
			default:
				return null;
			}
			return wep;
		}

		// Token: 0x02000206 RID: 518
		public enum WeaponType
		{
			// Token: 0x04000872 RID: 2162
			Regular,
			// Token: 0x04000873 RID: 2163
			Shotgun,
			// Token: 0x04000874 RID: 2164
			Scoped
		}
	}
}
