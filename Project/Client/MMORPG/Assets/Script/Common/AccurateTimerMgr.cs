using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AccurateTimer
{
    private long _beginTime;
    private long _passedTime;

    public long passedTime
    {
        get { return _passedTime; }
    }

    private long _leftTime;
    public long leftTime
    {
        get { return _leftTime; }
    }

    private Action _passGivenTimeCallback = null;

    private long _givenCallbackTime = -1;

    private bool _startReckon = false;

    private bool _autoRelease = true;

    public AccurateTimer(long givenCallbackTime = -1, Action passGivenTimeCallback = null, bool autoRelease = true)
    {
        _beginTime = ConvertUtil.ConvertToTimeStamp(DateTime.Now);
        _passedTime = 0;

        if (passGivenTimeCallback != null && givenCallbackTime != -1)
        {
            _passGivenTimeCallback = passGivenTimeCallback;
            _givenCallbackTime = givenCallbackTime;
            _startReckon = true;
            _autoRelease = autoRelease;
        }
    }

    public void ResetBeginTime()
    {
        _beginTime = ConvertUtil.ConvertToTimeStamp(DateTime.Now);
        _passedTime = 0;
        _passGivenTimeCallback = null;
    }

    public void ResetCallbackTimer()
    {
        _beginTime = ConvertUtil.ConvertToTimeStamp(DateTime.Now);
        _passedTime = 0;
        _leftTime = 0;
        _startReckon = true;
    }

    public void Update()
    {
        _passedTime = ConvertUtil.ConvertToTimeStamp(DateTime.Now) - _beginTime;

        if (_startReckon)
        {
            if (_passedTime >= _givenCallbackTime)
            {
                _passGivenTimeCallback();
                _startReckon = false;
                if (_autoRelease)
                {
                    AccurateTimerMgr.Instance.ReleaseTimer(this);
                }
            }
            else
            {
                _leftTime = _givenCallbackTime - _passedTime;
            }
        }
    }

}

public class AccurateTimerMgr : SingletonMono<AccurateTimerMgr>
{
    private List<AccurateTimer> _timerList = new List<AccurateTimer>();

    /// <summary>
    /// 单位为微妙
    /// </summary>
    /// <param name="givenCallbackTime"></param>
    /// <param name="passGivenTimeCallback"></param>
    /// <returns></returns>

    public AccurateTimer CreateTimer(long givenCallbackTime = -1, Action passGivenTimeCallback = null, bool autoRelease = true)
    {
        AccurateTimer timer = new AccurateTimer(givenCallbackTime, passGivenTimeCallback, autoRelease);
        _timerList.Add(timer);
        return timer;
    }

    public void ReleaseTimer(AccurateTimer timer)
    {
        if (_timerList.Contains(timer))
        {
            _timerList.Remove(timer);
        }
        timer = null;
    }

    public void ResetTimer(AccurateTimer timer)
    {
        if (_timerList.Contains(timer))
        {
            timer.ResetBeginTime();
        }
    }

    public void ResetCallbackTimer(AccurateTimer timer)
    {
        if (_timerList.Contains(timer))
        {
            timer.ResetCallbackTimer();
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        for (int i = 0; i < _timerList.Count; i++)
        {
            _timerList[i].Update();
        }
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        for (int i = 0; i < _timerList.Count; i++)
        {
            _timerList[i] = null;
        }
        _timerList.Clear();
    }

}