using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var results = new List<string>();
        foreach (var word in words)
        {
            if (word[0] == word[1]) continue; // skip "aa"

            var reversed = new string(new char[] { word[1], word[0] });

            if (seen.Contains(reversed))
            {
                results.Add($"{word} & {reversed}");
            }
            else
            {
                seen.Add(word);
            }
        }
        return results.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  
    /// The degree is in the 4th column of each line.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length >= 4)
            {
                var degree = fields[3].Trim();
                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  
    /// Example: "CAT" and "ACT" → true
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length) return false;

        var counts = new Dictionary<char, int>();

        foreach (var c in word1)
        {
            if (counts.ContainsKey(c))
                counts[c]++;
            else
                counts[c] = 1;
        }

        foreach (var c in word2)
        {
            if (!counts.ContainsKey(c)) return false;
            counts[c]--;
            if (counts[c] < 0) return false;
        }

        foreach (var kvp in counts)
        {
            if (kvp.Value != 0) return false;
        }

        return true;
    }

    /// <summary>
    /// Read JSON earthquake data from USGS and return summary strings.
    /// Each string contains magnitude and location.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        var json = client.GetStringAsync(uri).Result;
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var results = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            // Handle missing values safely
            var mag = feature.Properties.Mag.HasValue ? feature.Properties.Mag.Value.ToString("0.0") : "0.0";
            var place = string.IsNullOrWhiteSpace(feature.Properties.Place) ? "Unknown location" : feature.Properties.Place;
            results.Add($"{place} - Mag {mag}");
        }

        return results.ToArray();
    }
}

/// <summary>
/// Classes to describe the JSON structure from USGS.
/// These are required so JsonSerializer can map the JSON into C# objects.
/// </summary>
public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public double? Mag { get; set; }
    public string Place { get; set; }
}

