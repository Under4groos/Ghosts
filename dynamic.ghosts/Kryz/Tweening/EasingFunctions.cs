using System;
using System.Runtime.CompilerServices;

namespace Kryz.Tweening
{
	// Token: 0x02000010 RID: 16
	public static class EasingFunctions
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000079BA File Offset: 0x00005BBA
		public static float Linear(float t)
		{
			return t;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000079BD File Offset: 0x00005BBD
		public static float InQuad(float t)
		{
			return t * t;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000079C2 File Offset: 0x00005BC2
		public static float OutQuad(float t)
		{
			return 1f - EasingFunctions.InQuad(1f - t);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000079D6 File Offset: 0x00005BD6
		public static float InOutQuad(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InQuad(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InQuad((1f - t) * 2f) / 2f;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007A16 File Offset: 0x00005C16
		public static float InCubic(float t)
		{
			return t * t * t;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007A1D File Offset: 0x00005C1D
		public static float OutCubic(float t)
		{
			return 1f - EasingFunctions.InCubic(1f - t);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007A31 File Offset: 0x00005C31
		public static float InOutCubic(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InCubic(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InCubic((1f - t) * 2f) / 2f;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007A71 File Offset: 0x00005C71
		public static float InQuart(float t)
		{
			return t * t * t * t;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007A7A File Offset: 0x00005C7A
		public static float OutQuart(float t)
		{
			return 1f - EasingFunctions.InQuart(1f - t);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007A8E File Offset: 0x00005C8E
		public static float InOutQuart(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InQuart(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InQuart((1f - t) * 2f) / 2f;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007ACE File Offset: 0x00005CCE
		public static float InQuint(float t)
		{
			return t * t * t * t * t;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007AD9 File Offset: 0x00005CD9
		public static float OutQuint(float t)
		{
			return 1f - EasingFunctions.InQuint(1f - t);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007AED File Offset: 0x00005CED
		public static float InOutQuint(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InQuint(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InQuint((1f - t) * 2f) / 2f;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007B2D File Offset: 0x00005D2D
		public static float InSine(float t)
		{
			return (float)(-(float)Math.Cos((double)t * 3.141592653589793 / 2.0));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007B4C File Offset: 0x00005D4C
		public static float OutSine(float t)
		{
			return (float)Math.Sin((double)t * 3.141592653589793 / 2.0);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007B6A File Offset: 0x00005D6A
		public static float InOutSine(float t)
		{
			return (float)(Math.Cos((double)t * 3.141592653589793) - 1.0) / -2f;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007B8E File Offset: 0x00005D8E
		public static float InExpo(float t)
		{
			return (float)Math.Pow(2.0, (double)(10f * (t - 1f)));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007BAD File Offset: 0x00005DAD
		public static float OutExpo(float t)
		{
			return 1f - EasingFunctions.InExpo(1f - t);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007BC1 File Offset: 0x00005DC1
		public static float InOutExpo(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InExpo(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InExpo((1f - t) * 2f) / 2f;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007C01 File Offset: 0x00005E01
		public static float InCirc(float t)
		{
			return -((float)Math.Sqrt((double)(1f - t * t)) - 1f);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007C1A File Offset: 0x00005E1A
		public static float OutCirc(float t)
		{
			return 1f - EasingFunctions.InCirc(1f - t);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007C2E File Offset: 0x00005E2E
		public static float InOutCirc(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InCirc(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InCirc((1f - t) * 2f) / 2f;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007C6E File Offset: 0x00005E6E
		public static float InElastic(float t)
		{
			return 1f - EasingFunctions.OutElastic(1f - t);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007C84 File Offset: 0x00005E84
		public static float OutElastic(float t)
		{
			float p = 0.3f;
			return (float)Math.Pow(2.0, (double)(-10f * t)) * (float)Math.Sin((double)(t - p / 4f) * 6.283185307179586 / (double)p) + 1f;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007CD2 File Offset: 0x00005ED2
		public static float InOutElastic(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InElastic(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InElastic((1f - t) * 2f) / 2f;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007D14 File Offset: 0x00005F14
		public static float InBack(float t)
		{
			float s = 1.70158f;
			return t * t * ((s + 1f) * t - s);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007D36 File Offset: 0x00005F36
		public static float OutBack(float t)
		{
			return 1f - EasingFunctions.InBack(1f - t);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007D4A File Offset: 0x00005F4A
		public static float InOutBack(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InBack(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InBack((1f - t) * 2f) / 2f;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007D8A File Offset: 0x00005F8A
		public static float InBounce(float t)
		{
			return 1f - EasingFunctions.OutBounce(1f - t);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public static float OutBounce(float t)
		{
			float div = 2.75f;
			float mult = 7.5625f;
			if (t < 1f / div)
			{
				return mult * t * t;
			}
			if (t < 2f / div)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				t -= 1.5f / div;
				return mult * t * t + 0.75f;
			}
			if ((double)t < 2.5 / (double)div)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				t -= 2.25f / div;
				return mult * t * t + 0.9375f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			t -= 2.625f / div;
			return mult * t * t + 0.984375f;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007E36 File Offset: 0x00006036
		public static float InOutBounce(float t)
		{
			if ((double)t < 0.5)
			{
				return EasingFunctions.InBounce(t * 2f) / 2f;
			}
			return 1f - EasingFunctions.InBounce((1f - t) * 2f) / 2f;
		}
	}
}
