using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace Smartstock.Application.Helpers;

public class LoginAttemptTracker
{
    private static readonly ConcurrentDictionary<string, (int attempts, DateTime? lockoutUntil)> _attempts = new();
    private const int MaxAttempts = 3;
    private const int LockoutMinutes = 10;

    public static string GetClientKey(HttpContext context)
    {
        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    public static bool IsLocked(string key)
    {
        if (_attempts.TryGetValue(key, out var data))
            return data.lockoutUntil.HasValue && data.lockoutUntil > DateTime.Now;
        return false;
    }

    public static void RegisterFailedAttempt(string key)
    {
        if (!_attempts.ContainsKey(key))
            _attempts[key] = (1, null);
        else
        {
            var (attempts, _) = _attempts[key];
            attempts++;

            if (attempts >= MaxAttempts)
                _attempts[key] = (attempts, DateTime.Now.AddMinutes(LockoutMinutes));
            else
                _attempts[key] = (attempts, null);
        }
    }

    public static int GetRemainingAttempts(string key)
    {
        if (_attempts.TryGetValue(key, out var data))
            return MaxAttempts - data.attempts;
        return MaxAttempts - 1;
    }

    public static void Reset(string key)
    {
        _attempts.TryRemove(key, out _);
    }

    public static TimeSpan? GetRemainingLockout(string key)
    {
        if (_attempts.TryGetValue(key, out var data) && data.lockoutUntil.HasValue)
            return data.lockoutUntil.Value - DateTime.Now;
        return null;
    }
}