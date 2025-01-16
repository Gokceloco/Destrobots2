using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    float _originalFixedDeltaTime;
    float _defaultTimeScale = 1f;

    [Required, SerializeField] OverrideSwitch overrideSwitch;

    IEnumerator _lastCoroutine;

    void Start()
    {
        _originalFixedDeltaTime = Time.fixedDeltaTime;

        if (overrideSwitch.isInOverrideMode && overrideSwitch.overrideProfile.overrideTimeScale)
        {
            _defaultTimeScale = overrideSwitch.overrideProfile.timeScale;
        }

        timeScale = _defaultTimeScale;
    }

    public void PlaySlowMotionEffect(float targetTimeScale, float duration, float easeInDuration = 0f ,float easeOutDuration = 0f)
    {
        StopTimeAnimation();
        _lastCoroutine = SlowMotionCoroutine(_defaultTimeScale, targetTimeScale, duration, easeInDuration, easeOutDuration);
        StartCoroutine(_lastCoroutine);
    }

    public float timeScale
    {
        get
        {
            return Time.timeScale;
        }
        set
        {
            Time.timeScale = value;
            if (Mathf.Approximately(value, 0f))
            {
                Time.fixedDeltaTime = _originalFixedDeltaTime;
            }
            else
            {
                Time.fixedDeltaTime = _originalFixedDeltaTime * value;
            }
        }
    }

    public bool isInNormalTimeScale => Mathf.Approximately(timeScale, _defaultTimeScale);

    public void RestoreTimeScale()
    {
        timeScale = _defaultTimeScale;
    }

    bool _paused;
    float _timeScaleAtPause;

    public void Pause()
    {
        if (_paused)
        {
            return;
        }
        
        _paused = true;
        _timeScaleAtPause = timeScale;
        timeScale = 0f;
    }

    public void Resume()
    {
        if (!_paused)
        {
            return;
        }
        
        _paused = false;
        timeScale = _timeScaleAtPause;
    }

    IEnumerator SlowMotionCoroutine(float initialTimeScale, float targetTimeScale, float duration, float easeInDuration, float easeOutDuration)
    {
        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            if (_paused)
            {
                t -= Time.unscaledDeltaTime;
                timeScale = 0f;
                yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
                continue;
            }

            if (t > duration)
            {
                timeScale = initialTimeScale;
                yield break;
            }
            if (t < easeInDuration)
            {
                var currentEaseTime = t;
                var currentTimeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, currentEaseTime / easeInDuration);
                timeScale = currentTimeScale;
            }
            else if (t > duration - easeOutDuration)
            {
                var currentEaseTime = t - duration + easeOutDuration;
                var currentTimeScale = Mathf.Lerp(targetTimeScale, initialTimeScale, currentEaseTime / easeOutDuration);
                timeScale = currentTimeScale;
            }
            else
            {
                timeScale = targetTimeScale;
            }
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }

        timeScale = initialTimeScale;
    }

    public void AnimateTimeScale(float targetTimeScale, float duration)
    {
        var initialTimeScale = timeScale;
        StopTimeAnimation();
        _lastCoroutine = AnimateTimeScaleCoroutine(initialTimeScale, targetTimeScale, duration);
        StartCoroutine(_lastCoroutine);
    }

    IEnumerator AnimateTimeScaleCoroutine(float initialTimeScale, float targetTimeScale, float duration)
    {
        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            if (_paused)
            {
                t -= Time.unscaledDeltaTime;
                timeScale = 0f;
                yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
                continue;
            }

            if (t > duration)
            {
                timeScale = targetTimeScale;
                yield break;
            }
            else
            {
                var currentTimeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, t / duration);
                timeScale = currentTimeScale;
            }
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }

        timeScale = targetTimeScale;
    }

    public void StopTimeAnimation()
    {
        if (_lastCoroutine == null)
        {
            return;
        }
        StopCoroutine(_lastCoroutine);
    }

    #region Singleton
    public static TimeScaleManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
