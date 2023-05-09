namespace MoogleEngine;

public class Book
{
    string Location { get; set; }
    public string Title { get; private set; }
    Dictionary<string,float> TFS { get; set;}
    float MaxTF { get; set; }
    public Book(string Location)
    {
        TFS = new Dictionary<string, float>();
        MaxTF = 0f;
        this.Location = Location;
        Title = Location.Substring(Location.LastIndexOf("/") + 1);
        Title = Title.Substring(0,Title.Length - 4);
        StreamReader reader = new StreamReader(Location);
        string text = reader.ReadToEnd().ToLower();
        reader.Close();
        string[] words = text.Split(' ',',','.',';',':','\n','\t');
        words = Tools.ClearWhiteSpaces(words);
        foreach(var word in words)
        {
            if(!TFS.Keys.Contains(word))
                TFS[word] = 1;
            else
                TFS[word]++;
            if(TFS[word] > MaxTF)
                MaxTF = TFS[word];
        }
    }
    public string[] Words
    {
        get
        {
            return TFS.Keys.ToArray();
        }
    }
    public float TF(string word)
    {
        return TFS.Keys.Contains(word) ? TFS[word]/MaxTF : 0f;
    }
    public string GetFragment(string[] words)
    {
        StreamReader reader = new StreamReader(this.Location);
        string content = reader.ReadToEnd();
        foreach(var word in words)
        {
            if(content.Contains(word))
            {
                int index = content.IndexOf(word);
                return content.Substring(index, Math.Min(400,content.Length - index));
            }
        }
        return content.Substring(0,Math.Min(400,content.Length));
    }
}