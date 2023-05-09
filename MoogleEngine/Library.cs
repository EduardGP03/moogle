namespace MoogleEngine;

public class Library
{
    string Location { get; set;}
    public Book[] Books { get; private set; }
    Dictionary<string,float> IDFS { get; set; }
    public Library(string Location)
    {
        IDFS = new Dictionary<string, float>();
        this.Location = Location;
        string[] books = Tools.GetTxtFiles(Location);
        Books = new Book[books.Length];
        for(int i = 0; i < Books.Length; i++)
            Books[i] = new Book(books[i]);
        foreach(var book in Books)
        {
            foreach(var word in book.Words)
            {
                if(!IDFS.Keys.Contains(word))
                    IDFS[word] = 1;
                else
                    IDFS[word]++;
            }
        }
        foreach(var key in IDFS.Keys)
            IDFS[key] = (float)Math.Log10(Books.Length/IDFS[key]);
    }
    public float[,] WordsTF(string[] words)
    {
        float[,] result = new float[Books.Length,words.Length];
        for(int i = 0; i < result.GetLength(0); i++)
        {
            for(int j = 0; j < result.GetLength(1); j++)
            {
                result[i,j] = Books[i].TF(words[j]);
            }
        }
        return result;
    }
    public float[] WordsIDF(string[] words)
    {
        float[] result = new float[words.Length];
        for(int i = 0; i < words.Length; i++)
        {
            if(IDFS.Keys.Contains(words[i]))
                result[i] = IDFS[words[i]];
            else
                result[i] = -1;
        }
        return result;
    }
    public string GetSuggestion(string[] query)
    {
        foreach(var word in query)
            System.Console.WriteLine(IDFS.Keys.Contains(word) + " " + word);
        Dictionary<string,string> suggestions = new Dictionary<string, string>();
        foreach(var word in query)
        {
            if(!suggestions.Keys.Contains(word))
            {
                suggestions[word] = GetWordSuggestion(word);
            }
        }
        string result = "";
        foreach(var word in query)
        {
            if(!IDFS.Keys.Contains(word))
                result += suggestions[word] + " ";
            else
                result += word + " ";
        }
        return result;
    }
    string GetWordSuggestion(string word)
    {
        float index_coincidences = 0f;
        string result = "";
        foreach(var w in IDFS.Keys)
        {
            if(word.Length - w.Length < 4)
            {
                float temp = IndexCoincidences(word,w);
                if(temp > index_coincidences)
                {
                    index_coincidences = temp;
                    result = w;
                }
            }
            if(index_coincidences == 1)
                return w;
        }
        return result;
    }
    float IndexCoincidences(string word1, string word2)
    {
        string longer_word;
        string shorter_word;
        if(word1.Length > word2.Length)
        {
            longer_word = word1;
            shorter_word = word2;
        }
        else
        {
            longer_word = word2;
            shorter_word = word1;
        }
        bool[] matched = new bool[longer_word.Length];
        return MaxIndexOfCoincidences(longer_word.ToLower(),shorter_word.ToLower(),longer_word.Length - shorter_word.Length,0f,matched);
    }
    float MaxIndexOfCoincidences(string word1,string word2,int remains,float index_of_coincidences, bool[] matched)
    {
        if(remains == 0)
        {
            int count = 0;
            int position = 0;
            for(int i = 0; (i < word1.Length) && (i < word2.Length); i++)
            {
                if(!matched[i] && (i + position < word1.Length) && (word1[i + position] == word2[i]))
                    count++;
                if(matched[i])
                    position++;
            }
        return Math.Max((float)count / (float)word1.Length,index_of_coincidences);
        }
        for(int i = 0; i < matched.Length; i++)
        {
            matched[i] = true;
            index_of_coincidences = MaxIndexOfCoincidences(word1,word2,remains - 1,index_of_coincidences,matched);
            matched[i] = false;
        }
        return index_of_coincidences;
    }
}