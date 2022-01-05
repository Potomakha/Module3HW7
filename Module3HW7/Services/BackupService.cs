using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module3HW7.Configs;
using Module3HW7.Interfaces;

namespace Module3HW7.Services
{
    public class BackupService : IBackupService
    {
        private readonly IConfigurationService _configurationService;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private string _logerFilePath;
        private string _backupPath;
        private Config _config;
        private int _colisionNum = 0;

        public BackupService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _config = _configurationService.GetConfig();
            var currentDirectory = Directory.GetCurrentDirectory();
            _backupPath = Path.Combine(currentDirectory, _config.BackupPath);
            _logerFilePath = Path.Combine(currentDirectory, _config.LoggerPath, _config.LogFileName);
            Directory.CreateDirectory(_backupPath);
        }

        public async Task Backup()
        {
            await _semaphoreSlim.WaitAsync();
            if (File.Exists(_logerFilePath))
            {
                var newFile = CreateNewFilePath();
                await Task.Run(() => { File.Copy(_logerFilePath, newFile); });
            }

            _semaphoreSlim.Release();
        }

        private string CreateNewFilePath()
        {
            var newFileName = DateTime.UtcNow.ToString(_config.FileNameFormat);
            var newFilePath = Path.Combine(_backupPath, newFileName + ".txt");
            if (CheckFileColision(newFilePath))
            {
                newFilePath = Path.Combine(_backupPath, newFileName + _colisionNum.ToString() + ".txt");
            }

            return newFilePath;
        }

        private bool CheckFileColision(string filePath)
        {
            if (File.Exists(filePath))
            {
                _colisionNum++;
                return true;
            }
            else
            {
                _colisionNum = 0;
                return false;
            }
        }
    }
}
