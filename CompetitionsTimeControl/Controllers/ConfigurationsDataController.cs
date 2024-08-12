using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace CompetitionsTimeControl.Controllers
{
    internal class ConfigurationsDataController
    {
        public BeepsController DataBeepsController { get; set; } = null!;
        public MusicsController DataMusicsController { get; set; } = null!;
        public CompetitionController DataCompetitionController { get; set; } = null!;
        public string? Checksum { get; set; }

        [JsonIgnore] private static string _lastDirectory = string.Empty;

        public static string SaveFileConfigurationAs(BeepsController beepsController, MusicsController musicsController,
            CompetitionController competitionController)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = $"Arquivo CTCI | *.ctci",
                Title = "Salvar arquivo CTCI",
                InitialDirectory = _lastDirectory
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return "";

            string filePath = "";

            if (SaveFileConfiguration(saveFileDialog.FileName, beepsController, musicsController, competitionController))
            {
                filePath = saveFileDialog.FileName;
                _lastDirectory = Path.GetDirectoryName(filePath) ?? "";
            }

            return filePath;
        }

        public static bool SaveFileConfiguration(string filePath, BeepsController beepsController,
            MusicsController musicsController, CompetitionController competitionController)
        {
            try
            {
                ConfigurationsDataController dataController = new()
                {
                    DataBeepsController = beepsController,
                    DataMusicsController = musicsController,
                    DataCompetitionController = competitionController
                };

                string json = JsonConvert.SerializeObject(dataController, Formatting.Indented);
                dataController.Checksum = CalculateChecksum(json);

                string finalJson = JsonConvert.SerializeObject(dataController, Formatting.Indented);
                File.WriteAllText(filePath, finalJson);

                StringBuilder sb = new("Os dados foram salvos no local abaixo com sucesso!\n\n");
                sb.Append($"[ {filePath} ]");

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (IOException ex)
            {
                StringBuilder sb = new("Houve algum problema ao salvar os dados.\n\n");

                if (ex.Message != "") sb.AppendLine($"Mensagem do sistema: [{ex.Message}].\n");

                sb.Append("Tente salvar em outro local.");

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static string GetFileToOpenConfiguration(out ConfigurationsDataController? dataController)
        {
            dataController = null;

            OpenFileDialog openFileDialog = new()
            {
                Filter = $"Arquivo CTCI | *.ctci",
                Title = "Selecione um arquivo para carregar",
                Multiselect = false,
                InitialDirectory = _lastDirectory
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return "";
            }

            string filePath;

            try
            {
                string finalJson = File.ReadAllText(openFileDialog.FileName);
                dataController = JsonConvert.DeserializeObject<ConfigurationsDataController>(finalJson);

                if (dataController == null)
                    throw new Exception();

                string readChecksum = dataController.Checksum ?? "";
                dataController.Checksum = null;
                string recalculatedJson = JsonConvert.SerializeObject(dataController, Formatting.Indented);
                string recalculatedChecksum = CalculateChecksum(recalculatedJson);

                if (readChecksum == recalculatedChecksum)
                {
                    filePath = openFileDialog.FileName;
                    _lastDirectory = Path.GetDirectoryName(filePath) ?? "";
                }
                else
                {
                    StringBuilder sb = new("O arquivo CTCI pode estar corrompido!\n\n");
                    sb.Append("Tente abrir um arquivo válido.");

                    filePath = "";

                    MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException ex)
            {
                StringBuilder sb = new("Houve algum problema ao carregar os dados.\n\n");

                if (ex.Message != "") sb.AppendLine($"Mensagem do sistema: [{ex.Message}].\n");

                sb.Append("Tente abrir um arquivo válido.");

                filePath = "";

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                StringBuilder sb = new("Este arquivo CTCI não é válido.\n\n");
                sb.Append("Tente abrir um arquivo válido.");

                filePath = "";

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return filePath;
        }

        private static string CalculateChecksum(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
