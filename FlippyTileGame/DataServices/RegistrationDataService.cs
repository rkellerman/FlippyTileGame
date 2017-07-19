using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlippyTileGame.DataServiceInterfaces;
using FlippyTileGame.Settings;

namespace FlippyTileGame.DataServices
{
    public class RegistrationDataService : IRegistrationDataService
    {
        private readonly HttpClient _client;
        private readonly string _serverAddress;

        public RegistrationDataService()
        {
            _client = new HttpClient();
            _serverAddress = File.ReadAllText(FlippyTileGameSettings.ServerPath);
        }


        public bool CheckRegistration()
        {
            return File.Exists(FlippyTileGameSettings.RegistryPath);
        }

        public async Task<bool> Register(string productKey)
        {
            var hardwareId = GetHardwareId();
            var url = _serverAddress + $"/api/Server/?productKey={productKey}&hardwareId={hardwareId}";
            var formContent = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("productKey", productKey),
                    new KeyValuePair<string, string>("hardwareId", hardwareId)
            });

            var result = await _client.PostAsync(url, formContent);
            if (!result.IsSuccessStatusCode) return false;
            File.WriteAllText(FlippyTileGameSettings.RegistryPath, productKey);
            return true;
        }

        public bool Validate()
        {

            var productKey = File.ReadAllText(FlippyTileGameSettings.RegistryPath);
            var hardwareId = GetHardwareId();
            var url = _serverAddress + $"/api/Server/?productKey={productKey}&hardwareId={hardwareId}";
            return _client.GetAsync(url).Result.IsSuccessStatusCode;
        }

        public bool UnRegister(string productKey)
        {
            var hardwareId = GetHardwareId();
            var url = _serverAddress + $"/api/Server/?productKey={productKey}&hardwareId={hardwareId}";
            return _client.DeleteAsync(url).Result.IsSuccessStatusCode;
        }

        public string GetHardwareId()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            var mbsList = mbs.Get();
            var id = "";
            foreach (var mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            return id;
        }
    }
}
