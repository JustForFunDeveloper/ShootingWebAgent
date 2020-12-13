using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using ShootingWebAgent.Hub;

namespace ShootingWebAgent.Services
{
    public class DataSingleton : IDataSingleton
    {
        private ConcurrentDictionary<string, int> _groupsDictionary = new ConcurrentDictionary<string, int>();
        private readonly ILogger<UpdateHub> _logger;

        public DataSingleton(ILogger<UpdateHub> logger)
        {
            _logger = logger;
        }

        public void AddUserToGroupsDictionary(string user, int matchId)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (_groupsDictionary.TryAdd(user, matchId))
                        return;
                }
                throw new Exception($"Couldn't add {user} | {matchId} to _groupsDictionary!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "AddUserToGroupsDictionary");
            }
        }

        public void DeleteUserFromGroupsDictionary(string user)
        {
            try
            {
                int matchId = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (_groupsDictionary.TryRemove(user, out matchId))
                        return;
                }
                throw new Exception($"Couldn't delete {user} | {matchId} from _groupsDictionary!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "DeleteUserFromGroupsDictionary");
            }
        }

        public int GetMatchFromGroupsDictionary(string user)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (_groupsDictionary.TryGetValue(user, out var matchId))
                        return matchId;
                }
                throw new Exception($"Couldn't get matchId from {user} in _groupsDictionary!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "DeleteUserFromGroupsDictionary");
                return -1;
            }
        }
    }
}