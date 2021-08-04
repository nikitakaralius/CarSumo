using System.Collections.Generic;

namespace Infrastructure.Settings
{
    public interface IProjectConfiguration
    {
        string RootDirectoryName { get; }
        IEnumerable<string> GetDataFilePaths();
    }
}