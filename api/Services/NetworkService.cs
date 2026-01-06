using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Services
{
    public interface INetworkService
    {
        Task<bool> PingDevice(string ip);
    }
    public class NetworkService : INetworkService
    {
        public async Task<bool> PingDevice(string ip)
        {
            using (Ping pingSender = new Ping())
            {
                try
                {
                    PingReply reply = await pingSender.SendPingAsync(ip, 1000);
                    return reply.Status == IPStatus.Success
                        ? true
                        : false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}