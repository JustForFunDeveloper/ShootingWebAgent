namespace ShootingWebAgent.Services
{
    public interface IDataSingleton
    {
        public void AddUserToGroupsDictionary(string user, int matchId);
        public void DeleteUserFromGroupsDictionary(string user);
        public int GetMatchFromGroupsDictionary(string user);
    }
}