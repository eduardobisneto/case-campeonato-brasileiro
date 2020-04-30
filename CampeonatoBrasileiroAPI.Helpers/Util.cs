using System;
using System.Globalization;
using System.Text;

namespace CampeonatoBrasileiroAPI.Helpers
{
    public static class Util
    {
        public static string RemoverAcentosNomeTime(string text)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                var array = text.Normalize(NormalizationForm.FormD).ToCharArray();
                foreach (char letra in array)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(letra) != UnicodeCategory.NonSpacingMark)
                        stringBuilder.Append(letra);
                }
                return stringBuilder.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static string PadronizarNomeTime(string time)
        {
            try
            {
                string nomeTimePadronizado = time.ToLower();

                var ajustarNomes = new string[,] {
                    { "athletico", "atletico"},
                    { "atletico", "atlético"},
                    { "goias", "goiás"},
                    { "avai", "avaí"},
                    { "sao ", "são "},
                    { "gremio", "grêmio"},
                    { "ceara", "ceará"},
                    { "avai", "avaí"},
                    { "vitoria", "vitória"},
                    { "america", "américa"},
                    { "parana", "paraná"}
                };

                for (short x = 0; x < ajustarNomes.Length / 2; x++)
                {
                    if (nomeTimePadronizado.Contains(ajustarNomes[x, 0]))
                        nomeTimePadronizado = nomeTimePadronizado.Replace(ajustarNomes[x, 0], ajustarNomes[x, 1]);
                }

                TextInfo info = new CultureInfo("pt-BR", false).TextInfo;
                nomeTimePadronizado = info.ToTitleCase(nomeTimePadronizado);

                return nomeTimePadronizado;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
