using System.Text;

namespace Casino.SaveLoadProfile
{

    public class FileSystemSaveLoadService : ISaveLoadService<List<Profile>>
    {
        private string _path = string.Empty;
        public FileSystemSaveLoadService(string FilePath)
        {
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                _path = FilePath;
                if (_path.Last() != '\\') _path += '\\';
            }
            else _path = string.Empty;
        }

        public List<Profile> LoadData(string Id)
        {
            if (_path == string.Empty) return new List<Profile>();

            try
            {
                List<Profile> list = new List<Profile>();
                Directory.CreateDirectory(_path);
                List<string> lines = new List<string>();

                // File.ReadAllLines(...) is better than "using" here, but with "using" as required
                using (var file = File.OpenText(_path + Id + ".txt"))
                {
                    while (true)
                    {
                        string? line = file.ReadLine();
                        if (line == null) break; else lines.Add(line);
                    }
                }

                foreach (var line in lines)
                {
                    var parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var profile = new Profile() { userName = parts[0], bank = int.Parse(parts[1]) };
                    list.Add(profile);
                }
                return list;
            }
            catch
            {
                return new List<Profile>();
            }
        }

        public Profile? LoadProfile(string UserName)
        {
            if (_path == string.Empty) return null;
            var profiles = LoadData("profiles");
            if (profiles != null)
            {
                return profiles.FirstOrDefault(x => x.userName == UserName);
            }
            else return null;
        }

        public void SaveProfile(Profile Profile)
        {
            if (_path == string.Empty) return;
            var profiles = LoadData("profiles");
            var exist = profiles.Where(x => x.userName == Profile.userName).FirstOrDefault();
            if (exist != null) profiles.Remove(exist);
            profiles.Add(Profile);
            SaveData(profiles, "profiles");
        }

        public void SaveData(List<Profile> Data, string Id)
        {
            if (_path == string.Empty) return;
            try
            {
                Directory.CreateDirectory(_path);
                StringBuilder sb = new StringBuilder();
                foreach (var profile in Data)
                {
                    sb.AppendLine($"{profile.userName};{profile.bank};");
                }
                File.WriteAllText(_path + Id + ".txt", sb.ToString());
            }
            catch
            {
            }
        }
    }
}
