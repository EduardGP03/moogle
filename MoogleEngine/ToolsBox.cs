namespace MoogleEngine;

public static class Tools
{
    public static string[] ClearWhiteSpaces(string[] text)
    {
        List<string> result = new List<string>();
        foreach(var word in text)
        {
            if(!String.IsNullOrWhiteSpace(word) && !String.IsNullOrEmpty(word) && char.IsLetter(word[0]))
                result.Add(word);
        }
        return result.ToArray();
    }
    public static string[] GetTxtFiles(string Location)
    {
        return Directory.EnumerateFiles(Location).ToArray();
    }
    public static string[] GetWords(string text)
    {
        string[] words = text.Split(' ');
        words = ClearWhiteSpaces(words);
        return words;
    }
    public static string[] GetWordsWithotRepeat(string[] words)
    {
        List<string> result = new List<string>();
        foreach(var word in words)
            if(!result.Contains(word))
                result.Add(word);
        return result.ToArray();
    }
    public static SearchItem[] Sort(SearchItem[] items)
    {
        //Esto es cuadratico
        for(int i = 0; i < items.Length; i++)
        {
            for(int j = i; j < items.Length; j++)
            {
                if(items[j].Score > items[i].Score)
                {
                    SearchItem temp = items[j];
                    items[j] = items[i];
                    items[i] = temp;
                }
            }
        }
        return items;
    }
}