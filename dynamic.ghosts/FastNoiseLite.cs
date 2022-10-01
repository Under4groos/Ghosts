using System;
using System.Runtime.CompilerServices;

// Token: 0x0200000A RID: 10
public class FastNoiseLite
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002500 File Offset: 0x00000700
	public FastNoiseLite(int seed = 1337)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.SetSeed(seed);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000259A File Offset: 0x0000079A
	[Description("Sets seed used for all noise types")]
	public void SetSeed(int seed)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mSeed = seed;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000025A8 File Offset: 0x000007A8
	[Description("Sets frequency for all noise types")]
	public void SetFrequency(float frequency)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mFrequency = frequency;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000025B6 File Offset: 0x000007B6
	[Description("Sets noise algorithm used for GetNoise(...)")]
	public void SetNoiseType(FastNoiseLite.NoiseType noiseType)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mNoiseType = noiseType;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.UpdateTransformType3D();
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000025CF File Offset: 0x000007CF
	[Description("Sets domain rotation type for 3D Noise and 3D DomainWarp. Can aid in reducing directional artifacts when sampling a 2D plane in 3D")]
	public void SetRotationType3D(FastNoiseLite.RotationType3D rotationType3D)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mRotationType3D = rotationType3D;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.UpdateTransformType3D();
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.UpdateWarpTransformType3D();
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000025F3 File Offset: 0x000007F3
	[Description("Sets method for combining octaves in all fractal noise types")]
	public void SetFractalType(FastNoiseLite.FractalType fractalType)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mFractalType = fractalType;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002601 File Offset: 0x00000801
	[Description("Sets octave count for all fractal noise types")]
	public void SetFractalOctaves(int octaves)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mOctaves = octaves;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.CalculateFractalBounding();
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000261A File Offset: 0x0000081A
	[Description("Sets octave lacunarity for all fractal noise types")]
	public void SetFractalLacunarity(float lacunarity)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mLacunarity = lacunarity;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002628 File Offset: 0x00000828
	[Description("Sets octave gain for all fractal noise types")]
	public void SetFractalGain(float gain)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mGain = gain;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.CalculateFractalBounding();
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002641 File Offset: 0x00000841
	[Description("Sets octave weighting for all none DomainWarp fratal types")]
	public void SetFractalWeightedStrength(float weightedStrength)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mWeightedStrength = weightedStrength;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000264F File Offset: 0x0000084F
	[Description("Sets strength of the fractal ping pong effect")]
	public void SetFractalPingPongStrength(float pingPongStrength)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mPingPongStrength = pingPongStrength;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000265D File Offset: 0x0000085D
	[Description("Sets distance function used in cellular noise calculations")]
	public void SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction cellularDistanceFunction)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mCellularDistanceFunction = cellularDistanceFunction;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000266B File Offset: 0x0000086B
	[Description("Sets return type from cellular noise calculations")]
	public void SetCellularReturnType(FastNoiseLite.CellularReturnType cellularReturnType)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mCellularReturnType = cellularReturnType;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002679 File Offset: 0x00000879
	[Description("Sets the maximum distance a cellular point can move from it's grid position")]
	public void SetCellularJitter(float cellularJitter)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mCellularJitterModifier = cellularJitter;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002687 File Offset: 0x00000887
	[Description("Sets the warp algorithm when using DomainWarp(...)")]
	public void SetDomainWarpType(FastNoiseLite.DomainWarpType domainWarpType)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mDomainWarpType = domainWarpType;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.UpdateWarpTransformType3D();
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000026A0 File Offset: 0x000008A0
	[Description("Sets the maximum warp distance from original position when using DomainWarp(...)")]
	public void SetDomainWarpAmp(float domainWarpAmp)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mDomainWarpAmp = domainWarpAmp;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000026B0 File Offset: 0x000008B0
	[Description("2D noise at given position using current settings")]
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	public float GetNoise(float x, float y)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.TransformNoiseCoordinate(ref x, ref y);
		switch (this.mFractalType)
		{
		case FastNoiseLite.FractalType.FBm:
			return this.GenFractalFBm(x, y);
		case FastNoiseLite.FractalType.Ridged:
			return this.GenFractalRidged(x, y);
		case FastNoiseLite.FractalType.PingPong:
			return this.GenFractalPingPong(x, y);
		default:
			return this.GenNoiseSingle(this.mSeed, x, y);
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002710 File Offset: 0x00000910
	[Description("3D noise at given position using current settings")]
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	public float GetNoise(float x, float y, float z)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.TransformNoiseCoordinate(ref x, ref y, ref z);
		switch (this.mFractalType)
		{
		case FastNoiseLite.FractalType.FBm:
			return this.GenFractalFBm(x, y, z);
		case FastNoiseLite.FractalType.Ridged:
			return this.GenFractalRidged(x, y, z);
		case FastNoiseLite.FractalType.PingPong:
			return this.GenFractalPingPong(x, y, z);
		default:
			return this.GenNoiseSingle(this.mSeed, x, y, z);
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002778 File Offset: 0x00000978
	[Description("2D warps the input position using current domain warp settings")]
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	public void DomainWarp(ref float x, ref float y)
	{
		FastNoiseLite.FractalType fractalType = this.mFractalType;
		if (fractalType == FastNoiseLite.FractalType.DomainWarpProgressive)
		{
			this.DomainWarpFractalProgressive(ref x, ref y);
			return;
		}
		if (fractalType != FastNoiseLite.FractalType.DomainWarpIndependent)
		{
			this.DomainWarpSingle(ref x, ref y);
			return;
		}
		this.DomainWarpFractalIndependent(ref x, ref y);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000027B0 File Offset: 0x000009B0
	[Description("3D warps the input position using current domain warp settings")]
	[MethodImpl(MethodImplOptions.AggressiveOptimization)]
	public void DomainWarp(ref float x, ref float y, ref float z)
	{
		FastNoiseLite.FractalType fractalType = this.mFractalType;
		if (fractalType == FastNoiseLite.FractalType.DomainWarpProgressive)
		{
			this.DomainWarpFractalProgressive(ref x, ref y, ref z);
			return;
		}
		if (fractalType != FastNoiseLite.FractalType.DomainWarpIndependent)
		{
			this.DomainWarpSingle(ref x, ref y, ref z);
			return;
		}
		this.DomainWarpFractalIndependent(ref x, ref y, ref z);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000027E9 File Offset: 0x000009E9
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float FastMin(float a, float b)
	{
		if (a >= b)
		{
			return b;
		}
		return a;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000027F2 File Offset: 0x000009F2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float FastMax(float a, float b)
	{
		if (a <= b)
		{
			return b;
		}
		return a;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000027FB File Offset: 0x000009FB
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float FastAbs(float f)
	{
		if (f >= 0f)
		{
			return f;
		}
		return -f;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002809 File Offset: 0x00000A09
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float FastSqrt(float f)
	{
		return (float)Math.Sqrt((double)f);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002813 File Offset: 0x00000A13
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int FastFloor(float f)
	{
		if (f < 0f)
		{
			return (int)f - 1;
		}
		return (int)f;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002824 File Offset: 0x00000A24
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int FastRound(float f)
	{
		if (f < 0f)
		{
			return (int)(f - 0.5f);
		}
		return (int)(f + 0.5f);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000283F File Offset: 0x00000A3F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float Lerp(float a, float b, float t)
	{
		return a + t * (b - a);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002848 File Offset: 0x00000A48
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float InterpHermite(float t)
	{
		return t * t * (3f - 2f * t);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000285B File Offset: 0x00000A5B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float InterpQuintic(float t)
	{
		return t * t * t * (t * (t * 6f - 15f) + 10f);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002878 File Offset: 0x00000A78
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float CubicLerp(float a, float b, float c, float d, float t)
	{
		float p = d - c - (a - b);
		return t * t * t * p + t * t * (a - b - p) + t * (c - a) + b;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000028AC File Offset: 0x00000AAC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float PingPong(float t)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		t -= (float)((int)(t * 0.5f) * 2);
		if (t >= 1f)
		{
			return 2f - t;
		}
		return t;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000028D4 File Offset: 0x00000AD4
	private void CalculateFractalBounding()
	{
		float gain = FastNoiseLite.FastAbs(this.mGain);
		float amp = gain;
		float ampFractal = 1f;
		for (int i = 1; i < this.mOctaves; i++)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ampFractal += amp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= gain;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.mFractalBounding = 1f / ampFractal;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x0000292A File Offset: 0x00000B2A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int Hash(int seed, int xPrimed, int yPrimed)
	{
		int num = seed ^ xPrimed ^ yPrimed;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		return num * 668265261;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x0000293C File Offset: 0x00000B3C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int Hash(int seed, int xPrimed, int yPrimed, int zPrimed)
	{
		int num = seed ^ xPrimed ^ yPrimed ^ zPrimed;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		return num * 668265261;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002950 File Offset: 0x00000B50
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float ValCoord(int seed, int xPrimed, int yPrimed)
	{
		int num = FastNoiseLite.Hash(seed, xPrimed, yPrimed);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		int num2 = num * num;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		return (float)(num2 ^ num2 << 19) * 4.656613E-10f;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002972 File Offset: 0x00000B72
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float ValCoord(int seed, int xPrimed, int yPrimed, int zPrimed)
	{
		int num = FastNoiseLite.Hash(seed, xPrimed, yPrimed, zPrimed);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		int num2 = num * num;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		return (float)(num2 ^ num2 << 19) * 4.656613E-10f;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002998 File Offset: 0x00000B98
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float GradCoord(int seed, int xPrimed, int yPrimed, float xd, float yd)
	{
		int hash = FastNoiseLite.Hash(seed, xPrimed, yPrimed);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash ^= hash >> 15;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash &= 254;
		float xg = FastNoiseLite.Gradients2D[hash];
		float yg = FastNoiseLite.Gradients2D[hash | 1];
		return xd * xg + yd * yg;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000029E4 File Offset: 0x00000BE4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float GradCoord(int seed, int xPrimed, int yPrimed, int zPrimed, float xd, float yd, float zd)
	{
		int hash = FastNoiseLite.Hash(seed, xPrimed, yPrimed, zPrimed);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash ^= hash >> 15;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash &= 252;
		float xg = FastNoiseLite.Gradients3D[hash];
		float yg = FastNoiseLite.Gradients3D[hash | 1];
		float zg = FastNoiseLite.Gradients3D[hash | 2];
		return xd * xg + yd * yg + zd * zg;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002A40 File Offset: 0x00000C40
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void GradCoordOut(int seed, int xPrimed, int yPrimed, out float xo, out float yo)
	{
		int hash = FastNoiseLite.Hash(seed, xPrimed, yPrimed) & 510;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xo = FastNoiseLite.RandVecs2D[hash];
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yo = FastNoiseLite.RandVecs2D[hash | 1];
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002A7C File Offset: 0x00000C7C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void GradCoordOut(int seed, int xPrimed, int yPrimed, int zPrimed, out float xo, out float yo, out float zo)
	{
		int hash = FastNoiseLite.Hash(seed, xPrimed, yPrimed, zPrimed) & 1020;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xo = FastNoiseLite.RandVecs3D[hash];
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yo = FastNoiseLite.RandVecs3D[hash | 1];
		RuntimeHelpers.EnsureSufficientExecutionStack();
		zo = FastNoiseLite.RandVecs3D[hash | 2];
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002ACC File Offset: 0x00000CCC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void GradCoordDual(int seed, int xPrimed, int yPrimed, float xd, float yd, out float xo, out float yo)
	{
		int num = FastNoiseLite.Hash(seed, xPrimed, yPrimed);
		int index = num & 254;
		int index2 = num >> 7 & 510;
		float xg = FastNoiseLite.Gradients2D[index];
		float yg = FastNoiseLite.Gradients2D[index | 1];
		float value = xd * xg + yd * yg;
		float xgo = FastNoiseLite.RandVecs2D[index2];
		float ygo = FastNoiseLite.RandVecs2D[index2 | 1];
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xo = value * xgo;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yo = value * ygo;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002B3C File Offset: 0x00000D3C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void GradCoordDual(int seed, int xPrimed, int yPrimed, int zPrimed, float xd, float yd, float zd, out float xo, out float yo, out float zo)
	{
		int num = FastNoiseLite.Hash(seed, xPrimed, yPrimed, zPrimed);
		int index = num & 252;
		int index2 = num >> 6 & 1020;
		float xg = FastNoiseLite.Gradients3D[index];
		float yg = FastNoiseLite.Gradients3D[index | 1];
		float zg = FastNoiseLite.Gradients3D[index | 2];
		float value = xd * xg + yd * yg + zd * zg;
		float xgo = FastNoiseLite.RandVecs3D[index2];
		float ygo = FastNoiseLite.RandVecs3D[index2 | 1];
		float zgo = FastNoiseLite.RandVecs3D[index2 | 2];
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xo = value * xgo;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yo = value * ygo;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		zo = value * zgo;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002BD8 File Offset: 0x00000DD8
	private float GenNoiseSingle(int seed, float x, float y)
	{
		switch (this.mNoiseType)
		{
		case FastNoiseLite.NoiseType.OpenSimplex2:
			return this.SingleSimplex(seed, x, y);
		case FastNoiseLite.NoiseType.OpenSimplex2S:
			return this.SingleOpenSimplex2S(seed, x, y);
		case FastNoiseLite.NoiseType.Cellular:
			return this.SingleCellular(seed, x, y);
		case FastNoiseLite.NoiseType.Perlin:
			return this.SinglePerlin(seed, x, y);
		case FastNoiseLite.NoiseType.ValueCubic:
			return this.SingleValueCubic(seed, x, y);
		case FastNoiseLite.NoiseType.Value:
			return this.SingleValue(seed, x, y);
		default:
			return 0f;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002C50 File Offset: 0x00000E50
	private float GenNoiseSingle(int seed, float x, float y, float z)
	{
		switch (this.mNoiseType)
		{
		case FastNoiseLite.NoiseType.OpenSimplex2:
			return this.SingleOpenSimplex2(seed, x, y, z);
		case FastNoiseLite.NoiseType.OpenSimplex2S:
			return this.SingleOpenSimplex2S(seed, x, y, z);
		case FastNoiseLite.NoiseType.Cellular:
			return this.SingleCellular(seed, x, y, z);
		case FastNoiseLite.NoiseType.Perlin:
			return this.SinglePerlin(seed, x, y, z);
		case FastNoiseLite.NoiseType.ValueCubic:
			return this.SingleValueCubic(seed, x, y, z);
		case FastNoiseLite.NoiseType.Value:
			return this.SingleValue(seed, x, y, z);
		default:
			return 0f;
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002CD4 File Offset: 0x00000ED4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void TransformNoiseCoordinate(ref float x, ref float y)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x *= this.mFrequency;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y *= this.mFrequency;
		FastNoiseLite.NoiseType noiseType = this.mNoiseType;
		if (noiseType <= FastNoiseLite.NoiseType.OpenSimplex2S)
		{
			float t = (x + y) * 0.3660254f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x += t;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y += t;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002D30 File Offset: 0x00000F30
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void TransformNoiseCoordinate(ref float x, ref float y, ref float z)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x *= this.mFrequency;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y *= this.mFrequency;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		z *= this.mFrequency;
		switch (this.mTransformType3D)
		{
		case FastNoiseLite.TransformType3D.ImproveXYPlanes:
		{
			float xy = x + y;
			float s2 = xy * -0.21132487f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z *= 0.57735026f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x += s2 - z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y = y + s2 - z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z += xy * 0.57735026f;
			return;
		}
		case FastNoiseLite.TransformType3D.ImproveXZPlanes:
		{
			float xz = x + z;
			float s3 = xz * -0.21132487f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= 0.57735026f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x += s3 - y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z += s3 - y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y += xz * 0.57735026f;
			return;
		}
		case FastNoiseLite.TransformType3D.DefaultOpenSimplex2:
		{
			float r = (x + y + z) * 0.6666667f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x = r - x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y = r - y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z = r - z;
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002E58 File Offset: 0x00001058
	private void UpdateTransformType3D()
	{
		FastNoiseLite.RotationType3D rotationType3D = this.mRotationType3D;
		if (rotationType3D == FastNoiseLite.RotationType3D.ImproveXYPlanes)
		{
			this.mTransformType3D = FastNoiseLite.TransformType3D.ImproveXYPlanes;
			return;
		}
		if (rotationType3D == FastNoiseLite.RotationType3D.ImproveXZPlanes)
		{
			this.mTransformType3D = FastNoiseLite.TransformType3D.ImproveXZPlanes;
			return;
		}
		FastNoiseLite.NoiseType noiseType = this.mNoiseType;
		if (noiseType <= FastNoiseLite.NoiseType.OpenSimplex2S)
		{
			this.mTransformType3D = FastNoiseLite.TransformType3D.DefaultOpenSimplex2;
			return;
		}
		this.mTransformType3D = FastNoiseLite.TransformType3D.None;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002EA0 File Offset: 0x000010A0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void TransformDomainWarpCoordinate(ref float x, ref float y)
	{
		FastNoiseLite.DomainWarpType domainWarpType = this.mDomainWarpType;
		if (domainWarpType <= FastNoiseLite.DomainWarpType.OpenSimplex2Reduced)
		{
			float t = (x + y) * 0.3660254f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x += t;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y += t;
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002EDC File Offset: 0x000010DC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void TransformDomainWarpCoordinate(ref float x, ref float y, ref float z)
	{
		switch (this.mWarpTransformType3D)
		{
		case FastNoiseLite.TransformType3D.ImproveXYPlanes:
		{
			float xy = x + y;
			float s2 = xy * -0.21132487f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z *= 0.57735026f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x += s2 - z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y = y + s2 - z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z += xy * 0.57735026f;
			return;
		}
		case FastNoiseLite.TransformType3D.ImproveXZPlanes:
		{
			float xz = x + z;
			float s3 = xz * -0.21132487f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= 0.57735026f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x += s3 - y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z += s3 - y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y += xz * 0.57735026f;
			return;
		}
		case FastNoiseLite.TransformType3D.DefaultOpenSimplex2:
		{
			float r = (x + y + z) * 0.6666667f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x = r - x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y = r - y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z = r - z;
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002FD4 File Offset: 0x000011D4
	private void UpdateWarpTransformType3D()
	{
		FastNoiseLite.RotationType3D rotationType3D = this.mRotationType3D;
		if (rotationType3D == FastNoiseLite.RotationType3D.ImproveXYPlanes)
		{
			this.mWarpTransformType3D = FastNoiseLite.TransformType3D.ImproveXYPlanes;
			return;
		}
		if (rotationType3D == FastNoiseLite.RotationType3D.ImproveXZPlanes)
		{
			this.mWarpTransformType3D = FastNoiseLite.TransformType3D.ImproveXZPlanes;
			return;
		}
		FastNoiseLite.DomainWarpType domainWarpType = this.mDomainWarpType;
		if (domainWarpType <= FastNoiseLite.DomainWarpType.OpenSimplex2Reduced)
		{
			this.mWarpTransformType3D = FastNoiseLite.TransformType3D.DefaultOpenSimplex2;
			return;
		}
		this.mWarpTransformType3D = FastNoiseLite.TransformType3D.None;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000301C File Offset: 0x0000121C
	private float GenFractalFBm(float x, float y)
	{
		int seed = this.mSeed;
		float sum = 0f;
		float amp = this.mFractalBounding;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float noise = this.GenNoiseSingle(seed++, x, y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sum += noise * amp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= FastNoiseLite.Lerp(1f, FastNoiseLite.FastMin(noise + 1f, 2f) * 0.5f, this.mWeightedStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
		}
		return sum;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000030CC File Offset: 0x000012CC
	private float GenFractalFBm(float x, float y, float z)
	{
		int seed = this.mSeed;
		float sum = 0f;
		float amp = this.mFractalBounding;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float noise = this.GenNoiseSingle(seed++, x, y, z);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sum += noise * amp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= FastNoiseLite.Lerp(1f, (noise + 1f) * 0.5f, this.mWeightedStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
		}
		return sum;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003184 File Offset: 0x00001384
	private float GenFractalRidged(float x, float y)
	{
		int seed = this.mSeed;
		float sum = 0f;
		float amp = this.mFractalBounding;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float noise = FastNoiseLite.FastAbs(this.GenNoiseSingle(seed++, x, y));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sum += (noise * -2f + 1f) * amp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= FastNoiseLite.Lerp(1f, 1f - noise, this.mWeightedStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
		}
		return sum;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003234 File Offset: 0x00001434
	private float GenFractalRidged(float x, float y, float z)
	{
		int seed = this.mSeed;
		float sum = 0f;
		float amp = this.mFractalBounding;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float noise = FastNoiseLite.FastAbs(this.GenNoiseSingle(seed++, x, y, z));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sum += (noise * -2f + 1f) * amp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= FastNoiseLite.Lerp(1f, 1f - noise, this.mWeightedStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
		}
		return sum;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000032F8 File Offset: 0x000014F8
	private float GenFractalPingPong(float x, float y)
	{
		int seed = this.mSeed;
		float sum = 0f;
		float amp = this.mFractalBounding;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float noise = FastNoiseLite.PingPong((this.GenNoiseSingle(seed++, x, y) + 1f) * this.mPingPongStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sum += (noise - 0.5f) * 2f * amp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= FastNoiseLite.Lerp(1f, noise, this.mWeightedStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
		}
		return sum;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000033B0 File Offset: 0x000015B0
	private float GenFractalPingPong(float x, float y, float z)
	{
		int seed = this.mSeed;
		float sum = 0f;
		float amp = this.mFractalBounding;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float noise = FastNoiseLite.PingPong((this.GenNoiseSingle(seed++, x, y, z) + 1f) * this.mPingPongStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sum += (noise - 0.5f) * 2f * amp;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= FastNoiseLite.Lerp(1f, noise, this.mWeightedStrength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z *= this.mLacunarity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
		}
		return sum;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003478 File Offset: 0x00001678
	private float SingleSimplex(int seed, float x, float y)
	{
		int i = FastNoiseLite.FastFloor(x);
		int j = FastNoiseLite.FastFloor(y);
		float num = x - (float)i;
		float yi = y - (float)j;
		float t = (num + yi) * 0.21132487f;
		float x2 = num - t;
		float y2 = yi - t;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		i *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		j *= 1136930381;
		float a = 0.5f - x2 * x2 - y2 * y2;
		float n0;
		if (a <= 0f)
		{
			n0 = 0f;
		}
		else
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			n0 = a * a * (a * a) * FastNoiseLite.GradCoord(seed, i, j, x2, y2);
		}
		float c = 3.1547005f * t + (-0.6666666f + a);
		float n;
		if (c <= 0f)
		{
			n = 0f;
		}
		else
		{
			float x3 = x2 + -0.57735026f;
			float y3 = y2 + -0.57735026f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			n = c * c * (c * c) * FastNoiseLite.GradCoord(seed, i + 501125321, j + 1136930381, x3, y3);
		}
		float n2;
		if (y2 > x2)
		{
			float x4 = x2 + 0.21132487f;
			float y4 = y2 + -0.7886751f;
			float b = 0.5f - x4 * x4 - y4 * y4;
			if (b <= 0f)
			{
				n2 = 0f;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				n2 = b * b * (b * b) * FastNoiseLite.GradCoord(seed, i, j + 1136930381, x4, y4);
			}
		}
		else
		{
			float x5 = x2 + -0.7886751f;
			float y5 = y2 + 0.21132487f;
			float b2 = 0.5f - x5 * x5 - y5 * y5;
			if (b2 <= 0f)
			{
				n2 = 0f;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				n2 = b2 * b2 * (b2 * b2) * FastNoiseLite.GradCoord(seed, i + 501125321, j, x5, y5);
			}
		}
		return (n0 + n2 + n) * 99.83685f;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x0000364C File Offset: 0x0000184C
	private float SingleOpenSimplex2(int seed, float x, float y, float z)
	{
		int i = FastNoiseLite.FastRound(x);
		int j = FastNoiseLite.FastRound(y);
		int k = FastNoiseLite.FastRound(z);
		float x2 = x - (float)i;
		float y2 = y - (float)j;
		float z2 = z - (float)k;
		int xNSign = (int)(-1f - x2) | 1;
		int yNSign = (int)(-1f - y2) | 1;
		int zNSign = (int)(-1f - z2) | 1;
		float ax0 = (float)xNSign * -x2;
		float ay0 = (float)yNSign * -y2;
		float az0 = (float)zNSign * -z2;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		i *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		j *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		k *= 1720413743;
		float value = 0f;
		float a = 0.6f - x2 * x2 - (y2 * y2 + z2 * z2);
		int l = 0;
		for (;;)
		{
			if (a > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += a * a * (a * a) * FastNoiseLite.GradCoord(seed, i, j, k, x2, y2, z2);
			}
			if (ax0 >= ay0 && ax0 >= az0)
			{
				float b = a + ax0 + ax0;
				if (b > 1f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					b -= 1f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += b * b * (b * b) * FastNoiseLite.GradCoord(seed, i - xNSign * 501125321, j, k, x2 + (float)xNSign, y2, z2);
				}
			}
			else if (ay0 > ax0 && ay0 >= az0)
			{
				float b2 = a + ay0 + ay0;
				if (b2 > 1f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					b2 -= 1f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += b2 * b2 * (b2 * b2) * FastNoiseLite.GradCoord(seed, i, j - yNSign * 1136930381, k, x2, y2 + (float)yNSign, z2);
				}
			}
			else
			{
				float b3 = a + az0 + az0;
				if (b3 > 1f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					b3 -= 1f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += b3 * b3 * (b3 * b3) * FastNoiseLite.GradCoord(seed, i, j, k - zNSign * 1720413743, x2, y2, z2 + (float)zNSign);
				}
			}
			if (l == 1)
			{
				break;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ax0 = 0.5f - ax0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ay0 = 0.5f - ay0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			az0 = 0.5f - az0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x2 = (float)xNSign * ax0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y2 = (float)yNSign * ay0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z2 = (float)zNSign * az0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			a += 0.75f - ax0 - (ay0 + az0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			i += (xNSign >> 1 & 501125321);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			j += (yNSign >> 1 & 1136930381);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			k += (zNSign >> 1 & 1720413743);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			xNSign = -xNSign;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			yNSign = -yNSign;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			zNSign = -zNSign;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			seed = ~seed;
			l++;
		}
		return value * 32.694283f;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000393C File Offset: 0x00001B3C
	private float SingleOpenSimplex2S(int seed, float x, float y)
	{
		int i = FastNoiseLite.FastFloor(x);
		int j = FastNoiseLite.FastFloor(y);
		float xi = x - (float)i;
		float yi = y - (float)j;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		i *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		j *= 1136930381;
		int i2 = i + 501125321;
		int j2 = j + 1136930381;
		float t = (xi + yi) * 0.21132487f;
		float x2 = xi - t;
		float y2 = yi - t;
		float a0 = 0.6666667f - x2 * x2 - y2 * y2;
		float value = a0 * a0 * (a0 * a0) * FastNoiseLite.GradCoord(seed, i, j, x2, y2);
		float a = 3.1547005f * t + (-0.6666666f + a0);
		float x3 = x2 - 0.57735026f;
		float y3 = y2 - 0.57735026f;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		value += a * a * (a * a) * FastNoiseLite.GradCoord(seed, i2, j2, x3, y3);
		float xmyi = xi - yi;
		if (t > 0.21132487f)
		{
			if (xi + xmyi > 1f)
			{
				float x4 = x2 + -1.3660254f;
				float y4 = y2 + -0.3660254f;
				float a2 = 0.6666667f - x4 * x4 - y4 * y4;
				if (a2 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a2 * a2 * (a2 * a2) * FastNoiseLite.GradCoord(seed, i + 1002250642, j + 1136930381, x4, y4);
				}
			}
			else
			{
				float x5 = x2 + 0.21132487f;
				float y5 = y2 + -0.7886751f;
				float a3 = 0.6666667f - x5 * x5 - y5 * y5;
				if (a3 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a3 * a3 * (a3 * a3) * FastNoiseLite.GradCoord(seed, i, j + 1136930381, x5, y5);
				}
			}
			if (yi - xmyi > 1f)
			{
				float x6 = x2 + -0.3660254f;
				float y6 = y2 + -1.3660254f;
				float a4 = 0.6666667f - x6 * x6 - y6 * y6;
				if (a4 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a4 * a4 * (a4 * a4) * FastNoiseLite.GradCoord(seed, i + 501125321, j + -2021106534, x6, y6);
				}
			}
			else
			{
				float x7 = x2 + -0.7886751f;
				float y7 = y2 + 0.21132487f;
				float a5 = 0.6666667f - x7 * x7 - y7 * y7;
				if (a5 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a5 * a5 * (a5 * a5) * FastNoiseLite.GradCoord(seed, i + 501125321, j, x7, y7);
				}
			}
		}
		else
		{
			if (xi + xmyi < 0f)
			{
				float x8 = x2 + 0.7886751f;
				float y8 = y2 - 0.21132487f;
				float a6 = 0.6666667f - x8 * x8 - y8 * y8;
				if (a6 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a6 * a6 * (a6 * a6) * FastNoiseLite.GradCoord(seed, i - 501125321, j, x8, y8);
				}
			}
			else
			{
				float x9 = x2 + -0.7886751f;
				float y9 = y2 + 0.21132487f;
				float a7 = 0.6666667f - x9 * x9 - y9 * y9;
				if (a7 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a7 * a7 * (a7 * a7) * FastNoiseLite.GradCoord(seed, i + 501125321, j, x9, y9);
				}
			}
			if (yi < xmyi)
			{
				float x10 = x2 - 0.21132487f;
				float y10 = y2 - -0.7886751f;
				float a8 = 0.6666667f - x10 * x10 - y10 * y10;
				if (a8 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a8 * a8 * (a8 * a8) * FastNoiseLite.GradCoord(seed, i, j - 1136930381, x10, y10);
				}
			}
			else
			{
				float x11 = x2 + 0.21132487f;
				float y11 = y2 + -0.7886751f;
				float a9 = 0.6666667f - x11 * x11 - y11 * y11;
				if (a9 > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					value += a9 * a9 * (a9 * a9) * FastNoiseLite.GradCoord(seed, i, j + 1136930381, x11, y11);
				}
			}
		}
		return value * 18.241962f;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003D48 File Offset: 0x00001F48
	private float SingleOpenSimplex2S(int seed, float x, float y, float z)
	{
		int i = FastNoiseLite.FastFloor(x);
		int j = FastNoiseLite.FastFloor(y);
		int k = FastNoiseLite.FastFloor(z);
		float xi = x - (float)i;
		float yi = y - (float)j;
		float zi = z - (float)k;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		i *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		j *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		k *= 1720413743;
		int seed2 = seed + 1293373;
		int xNMask = (int)(-0.5f - xi);
		int yNMask = (int)(-0.5f - yi);
		int zNMask = (int)(-0.5f - zi);
		float x2 = xi + (float)xNMask;
		float y2 = yi + (float)yNMask;
		float z2 = zi + (float)zNMask;
		float a0 = 0.75f - x2 * x2 - y2 * y2 - z2 * z2;
		float value = a0 * a0 * (a0 * a0) * FastNoiseLite.GradCoord(seed, i + (xNMask & 501125321), j + (yNMask & 1136930381), k + (zNMask & 1720413743), x2, y2, z2);
		float x3 = xi - 0.5f;
		float y3 = yi - 0.5f;
		float z3 = zi - 0.5f;
		float a = 0.75f - x3 * x3 - y3 * y3 - z3 * z3;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		value += a * a * (a * a) * FastNoiseLite.GradCoord(seed2, i + 501125321, j + 1136930381, k + 1720413743, x3, y3, z3);
		float xAFlipMask0 = (float)((xNMask | 1) << 1) * x3;
		float yAFlipMask0 = (float)((yNMask | 1) << 1) * y3;
		float zAFlipMask0 = (float)((zNMask | 1) << 1) * z3;
		float xAFlipMask = (float)(-2 - (xNMask << 2)) * x3 - 1f;
		float yAFlipMask = (float)(-2 - (yNMask << 2)) * y3 - 1f;
		float zAFlipMask = (float)(-2 - (zNMask << 2)) * z3 - 1f;
		bool skip5 = false;
		float a2 = xAFlipMask0 + a0;
		if (a2 > 0f)
		{
			float x4 = x2 - (float)(xNMask | 1);
			float y4 = y2;
			float z4 = z2;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			value += a2 * a2 * (a2 * a2) * FastNoiseLite.GradCoord(seed, i + (~xNMask & 501125321), j + (yNMask & 1136930381), k + (zNMask & 1720413743), x4, y4, z4);
		}
		else
		{
			float a3 = yAFlipMask0 + zAFlipMask0 + a0;
			if (a3 > 0f)
			{
				float x5 = x2;
				float y5 = y2 - (float)(yNMask | 1);
				float z5 = z2 - (float)(zNMask | 1);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += a3 * a3 * (a3 * a3) * FastNoiseLite.GradCoord(seed, i + (xNMask & 501125321), j + (~yNMask & 1136930381), k + (~zNMask & 1720413743), x5, y5, z5);
			}
			float a4 = xAFlipMask + a;
			if (a4 > 0f)
			{
				float x6 = (float)(xNMask | 1) + x3;
				float y6 = y3;
				float z6 = z3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += a4 * a4 * (a4 * a4) * FastNoiseLite.GradCoord(seed2, i + (xNMask & 1002250642), j + 1136930381, k + 1720413743, x6, y6, z6);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				skip5 = true;
			}
		}
		bool skip6 = false;
		float a5 = yAFlipMask0 + a0;
		if (a5 > 0f)
		{
			float x7 = x2;
			float y7 = y2 - (float)(yNMask | 1);
			float z7 = z2;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			value += a5 * a5 * (a5 * a5) * FastNoiseLite.GradCoord(seed, i + (xNMask & 501125321), j + (~yNMask & 1136930381), k + (zNMask & 1720413743), x7, y7, z7);
		}
		else
		{
			float a6 = xAFlipMask0 + zAFlipMask0 + a0;
			if (a6 > 0f)
			{
				float x8 = x2 - (float)(xNMask | 1);
				float y8 = y2;
				float z8 = z2 - (float)(zNMask | 1);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += a6 * a6 * (a6 * a6) * FastNoiseLite.GradCoord(seed, i + (~xNMask & 501125321), j + (yNMask & 1136930381), k + (~zNMask & 1720413743), x8, y8, z8);
			}
			float a7 = yAFlipMask + a;
			if (a7 > 0f)
			{
				float x9 = x3;
				float y9 = (float)(yNMask | 1) + y3;
				float z9 = z3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += a7 * a7 * (a7 * a7) * FastNoiseLite.GradCoord(seed2, i + 501125321, j + (yNMask & -2021106534), k + 1720413743, x9, y9, z9);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				skip6 = true;
			}
		}
		bool skipD = false;
		float aA = zAFlipMask0 + a0;
		if (aA > 0f)
		{
			float xA = x2;
			float yA = y2;
			float zA = z2 - (float)(zNMask | 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			value += aA * aA * (aA * aA) * FastNoiseLite.GradCoord(seed, i + (xNMask & 501125321), j + (yNMask & 1136930381), k + (~zNMask & 1720413743), xA, yA, zA);
		}
		else
		{
			float aB = xAFlipMask0 + yAFlipMask0 + a0;
			if (aB > 0f)
			{
				float xB = x2 - (float)(xNMask | 1);
				float yB = y2 - (float)(yNMask | 1);
				float zB = z2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += aB * aB * (aB * aB) * FastNoiseLite.GradCoord(seed, i + (~xNMask & 501125321), j + (~yNMask & 1136930381), k + (zNMask & 1720413743), xB, yB, zB);
			}
			float aC = zAFlipMask + a;
			if (aC > 0f)
			{
				float xC = x3;
				float yC = y3;
				float zC = (float)(zNMask | 1) + z3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += aC * aC * (aC * aC) * FastNoiseLite.GradCoord(seed2, i + 501125321, j + 1136930381, k + (zNMask & -854139810), xC, yC, zC);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				skipD = true;
			}
		}
		if (!skip5)
		{
			float a8 = yAFlipMask + zAFlipMask + a;
			if (a8 > 0f)
			{
				float x10 = x3;
				float y10 = (float)(yNMask | 1) + y3;
				float z10 = (float)(zNMask | 1) + z3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += a8 * a8 * (a8 * a8) * FastNoiseLite.GradCoord(seed2, i + 501125321, j + (yNMask & -2021106534), k + (zNMask & -854139810), x10, y10, z10);
			}
		}
		if (!skip6)
		{
			float a9 = xAFlipMask + zAFlipMask + a;
			if (a9 > 0f)
			{
				float x11 = (float)(xNMask | 1) + x3;
				float y11 = y3;
				float z11 = (float)(zNMask | 1) + z3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += a9 * a9 * (a9 * a9) * FastNoiseLite.GradCoord(seed2, i + (xNMask & 1002250642), j + 1136930381, k + (zNMask & -854139810), x11, y11, z11);
			}
		}
		if (!skipD)
		{
			float aD = xAFlipMask + yAFlipMask + a;
			if (aD > 0f)
			{
				float xD = (float)(xNMask | 1) + x3;
				float yD = (float)(yNMask | 1) + y3;
				float zD = z3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				value += aD * aD * (aD * aD) * FastNoiseLite.GradCoord(seed2, i + (xNMask & 1002250642), j + (yNMask & -2021106534), k + 1720413743, xD, yD, zD);
			}
		}
		return value * 9.046026f;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0000440C File Offset: 0x0000260C
	private float SingleCellular(int seed, float x, float y)
	{
		int xr = FastNoiseLite.FastRound(x);
		int yr = FastNoiseLite.FastRound(y);
		float distance0 = float.MaxValue;
		float distance = float.MaxValue;
		int closestHash = 0;
		float cellularJitter = 0.43701595f * this.mCellularJitterModifier;
		int xPrimed = (xr - 1) * 501125321;
		int yPrimedBase = (yr - 1) * 1136930381;
		switch (this.mCellularDistanceFunction)
		{
		default:
			for (int xi = xr - 1; xi <= xr + 1; xi++)
			{
				int yPrimed = yPrimedBase;
				for (int yi = yr - 1; yi <= yr + 1; yi++)
				{
					int hash = FastNoiseLite.Hash(seed, xPrimed, yPrimed);
					int idx = hash & 510;
					float num = (float)xi - x + FastNoiseLite.RandVecs2D[idx] * cellularJitter;
					float vecY = (float)yi - y + FastNoiseLite.RandVecs2D[idx | 1] * cellularJitter;
					float newDistance = num * num + vecY * vecY;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					distance = FastNoiseLite.FastMax(FastNoiseLite.FastMin(distance, newDistance), distance0);
					if (newDistance < distance0)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						distance0 = newDistance;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						closestHash = hash;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					yPrimed += 1136930381;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xPrimed += 501125321;
			}
			break;
		case FastNoiseLite.CellularDistanceFunction.Manhattan:
			for (int xi2 = xr - 1; xi2 <= xr + 1; xi2++)
			{
				int yPrimed2 = yPrimedBase;
				for (int yi2 = yr - 1; yi2 <= yr + 1; yi2++)
				{
					int hash2 = FastNoiseLite.Hash(seed, xPrimed, yPrimed2);
					int idx2 = hash2 & 510;
					float f = (float)xi2 - x + FastNoiseLite.RandVecs2D[idx2] * cellularJitter;
					float vecY2 = (float)yi2 - y + FastNoiseLite.RandVecs2D[idx2 | 1] * cellularJitter;
					float newDistance2 = FastNoiseLite.FastAbs(f) + FastNoiseLite.FastAbs(vecY2);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					distance = FastNoiseLite.FastMax(FastNoiseLite.FastMin(distance, newDistance2), distance0);
					if (newDistance2 < distance0)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						distance0 = newDistance2;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						closestHash = hash2;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					yPrimed2 += 1136930381;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xPrimed += 501125321;
			}
			break;
		case FastNoiseLite.CellularDistanceFunction.Hybrid:
			for (int xi3 = xr - 1; xi3 <= xr + 1; xi3++)
			{
				int yPrimed3 = yPrimedBase;
				for (int yi3 = yr - 1; yi3 <= yr + 1; yi3++)
				{
					int hash3 = FastNoiseLite.Hash(seed, xPrimed, yPrimed3);
					int idx3 = hash3 & 510;
					float vecX = (float)xi3 - x + FastNoiseLite.RandVecs2D[idx3] * cellularJitter;
					float vecY3 = (float)yi3 - y + FastNoiseLite.RandVecs2D[idx3 | 1] * cellularJitter;
					float newDistance3 = FastNoiseLite.FastAbs(vecX) + FastNoiseLite.FastAbs(vecY3) + (vecX * vecX + vecY3 * vecY3);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					distance = FastNoiseLite.FastMax(FastNoiseLite.FastMin(distance, newDistance3), distance0);
					if (newDistance3 < distance0)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						distance0 = newDistance3;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						closestHash = hash3;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					yPrimed3 += 1136930381;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xPrimed += 501125321;
			}
			break;
		}
		if (this.mCellularDistanceFunction == FastNoiseLite.CellularDistanceFunction.Euclidean && this.mCellularReturnType >= FastNoiseLite.CellularReturnType.Distance)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			distance0 = FastNoiseLite.FastSqrt(distance0);
			if (this.mCellularReturnType >= FastNoiseLite.CellularReturnType.Distance2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				distance = FastNoiseLite.FastSqrt(distance);
			}
		}
		switch (this.mCellularReturnType)
		{
		case FastNoiseLite.CellularReturnType.CellValue:
			return (float)closestHash * 4.656613E-10f;
		case FastNoiseLite.CellularReturnType.Distance:
			return distance0 - 1f;
		case FastNoiseLite.CellularReturnType.Distance2:
			return distance - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Add:
			return (distance + distance0) * 0.5f - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Sub:
			return distance - distance0 - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Mul:
			return distance * distance0 * 0.5f - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Div:
			return distance0 / distance - 1f;
		default:
			return 0f;
		}
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000047A8 File Offset: 0x000029A8
	private float SingleCellular(int seed, float x, float y, float z)
	{
		int xr = FastNoiseLite.FastRound(x);
		int yr = FastNoiseLite.FastRound(y);
		int zr = FastNoiseLite.FastRound(z);
		float distance0 = float.MaxValue;
		float distance = float.MaxValue;
		int closestHash = 0;
		float cellularJitter = 0.39614353f * this.mCellularJitterModifier;
		int xPrimed = (xr - 1) * 501125321;
		int yPrimedBase = (yr - 1) * 1136930381;
		int zPrimedBase = (zr - 1) * 1720413743;
		switch (this.mCellularDistanceFunction)
		{
		case FastNoiseLite.CellularDistanceFunction.Euclidean:
		case FastNoiseLite.CellularDistanceFunction.EuclideanSq:
			for (int xi = xr - 1; xi <= xr + 1; xi++)
			{
				int yPrimed = yPrimedBase;
				for (int yi = yr - 1; yi <= yr + 1; yi++)
				{
					int zPrimed = zPrimedBase;
					for (int zi = zr - 1; zi <= zr + 1; zi++)
					{
						int hash = FastNoiseLite.Hash(seed, xPrimed, yPrimed, zPrimed);
						int idx = hash & 1020;
						float num = (float)xi - x + FastNoiseLite.RandVecs3D[idx] * cellularJitter;
						float vecY = (float)yi - y + FastNoiseLite.RandVecs3D[idx | 1] * cellularJitter;
						float vecZ = (float)zi - z + FastNoiseLite.RandVecs3D[idx | 2] * cellularJitter;
						float newDistance = num * num + vecY * vecY + vecZ * vecZ;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						distance = FastNoiseLite.FastMax(FastNoiseLite.FastMin(distance, newDistance), distance0);
						if (newDistance < distance0)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							distance0 = newDistance;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							closestHash = hash;
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						zPrimed += 1720413743;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					yPrimed += 1136930381;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xPrimed += 501125321;
			}
			break;
		case FastNoiseLite.CellularDistanceFunction.Manhattan:
			for (int xi2 = xr - 1; xi2 <= xr + 1; xi2++)
			{
				int yPrimed2 = yPrimedBase;
				for (int yi2 = yr - 1; yi2 <= yr + 1; yi2++)
				{
					int zPrimed2 = zPrimedBase;
					for (int zi2 = zr - 1; zi2 <= zr + 1; zi2++)
					{
						int hash2 = FastNoiseLite.Hash(seed, xPrimed, yPrimed2, zPrimed2);
						int idx2 = hash2 & 1020;
						float f = (float)xi2 - x + FastNoiseLite.RandVecs3D[idx2] * cellularJitter;
						float vecY2 = (float)yi2 - y + FastNoiseLite.RandVecs3D[idx2 | 1] * cellularJitter;
						float vecZ2 = (float)zi2 - z + FastNoiseLite.RandVecs3D[idx2 | 2] * cellularJitter;
						float newDistance2 = FastNoiseLite.FastAbs(f) + FastNoiseLite.FastAbs(vecY2) + FastNoiseLite.FastAbs(vecZ2);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						distance = FastNoiseLite.FastMax(FastNoiseLite.FastMin(distance, newDistance2), distance0);
						if (newDistance2 < distance0)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							distance0 = newDistance2;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							closestHash = hash2;
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						zPrimed2 += 1720413743;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					yPrimed2 += 1136930381;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xPrimed += 501125321;
			}
			break;
		case FastNoiseLite.CellularDistanceFunction.Hybrid:
			for (int xi3 = xr - 1; xi3 <= xr + 1; xi3++)
			{
				int yPrimed3 = yPrimedBase;
				for (int yi3 = yr - 1; yi3 <= yr + 1; yi3++)
				{
					int zPrimed3 = zPrimedBase;
					for (int zi3 = zr - 1; zi3 <= zr + 1; zi3++)
					{
						int hash3 = FastNoiseLite.Hash(seed, xPrimed, yPrimed3, zPrimed3);
						int idx3 = hash3 & 1020;
						float vecX = (float)xi3 - x + FastNoiseLite.RandVecs3D[idx3] * cellularJitter;
						float vecY3 = (float)yi3 - y + FastNoiseLite.RandVecs3D[idx3 | 1] * cellularJitter;
						float vecZ3 = (float)zi3 - z + FastNoiseLite.RandVecs3D[idx3 | 2] * cellularJitter;
						float newDistance3 = FastNoiseLite.FastAbs(vecX) + FastNoiseLite.FastAbs(vecY3) + FastNoiseLite.FastAbs(vecZ3) + (vecX * vecX + vecY3 * vecY3 + vecZ3 * vecZ3);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						distance = FastNoiseLite.FastMax(FastNoiseLite.FastMin(distance, newDistance3), distance0);
						if (newDistance3 < distance0)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							distance0 = newDistance3;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							closestHash = hash3;
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						zPrimed3 += 1720413743;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					yPrimed3 += 1136930381;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xPrimed += 501125321;
			}
			break;
		}
		if (this.mCellularDistanceFunction == FastNoiseLite.CellularDistanceFunction.Euclidean && this.mCellularReturnType >= FastNoiseLite.CellularReturnType.Distance)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			distance0 = FastNoiseLite.FastSqrt(distance0);
			if (this.mCellularReturnType >= FastNoiseLite.CellularReturnType.Distance2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				distance = FastNoiseLite.FastSqrt(distance);
			}
		}
		switch (this.mCellularReturnType)
		{
		case FastNoiseLite.CellularReturnType.CellValue:
			return (float)closestHash * 4.656613E-10f;
		case FastNoiseLite.CellularReturnType.Distance:
			return distance0 - 1f;
		case FastNoiseLite.CellularReturnType.Distance2:
			return distance - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Add:
			return (distance + distance0) * 0.5f - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Sub:
			return distance - distance0 - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Mul:
			return distance * distance0 * 0.5f - 1f;
		case FastNoiseLite.CellularReturnType.Distance2Div:
			return distance0 / distance - 1f;
		default:
			return 0f;
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00004C58 File Offset: 0x00002E58
	private float SinglePerlin(int seed, float x, float y)
	{
		int x2 = FastNoiseLite.FastFloor(x);
		int y2 = FastNoiseLite.FastFloor(y);
		float xd0 = x - (float)x2;
		float yd0 = y - (float)y2;
		float xd = xd0 - 1f;
		float yd = yd0 - 1f;
		float xs = FastNoiseLite.InterpQuintic(xd0);
		float ys = FastNoiseLite.InterpQuintic(yd0);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		int x3 = x2 + 501125321;
		int y3 = y2 + 1136930381;
		float a = FastNoiseLite.Lerp(FastNoiseLite.GradCoord(seed, x2, y2, xd0, yd0), FastNoiseLite.GradCoord(seed, x3, y2, xd, yd0), xs);
		float xf = FastNoiseLite.Lerp(FastNoiseLite.GradCoord(seed, x2, y3, xd0, yd), FastNoiseLite.GradCoord(seed, x3, y3, xd, yd), xs);
		return FastNoiseLite.Lerp(a, xf, ys) * 1.4247692f;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00004D1C File Offset: 0x00002F1C
	private float SinglePerlin(int seed, float x, float y, float z)
	{
		int x2 = FastNoiseLite.FastFloor(x);
		int y2 = FastNoiseLite.FastFloor(y);
		int z2 = FastNoiseLite.FastFloor(z);
		float xd0 = x - (float)x2;
		float yd0 = y - (float)y2;
		float zd0 = z - (float)z2;
		float xd = xd0 - 1f;
		float yd = yd0 - 1f;
		float zd = zd0 - 1f;
		float xs = FastNoiseLite.InterpQuintic(xd0);
		float ys = FastNoiseLite.InterpQuintic(yd0);
		float zs = FastNoiseLite.InterpQuintic(zd0);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		z2 *= 1720413743;
		int x3 = x2 + 501125321;
		int y3 = y2 + 1136930381;
		int z3 = z2 + 1720413743;
		float a = FastNoiseLite.Lerp(FastNoiseLite.GradCoord(seed, x2, y2, z2, xd0, yd0, zd0), FastNoiseLite.GradCoord(seed, x3, y2, z2, xd, yd0, zd0), xs);
		float xf10 = FastNoiseLite.Lerp(FastNoiseLite.GradCoord(seed, x2, y3, z2, xd0, yd, zd0), FastNoiseLite.GradCoord(seed, x3, y3, z2, xd, yd, zd0), xs);
		float xf11 = FastNoiseLite.Lerp(FastNoiseLite.GradCoord(seed, x2, y2, z3, xd0, yd0, zd), FastNoiseLite.GradCoord(seed, x3, y2, z3, xd, yd0, zd), xs);
		float xf12 = FastNoiseLite.Lerp(FastNoiseLite.GradCoord(seed, x2, y3, z3, xd0, yd, zd), FastNoiseLite.GradCoord(seed, x3, y3, z3, xd, yd, zd), xs);
		float a2 = FastNoiseLite.Lerp(a, xf10, ys);
		float yf = FastNoiseLite.Lerp(xf11, xf12, ys);
		return FastNoiseLite.Lerp(a2, yf, zs) * 0.9649214f;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004E94 File Offset: 0x00003094
	private float SingleValueCubic(int seed, float x, float y)
	{
		int x2 = FastNoiseLite.FastFloor(x);
		int y2 = FastNoiseLite.FastFloor(y);
		float xs = x - (float)x2;
		float ys = y - (float)y2;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		int x3 = x2 - 501125321;
		int y3 = y2 - 1136930381;
		int x4 = x2 + 501125321;
		int y4 = y2 + 1136930381;
		int x5 = x2 + 1002250642;
		int y5 = y2 + -2021106534;
		return FastNoiseLite.CubicLerp(FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y3), FastNoiseLite.ValCoord(seed, x2, y3), FastNoiseLite.ValCoord(seed, x4, y3), FastNoiseLite.ValCoord(seed, x5, y3), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y2), FastNoiseLite.ValCoord(seed, x2, y2), FastNoiseLite.ValCoord(seed, x4, y2), FastNoiseLite.ValCoord(seed, x5, y2), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y4), FastNoiseLite.ValCoord(seed, x2, y4), FastNoiseLite.ValCoord(seed, x4, y4), FastNoiseLite.ValCoord(seed, x5, y4), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y5), FastNoiseLite.ValCoord(seed, x2, y5), FastNoiseLite.ValCoord(seed, x4, y5), FastNoiseLite.ValCoord(seed, x5, y5), xs), ys) * 0.44444445f;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00004FC8 File Offset: 0x000031C8
	private float SingleValueCubic(int seed, float x, float y, float z)
	{
		int x2 = FastNoiseLite.FastFloor(x);
		int y2 = FastNoiseLite.FastFloor(y);
		int z2 = FastNoiseLite.FastFloor(z);
		float xs = x - (float)x2;
		float ys = y - (float)y2;
		float zs = z - (float)z2;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		z2 *= 1720413743;
		int x3 = x2 - 501125321;
		int y3 = y2 - 1136930381;
		int z3 = z2 - 1720413743;
		int x4 = x2 + 501125321;
		int y4 = y2 + 1136930381;
		int z4 = z2 + 1720413743;
		int x5 = x2 + 1002250642;
		int y5 = y2 + -2021106534;
		int z5 = z2 + -854139810;
		return FastNoiseLite.CubicLerp(FastNoiseLite.CubicLerp(FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y3, z3), FastNoiseLite.ValCoord(seed, x2, y3, z3), FastNoiseLite.ValCoord(seed, x4, y3, z3), FastNoiseLite.ValCoord(seed, x5, y3, z3), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y2, z3), FastNoiseLite.ValCoord(seed, x2, y2, z3), FastNoiseLite.ValCoord(seed, x4, y2, z3), FastNoiseLite.ValCoord(seed, x5, y2, z3), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y4, z3), FastNoiseLite.ValCoord(seed, x2, y4, z3), FastNoiseLite.ValCoord(seed, x4, y4, z3), FastNoiseLite.ValCoord(seed, x5, y4, z3), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y5, z3), FastNoiseLite.ValCoord(seed, x2, y5, z3), FastNoiseLite.ValCoord(seed, x4, y5, z3), FastNoiseLite.ValCoord(seed, x5, y5, z3), xs), ys), FastNoiseLite.CubicLerp(FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y3, z2), FastNoiseLite.ValCoord(seed, x2, y3, z2), FastNoiseLite.ValCoord(seed, x4, y3, z2), FastNoiseLite.ValCoord(seed, x5, y3, z2), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y2, z2), FastNoiseLite.ValCoord(seed, x2, y2, z2), FastNoiseLite.ValCoord(seed, x4, y2, z2), FastNoiseLite.ValCoord(seed, x5, y2, z2), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y4, z2), FastNoiseLite.ValCoord(seed, x2, y4, z2), FastNoiseLite.ValCoord(seed, x4, y4, z2), FastNoiseLite.ValCoord(seed, x5, y4, z2), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y5, z2), FastNoiseLite.ValCoord(seed, x2, y5, z2), FastNoiseLite.ValCoord(seed, x4, y5, z2), FastNoiseLite.ValCoord(seed, x5, y5, z2), xs), ys), FastNoiseLite.CubicLerp(FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y3, z4), FastNoiseLite.ValCoord(seed, x2, y3, z4), FastNoiseLite.ValCoord(seed, x4, y3, z4), FastNoiseLite.ValCoord(seed, x5, y3, z4), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y2, z4), FastNoiseLite.ValCoord(seed, x2, y2, z4), FastNoiseLite.ValCoord(seed, x4, y2, z4), FastNoiseLite.ValCoord(seed, x5, y2, z4), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y4, z4), FastNoiseLite.ValCoord(seed, x2, y4, z4), FastNoiseLite.ValCoord(seed, x4, y4, z4), FastNoiseLite.ValCoord(seed, x5, y4, z4), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y5, z4), FastNoiseLite.ValCoord(seed, x2, y5, z4), FastNoiseLite.ValCoord(seed, x4, y5, z4), FastNoiseLite.ValCoord(seed, x5, y5, z4), xs), ys), FastNoiseLite.CubicLerp(FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y3, z5), FastNoiseLite.ValCoord(seed, x2, y3, z5), FastNoiseLite.ValCoord(seed, x4, y3, z5), FastNoiseLite.ValCoord(seed, x5, y3, z5), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y2, z5), FastNoiseLite.ValCoord(seed, x2, y2, z5), FastNoiseLite.ValCoord(seed, x4, y2, z5), FastNoiseLite.ValCoord(seed, x5, y2, z5), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y4, z5), FastNoiseLite.ValCoord(seed, x2, y4, z5), FastNoiseLite.ValCoord(seed, x4, y4, z5), FastNoiseLite.ValCoord(seed, x5, y4, z5), xs), FastNoiseLite.CubicLerp(FastNoiseLite.ValCoord(seed, x3, y5, z5), FastNoiseLite.ValCoord(seed, x2, y5, z5), FastNoiseLite.ValCoord(seed, x4, y5, z5), FastNoiseLite.ValCoord(seed, x5, y5, z5), xs), ys), zs) * 0.2962963f;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000053D4 File Offset: 0x000035D4
	private float SingleValue(int seed, float x, float y)
	{
		int x2 = FastNoiseLite.FastFloor(x);
		int y2 = FastNoiseLite.FastFloor(y);
		float xs = FastNoiseLite.InterpHermite(x - (float)x2);
		float ys = FastNoiseLite.InterpHermite(y - (float)y2);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		int x3 = x2 + 501125321;
		int y3 = y2 + 1136930381;
		float a = FastNoiseLite.Lerp(FastNoiseLite.ValCoord(seed, x2, y2), FastNoiseLite.ValCoord(seed, x3, y2), xs);
		float xf = FastNoiseLite.Lerp(FastNoiseLite.ValCoord(seed, x2, y3), FastNoiseLite.ValCoord(seed, x3, y3), xs);
		return FastNoiseLite.Lerp(a, xf, ys);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000546C File Offset: 0x0000366C
	private float SingleValue(int seed, float x, float y, float z)
	{
		int x2 = FastNoiseLite.FastFloor(x);
		int y2 = FastNoiseLite.FastFloor(y);
		int z2 = FastNoiseLite.FastFloor(z);
		float xs = FastNoiseLite.InterpHermite(x - (float)x2);
		float ys = FastNoiseLite.InterpHermite(y - (float)y2);
		float zs = FastNoiseLite.InterpHermite(z - (float)z2);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		z2 *= 1720413743;
		int x3 = x2 + 501125321;
		int y3 = y2 + 1136930381;
		int z3 = z2 + 1720413743;
		float a = FastNoiseLite.Lerp(FastNoiseLite.ValCoord(seed, x2, y2, z2), FastNoiseLite.ValCoord(seed, x3, y2, z2), xs);
		float xf10 = FastNoiseLite.Lerp(FastNoiseLite.ValCoord(seed, x2, y3, z2), FastNoiseLite.ValCoord(seed, x3, y3, z2), xs);
		float xf11 = FastNoiseLite.Lerp(FastNoiseLite.ValCoord(seed, x2, y2, z3), FastNoiseLite.ValCoord(seed, x3, y2, z3), xs);
		float xf12 = FastNoiseLite.Lerp(FastNoiseLite.ValCoord(seed, x2, y3, z3), FastNoiseLite.ValCoord(seed, x3, y3, z3), xs);
		float a2 = FastNoiseLite.Lerp(a, xf10, ys);
		float yf = FastNoiseLite.Lerp(xf11, xf12, ys);
		return FastNoiseLite.Lerp(a2, yf, zs);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005588 File Offset: 0x00003788
	private void DoSingleDomainWarp(int seed, float amp, float freq, float x, float y, ref float xr, ref float yr)
	{
		switch (this.mDomainWarpType)
		{
		case FastNoiseLite.DomainWarpType.OpenSimplex2:
			this.SingleDomainWarpSimplexGradient(seed, amp * 38.283688f, freq, x, y, ref xr, ref yr, false);
			return;
		case FastNoiseLite.DomainWarpType.OpenSimplex2Reduced:
			this.SingleDomainWarpSimplexGradient(seed, amp * 16f, freq, x, y, ref xr, ref yr, true);
			return;
		case FastNoiseLite.DomainWarpType.BasicGrid:
			this.SingleDomainWarpBasicGrid(seed, amp, freq, x, y, ref xr, ref yr);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x000055F4 File Offset: 0x000037F4
	private void DoSingleDomainWarp(int seed, float amp, float freq, float x, float y, float z, ref float xr, ref float yr, ref float zr)
	{
		switch (this.mDomainWarpType)
		{
		case FastNoiseLite.DomainWarpType.OpenSimplex2:
			this.SingleDomainWarpOpenSimplex2Gradient(seed, amp * 32.694283f, freq, x, y, z, ref xr, ref yr, ref zr, false);
			return;
		case FastNoiseLite.DomainWarpType.OpenSimplex2Reduced:
			this.SingleDomainWarpOpenSimplex2Gradient(seed, amp * 7.716049f, freq, x, y, z, ref xr, ref yr, ref zr, true);
			return;
		case FastNoiseLite.DomainWarpType.BasicGrid:
			this.SingleDomainWarpBasicGrid(seed, amp, freq, x, y, z, ref xr, ref yr, ref zr);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000566C File Offset: 0x0000386C
	private void DomainWarpSingle(ref float x, ref float y)
	{
		int seed = this.mSeed;
		float amp = this.mDomainWarpAmp * this.mFractalBounding;
		float freq = this.mFrequency;
		float xs = x;
		float ys = y;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.TransformDomainWarpCoordinate(ref xs, ref ys);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000056C0 File Offset: 0x000038C0
	private void DomainWarpSingle(ref float x, ref float y, ref float z)
	{
		int seed = this.mSeed;
		float amp = this.mDomainWarpAmp * this.mFractalBounding;
		float freq = this.mFrequency;
		float xs = x;
		float ys = y;
		float zs = z;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000571C File Offset: 0x0000391C
	private void DomainWarpFractalProgressive(ref float x, ref float y)
	{
		int seed = this.mSeed;
		float amp = this.mDomainWarpAmp * this.mFractalBounding;
		float freq = this.mFrequency;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float xs = x;
			float ys = y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TransformDomainWarpCoordinate(ref xs, ref ys);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			seed++;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			freq *= this.mLacunarity;
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000057A8 File Offset: 0x000039A8
	private void DomainWarpFractalProgressive(ref float x, ref float y, ref float z)
	{
		int seed = this.mSeed;
		float amp = this.mDomainWarpAmp * this.mFractalBounding;
		float freq = this.mFrequency;
		for (int i = 0; i < this.mOctaves; i++)
		{
			float xs = x;
			float ys = y;
			float zs = z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			seed++;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			freq *= this.mLacunarity;
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000583C File Offset: 0x00003A3C
	private void DomainWarpFractalIndependent(ref float x, ref float y)
	{
		float xs = x;
		float ys = y;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.TransformDomainWarpCoordinate(ref xs, ref ys);
		int seed = this.mSeed;
		float amp = this.mDomainWarpAmp * this.mFractalBounding;
		float freq = this.mFrequency;
		for (int i = 0; i < this.mOctaves; i++)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoSingleDomainWarp(seed, amp, freq, xs, ys, ref x, ref y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			seed++;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			freq *= this.mLacunarity;
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000058CC File Offset: 0x00003ACC
	private void DomainWarpFractalIndependent(ref float x, ref float y, ref float z)
	{
		float xs = x;
		float ys = y;
		float zs = z;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.TransformDomainWarpCoordinate(ref xs, ref ys, ref zs);
		int seed = this.mSeed;
		float amp = this.mDomainWarpAmp * this.mFractalBounding;
		float freq = this.mFrequency;
		for (int i = 0; i < this.mOctaves; i++)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoSingleDomainWarp(seed, amp, freq, xs, ys, zs, ref x, ref y, ref z);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			seed++;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			amp *= this.mGain;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			freq *= this.mLacunarity;
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00005968 File Offset: 0x00003B68
	private void SingleDomainWarpBasicGrid(int seed, float warpAmp, float frequency, float x, float y, ref float xr, ref float yr)
	{
		float num = x * frequency;
		float yf = y * frequency;
		int x2 = FastNoiseLite.FastFloor(num);
		int y2 = FastNoiseLite.FastFloor(yf);
		float xs = FastNoiseLite.InterpHermite(num - (float)x2);
		float ys = FastNoiseLite.InterpHermite(yf - (float)y2);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		int x3 = x2 + 501125321;
		int y3 = y2 + 1136930381;
		int hash0 = FastNoiseLite.Hash(seed, x2, y2) & 510;
		int hash = FastNoiseLite.Hash(seed, x3, y2) & 510;
		float lx0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs2D[hash0], FastNoiseLite.RandVecs2D[hash], xs);
		float ly0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs2D[hash0 | 1], FastNoiseLite.RandVecs2D[hash | 1], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash0 = (FastNoiseLite.Hash(seed, x2, y3) & 510);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash = (FastNoiseLite.Hash(seed, x3, y3) & 510);
		float lx1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs2D[hash0], FastNoiseLite.RandVecs2D[hash], xs);
		float ly1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs2D[hash0 | 1], FastNoiseLite.RandVecs2D[hash | 1], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xr += FastNoiseLite.Lerp(lx0x, lx1x, ys) * warpAmp;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yr += FastNoiseLite.Lerp(ly0x, ly1x, ys) * warpAmp;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00005AB8 File Offset: 0x00003CB8
	private void SingleDomainWarpBasicGrid(int seed, float warpAmp, float frequency, float x, float y, float z, ref float xr, ref float yr, ref float zr)
	{
		float num = x * frequency;
		float yf = y * frequency;
		float zf = z * frequency;
		int x2 = FastNoiseLite.FastFloor(num);
		int y2 = FastNoiseLite.FastFloor(yf);
		int z2 = FastNoiseLite.FastFloor(zf);
		float xs = FastNoiseLite.InterpHermite(num - (float)x2);
		float ys = FastNoiseLite.InterpHermite(yf - (float)y2);
		float zs = FastNoiseLite.InterpHermite(zf - (float)z2);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x2 *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y2 *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		z2 *= 1720413743;
		int x3 = x2 + 501125321;
		int y3 = y2 + 1136930381;
		int z3 = z2 + 1720413743;
		int hash0 = FastNoiseLite.Hash(seed, x2, y2, z2) & 1020;
		int hash = FastNoiseLite.Hash(seed, x3, y2, z2) & 1020;
		float lx0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0], FastNoiseLite.RandVecs3D[hash], xs);
		float ly0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 1], FastNoiseLite.RandVecs3D[hash | 1], xs);
		float lz0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 2], FastNoiseLite.RandVecs3D[hash | 2], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash0 = (FastNoiseLite.Hash(seed, x2, y3, z2) & 1020);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash = (FastNoiseLite.Hash(seed, x3, y3, z2) & 1020);
		float lx1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0], FastNoiseLite.RandVecs3D[hash], xs);
		float ly1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 1], FastNoiseLite.RandVecs3D[hash | 1], xs);
		float lz1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 2], FastNoiseLite.RandVecs3D[hash | 2], xs);
		float lx0y = FastNoiseLite.Lerp(lx0x, lx1x, ys);
		float ly0y = FastNoiseLite.Lerp(ly0x, ly1x, ys);
		float lz0y = FastNoiseLite.Lerp(lz0x, lz1x, ys);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash0 = (FastNoiseLite.Hash(seed, x2, y2, z3) & 1020);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash = (FastNoiseLite.Hash(seed, x3, y2, z3) & 1020);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		lx0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0], FastNoiseLite.RandVecs3D[hash], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		ly0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 1], FastNoiseLite.RandVecs3D[hash | 1], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		lz0x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 2], FastNoiseLite.RandVecs3D[hash | 2], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash0 = (FastNoiseLite.Hash(seed, x2, y3, z3) & 1020);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		hash = (FastNoiseLite.Hash(seed, x3, y3, z3) & 1020);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		lx1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0], FastNoiseLite.RandVecs3D[hash], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		ly1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 1], FastNoiseLite.RandVecs3D[hash | 1], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		lz1x = FastNoiseLite.Lerp(FastNoiseLite.RandVecs3D[hash0 | 2], FastNoiseLite.RandVecs3D[hash | 2], xs);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xr += FastNoiseLite.Lerp(lx0y, FastNoiseLite.Lerp(lx0x, lx1x, ys), zs) * warpAmp;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yr += FastNoiseLite.Lerp(ly0y, FastNoiseLite.Lerp(ly0x, ly1x, ys), zs) * warpAmp;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		zr += FastNoiseLite.Lerp(lz0y, FastNoiseLite.Lerp(lz0x, lz1x, ys), zs) * warpAmp;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00005E00 File Offset: 0x00004000
	private void SingleDomainWarpSimplexGradient(int seed, float warpAmp, float frequency, float x, float y, ref float xr, ref float yr, bool outGradOnly)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x *= frequency;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y *= frequency;
		int i = FastNoiseLite.FastFloor(x);
		int j = FastNoiseLite.FastFloor(y);
		float num = x - (float)i;
		float yi = y - (float)j;
		float t = (num + yi) * 0.21132487f;
		float x2 = num - t;
		float y2 = yi - t;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		i *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		j *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		float vx;
		float vy = vx = 0f;
		float a = 0.5f - x2 * x2 - y2 * y2;
		if (a > 0f)
		{
			float aaaa = a * a * (a * a);
			float xo;
			float yo;
			if (outGradOnly)
			{
				FastNoiseLite.GradCoordOut(seed, i, j, out xo, out yo);
			}
			else
			{
				FastNoiseLite.GradCoordDual(seed, i, j, x2, y2, out xo, out yo);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vx += aaaa * xo;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vy += aaaa * yo;
		}
		float c = 3.1547005f * t + (-0.6666666f + a);
		if (c > 0f)
		{
			float x3 = x2 + -0.57735026f;
			float y3 = y2 + -0.57735026f;
			float cccc = c * c * (c * c);
			float xo2;
			float yo2;
			if (outGradOnly)
			{
				FastNoiseLite.GradCoordOut(seed, i + 501125321, j + 1136930381, out xo2, out yo2);
			}
			else
			{
				FastNoiseLite.GradCoordDual(seed, i + 501125321, j + 1136930381, x3, y3, out xo2, out yo2);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vx += cccc * xo2;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vy += cccc * yo2;
		}
		if (y2 > x2)
		{
			float x4 = x2 + 0.21132487f;
			float y4 = y2 + -0.7886751f;
			float b = 0.5f - x4 * x4 - y4 * y4;
			if (b > 0f)
			{
				float bbbb = b * b * (b * b);
				float xo3;
				float yo3;
				if (outGradOnly)
				{
					FastNoiseLite.GradCoordOut(seed, i, j + 1136930381, out xo3, out yo3);
				}
				else
				{
					FastNoiseLite.GradCoordDual(seed, i, j + 1136930381, x4, y4, out xo3, out yo3);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vx += bbbb * xo3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vy += bbbb * yo3;
			}
		}
		else
		{
			float x5 = x2 + -0.7886751f;
			float y5 = y2 + 0.21132487f;
			float b2 = 0.5f - x5 * x5 - y5 * y5;
			if (b2 > 0f)
			{
				float bbbb2 = b2 * b2 * (b2 * b2);
				float xo4;
				float yo4;
				if (outGradOnly)
				{
					FastNoiseLite.GradCoordOut(seed, i + 501125321, j, out xo4, out yo4);
				}
				else
				{
					FastNoiseLite.GradCoordDual(seed, i + 501125321, j, x5, y5, out xo4, out yo4);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vx += bbbb2 * xo4;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vy += bbbb2 * yo4;
			}
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xr += vx * warpAmp;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yr += vy * warpAmp;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000060C0 File Offset: 0x000042C0
	private void SingleDomainWarpOpenSimplex2Gradient(int seed, float warpAmp, float frequency, float x, float y, float z, ref float xr, ref float yr, ref float zr, bool outGradOnly)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		x *= frequency;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		y *= frequency;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		z *= frequency;
		int i = FastNoiseLite.FastRound(x);
		int j = FastNoiseLite.FastRound(y);
		int k = FastNoiseLite.FastRound(z);
		float x2 = x - (float)i;
		float y2 = y - (float)j;
		float z2 = z - (float)k;
		int xNSign = (int)(-x2 - 1f) | 1;
		int yNSign = (int)(-y2 - 1f) | 1;
		int zNSign = (int)(-z2 - 1f) | 1;
		float ax0 = (float)xNSign * -x2;
		float ay0 = (float)yNSign * -y2;
		float az0 = (float)zNSign * -z2;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		i *= 501125321;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		j *= 1136930381;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		k *= 1720413743;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		float vz;
		float vx;
		float vy = vx = (vz = 0f);
		float a = 0.6f - x2 * x2 - (y2 * y2 + z2 * z2);
		int l = 0;
		for (;;)
		{
			if (a > 0f)
			{
				float aaaa = a * a * (a * a);
				float xo;
				float yo;
				float zo;
				if (outGradOnly)
				{
					FastNoiseLite.GradCoordOut(seed, i, j, k, out xo, out yo, out zo);
				}
				else
				{
					FastNoiseLite.GradCoordDual(seed, i, j, k, x2, y2, z2, out xo, out yo, out zo);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vx += aaaa * xo;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vy += aaaa * yo;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vz += aaaa * zo;
			}
			float b = a;
			int i2 = i;
			int j2 = j;
			int k2 = k;
			float x3 = x2;
			float y3 = y2;
			float z3 = z2;
			if (ax0 >= ay0 && ax0 >= az0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				x3 += (float)xNSign;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				b = b + ax0 + ax0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				i2 -= xNSign * 501125321;
			}
			else if (ay0 > ax0 && ay0 >= az0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				y3 += (float)yNSign;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				b = b + ay0 + ay0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				j2 -= yNSign * 1136930381;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				z3 += (float)zNSign;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				b = b + az0 + az0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				k2 -= zNSign * 1720413743;
			}
			if (b > 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				b -= 1f;
				float bbbb = b * b * (b * b);
				float xo2;
				float yo2;
				float zo2;
				if (outGradOnly)
				{
					FastNoiseLite.GradCoordOut(seed, i2, j2, k2, out xo2, out yo2, out zo2);
				}
				else
				{
					FastNoiseLite.GradCoordDual(seed, i2, j2, k2, x3, y3, z3, out xo2, out yo2, out zo2);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vx += bbbb * xo2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vy += bbbb * yo2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vz += bbbb * zo2;
			}
			if (l == 1)
			{
				break;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ax0 = 0.5f - ax0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ay0 = 0.5f - ay0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			az0 = 0.5f - az0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			x2 = (float)xNSign * ax0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			y2 = (float)yNSign * ay0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			z2 = (float)zNSign * az0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			a += 0.75f - ax0 - (ay0 + az0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			i += (xNSign >> 1 & 501125321);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			j += (yNSign >> 1 & 1136930381);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			k += (zNSign >> 1 & 1720413743);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			xNSign = -xNSign;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			yNSign = -yNSign;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			zNSign = -zNSign;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			seed += 1293373;
			l++;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		xr += vx * warpAmp;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		yr += vy * warpAmp;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		zr += vz * warpAmp;
	}

	// Token: 0x0400000C RID: 12
	private const short INLINE = 256;

	// Token: 0x0400000D RID: 13
	private const short OPTIMISE = 512;

	// Token: 0x0400000E RID: 14
	private int mSeed = 1337;

	// Token: 0x0400000F RID: 15
	private float mFrequency = 0.01f;

	// Token: 0x04000010 RID: 16
	private FastNoiseLite.NoiseType mNoiseType;

	// Token: 0x04000011 RID: 17
	private FastNoiseLite.RotationType3D mRotationType3D;

	// Token: 0x04000012 RID: 18
	private FastNoiseLite.TransformType3D mTransformType3D = FastNoiseLite.TransformType3D.DefaultOpenSimplex2;

	// Token: 0x04000013 RID: 19
	private FastNoiseLite.FractalType mFractalType;

	// Token: 0x04000014 RID: 20
	private int mOctaves = 3;

	// Token: 0x04000015 RID: 21
	private float mLacunarity = 2f;

	// Token: 0x04000016 RID: 22
	private float mGain = 0.5f;

	// Token: 0x04000017 RID: 23
	private float mWeightedStrength;

	// Token: 0x04000018 RID: 24
	private float mPingPongStrength = 2f;

	// Token: 0x04000019 RID: 25
	private float mFractalBounding = 0.5714286f;

	// Token: 0x0400001A RID: 26
	private FastNoiseLite.CellularDistanceFunction mCellularDistanceFunction = FastNoiseLite.CellularDistanceFunction.EuclideanSq;

	// Token: 0x0400001B RID: 27
	private FastNoiseLite.CellularReturnType mCellularReturnType = FastNoiseLite.CellularReturnType.Distance;

	// Token: 0x0400001C RID: 28
	private float mCellularJitterModifier = 1f;

	// Token: 0x0400001D RID: 29
	private FastNoiseLite.DomainWarpType mDomainWarpType;

	// Token: 0x0400001E RID: 30
	private FastNoiseLite.TransformType3D mWarpTransformType3D = FastNoiseLite.TransformType3D.DefaultOpenSimplex2;

	// Token: 0x0400001F RID: 31
	private float mDomainWarpAmp = 1f;

	// Token: 0x04000020 RID: 32
	private static readonly float[] Gradients2D = new float[]
	{
		0.13052619f,
		0.9914449f,
		0.38268343f,
		0.9238795f,
		0.6087614f,
		0.7933533f,
		0.7933533f,
		0.6087614f,
		0.9238795f,
		0.38268343f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		-0.13052619f,
		0.9238795f,
		-0.38268343f,
		0.7933533f,
		-0.6087614f,
		0.6087614f,
		-0.7933533f,
		0.38268343f,
		-0.9238795f,
		0.13052619f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		-0.38268343f,
		-0.9238795f,
		-0.6087614f,
		-0.7933533f,
		-0.7933533f,
		-0.6087614f,
		-0.9238795f,
		-0.38268343f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		0.13052619f,
		-0.9238795f,
		0.38268343f,
		-0.7933533f,
		0.6087614f,
		-0.6087614f,
		0.7933533f,
		-0.38268343f,
		0.9238795f,
		-0.13052619f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		0.38268343f,
		0.9238795f,
		0.6087614f,
		0.7933533f,
		0.7933533f,
		0.6087614f,
		0.9238795f,
		0.38268343f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		-0.13052619f,
		0.9238795f,
		-0.38268343f,
		0.7933533f,
		-0.6087614f,
		0.6087614f,
		-0.7933533f,
		0.38268343f,
		-0.9238795f,
		0.13052619f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		-0.38268343f,
		-0.9238795f,
		-0.6087614f,
		-0.7933533f,
		-0.7933533f,
		-0.6087614f,
		-0.9238795f,
		-0.38268343f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		0.13052619f,
		-0.9238795f,
		0.38268343f,
		-0.7933533f,
		0.6087614f,
		-0.6087614f,
		0.7933533f,
		-0.38268343f,
		0.9238795f,
		-0.13052619f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		0.38268343f,
		0.9238795f,
		0.6087614f,
		0.7933533f,
		0.7933533f,
		0.6087614f,
		0.9238795f,
		0.38268343f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		-0.13052619f,
		0.9238795f,
		-0.38268343f,
		0.7933533f,
		-0.6087614f,
		0.6087614f,
		-0.7933533f,
		0.38268343f,
		-0.9238795f,
		0.13052619f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		-0.38268343f,
		-0.9238795f,
		-0.6087614f,
		-0.7933533f,
		-0.7933533f,
		-0.6087614f,
		-0.9238795f,
		-0.38268343f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		0.13052619f,
		-0.9238795f,
		0.38268343f,
		-0.7933533f,
		0.6087614f,
		-0.6087614f,
		0.7933533f,
		-0.38268343f,
		0.9238795f,
		-0.13052619f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		0.38268343f,
		0.9238795f,
		0.6087614f,
		0.7933533f,
		0.7933533f,
		0.6087614f,
		0.9238795f,
		0.38268343f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		-0.13052619f,
		0.9238795f,
		-0.38268343f,
		0.7933533f,
		-0.6087614f,
		0.6087614f,
		-0.7933533f,
		0.38268343f,
		-0.9238795f,
		0.13052619f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		-0.38268343f,
		-0.9238795f,
		-0.6087614f,
		-0.7933533f,
		-0.7933533f,
		-0.6087614f,
		-0.9238795f,
		-0.38268343f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		0.13052619f,
		-0.9238795f,
		0.38268343f,
		-0.7933533f,
		0.6087614f,
		-0.6087614f,
		0.7933533f,
		-0.38268343f,
		0.9238795f,
		-0.13052619f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		0.38268343f,
		0.9238795f,
		0.6087614f,
		0.7933533f,
		0.7933533f,
		0.6087614f,
		0.9238795f,
		0.38268343f,
		0.9914449f,
		0.13052619f,
		0.9914449f,
		-0.13052619f,
		0.9238795f,
		-0.38268343f,
		0.7933533f,
		-0.6087614f,
		0.6087614f,
		-0.7933533f,
		0.38268343f,
		-0.9238795f,
		0.13052619f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		-0.38268343f,
		-0.9238795f,
		-0.6087614f,
		-0.7933533f,
		-0.7933533f,
		-0.6087614f,
		-0.9238795f,
		-0.38268343f,
		-0.9914449f,
		-0.13052619f,
		-0.9914449f,
		0.13052619f,
		-0.9238795f,
		0.38268343f,
		-0.7933533f,
		0.6087614f,
		-0.6087614f,
		0.7933533f,
		-0.38268343f,
		0.9238795f,
		-0.13052619f,
		0.9914449f,
		0.38268343f,
		0.9238795f,
		0.9238795f,
		0.38268343f,
		0.9238795f,
		-0.38268343f,
		0.38268343f,
		-0.9238795f,
		-0.38268343f,
		-0.9238795f,
		-0.9238795f,
		-0.38268343f,
		-0.9238795f,
		0.38268343f,
		-0.38268343f,
		0.9238795f
	};

	// Token: 0x04000021 RID: 33
	private static readonly float[] RandVecs2D = new float[]
	{
		-0.2700222f,
		-0.9628541f,
		0.38630927f,
		-0.9223693f,
		0.04444859f,
		-0.9990117f,
		-0.59925234f,
		-0.80056024f,
		-0.781928f,
		0.62336874f,
		0.9464672f,
		0.32279992f,
		-0.6514147f,
		-0.7587219f,
		0.93784726f,
		0.34704837f,
		-0.8497876f,
		-0.52712524f,
		-0.87904257f,
		0.47674325f,
		-0.8923003f,
		-0.45144236f,
		-0.37984443f,
		-0.9250504f,
		-0.9951651f,
		0.09821638f,
		0.7724398f,
		-0.635088f,
		0.75732833f,
		-0.6530343f,
		-0.9928005f,
		-0.119780056f,
		-0.05326657f,
		0.99858034f,
		0.97542536f,
		-0.22033007f,
		-0.76650184f,
		0.64224213f,
		0.9916367f,
		0.12906061f,
		-0.99469686f,
		0.10285038f,
		-0.53792053f,
		-0.8429955f,
		0.50228155f,
		-0.86470413f,
		0.45598215f,
		-0.8899889f,
		-0.8659131f,
		-0.50019443f,
		0.08794584f,
		-0.9961253f,
		-0.5051685f,
		0.8630207f,
		0.7753185f,
		-0.6315704f,
		-0.69219446f,
		0.72171104f,
		-0.51916593f,
		-0.85467345f,
		0.8978623f,
		-0.4402764f,
		-0.17067741f,
		0.98532695f,
		-0.935343f,
		-0.35374206f,
		-0.99924046f,
		0.038967468f,
		-0.2882064f,
		-0.9575683f,
		-0.96638113f,
		0.2571138f,
		-0.87597144f,
		-0.48236302f,
		-0.8303123f,
		-0.55729836f,
		0.051101338f,
		-0.99869347f,
		-0.85583735f,
		-0.51724505f,
		0.098870255f,
		0.9951003f,
		0.9189016f,
		0.39448678f,
		-0.24393758f,
		-0.96979094f,
		-0.81214094f,
		-0.5834613f,
		-0.99104315f,
		0.13354214f,
		0.8492424f,
		-0.52800316f,
		-0.9717839f,
		-0.23587295f,
		0.9949457f,
		0.10041421f,
		0.6241065f,
		-0.7813392f,
		0.6629103f,
		0.74869883f,
		-0.7197418f,
		0.6942418f,
		-0.8143371f,
		-0.58039224f,
		0.10452105f,
		-0.9945227f,
		-0.10659261f,
		-0.99430275f,
		0.44579968f,
		-0.8951328f,
		0.105547406f,
		0.99441427f,
		-0.9927903f,
		0.11986445f,
		-0.83343667f,
		0.55261505f,
		0.9115562f,
		-0.4111756f,
		0.8285545f,
		-0.55990845f,
		0.7217098f,
		-0.6921958f,
		0.49404928f,
		-0.8694339f,
		-0.36523214f,
		-0.9309165f,
		-0.9696607f,
		0.24445485f,
		0.089255095f,
		-0.9960088f,
		0.5354071f,
		-0.8445941f,
		-0.10535762f,
		0.9944344f,
		-0.98902845f,
		0.1477251f,
		0.004856105f,
		0.9999882f,
		0.98855984f,
		0.15082914f,
		0.92861295f,
		-0.37104982f,
		-0.5832394f,
		-0.8123003f,
		0.30152076f,
		0.9534596f,
		-0.95751107f,
		0.28839657f,
		0.9715802f,
		-0.23671055f,
		0.2299818f,
		0.97319496f,
		0.9557638f,
		-0.2941352f,
		0.7409561f,
		0.67155343f,
		-0.9971514f,
		-0.07542631f,
		0.69057107f,
		-0.7232645f,
		-0.2907137f,
		-0.9568101f,
		0.5912778f,
		-0.80646795f,
		-0.94545925f,
		-0.3257405f,
		0.66644555f,
		0.7455537f,
		0.6236135f,
		0.78173286f,
		0.9126994f,
		-0.40863165f,
		-0.8191762f,
		0.57354194f,
		-0.8812746f,
		-0.4726046f,
		0.99533135f,
		0.09651673f,
		0.98556507f,
		-0.16929697f,
		-0.8495981f,
		0.52743065f,
		0.6174854f,
		-0.78658235f,
		0.85081565f,
		0.5254643f,
		0.99850327f,
		-0.0546925f,
		0.19713716f,
		-0.98037595f,
		0.66078556f,
		-0.7505747f,
		-0.030974941f,
		0.9995202f,
		-0.6731661f,
		0.73949134f,
		-0.71950185f,
		-0.69449055f,
		0.97275114f,
		0.2318516f,
		0.9997059f,
		-0.02425069f,
		0.44217876f,
		-0.89692694f,
		0.9981351f,
		-0.061043672f,
		-0.9173661f,
		-0.39804456f,
		-0.81500566f,
		-0.579453f,
		-0.87893313f,
		0.476945f,
		0.015860584f,
		0.99987423f,
		-0.8095465f,
		0.5870558f,
		-0.9165899f,
		-0.39982867f,
		-0.8023543f,
		0.5968481f,
		-0.5176738f,
		0.85557806f,
		-0.8154407f,
		-0.57884055f,
		0.40220103f,
		-0.91555136f,
		-0.9052557f,
		-0.4248672f,
		0.7317446f,
		0.681579f,
		-0.56476325f,
		-0.825253f,
		-0.8403276f,
		-0.54207885f,
		-0.93142813f,
		0.36392525f,
		0.52381986f,
		0.85182905f,
		0.7432804f,
		-0.66898f,
		-0.9853716f,
		-0.17041974f,
		0.46014687f,
		0.88784283f,
		0.8258554f,
		0.56388193f,
		0.6182366f,
		0.785992f,
		0.83315027f,
		-0.55304664f,
		0.15003075f,
		0.9886813f,
		-0.6623304f,
		-0.7492119f,
		-0.66859865f,
		0.74362344f,
		0.7025606f,
		0.7116239f,
		-0.54193896f,
		-0.84041786f,
		-0.33886164f,
		0.9408362f,
		0.833153f,
		0.55304253f,
		-0.29897207f,
		-0.95426184f,
		0.2638523f,
		0.9645631f,
		0.12410874f,
		-0.9922686f,
		-0.7282649f,
		-0.6852957f,
		0.69625f,
		0.71779937f,
		-0.91835356f,
		0.395761f,
		-0.6326102f,
		-0.7744703f,
		-0.9331892f,
		-0.35938552f,
		-0.11537793f,
		-0.99332166f,
		0.9514975f,
		-0.30765656f,
		-0.08987977f,
		-0.9959526f,
		0.6678497f,
		0.7442962f,
		0.79524004f,
		-0.6062947f,
		-0.6462007f,
		-0.7631675f,
		-0.27335986f,
		0.96191186f,
		0.966959f,
		-0.25493184f,
		-0.9792895f,
		0.20246519f,
		-0.5369503f,
		-0.84361386f,
		-0.27003646f,
		-0.9628501f,
		-0.6400277f,
		0.76835185f,
		-0.78545374f,
		-0.6189204f,
		0.060059056f,
		-0.9981948f,
		-0.024557704f,
		0.9996984f,
		-0.65983623f,
		0.7514095f,
		-0.62538946f,
		-0.7803128f,
		-0.6210409f,
		-0.7837782f,
		0.8348889f,
		0.55041856f,
		-0.15922752f,
		0.9872419f,
		0.83676225f,
		0.54756635f,
		-0.8675754f,
		-0.4973057f,
		-0.20226626f,
		-0.97933054f,
		0.939919f,
		0.34139755f,
		0.98774046f,
		-0.1561049f,
		-0.90344554f,
		0.42870283f,
		0.12698042f,
		-0.9919052f,
		-0.3819601f,
		0.92417884f,
		0.9754626f,
		0.22016525f,
		-0.32040158f,
		-0.94728184f,
		-0.9874761f,
		0.15776874f,
		0.025353484f,
		-0.99967855f,
		0.4835131f,
		-0.8753371f,
		-0.28508f,
		-0.9585037f,
		-0.06805516f,
		-0.99768156f,
		-0.7885244f,
		-0.61500347f,
		0.3185392f,
		-0.9479097f,
		0.8880043f,
		0.45983514f,
		0.64769214f,
		-0.76190215f,
		0.98202413f,
		0.18875542f,
		0.93572754f,
		-0.35272372f,
		-0.88948953f,
		0.45695552f,
		0.7922791f,
		0.6101588f,
		0.74838185f,
		0.66326815f,
		-0.728893f,
		-0.68462765f,
		0.8729033f,
		-0.48789328f,
		0.8288346f,
		0.5594937f,
		0.08074567f,
		0.99673474f,
		0.97991484f,
		-0.1994165f,
		-0.5807307f,
		-0.81409574f,
		-0.47000498f,
		-0.8826638f,
		0.2409493f,
		0.9705377f,
		0.9437817f,
		-0.33056942f,
		-0.89279985f,
		-0.45045355f,
		-0.80696225f,
		0.59060305f,
		0.062589735f,
		0.99803936f,
		-0.93125975f,
		0.36435598f,
		0.57774496f,
		0.81621736f,
		-0.3360096f,
		-0.9418586f,
		0.69793206f,
		-0.71616393f,
		-0.0020081573f,
		-0.999998f,
		-0.18272944f,
		-0.98316324f,
		-0.6523912f,
		0.7578824f,
		-0.43026268f,
		-0.9027037f,
		-0.9985126f,
		-0.054520912f,
		-0.010281022f,
		-0.99994713f,
		-0.49460712f,
		0.86911666f,
		-0.299935f,
		0.95395964f,
		0.8165472f,
		0.5772787f,
		0.26974604f,
		0.9629315f,
		-0.7306287f,
		-0.68277496f,
		-0.7590952f,
		-0.65097964f,
		-0.9070538f,
		0.4210146f,
		-0.5104861f,
		-0.859886f,
		0.86133504f,
		0.5080373f,
		0.50078815f,
		-0.8655699f,
		-0.6541582f,
		0.7563578f,
		-0.83827555f,
		-0.54524684f,
		0.6940071f,
		0.7199682f,
		0.06950936f,
		0.9975813f,
		0.17029423f,
		-0.9853933f,
		0.26959732f,
		0.9629731f,
		0.55196124f,
		-0.83386976f,
		0.2256575f,
		-0.9742067f,
		0.42152628f,
		-0.9068162f,
		0.48818734f,
		-0.87273884f,
		-0.3683855f,
		-0.92967314f,
		-0.98253906f,
		0.18605645f,
		0.81256473f,
		0.582871f,
		0.3196461f,
		-0.947537f,
		0.9570914f,
		0.28978625f,
		-0.6876655f,
		-0.7260276f,
		-0.9988771f,
		-0.04737673f,
		-0.1250179f,
		0.9921545f,
		-0.82801336f,
		0.56070834f,
		0.93248636f,
		-0.36120513f,
		0.63946533f,
		0.7688199f,
		-0.016238471f,
		-0.99986815f,
		-0.99550146f,
		-0.094746135f,
		-0.8145332f,
		0.580117f,
		0.4037328f,
		-0.91487694f,
		0.9944263f,
		0.10543368f,
		-0.16247116f,
		0.9867133f,
		-0.9949488f,
		-0.10038388f,
		-0.69953024f,
		0.714603f,
		0.5263415f,
		-0.85027325f,
		-0.5395222f,
		0.8419714f,
		0.65793705f,
		0.7530729f,
		0.014267588f,
		-0.9998982f,
		-0.6734384f,
		0.7392433f,
		0.6394121f,
		-0.7688642f,
		0.9211571f,
		0.38919085f,
		-0.14663722f,
		-0.98919034f,
		-0.7823181f,
		0.6228791f,
		-0.5039611f,
		-0.8637264f,
		-0.774312f,
		-0.632804f
	};

	// Token: 0x04000022 RID: 34
	private static readonly float[] Gradients3D = new float[]
	{
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		0f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		0f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		0f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		0f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		1f,
		0f,
		1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		-1f,
		0f,
		1f,
		1f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		1f,
		-1f,
		0f,
		0f,
		-1f,
		-1f,
		0f,
		0f,
		1f,
		1f,
		0f,
		0f,
		0f,
		-1f,
		1f,
		0f,
		-1f,
		1f,
		0f,
		0f,
		0f,
		-1f,
		-1f,
		0f
	};

	// Token: 0x04000023 RID: 35
	private static readonly float[] RandVecs3D = new float[]
	{
		-0.7292737f,
		-0.66184396f,
		0.17355819f,
		0f,
		0.7902921f,
		-0.5480887f,
		-0.2739291f,
		0f,
		0.7217579f,
		0.62262124f,
		-0.3023381f,
		0f,
		0.5656831f,
		-0.8208298f,
		-0.079000026f,
		0f,
		0.76004905f,
		-0.55559796f,
		-0.33709997f,
		0f,
		0.37139457f,
		0.50112647f,
		0.78162545f,
		0f,
		-0.12770624f,
		-0.4254439f,
		-0.8959289f,
		0f,
		-0.2881561f,
		-0.5815839f,
		0.7607406f,
		0f,
		0.5849561f,
		-0.6628202f,
		-0.4674352f,
		0f,
		0.33071712f,
		0.039165374f,
		0.94291687f,
		0f,
		0.8712122f,
		-0.41133744f,
		-0.26793817f,
		0f,
		0.580981f,
		0.7021916f,
		0.41156778f,
		0f,
		0.5037569f,
		0.6330057f,
		-0.5878204f,
		0f,
		0.44937122f,
		0.6013902f,
		0.6606023f,
		0f,
		-0.6878404f,
		0.090188906f,
		-0.7202372f,
		0f,
		-0.59589565f,
		-0.64693505f,
		0.47579765f,
		0f,
		-0.5127052f,
		0.1946922f,
		-0.83619875f,
		0f,
		-0.99115074f,
		-0.054102764f,
		-0.12121531f,
		0f,
		-0.21497211f,
		0.9720882f,
		-0.09397608f,
		0f,
		-0.7518651f,
		-0.54280573f,
		0.37424695f,
		0f,
		0.5237069f,
		0.8516377f,
		-0.021078179f,
		0f,
		0.6333505f,
		0.19261672f,
		-0.74951047f,
		0f,
		-0.06788242f,
		0.39983058f,
		0.9140719f,
		0f,
		-0.55386287f,
		-0.47298968f,
		-0.6852129f,
		0f,
		-0.72614557f,
		-0.5911991f,
		0.35099334f,
		0f,
		-0.9229275f,
		-0.17828088f,
		0.34120494f,
		0f,
		-0.6968815f,
		0.65112746f,
		0.30064803f,
		0f,
		0.96080446f,
		-0.20983632f,
		-0.18117249f,
		0f,
		0.068171464f,
		-0.9743405f,
		0.21450691f,
		0f,
		-0.3577285f,
		-0.6697087f,
		-0.65078455f,
		0f,
		-0.18686211f,
		0.7648617f,
		-0.61649746f,
		0f,
		-0.65416974f,
		0.3967915f,
		0.64390874f,
		0f,
		0.699334f,
		-0.6164538f,
		0.36182392f,
		0f,
		-0.15466657f,
		0.6291284f,
		0.7617583f,
		0f,
		-0.6841613f,
		-0.2580482f,
		-0.68215424f,
		0f,
		0.5383981f,
		0.4258655f,
		0.727163f,
		0f,
		-0.5026988f,
		-0.7939833f,
		-0.3418837f,
		0f,
		0.32029718f,
		0.28344154f,
		0.9039196f,
		0f,
		0.86832273f,
		-0.00037626564f,
		-0.49599952f,
		0f,
		0.79112005f,
		-0.085110456f,
		0.60571057f,
		0f,
		-0.04011016f,
		-0.43972486f,
		0.8972364f,
		0f,
		0.914512f,
		0.35793462f,
		-0.18854876f,
		0f,
		-0.96120393f,
		-0.27564842f,
		0.010246669f,
		0f,
		0.65103614f,
		-0.28777993f,
		-0.70237786f,
		0f,
		-0.20417863f,
		0.73652375f,
		0.6448596f,
		0f,
		-0.7718264f,
		0.37906268f,
		0.5104856f,
		0f,
		-0.30600828f,
		-0.7692988f,
		0.56083715f,
		0f,
		0.45400733f,
		-0.5024843f,
		0.73578995f,
		0f,
		0.48167956f,
		0.6021208f,
		-0.636738f,
		0f,
		0.69619805f,
		-0.32221973f,
		0.6414692f,
		0f,
		-0.65321606f,
		-0.6781149f,
		0.33685157f,
		0f,
		0.50893015f,
		-0.61546624f,
		-0.60182345f,
		0f,
		-0.16359198f,
		-0.9133605f,
		-0.37284088f,
		0f,
		0.5240802f,
		-0.8437664f,
		0.11575059f,
		0f,
		0.5902587f,
		0.4983818f,
		-0.63498837f,
		0f,
		0.5863228f,
		0.49476475f,
		0.6414308f,
		0f,
		0.6779335f,
		0.23413453f,
		0.6968409f,
		0f,
		0.7177054f,
		-0.68589795f,
		0.12017863f,
		0f,
		-0.532882f,
		-0.5205125f,
		0.6671608f,
		0f,
		-0.8654874f,
		-0.07007271f,
		-0.4960054f,
		0f,
		-0.286181f,
		0.79520893f,
		0.53454953f,
		0f,
		-0.048495296f,
		0.98108363f,
		-0.18741156f,
		0f,
		-0.63585216f,
		0.60583484f,
		0.47818002f,
		0f,
		0.62547946f,
		-0.28616196f,
		0.72586966f,
		0f,
		-0.258526f,
		0.50619495f,
		-0.8227582f,
		0f,
		0.021363068f,
		0.50640166f,
		-0.862033f,
		0f,
		0.20011178f,
		0.85992634f,
		0.46955505f,
		0f,
		0.47435614f,
		0.6014985f,
		-0.6427953f,
		0f,
		0.6622994f,
		-0.52024746f,
		-0.539168f,
		0f,
		0.08084973f,
		-0.65327203f,
		0.7527941f,
		0f,
		-0.6893687f,
		0.059286036f,
		0.7219805f,
		0f,
		-0.11218871f,
		-0.96731853f,
		0.22739525f,
		0f,
		0.7344116f,
		0.59796685f,
		-0.3210533f,
		0f,
		0.5789393f,
		-0.24888498f,
		0.776457f,
		0f,
		0.69881827f,
		0.35571697f,
		-0.6205791f,
		0f,
		-0.86368454f,
		-0.27487713f,
		-0.4224826f,
		0f,
		-0.4247028f,
		-0.46408808f,
		0.77733505f,
		0f,
		0.5257723f,
		-0.84270173f,
		0.11583299f,
		0f,
		0.93438303f,
		0.31630248f,
		-0.16395439f,
		0f,
		-0.10168364f,
		-0.8057303f,
		-0.58348876f,
		0f,
		-0.6529239f,
		0.50602126f,
		-0.5635893f,
		0f,
		-0.24652861f,
		-0.9668206f,
		-0.06694497f,
		0f,
		-0.9776897f,
		-0.20992506f,
		-0.0073688254f,
		0f,
		0.7736893f,
		0.57342446f,
		0.2694238f,
		0f,
		-0.6095088f,
		0.4995679f,
		0.6155737f,
		0f,
		0.5794535f,
		0.7434547f,
		0.33392924f,
		0f,
		-0.8226211f,
		0.081425816f,
		0.56272936f,
		0f,
		-0.51038545f,
		0.47036678f,
		0.719904f,
		0f,
		-0.5764972f,
		-0.072316565f,
		-0.81389266f,
		0f,
		0.7250629f,
		0.39499715f,
		-0.56414634f,
		0f,
		-0.1525424f,
		0.48608407f,
		-0.8604958f,
		0f,
		-0.55509764f,
		-0.49578208f,
		0.6678823f,
		0f,
		-0.18836144f,
		0.91458696f,
		0.35784173f,
		0f,
		0.76255566f,
		-0.54144084f,
		-0.35404897f,
		0f,
		-0.5870232f,
		-0.3226498f,
		-0.7424964f,
		0f,
		0.30511242f,
		0.2262544f,
		-0.9250488f,
		0f,
		0.63795763f,
		0.57724243f,
		-0.50970703f,
		0f,
		-0.5966776f,
		0.14548524f,
		-0.7891831f,
		0f,
		-0.65833056f,
		0.65554875f,
		-0.36994147f,
		0f,
		0.74348927f,
		0.23510846f,
		0.6260573f,
		0f,
		0.5562114f,
		0.82643604f,
		-0.08736329f,
		0f,
		-0.302894f,
		-0.8251527f,
		0.47684193f,
		0f,
		0.11293438f,
		-0.9858884f,
		-0.123571075f,
		0f,
		0.5937653f,
		-0.5896814f,
		0.5474657f,
		0f,
		0.6757964f,
		-0.58357584f,
		-0.45026484f,
		0f,
		0.7242303f,
		-0.11527198f,
		0.67985505f,
		0f,
		-0.9511914f,
		0.0753624f,
		-0.29925808f,
		0f,
		0.2539471f,
		-0.18863393f,
		0.9486454f,
		0f,
		0.5714336f,
		-0.16794509f,
		-0.8032796f,
		0f,
		-0.06778235f,
		0.39782694f,
		0.9149532f,
		0f,
		0.6074973f,
		0.73306f,
		-0.30589226f,
		0f,
		-0.54354787f,
		0.16758224f,
		0.8224791f,
		0f,
		-0.5876678f,
		-0.3380045f,
		-0.7351187f,
		0f,
		-0.79675627f,
		0.040978227f,
		-0.60290986f,
		0f,
		-0.19963509f,
		0.8706295f,
		0.4496111f,
		0f,
		-0.027876602f,
		-0.91062325f,
		-0.4122962f,
		0f,
		-0.7797626f,
		-0.6257635f,
		0.019757755f,
		0f,
		-0.5211233f,
		0.74016446f,
		-0.42495546f,
		0f,
		0.8575425f,
		0.4053273f,
		-0.31675017f,
		0f,
		0.10452233f,
		0.8390196f,
		-0.53396744f,
		0f,
		0.3501823f,
		0.9242524f,
		-0.15208502f,
		0f,
		0.19878499f,
		0.076476134f,
		0.9770547f,
		0f,
		0.78459966f,
		0.6066257f,
		-0.12809642f,
		0f,
		0.09006737f,
		-0.97509897f,
		-0.20265691f,
		0f,
		-0.82743436f,
		-0.54229957f,
		0.14582036f,
		0f,
		-0.34857976f,
		-0.41580227f,
		0.8400004f,
		0f,
		-0.2471779f,
		-0.730482f,
		-0.6366311f,
		0f,
		-0.3700155f,
		0.8577948f,
		0.35675845f,
		0f,
		0.59133947f,
		-0.54831195f,
		-0.59133035f,
		0f,
		0.120487355f,
		-0.7626472f,
		-0.6354935f,
		0f,
		0.6169593f,
		0.03079648f,
		0.7863923f,
		0f,
		0.12581569f,
		-0.664083f,
		-0.73699677f,
		0f,
		-0.6477565f,
		-0.17401473f,
		-0.74170774f,
		0f,
		0.6217889f,
		-0.7804431f,
		-0.06547655f,
		0f,
		0.6589943f,
		-0.6096988f,
		0.44044736f,
		0f,
		-0.26898375f,
		-0.6732403f,
		-0.68876356f,
		0f,
		-0.38497752f,
		0.56765425f,
		0.7277094f,
		0f,
		0.57544446f,
		0.81104714f,
		-0.10519635f,
		0f,
		0.91415936f,
		0.3832948f,
		0.13190056f,
		0f,
		-0.10792532f,
		0.9245494f,
		0.36545935f,
		0f,
		0.3779771f,
		0.30431488f,
		0.87437165f,
		0f,
		-0.21428852f,
		-0.8259286f,
		0.5214617f,
		0f,
		0.58025444f,
		0.41480985f,
		-0.7008834f,
		0f,
		-0.19826609f,
		0.85671616f,
		-0.47615966f,
		0f,
		-0.033815537f,
		0.37731808f,
		-0.9254661f,
		0f,
		-0.68679225f,
		-0.6656598f,
		0.29191336f,
		0f,
		0.7731743f,
		-0.28757936f,
		-0.565243f,
		0f,
		-0.09655942f,
		0.91937083f,
		-0.3813575f,
		0f,
		0.27157024f,
		-0.957791f,
		-0.09426606f,
		0f,
		0.24510157f,
		-0.6917999f,
		-0.6792188f,
		0f,
		0.97770077f,
		-0.17538553f,
		0.115503654f,
		0f,
		-0.522474f,
		0.8521607f,
		0.029036159f,
		0f,
		-0.77348804f,
		-0.52612925f,
		0.35341796f,
		0f,
		-0.71344924f,
		-0.26954725f,
		0.6467878f,
		0f,
		0.16440372f,
		0.5105846f,
		-0.84396374f,
		0f,
		0.6494636f,
		0.055856112f,
		0.7583384f,
		0f,
		-0.4711971f,
		0.50172806f,
		-0.7254256f,
		0f,
		-0.63357645f,
		-0.23816863f,
		-0.7361091f,
		0f,
		-0.9021533f,
		-0.2709478f,
		-0.33571818f,
		0f,
		-0.3793711f,
		0.8722581f,
		0.3086152f,
		0f,
		-0.68555987f,
		-0.32501432f,
		0.6514394f,
		0f,
		0.29009423f,
		-0.7799058f,
		-0.5546101f,
		0f,
		-0.20983194f,
		0.8503707f,
		0.48253515f,
		0f,
		-0.45926037f,
		0.6598504f,
		-0.5947077f,
		0f,
		0.87159455f,
		0.09616365f,
		-0.48070312f,
		0f,
		-0.6776666f,
		0.71185046f,
		-0.1844907f,
		0f,
		0.7044378f,
		0.3124276f,
		0.637304f,
		0f,
		-0.7052319f,
		-0.24010932f,
		-0.6670798f,
		0f,
		0.081921004f,
		-0.72073364f,
		-0.68835455f,
		0f,
		-0.6993681f,
		-0.5875763f,
		-0.4069869f,
		0f,
		-0.12814544f,
		0.6419896f,
		0.75592864f,
		0f,
		-0.6337388f,
		-0.67854714f,
		-0.3714147f,
		0f,
		0.5565052f,
		-0.21688876f,
		-0.8020357f,
		0f,
		-0.57915545f,
		0.7244372f,
		-0.3738579f,
		0f,
		0.11757791f,
		-0.7096451f,
		0.69467926f,
		0f,
		-0.613462f,
		0.13236311f,
		0.7785528f,
		0f,
		0.69846356f,
		-0.029805163f,
		-0.7150247f,
		0f,
		0.83180827f,
		-0.3930172f,
		0.39195976f,
		0f,
		0.14695764f,
		0.055416517f,
		-0.98758924f,
		0f,
		0.70886856f,
		-0.2690504f,
		0.65201014f,
		0f,
		0.27260533f,
		0.67369765f,
		-0.68688995f,
		0f,
		-0.65912956f,
		0.30354586f,
		-0.68804663f,
		0f,
		0.48151314f,
		-0.752827f,
		0.4487723f,
		0f,
		0.943001f,
		0.16756473f,
		-0.28752613f,
		0f,
		0.43480295f,
		0.7695305f,
		-0.46772778f,
		0f,
		0.39319962f,
		0.5944736f,
		0.70142365f,
		0f,
		0.72543365f,
		-0.60392565f,
		0.33018148f,
		0f,
		0.75902355f,
		-0.6506083f,
		0.024333132f,
		0f,
		-0.8552769f,
		-0.3430043f,
		0.38839358f,
		0f,
		-0.6139747f,
		0.6981725f,
		0.36822575f,
		0f,
		-0.74659055f,
		-0.575201f,
		0.33428493f,
		0f,
		0.5730066f,
		0.8105555f,
		-0.12109168f,
		0f,
		-0.92258775f,
		-0.3475211f,
		-0.16751404f,
		0f,
		-0.71058166f,
		-0.47196922f,
		-0.5218417f,
		0f,
		-0.0856461f,
		0.35830015f,
		0.9296697f,
		0f,
		-0.8279698f,
		-0.2043157f,
		0.5222271f,
		0f,
		0.42794403f,
		0.278166f,
		0.8599346f,
		0f,
		0.539908f,
		-0.78571206f,
		-0.3019204f,
		0f,
		0.5678404f,
		-0.5495414f,
		-0.61283076f,
		0f,
		-0.9896071f,
		0.13656391f,
		-0.045034185f,
		0f,
		-0.6154343f,
		-0.64408755f,
		0.45430374f,
		0f,
		0.10742044f,
		-0.79463404f,
		0.59750944f,
		0f,
		-0.359545f,
		-0.888553f,
		0.28495783f,
		0f,
		-0.21804053f,
		0.1529889f,
		0.9638738f,
		0f,
		-0.7277432f,
		-0.61640507f,
		-0.30072346f,
		0f,
		0.7249729f,
		-0.0066971947f,
		0.68874484f,
		0f,
		-0.5553659f,
		-0.5336586f,
		0.6377908f,
		0f,
		0.5137558f,
		0.79762083f,
		-0.316f,
		0f,
		-0.3794025f,
		0.92456084f,
		-0.035227515f,
		0f,
		0.82292485f,
		0.27453658f,
		-0.49741766f,
		0f,
		-0.5404114f,
		0.60911417f,
		0.5804614f,
		0f,
		0.8036582f,
		-0.27030295f,
		0.5301602f,
		0f,
		0.60443187f,
		0.68329686f,
		0.40959433f,
		0f,
		0.06389989f,
		0.96582085f,
		-0.2512108f,
		0f,
		0.10871133f,
		0.74024713f,
		-0.6634878f,
		0f,
		-0.7134277f,
		-0.6926784f,
		0.10591285f,
		0f,
		0.64588976f,
		-0.57245487f,
		-0.50509584f,
		0f,
		-0.6553931f,
		0.73814714f,
		0.15999562f,
		0f,
		0.39109614f,
		0.91888714f,
		-0.05186756f,
		0f,
		-0.48790225f,
		-0.5904377f,
		0.64291114f,
		0f,
		0.601479f,
		0.77074414f,
		-0.21018201f,
		0f,
		-0.5677173f,
		0.7511361f,
		0.33688518f,
		0f,
		0.7858574f,
		0.22667466f,
		0.5753667f,
		0f,
		-0.45203456f,
		-0.6042227f,
		-0.65618575f,
		0f,
		0.0022721163f,
		0.4132844f,
		-0.9105992f,
		0f,
		-0.58157516f,
		-0.5162926f,
		0.6286591f,
		0f,
		-0.03703705f,
		0.8273786f,
		0.5604221f,
		0f,
		-0.51196927f,
		0.79535437f,
		-0.324498f,
		0f,
		-0.26824173f,
		-0.957229f,
		-0.10843876f,
		0f,
		-0.23224828f,
		-0.9679131f,
		-0.09594243f,
		0f,
		0.3554329f,
		-0.8881506f,
		0.29130062f,
		0f,
		0.73465204f,
		-0.4371373f,
		0.5188423f,
		0f,
		0.998512f,
		0.046590112f,
		-0.028339446f,
		0f,
		-0.37276876f,
		-0.9082481f,
		0.19007573f,
		0f,
		0.9173738f,
		-0.3483642f,
		0.19252984f,
		0f,
		0.2714911f,
		0.41475296f,
		-0.86848867f,
		0f,
		0.5131763f,
		-0.71163344f,
		0.4798207f,
		0f,
		-0.87373537f,
		0.18886992f,
		-0.44823506f,
		0f,
		0.84600437f,
		-0.3725218f,
		0.38145f,
		0f,
		0.89787275f,
		-0.17802091f,
		-0.40265754f,
		0f,
		0.21780656f,
		-0.9698323f,
		-0.10947895f,
		0f,
		-0.15180314f,
		-0.7788918f,
		-0.6085091f,
		0f,
		-0.2600385f,
		-0.4755398f,
		-0.840382f,
		0f,
		0.5723135f,
		-0.7474341f,
		-0.33734185f,
		0f,
		-0.7174141f,
		0.16990171f,
		-0.67561114f,
		0f,
		-0.6841808f,
		0.021457076f,
		-0.72899675f,
		0f,
		-0.2007448f,
		0.06555606f,
		-0.9774477f,
		0f,
		-0.11488037f,
		-0.8044887f,
		0.5827524f,
		0f,
		-0.787035f,
		0.03447489f,
		0.6159443f,
		0f,
		-0.20155965f,
		0.68598723f,
		0.69913894f,
		0f,
		-0.085810825f,
		-0.10920836f,
		-0.99030805f,
		0f,
		0.5532693f,
		0.73252505f,
		-0.39661077f,
		0f,
		-0.18424894f,
		-0.9777375f,
		-0.100407675f,
		0f,
		0.07754738f,
		-0.9111506f,
		0.40471104f,
		0f,
		0.13998385f,
		0.7601631f,
		-0.63447344f,
		0f,
		0.44844192f,
		-0.84528923f,
		0.29049253f,
		0f
	};

	// Token: 0x04000024 RID: 36
	private const int PrimeX = 501125321;

	// Token: 0x04000025 RID: 37
	private const int PrimeY = 1136930381;

	// Token: 0x04000026 RID: 38
	private const int PrimeZ = 1720413743;

	// Token: 0x020001E9 RID: 489
	public enum NoiseType
	{
		// Token: 0x040007E3 RID: 2019
		OpenSimplex2,
		// Token: 0x040007E4 RID: 2020
		OpenSimplex2S,
		// Token: 0x040007E5 RID: 2021
		Cellular,
		// Token: 0x040007E6 RID: 2022
		Perlin,
		// Token: 0x040007E7 RID: 2023
		ValueCubic,
		// Token: 0x040007E8 RID: 2024
		Value
	}

	// Token: 0x020001EA RID: 490
	public enum RotationType3D
	{
		// Token: 0x040007EA RID: 2026
		None,
		// Token: 0x040007EB RID: 2027
		ImproveXYPlanes,
		// Token: 0x040007EC RID: 2028
		ImproveXZPlanes
	}

	// Token: 0x020001EB RID: 491
	public enum FractalType
	{
		// Token: 0x040007EE RID: 2030
		None,
		// Token: 0x040007EF RID: 2031
		FBm,
		// Token: 0x040007F0 RID: 2032
		Ridged,
		// Token: 0x040007F1 RID: 2033
		PingPong,
		// Token: 0x040007F2 RID: 2034
		DomainWarpProgressive,
		// Token: 0x040007F3 RID: 2035
		DomainWarpIndependent
	}

	// Token: 0x020001EC RID: 492
	public enum CellularDistanceFunction
	{
		// Token: 0x040007F5 RID: 2037
		Euclidean,
		// Token: 0x040007F6 RID: 2038
		EuclideanSq,
		// Token: 0x040007F7 RID: 2039
		Manhattan,
		// Token: 0x040007F8 RID: 2040
		Hybrid
	}

	// Token: 0x020001ED RID: 493
	public enum CellularReturnType
	{
		// Token: 0x040007FA RID: 2042
		CellValue,
		// Token: 0x040007FB RID: 2043
		Distance,
		// Token: 0x040007FC RID: 2044
		Distance2,
		// Token: 0x040007FD RID: 2045
		Distance2Add,
		// Token: 0x040007FE RID: 2046
		Distance2Sub,
		// Token: 0x040007FF RID: 2047
		Distance2Mul,
		// Token: 0x04000800 RID: 2048
		Distance2Div
	}

	// Token: 0x020001EE RID: 494
	public enum DomainWarpType
	{
		// Token: 0x04000802 RID: 2050
		OpenSimplex2,
		// Token: 0x04000803 RID: 2051
		OpenSimplex2Reduced,
		// Token: 0x04000804 RID: 2052
		BasicGrid
	}

	// Token: 0x020001EF RID: 495
	private enum TransformType3D
	{
		// Token: 0x04000806 RID: 2054
		None,
		// Token: 0x04000807 RID: 2055
		ImproveXYPlanes,
		// Token: 0x04000808 RID: 2056
		ImproveXZPlanes,
		// Token: 0x04000809 RID: 2057
		DefaultOpenSimplex2
	}
}
