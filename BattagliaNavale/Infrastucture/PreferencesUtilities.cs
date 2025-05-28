using BattagliaNavale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattagliaNavale.Infrastucture
{
    public static class PreferencesUtilities
    {
        private static readonly JsonSerializerOptions _defaultJsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };


        public static List<PartitaStatistiche> GetFields()
        {
            var serializedSavedOrders = Preferences.Default.Get("fields", "");

            if (string.IsNullOrEmpty(serializedSavedOrders)) return [];

            var savedOrders = JsonSerializer.Deserialize<List<PartitaStatistiche>>(serializedSavedOrders, _defaultJsonSerializerOptions);
            return savedOrders ?? [];
        }

        public static PartitaStatistiche GetField(int id)
        {
            var savedOrders = GetFields();
            var order = savedOrders.Where(x => x.Id == id).First();

            return order;
        }

        public static void SaveField(Risultato risultatoPartita, TimeSpan tempoPartita, List<Tuple<int, int, bool>> barcheBot, List<Tuple<int, int, bool>> barchePlayer, StatoCampo[,] campoPlayer, StatoCampo[,] campoBot)
        {
            var savedFields = GetFields();

            List<Tuple<StatoCampo, int, int>> CampoBotListed = new List<Tuple<StatoCampo, int, int>> ();
            List<Tuple<StatoCampo, int, int>> CampoPlayerListed = new List<Tuple<StatoCampo, int, int>> ();

            for (int i = 0; i < campoBot.GetLength(0); i++)
            {
                for (int j = 0; j < campoBot.GetLength(1); j++)
                {
                    if (campoBot[i, j] != StatoCampo.ACQUA && campoBot[i, j] != StatoCampo.NAVE) CampoBotListed.Add(Tuple.Create(campoBot[i, j], i, j));
                    if (campoPlayer[i, j] != StatoCampo.ACQUA && campoPlayer[i, j] != StatoCampo.NAVE) CampoPlayerListed.Add(Tuple.Create(campoPlayer[i, j], i, j));
                }
            }

            savedFields.Add(new PartitaStatistiche
            {
                Id = savedFields.Count + 1,
                RisultatoPartita = risultatoPartita,
                TempoPartita = tempoPartita,
                BarcheBot = barcheBot,
                BarchePlayer = barchePlayer,
                CampoBot = CampoBotListed,
                CampoPlayer = CampoPlayerListed
            });

            var serializedOrders = JsonSerializer.Serialize(savedFields, _defaultJsonSerializerOptions);
            Preferences.Default.Set("fields", serializedOrders);
        }

        public static void DeleteOrder(int id)
        {
            var savedOrders = GetFields();
            savedOrders.RemoveAt(id - 1);

            var count = 1;
            foreach (var order in savedOrders)
            {
                order.Id = count;
                count++;
            }

            var serializedOrders = JsonSerializer.Serialize(savedOrders, _defaultJsonSerializerOptions);
            Preferences.Default.Set("fields", serializedOrders);
        }

        public static void SaveFieldsToJsonFile()
        {
            try
            {
                var fields = GetFields();
                var jsonString = JsonSerializer.Serialize(fields, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                });

                var fileName = $"battaglia_navale_debug_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                File.WriteAllText(filePath, jsonString);

                System.Diagnostics.Debug.WriteLine($"File JSON salvato in: {filePath}");

                // Per copiare anche nella cartella Documents (se accessibile)
                try
                {
                    var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    var documentsFile = Path.Combine(documentsPath, fileName);
                    File.WriteAllText(documentsFile, jsonString);
                    System.Diagnostics.Debug.WriteLine($"Copia salvata anche in: {documentsFile}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Non è stato possibile salvare in Documents: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Errore nel salvare il file JSON: {ex.Message}");
            }
        }
    }
}
