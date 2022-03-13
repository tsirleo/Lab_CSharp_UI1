#include "pch.h"
#include <time.h>
#include "mkl.h"
#include <chrono>

class Timer
{
private:
	using clock_t = std::chrono::high_resolution_clock;
	using second_t = std::chrono::duration<double, std::ratio<1> >;

	std::chrono::time_point<clock_t> m_beg;

public:
	Timer() : m_beg(clock_t::now())
	{
	}

	void reset()
	{
		m_beg = clock_t::now();
	}

	double elapsed() const
	{
		return std::chrono::duration_cast<second_t>(clock_t::now() - m_beg).count();
	}
};

extern "C"  _declspec(dllexport)
void VM_Sin(MKL_INT n, double* x, double* yHA, double* yLA, double* yEP, double& timeHA, double& timeLA, double& timeEP, int& ret)
{
	try
	{
		Timer tHA;
		vmdSin(n, x, yHA, VML_HA);
		timeHA = tHA.elapsed();

		Timer tLA;
		vmdSin(n, x, yLA, VML_LA);
		timeLA = tLA.elapsed();

		Timer tEP;
		vmdSin(n, x, yEP, VML_EP);
		timeEP = tEP.elapsed();

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}

extern "C"  _declspec(dllexport)
void VM_Cos(MKL_INT n, double* x, double* yHA, double* yLA, double* yEP, double& timeHA, double& timeLA, double& timeEP, int& ret)
{
	try
	{
		Timer tHA;
		vmdCos(n, x, yHA, VML_HA);
		timeHA = tHA.elapsed();

		Timer tLA;
		vmdCos(n, x, yLA, VML_LA);
		timeLA = tLA.elapsed();

		Timer tEP;
		vmdCos(n, x, yEP, VML_EP);
		timeEP = tEP.elapsed();

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}

extern "C"  _declspec(dllexport)
void VM_SinCos(MKL_INT n, double* x, double* yHA, double* yLA, double* yEP, double* zHA, double* zLA, double* zEP, double& timeHA, double& timeLA, double& timeEP, int& ret)
{
	try
	{
		Timer tHA;
		vmdSinCos(n, x, yHA, zHA, VML_HA);
		timeHA = tHA.elapsed();

		Timer tLA;
		vmdSinCos(n, x, yLA, zLA, VML_LA);
		timeLA = tLA.elapsed();

		Timer tEP;
		vmdSinCos(n, x, yEP, zEP, VML_EP);
		timeEP = tEP.elapsed();

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}