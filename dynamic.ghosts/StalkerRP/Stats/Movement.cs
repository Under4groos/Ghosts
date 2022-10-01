using System;
using Sandbox;

namespace StalkerRP.Stats
{
	// Token: 0x02000069 RID: 105
	public struct Movement
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00017DB8 File Offset: 0x00015FB8
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00017DC0 File Offset: 0x00015FC0
		public float SprintSpeed { readonly get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00017DC9 File Offset: 0x00015FC9
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00017DD1 File Offset: 0x00015FD1
		public float SprintForwardProportionality { readonly get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00017DDA File Offset: 0x00015FDA
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00017DE2 File Offset: 0x00015FE2
		public float WalkSpeed { readonly get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00017DEB File Offset: 0x00015FEB
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00017DF3 File Offset: 0x00015FF3
		public float DefaultSpeed { readonly get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00017DFC File Offset: 0x00015FFC
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00017E04 File Offset: 0x00016004
		public float Acceleration { readonly get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00017E0D File Offset: 0x0001600D
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00017E15 File Offset: 0x00016015
		public float AccelerationForwardProportionality { readonly get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00017E1E File Offset: 0x0001601E
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00017E26 File Offset: 0x00016026
		public float AirAcceleration { readonly get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00017E2F File Offset: 0x0001602F
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00017E37 File Offset: 0x00016037
		public float FallDamageThreshold { readonly get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00017E40 File Offset: 0x00016040
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00017E48 File Offset: 0x00016048
		public float FallDamageKillThreshold { readonly get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00017E51 File Offset: 0x00016051
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00017E59 File Offset: 0x00016059
		public Curve FallDamageCurve { readonly get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00017E62 File Offset: 0x00016062
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00017E6A File Offset: 0x0001606A
		public float GroundFriction { readonly get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00017E73 File Offset: 0x00016073
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00017E7B File Offset: 0x0001607B
		public float StopSpeed { readonly get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00017E84 File Offset: 0x00016084
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00017E8C File Offset: 0x0001608C
		public float GroundAngle { readonly get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00017E95 File Offset: 0x00016095
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x00017E9D File Offset: 0x0001609D
		public float StepSize { readonly get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00017EA6 File Offset: 0x000160A6
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00017EAE File Offset: 0x000160AE
		public float MaxNonJumpVelocity { readonly get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00017EB7 File Offset: 0x000160B7
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00017EBF File Offset: 0x000160BF
		public float BodyGirth { readonly get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00017EC8 File Offset: 0x000160C8
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00017ED0 File Offset: 0x000160D0
		public float BodyHeight { readonly get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00017ED9 File Offset: 0x000160D9
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00017EE1 File Offset: 0x000160E1
		public float EyeHeight { readonly get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00017EEA File Offset: 0x000160EA
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00017EF2 File Offset: 0x000160F2
		public float Gravity { readonly get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00017EFB File Offset: 0x000160FB
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00017F03 File Offset: 0x00016103
		public float AirControl { readonly get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00017F0C File Offset: 0x0001610C
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00017F14 File Offset: 0x00016114
		public bool AutoJump { readonly get; set; }
	}
}
